/*
* Copyright (c) 2022 Kagamia Studio
*/
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;
using Newtonsoft.Json;

string manifestFileUrl = "http://download2.nexon.net/Game/nxl/games/10100/10100.pub230_1_0_20220208_bdd79f9202e32505e4314180fd4cf753.manifest.hash";
string outDir = @"C:\Game\GMS230_1";
var client = new HttpClient();

// 1-download manifest
string manifestHash = await client.GetStringAsync(manifestFileUrl);
string manifestFile = Path.Combine(outDir, manifestHash);
if (!File.Exists(manifestFile)) {
    using (var ns = await client.GetStreamAsync(new Uri(new Uri(manifestFileUrl), manifestHash))) {
        using (var fs = File.OpenWrite(manifestFile)) {
            await ns.CopyToAsync(fs);
        }
    }
}

// 2-decompress manifest json
GMSManifest manifest;
using (var fs = File.OpenRead(manifestFile)) {
    fs.Seek(2, SeekOrigin.Current);
    using (var ds = new DeflateStream(fs, CompressionMode.Decompress))
    using (var ts = new StreamReader(ds))
    using (var jr = new JsonTextReader(ts)) 
    {
        var jss = JsonSerializer.CreateDefault();
        manifest = jss.Deserialize<GMSManifest>(jr);
    }
}

// 3-download files one by one
Console.WriteLine("Total files: {0}", manifest.files.Count);
Console.WriteLine("Total size: {0:N0}", manifest.total_uncompressed_size);
var fileNameEnc = Encoding.UTF8;
switch(manifest.filepath_encoding) {
    case "utf16":
        fileNameEnc = Encoding.Unicode;
        break;
}
foreach (var kv in manifest.files)
{
    // consume unicode bom chars: 'FF FE'
    string fileName = new StreamReader(new MemoryStream(Convert.FromBase64String(kv.Key))).ReadToEnd();
    
    string fullFileName = Path.Combine(outDir, fileName);
    if (kv.Value.objects[0] == "__DIR__") {
        if (!Directory.Exists(fullFileName)) {
            Console.WriteLine("Create dir: {0}", fullFileName);
            Directory.CreateDirectory(fullFileName);
        }
    }
    else {
        if (!File.Exists(fullFileName) || new FileInfo(fullFileName).Length != kv.Value.fsize) {
            Console.WriteLine("Download file: {0}", fullFileName);
            using (var fs = File.Create(fullFileName)) {
                for(int p=0; p<kv.Value.objects.Length; p++)
                {
                    var objID = kv.Value.objects[p];
                    var objUrl = new Uri(new Uri(manifestFileUrl), $"10100/{objID.Substring(0,2)}/{objID}");
                    Console.WriteLine("part {0}/{1}: {2}", p+1, kv.Value.objects.Length, objUrl);
                    using (var ns = await client.GetStreamAsync(objUrl)) {
                        ns.Read(new byte[2], 0, 2);
                        using (var ds = new DeflateStream(ns, CompressionMode.Decompress))
                        {
                            ds.CopyTo(fs);
                        }
                        fs.Flush();
                    }
                }
            }
        } else {
            Console.WriteLine("File {0} already exists, skipping.", fullFileName);
        }
    }
}

class GMSManifest {
	public double buildtime;
	public string filepath_encoding;
	public Dictionary<string, GMSFileInfo> files;
	public string platform;
    public string product;
    public long total_compressed_size;
    public int total_objects;
    public long total_uncompressed_size;
    public string version;
}

class GMSFileInfo {
	public long fsize;
	public double mtime;
	public string[] objects;
	public int[] objects_fsize;
}
/*
* Copyright (c) 2022 Kagamia Studio
*/
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;
using Newtonsoft.Json;

string srcHash = "5ced89cf34977e443b9a81f37341e232ddeec73d";
string dstHash = "0574b97a1a9fd0da8fffb5ac441633d4963463df";
string diffManifestUrl = $"https://download2.nexon.net/Game/nxl/games/10100/patches/patch-{srcHash[0..8]}-{dstHash[0..8]}/diff_manifest.hash";
string msDir = @"C:\Game\GMS229_3_copy";
string patcherTempDir = Path.Combine(msDir, "patcher");
var client = new HttpClient();

// 1-download diff manifest
string manifestHash = await client.GetStringAsync(diffManifestUrl);
string manifestFile = Path.Combine(patcherTempDir, manifestHash);
if (!File.Exists(manifestFile))
{
    using (var ns = await client.GetStreamAsync(new Uri(new Uri(diffManifestUrl), manifestHash)))
    {
        using (var fs = File.OpenWrite(manifestFile))
        {
            await ns.CopyToAsync(fs);
        }
    }
}

// 2-decompress diff json
GMSDiffManifest manifest;
using (var fs = File.OpenRead(manifestFile))
{
    fs.Seek(2, SeekOrigin.Current);
    using (var ds = new DeflateStream(fs, CompressionMode.Decompress))
    using (var ts = new StreamReader(ds))
    using (var jr = new JsonTextReader(ts))
    {
        var jss = JsonSerializer.CreateDefault();
        manifest = jss.Deserialize<GMSDiffManifest>(jr);
    }
}

// 3-download diff files one by one
for (int i=0, cnt=manifest.diff_result.Length; i<cnt; i++) {
    var diff = manifest.diff_result[i];
    string fileName = diff.path+".diff";
    string fullFileName = Path.Combine(patcherTempDir, fileName);
    string localDir = Path.GetDirectoryName(fullFileName);
    if (!Directory.Exists(localDir)) {
        Directory.CreateDirectory(localDir);
    }

    if (!File.Exists(fullFileName) || new FileInfo(fullFileName).Length != diff.file_size)
    {
        var objUrl = new Uri(new Uri(diffManifestUrl), $"10100/{fileName}");
        Console.WriteLine("part {0}/{1}: {2}", i + 1, cnt, objUrl);
        using (var fs = File.Create(fullFileName))
        using (var ns = await client.GetStreamAsync(objUrl))
        {
            ns.CopyTo(fs);
        }

        // 4-apply diff file
        // TODO: validate old and new file hash.
        Console.WriteLine("Patching file {0}...", diff.path);
        string oldFileName = Path.Join(msDir, diff.path);
        string tempFileName = Path.Join(patcherTempDir, $"{diff.path}.tmp");
        DoPatch(fullFileName, oldFileName, tempFileName);
        Console.WriteLine("Apply {0}", diff.path);
        File.Move(tempFileName, oldFileName, true);
    }
}

void DoPatch(string diffFile, string oldFile, string newFile){
	using (var fsDiff = File.OpenRead(diffFile))
	using (var ds = new DeflateStream(fsDiff, CompressionMode.Decompress))
	using (var fsOld = File.OpenRead(oldFile))
	using (var fsNew = new FileStream(newFile, FileMode.CreateNew))
	{
		fsDiff.Position = 2;
		int totalLen = 0;
		var br = new BinaryReader(ds);
		while(true) {
			byte cmd;
			try {
				 cmd = br.ReadByte();
			} catch (EndOfStreamException) {
				break;
			}
			/*
			  cmd:     1
			  pos:     1-4
			  dataLen: 1-4
			  data:    0-dataLen

			  cmd = aabbcc00
			  aa: source flag
			      00-from old file, 01-from diff file
			  bb: (aa==00): old file pos, (aa==01): cur pos
			      00-8bit, 01-16bit, 10-32bit
		      cc: data length
			      00-8bit, 01-16bit, 10-32bit
				  
			  eg:  cmd=0x68=0b_0110_1000
			      01  from diff file
				  10  32bit pos length
				  10  32bit data length
				  00  unused
			*/
			int pos, len;
			switch((cmd & 0b0011_0000)>>4) {
				case 0b00: pos = br.ReadByte(); break;
				case 0b01: pos = br.ReadUInt16(); break;
				case 0b10: pos = br.ReadInt32(); break;
				default: throw new Exception($"unknown cmd: {cmd:x2}");
			}
			switch((cmd & 0b0000_1100)>>2) {
				case 0b00: len = br.ReadByte(); break;
				case 0b01: len = br.ReadUInt16(); break;
				case 0b10: len = br.ReadInt32(); break;
				default: throw new Exception($"unknown cmd: {cmd:x2}");
			}
			switch ((cmd & 0b1100_0000) >> 6) {
				case 0b00: {
						fsOld.Position = pos;
						var buff = new byte[len];
						if (len != fsOld.Read(buff, 0, len))
							throw new Exception($"failed to read data from old file, pos={pos}, len={len}.");
						fsNew.Write(buff);
					}
					break;
					
				case 0b01: {
						if (pos != totalLen) throw new Exception("file pos mismatch");
						var data = br.ReadBytes(len);
						fsNew.Write(data);
					}
					break;
					
				default:
					throw new Exception($"unknown cmd: {cmd:x2}");
			}
			
			totalLen += len;
		}
	}
}

class GMSDiffManifest
{
	public int compress_level;
	public GMSDiffResult[] diff_result;
	public string dst_deploy_id;
    public string dst_manifest_hash_url;
    public string patcher_type;
    public string src_deploy_id;
    public string src_manifest_hash_url;
    public long total_size;
    public string version;
}

class GMSDiffResult
{
    public string checksum;
    public long file_size;
    public string path;
    public int type;
}

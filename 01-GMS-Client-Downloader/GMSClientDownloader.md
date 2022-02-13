# GMS Client Downloader

Starting from early 2020, GMS has gave up the old patcher format, which initiated by Nexon 17 years ago. Instead of that, GMS has introduced the modern client downloader and patcher procedure, also prevents us from detecting patcher urls for manual update and comparison.

This article introduces the lastest (Jan 2022) GMS client downloading protocol. Knowing this, you can manually download a complete GMS client from an empty folder.


## Client Manifest

In a nutshell, GMS use a json file to describe all client file information, and each file consists of multiple blocks. To download the client, you should find the json file in every possible way, and download each file parts one by one, then join the parts into complete files.

The first question is, how to get the `json file` I mentioned above?

Unfortunately, we don't have **an easy way** to find it. Up to now, the url of that json file is only found from a private api which exposed by Nexon, and the API requires cookies authentication so that it does not accept anonymous request.

Anyway, we assume that you have already logged in via web browser or Nexon Launcher, and get a serious of cookies like `AToken`, `g_AToken` and `NxLSession`, you can access the first and the most critical API with these cookies:

```
https://www.nexon.com/api/game-build/v1/branch/games/10100/public
```

expected result:
```
{
    "branchName": "Public",
    "executablePath": null,
    "parameter": null,
    "useBranchDirectory": false,
    "manifestUrl": "http://download2.nexon.net/Game/nxl/games/10100/10100.pub229_3_0_3ed487493a3359742d807712ddb180e9.manifest.hash",
    "releaseDate": "2022-01-20T17:42:33.579z",
    "serviceId": "1049736197",
    "toyServiceId": null
}
```

The `manifestUrl` is what we need, the file is published to CDN so it supports public access. Then we can request the url, and we'll get this string:

```
5ced89cf34977e443b9a81f37341e232ddeec73d
```

Note that the file extension is `.hash`, so this string represents the hash (in detail, it is SHA1 hash) of the manifest file, and the actual url of the manifest file is:
 
```
http://download2.nexon.net/Game/nxl/games/10100/5ced89cf34977e443b9a81f37341e232ddeec73d
```

The file is actual in binary format, as I said above, `download2.nexon.net` is a CDN host, so most of the files are compressed by default. **Nexon always use `deflate` method to compress manifest and file part files**. (but never returns the standard `Content-Encoding` header from http response), the first 2 bytes `78 9c` is the `zlib` header, so you can use the standard deflate algorithm to decompress this file.

Here's some example code to help you decompress the file, and calculate file SHA1 hash.

powershell 5
```
$saveFilePath = (Join-Path $(pwd) "manifest.json")
$requestUrl = "http://download2.nexon.net/Game/nxl/games/10100/5ced89cf34977e443b9a81f37341e232ddeec73d"

$resp=Invoke-WebRequest $requestUrl
$body=$resp.RawContentStream
$body.Position = 2
$ds = New-Object System.IO.Compression.DeflateStream($body, [System.IO.Compression.CompressionMode]::Decompress)
$fsOut = [System.IO.File]::OpenWrite($saveFilePath)
$ds.CopyTo($fsOut)
$ds.Close()
$fsOut.Close();

[System.BitConverter]::ToString([System.Security.Cryptography.Sha1]::Create().ComputeHash([System.IO.File]::ReadAllBytes($saveFilePath))) -replace "-",""
```

bash
```
apt install qpdf curl

saveFilePath="./manifest.json"
requestUrl="http://download2.nexon.net/Game/nxl/games/10100/5ced89cf34977e443b9a81f37341e232ddeec73d"

curl $requestUrl -o - | zlib-flate -uncompress > $saveFilePath
sha1sum $saveFilePath
```

## Analyze manifest file

In the previous step, you should have got the decompressed manifest json file, it looks like this:

```
{
    "buildtime": 1642528875.454, 
    "filepath_encoding": "utf16", 
    "files": {
        "//52ADMAaAB1AG4AdAAuAGQAbABsAA==": {
            "fsize": 142040, 
            "mtime": 1631527659, 
            "objects": [
                "ab4d653972447afe9c2fcbf2e0008c017e950fd7"
            ], 
            "objects_fsize": [
                142040
            ]
        }, 
        "//5CAGwAYQBjAGsAQwBpAHAAaABlAHIA": {
            "fsize": 0, 
            "mtime": 1639375436, 
            "objects": [
                "__DIR__"
            ], 
            "objects_fsize": [
                0
            ]
        }, 
        ......
    },
    "platform": null, 
    "product": "10100", 
    "total_compressed_size": 0, 
    "total_objects": 5289, 
    "total_uncompressed_size": 21676736368, 
    "version": "0.5"
}
```

All of the fields are self-explanatory, except the keys in `files` object. The keys looks like a base64 string and it must represent the file path, and it has declared the "filepath_encoding" is "utf16", we can try to decode the file path with these information.

> Base64: `//52ADMAaAB1AG4AdAAuAGQAbABsAA==`  
> Hex Bytes: `FF FE 76 00 33 00 68 00 75 00 6E 00 74 00 2E 00 64 00 6C 00 6C 00`  
> Utf16 String: `"\uFEFFv3hunt.dll"`

The first char is [Byte order mark](https://en.wikipedia.org/wiki/Byte_order_mark), we must trim the Bom char so that it won't affect the subsequent processing.

The `objects` array of each file represents the SHA1 hash of each file block, the actual download url is a little complicated, for example:

> object_id: `ab4d653972447afe9c2fcbf2e0008c017e950fd7`  
> url: `http://download2.nexon.net/Game/nxl/games/10100/10100/ab/ab4d653972447afe9c2fcbf2e0008c017e950fd7`

Please node the relative path we added, `10010/` is fixed and the `ab/` is the first two chars of the `object_id`.

The file blocks are still being compressed, using the same code I wrote above, you can download the file blocks one by one, and validate with SHA1 hash and `objects_fsize`. Finally, joining all file blocks into one file, and save to the decoded file path. Repeating the process, a complete client could be downloaded. 

## Summary

We finally have an overview of how GMS client downloader works, a sample code will also be attached with this article, but

In the next article we'll be looking at the GMS patcher.

## Credits

Thanks to `好人` and `Goldentube` for their kind help.

## License

[CC BY-NC 4.0](https://creativecommons.org/licenses/by-nc/4.0/)

## Change log

- 2022-02-13, v1.0

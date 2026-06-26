# CMS Patcher

## Available Patch File List

At beginning, the Launcher would request a list of available patches in JSON from following URL (URL signing is not required):
```
https://v3launcher.jijiagames.com/v3launcher/build/ver2data/5/8848/-1/ver2.dat
```

As of when the article is written, the content of list is this:
```json
{
"baseUrl":"https://mxdver0.jijiagames.com/v3client/build/5/8848/diff",
"backupBaseUrl":"https://mxdver0.jijiagames.com/v3client/build/5/8848/diff",
"areas":[
   {
      "back" : "0.0.0.0",
      "id" : "0",
      "max" : "0.0.0.15",
      "min" : "0.0.0.9",
      "must" : "0.0.0.15"
   }
]
,
"packages":[
                {
                   "fileListUrl" : "/0.0.0.1-0.0.0.3-81b9226189064c26b301750df77e2f43/5_0.0.0.1-0.0.0.3_FileList.dat",
                   "forcetype" : 0,
                   "from" : "0.0.0.1",
                   "md5" : "CF6CD3EC7590EA60B9A6B38C60B1DBF9",
                   "name" : "patch-0.0.0.1-0.0.0.3",
                   "to" : "0.0.0.3",
                   "versionView" : "v222"
                },
                {
                   "fileListUrl" : "/0.0.0.3-0.0.0.4-8f0a570d346f4a4ea7c4aaac7fd4c990/5_0.0.0.3-0.0.0.4_FileList.dat",
                   "forcetype" : 0,
                   "from" : "0.0.0.3",
                   "md5" : "FF061E8EE0917E0B8C5245474C43B8E8",
                   "name" : "patch-0.0.0.3-0.0.0.4",
                   "to" : "0.0.0.4",
                   "versionView" : "v222_1"
                },
                {
                   "fileListUrl" : "/0.0.0.4-0.0.0.5-5102b7cbec554e699af92faa439599ab/5_0.0.0.4-0.0.0.5_FileList.dat",
                   "forcetype" : 0,
                   "from" : "0.0.0.4",
                   "md5" : "6F153FE4CD6320B8C539B6564E055CCE",
                   "name" : "patch-0.0.0.4-0.0.0.5",
                   "to" : "0.0.0.5",
                   "versionView" : "v223"
                },
                {
                   "fileListUrl" : "/0.0.0.5-0.0.0.6-150d3dfabf624e4ca8d858aed5424709/5_0.0.0.5-0.0.0.6_FileList.dat",
                   "forcetype" : 0,
                   "from" : "0.0.0.5",
                   "md5" : "2697BE7DC0FBFBFBFF09E8C712226DD6",
                   "name" : "patch-0.0.0.5-0.0.0.6",
                   "to" : "0.0.0.6",
                   "versionView" : "v224"
                },
                {
                   "fileListUrl" : "/0.0.0.6-0.0.0.7-ad06a8c68c7845a1a3698f64c3bd6de3/5_0.0.0.6-0.0.0.7_FileList.dat",
                   "forcetype" : 0,
                   "from" : "0.0.0.6",
                   "md5" : "40DFF124712209C104D4ECA765DF32A0",
                   "name" : "patch-0.0.0.6-0.0.0.7",
                   "to" : "0.0.0.7",
                   "versionView" : "v225_2G"
                },
                {
                   "fileListUrl" : "/0.0.0.7-0.0.0.9-ff37ae2afdda4ab89851482dc48c50a8/5_0.0.0.7-0.0.0.9_FileList.dat",
                   "forcetype" : 0,
                   "from" : "0.0.0.7",
                   "md5" : "29C420D5DE3710A84B0074D8F237E1EB",
                   "name" : "patch-0.0.0.7-0.0.0.9",
                   "to" : "0.0.0.9",
                   "versionView" : "V225.1"
                },
                {
                   "fileListUrl" : "/0.0.0.9-0.0.0.10-2343190ad8804358884760a5d1c71871/5_0.0.0.9-0.0.0.10_FileList.dat",
                   "forcetype" : 0,
                   "from" : "0.0.0.9",
                   "md5" : "A4834776F125ACFFFFB2F9FF1C39BDE5",
                   "name" : "patch-0.0.0.9-0.0.0.10",
                   "to" : "0.0.0.10",
                   "versionView" : "V225.2"
                },
                {
                   "fileListUrl" : "/0.0.0.10-0.0.0.11-e78ba150d24a46a1a91f6f103b16570a/5_0.0.0.10-0.0.0.11_FileList.dat",
                   "forcetype" : 0,
                   "from" : "0.0.0.10",
                   "md5" : "4396FCAE7071AAE3E50CA6EBFADD1EEA",
                   "name" : "patch-0.0.0.10-0.0.0.11",
                   "to" : "0.0.0.11",
                   "versionView" : "V225.3"
                },
                {
                   "fileListUrl" : "/0.0.0.11-0.0.0.12-3423a70d1e9f4073b97e66ff521fa6fc/5_0.0.0.11-0.0.0.12_FileList.dat",
                   "forcetype" : 0,
                   "from" : "0.0.0.11",
                   "md5" : "2AB60DF7943BBD2B253F2EE505A650FD",
                   "name" : "patch-0.0.0.11-0.0.0.12",
                   "to" : "0.0.0.12",
                   "versionView" : "V225.4"
                },
                {
                   "fileListUrl" : "/0.0.0.12-0.0.0.13-a5442f80eba648e78e4d84c5cbd01d26/5_0.0.0.12-0.0.0.13_FileList.dat",
                   "forcetype" : 0,
                   "from" : "0.0.0.12",
                   "md5" : "26BACDD5AA05F8A6792AD9197AF9B7A2",
                   "name" : "patch-0.0.0.12-0.0.0.13",
                   "to" : "0.0.0.13",
                   "versionView" : "V225.5"
                },
                {
                   "fileListUrl" : "/0.0.0.13-0.0.0.14-46c2fe91daab4e1dbbaa5f319b664892/5_0.0.0.13-0.0.0.14_FileList.dat",
                   "forcetype" : 0,
                   "from" : "0.0.0.13",
                   "md5" : "524D6B046F088481D653D53AE77C0153",
                   "name" : "patch-0.0.0.13-0.0.0.14",
                   "to" : "0.0.0.14",
                   "versionView" : "V225.6"
                },
                {
                        "fileListUrl":"/0.0.0.14-0.0.0.15-02c012823140469880c530ff0418cde8/5_0.0.0.14-0.0.0.15_FileList.dat",
                        "forcetype":0,
                        "from":"0.0.0.14",
                        "md5":"82F7188F004D106303CDAECB12C9C0A0",
                        "name":"patch-0.0.0.14-0.0.0.15",
                        "versionView":"V226.1",
                        "to":"0.0.0.15"
                }
        ]
}
```

We can clearly see the Launcher uses version number defined by it's own (e.g. 0.0.0.15) to understand what patch files it needs to obtain, which is exactly the same as Version in client manifest mentioned in previous article. The Launcher uses a file named LocalVersion3.xml located at mxd directory to indicate the current version of game. For example:
```xml
<?xmlversion="1.0"encoding="utf-8"?><Root><zone5_8848_v3>{"product_name":"zone5_8848_v3","version":{"v":"0.0.0.14","view":"V225.6"}}</zone5_8848_v3></Root>
```

It only needs to read the version `0.0.0.14`, so it will only obtain patches from `0.0.0.14` all the way up to `0.0.0.15`. And apparently, if the current version is `0.0.0.9`, it will obtain patches from `0.0.0.9` all the way up to `0.0.0.15` to finish patching.

## Understand what's inside the file list
We assume your current client version is `0.0.0.14`, and you'd like to patch to `0.0.0.15`.

First, it will obtain the file list from following unsigned URL combined with baseURL and fileListUrl:
```
https://mxdver0.jijiagames.com/v3client/build/5/8848/diff/0.0.0.14-0.0.0.15-02c012823140469880c530ff0418cde8/5_0.0.0.14-0.0.0.15_FileList.dat
```

It's another manifest in JSON:

```json
{
	"baseUrl":"https://mxdver0.jijiagames.com/v3client/build/5/8848/diff/0.0.0.14-0.0.0.15-02c012823140469880c530ff0418cde8",
	"backupBaseUrl":"https://mxdver0.jijiagames.com/v3client/build/5/8848/diff/0.0.0.14-0.0.0.15-02c012823140469880c530ff0418cde8",
	"comment":"",
	"directFileUrl":"/5_0.0.0.14-0.0.0.15_PList.txt",
	"directFileType":"plist",
	"directFileSize":"724",
	"fileList":[
		{
			"url":"/5_0.0.0.14-0.0.0.15.zip",
			"path":"/5_0.0.0.14-0.0.0.15.zip",
			"md5":"D719A2F2DE067C4202F2168FD84DE51E",
			"size":"1571048847"
		},
		{
			"url":"/5_0.0.0.14-0.0.0.15_1.zip",
			"path":"/5_0.0.0.14-0.0.0.15_1.zip",
			"md5":"5C9A25F9BAFABFDC7D3E4C292789EF26",
			"size":"2082999017"
		},
		{
			"url":"/5_0.0.0.14-0.0.0.15_2.zip",
			"path":"/5_0.0.0.14-0.0.0.15_2.zip",
			"md5":"18A6356DB0A8B6810850DB3F2EAB3EB1",
			"size":"1930345939"
		},
		{
			"url":"/5_0.0.0.14-0.0.0.15_3.zip",
			"path":"/5_0.0.0.14-0.0.0.15_3.zip",
			"md5":"3E7DB62DB99B67D9BAD53E3D90BAD7FA",
			"size":"1311495010"
		},
		{
			"url":"/5_0.0.0.14-0.0.0.15_4.zip",
			"path":"/5_0.0.0.14-0.0.0.15_4.zip",
			"md5":"1AA3770BE1AA65B493748E19DC116C05",
			"size":"993287789"
		},
		{
			"url":"/5_0.0.0.14-0.0.0.15_5.zip",
			"path":"/5_0.0.0.14-0.0.0.15_5.zip",
			"md5":"7D48615147D5187535B643DDCE95C38C",
			"size":"2075386918"
		},
		{
			"url":"/5_0.0.0.14-0.0.0.15_6.zip",
			"path":"/5_0.0.0.14-0.0.0.15_6.zip",
			"md5":"F3019928C3EBCAB48203F0DA74A98186",
			"size":"1687327843"
		},
		{
			"url":"/5_0.0.0.14-0.0.0.15_7.zip",
			"path":"/5_0.0.0.14-0.0.0.15_7.zip",
			"md5":"6A86E2EEB0477746C7796477FA5D14E2",
			"size":"1848088130"
		},
		{
			"url":"/5_0.0.0.14-0.0.0.15_8.zip",
			"path":"/5_0.0.0.14-0.0.0.15_8.zip",
			"md5":"970095A3BC87C3039D4C4BB4A5BD718B",
			"size":"1626675013"
		},
		{
			"url":"/5_0.0.0.14-0.0.0.15_9.zip",
			"path":"/5_0.0.0.14-0.0.0.15_9.zip",
			"md5":"3C83D07110AE35D264D026443E22FD81",
			"size":"4240934"
		}
	]
}
```

As you can see, the patch from `0.0.0.14` to `0.0.0.15` consists of 10 zip files. In default behavior, the Launcher will download all 10 zip files at once from following unsigned URLs:
```
https://mxdver0.jijiagames.com/v3client/build/5/8848/diff/0.0.0.14-0.0.0.15-02c012823140469880c530ff0418cde8/5_0.0.0.14-0.0.0.15.zip
https://mxdver0.jijiagames.com/v3client/build/5/8848/diff/0.0.0.14-0.0.0.15-02c012823140469880c530ff0418cde8/5_0.0.0.14-0.0.0.15_1.zip
https://mxdver0.jijiagames.com/v3client/build/5/8848/diff/0.0.0.14-0.0.0.15-02c012823140469880c530ff0418cde8/5_0.0.0.14-0.0.0.15_2.zip
https://mxdver0.jijiagames.com/v3client/build/5/8848/diff/0.0.0.14-0.0.0.15-02c012823140469880c530ff0418cde8/5_0.0.0.14-0.0.0.15_3.zip
https://mxdver0.jijiagames.com/v3client/build/5/8848/diff/0.0.0.14-0.0.0.15-02c012823140469880c530ff0418cde8/5_0.0.0.14-0.0.0.15_4.zip
https://mxdver0.jijiagames.com/v3client/build/5/8848/diff/0.0.0.14-0.0.0.15-02c012823140469880c530ff0418cde8/5_0.0.0.14-0.0.0.15_5.zip
https://mxdver0.jijiagames.com/v3client/build/5/8848/diff/0.0.0.14-0.0.0.15-02c012823140469880c530ff0418cde8/5_0.0.0.14-0.0.0.15_6.zip
https://mxdver0.jijiagames.com/v3client/build/5/8848/diff/0.0.0.14-0.0.0.15-02c012823140469880c530ff0418cde8/5_0.0.0.14-0.0.0.15_7.zip
https://mxdver0.jijiagames.com/v3client/build/5/8848/diff/0.0.0.14-0.0.0.15-02c012823140469880c530ff0418cde8/5_0.0.0.14-0.0.0.15_8.zip
https://mxdver0.jijiagames.com/v3client/build/5/8848/diff/0.0.0.14-0.0.0.15-02c012823140469880c530ff0418cde8/5_0.0.0.14-0.0.0.15_9.zip
```

By extracting the first ZIP file, you'll find a file named `patch_delta_direct.dat`, which is a manifest file in XML contains DeltaPathInfo, DeltaMD5Info, OriginMD5Info, ResultMD5Info, NewPathInfo, NewMD5Info, DelPathInfo, DelMD5Info. It looks like this:
```xml
<XMLROOT>
<DeltaPathInfo>
<DeltaPathSubItem Key="mxd\Data\Base\Base.wz" Value="Pkg\mxd\Data\Base\Base.wz.hdiff"/>
<DeltaPathSubItem Key="mxd\Data\Character\Afterimage\Afterimage.wz" Value="Pkg\mxd\Data\Character\Afterimage\Afterimage.wz.hdiff"/>
<DeltaPathSubItem Key="mxd\Data\Character\Accessory\_Canvas\_Canvas.wz" Value="Pkg\mxd\Data\Character\Accessory\_Canvas\_Canvas.wz.hdiff"/>
<DeltaPathSubItem Key="mxd\Data\Character\Accessory\Accessory.wz" Value="Pkg\mxd\Data\Character\Accessory\Accessory.wz.hdiff"/>
<!--(truncated)-->
</DeltaPathInfo>
<DeltaMD5Info>
<DeltaMD5SubItem Key="mxd\Data\Base\Base.wz.hdiff" Value="1CE15A117D4BBB02EAC14089A9F34D84"/>
<DeltaMD5SubItem Key="mxd\Data\Character\Afterimage\Afterimage.wz.hdiff" Value="CADE4E5DBBFAF992D03D88A6A7711DED"/>
<DeltaMD5SubItem Key="mxd\Data\Character\Accessory\_Canvas\_Canvas.wz.hdiff" Value="C54E3A393D6BF74C6FBE89DD239A8F27"/>
<DeltaMD5SubItem Key="mxd\Data\Character\Accessory\Accessory.wz.hdiff" Value="CADE4E5DBBFAF992D03D88A6A7711DED"/>
<!--(truncated)-->
</DeltaMD5Info>
<OriginMD5Info>
<OriginMD5SubItem Key="mxd\Data\Base\Base.wz" Value="02A21289288E2FB5A448E61989265649"/>
<OriginMD5SubItem Key="mxd\Data\Character\Afterimage\Afterimage.wz" Value="160B2544B4BE992F7106C02CBE0B8CF8"/>
<OriginMD5SubItem Key="mxd\Data\Character\Accessory\_Canvas\_Canvas.wz" Value="DD47A6AF938484B29346BC5DA4F2DD5D"/>
<OriginMD5SubItem Key="mxd\Data\Character\Accessory\Accessory.wz" Value="160B2544B4BE992F7106C02CBE0B8CF8"/>
<!--(truncated)-->
</OriginMD5Info>
<ResultMD5Info>
<ResultMD5SubItem Key="mxd\Data\Base\Base.wz" Value="7AEE6EAE079C5CF5C99E0584A6761929"/>
<ResultMD5SubItem Key="mxd\Data\Character\Afterimage\Afterimage.wz" Value="F8DECB5C8ADF3A52D103B9D0678D4514"/>
<ResultMD5SubItem Key="mxd\Data\Character\Accessory\_Canvas\_Canvas.wz" Value="4A48F4E30F17605BC0B466C48842AAFD"/>
<ResultMD5SubItem Key="mxd\Data\Character\Accessory\Accessory.wz" Value="F8DECB5C8ADF3A52D103B9D0678D4514"/>
<!--(truncated)-->
</ResultMD5Info>
<NewPathInfo>
<NewPathSubItem Key="mxd\Data\Effect\_Canvas\_Canvas_004.wz" Value="Pkg\mxd\Data\Effect\_Canvas\_Canvas_004.wz"/>
<NewPathSubItem Key="mxd\Data\Mob\BossPattern\BossObjectAction\BossObjectAction.ini" Value="Pkg\mxd\Data\Mob\BossPattern\BossObjectAction\BossObjectAction.ini"/>
<NewPathSubItem Key="mxd\Data\Mob\BossPattern\BossObjectAction\BossObjectAction.wz" Value="Pkg\mxd\Data\Mob\BossPattern\BossObjectAction\BossObjectAction.wz"/>
<!--(truncated)-->
</NewPathInfo>
<NewMD5Info>
<NewMD5SubItem Key="mxd\Data\Effect\_Canvas\_Canvas_004.wz" Value="19122C65E43F9529639CB7CC0C9B55C7"/>
<NewMD5SubItem Key="mxd\Data\Mob\BossPattern\BossObjectAction\BossObjectAction.ini" Value="0BC2B8DFB68A391FDB8C3BCEE291294D"/>
<NewMD5SubItem Key="mxd\Data\Mob\BossPattern\BossObjectAction\BossObjectAction.wz" Value="F8DECB5C8ADF3A52D103B9D0678D4514"/>
<!--(truncated)-->
</NewMD5Info>
<DelPathInfo>
<DelPathSubItem Key="mxd\Sddyn09.dll" Value="mxd\Sddyn09.dll"/>
<DelPathSubItem Key="mxd\uninst.exe" Value="mxd\uninst.exe"/>
</DelPathInfo>
<DelMD5Info>
<DelMD5SubItem Key="mxd\Sddyn09.dll" Value="89FB7C8D380D3E15D8A932926E3BF390"/>
<DelMD5SubItem Key="mxd\uninst.exe" Value="6C69D07DE0C753DA924DFE390E8ABAD4"/>
</DelMD5Info>
</XMLROOT>
```

They clearly correspond to operations defined in existing `.patch` file.

## Applying a patch
By looking at ZIP file, you can clearly see patches are located at Pkg directory. So you need to ensure patches located at this directory can be correctly processed.

### Adding File
New files were included in patch ZIPs as is, so simply copy the newly added file to target directory.

### Deleting File
Just delete it. You don't even need to check if MD5 checksum matches.

### Updating File
Diff files for each files are in `.hdiff` format, and you can clearly see header magic `HDIFFSF20`, which indicates it use HDiffPatch as diff file algorithm. I'm sure you can easily find documentations about how HDiffPatch works, and HDiffPatch implementation is also readily available in various of languages, including C#, Rust, Python.

Basically:
1. Launcher will do checksum of existing target file.
2. If the MD5 checksum matches the result MD5, skip this file.
3. If the MD5 checksum matches the origin MD5, apply the corresponding hdiff patch, then check MD5 again.
4. If the MD5 checksum of patched file matches the result MD5, mark the target file as completed.
5. If the MD5 checksum mismatches before or after patching, or the file is missing, mark the target file as corrupted.

Repeating the process in every files listed in manifest, you'll eventually finish patching.

As for corrupted files that were discovered, they can be downloaded individually with method mentioned in previous article.

## Summary
Similar to GMS patcher, the latest CMS patcher has not supported to copy data from multiple old files like KMS does. But it relies on a static patch list file that does not require URL signing, which made patcher sniffing possible.

Combining with Client downloader, you can still have access to new client before SQ Games releasing a new version to public.

You can check [cmsdl](https://github.com/HikariCalyx/cmsdl), which is a Rust implementation of CMS client downloader and patcher. It can be used as replacement of official Launcher.

## Change log

- 2026-06-25, v1.0

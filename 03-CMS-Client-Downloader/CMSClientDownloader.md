# CMS Client Downloader

As of May 13, 2026 with v225 update, CMS has decided to move on to "Launcher v3", which was a proprietary launcher developed by SQ Games originally for Final Fantasy XIV, and scrapped old patcher format, which prevents us from downloading new client or manual updating in prior of maintenance.

This article introduces the latest (May 2026) CMS client downloading protocol. It may also apply on other games that used similar Launcher by SQ Games, like Final Fantasy XIV.


## URL Signing
Since around August 2024, SQ Games decided to implement dynamically signed URL as an effort to prevent unauthorized scrapping.

Before the introduction of Launcher v3, the signature was calculated with a backend API wrapped by domain secauth.gcdn.sdo.com. For example, you must visit this webpage with correct Referer Header from CMS official website:
```
https://secauth.gcdn.sdo.com/?url=https://mxdver0.jijiagames.com/226/MXD_v20260623_downloader.exe
```

After clearing the CAPTCHA challenge, backend API will generate the dynamically signed URL like this:
```
https://mxdver0.jijiagames.com/202606242335/3530913a19762d6edafbab2cc626f54a/226/MXD_v20260623_downloader.exe
```

Noticed the signed URL contains a timestamp (UTC+8) in yyyyMMddHHmm format, and a hash, but we don't know how it was calculated.

With the release of Launcher v3, we found the Launcher v3 could get the signed URL itself, without user interaction with CAPTCHA.

Getting the timestamp is easy, but getting the hash is difficult. With reverse engineering of the Launcher v3, how the URL signing works is discovered.

First, the Launcher itself contains a RSA public key:
```pem
-----BEGIN PUBLIC KEY-----
MIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQCbHyTRH+DWw75sjRijIHobLf2r
MNE3ob36WrpZePKU8V9ePQlLXvCVCQq4uFSF2KDtJwm9IBoSHzka36c38yMfYk/+
FO/uIjcWOhgyzGbDajHQqtsKSTGqCWuoDdJiBDdb/fAVyvUToTaRFwpc8hYLn62i
O8zhpevAa4tWgHDPFwIDAQAB
-----END PUBLIC KEY-----
```

And it would request server-let and client md5key from following XML manifest:
```
https://downloader.dorado.sdo.com/v3launcher/5/v3ctrl.xml
```

As of when the article is written, the server-let md5key is this:
```
451a58c3de16c4d133d4d3fa8fee0e4e0de76b77a4156224ca5ea186f22db1f4af56427d5f0ee7bf6a7f96401d1890a158f26d7542d170815b5e81514a869bef8bfb131109281b0125d7904597671aa62637c5fe9bcee704d8893ac5f0f2358eb82749b08ab493d526af2fc30e0aa8d7bd1677e945483db7570957910bd5ea48
```

The client md5key is this:
```
95338a729569daf8fdce6e0734be13296679204adf31007897a615b3b43c8597ac61f6979a97254cde9e8f45355221814cc69d1d1ab6d754a16982f078baf43d74ade36c8c494992318a97a62954587e2c12fb6f1d2e6553fbb1e46b3b53af6de95b8dda496f50b85652f7cde6612af53e770959b13254c4cf1031e45d590f10
```

You need to save them in 128-byte binary. Decrypting them with forementioned RSA public key will get 2 random strings. For example, here's the decrypted server-let key:
```
89T532jrQxUen6375E983L7758vajQSz
```

client key:
```
A9D8rTV72Fh7O8w7XPLp672657844VeS
```

By combining the first 16 characters of client key and last 16 characters of server-let key, you'll get a challenge key:
```
A9D8rTV72Fh7O8w75E983L7758vajQSz
```

About getting timestamp. To ensure the downloader will work in any timezone, I recommend you get UTC-based timestamp, then add UTC+8 offset afterwards.

Assuming the current UTC+8 timestamp you get is `202606242336`.

Next, we need to split the unsigned URL into 2 parts:
```
Base domain: https://mxdver0.jijiagames.com
URL Path: /226/MXD_v20260623_downloader.exe
```

Then, calculate the MD5 checksum of following string:
```
<challenge key><current UTC+8 timestamp><URL Path>
```
And in this case, you need to calculate MD5 checksum of following string:
```
A9D8rTV72Fh7O8w75E983L7758vajQSz202606232336/226/MXD_v20260623_downloader.exe
```

And we get following value:
```
0c204189c19ed06d4516eff8be5b29fc
```

Now, combine the URL again and you get a signed URL:
```
<Base Domain>/<current UTC+8 timestamp>/<md5 checksum><URL Path>
```

In this case:
```
https://mxdver0.jijiagames.com/202606232336/0c204189c19ed06d4516eff8be5b29fc/226/MXD_v20260623_downloader.exe
```

After you understand how to get signed URL for passing authorization, we can proceed to next step.

A recommended implementation would be creating 2 functions, one for getting challenge key, another is for signing URL. You should obtain challenge key once and store it into a variable, so it don't need to request XML manifest every time when downloading a file.

## Client manifest
So how does official launcher know where latest version is stored?

Unfortunately, this is hardcoded in every version of official launchers. 

In v225 update, the manifest file could be downloaded from following unsigned URL:
```
https://mxdver0.jijiagames.com/v3client/build/5/8848/apppc/961/client_all_files_list.dat
```

In v226 update, the manifest file could be downloaded from following unsigned URL:
```
https://mxdver0.jijiagames.com/v3client/build/5/8848/apppc/1020/client_all_files_list.dat
```

Noticed the number between apppc and client_all_files_list.dat doesn't strictly follow actual game version. For now, I would use exhaustive searching for getting the latest client number by checking from number 961 (which was the first version that used) and all the way up to next 160 versions since some versions between 961 and 1020 are invalid. Keep searching until the last known accessible version is discovered.

Anyway, once you find the latest version of manifest, you'll be able to see content inside.

```
https://mxdver0.jijiagames.com/v3client/build/5/8848/apppc/961|5|0.0.0.9
mxd\bdvid64.dll|8432048|02D3A68F0F7EE2DEFEE6C315DC2F873E
mxd\BlackCipher\BlackCall64.aes|48270816|876626E303E1229D908E972C58DC3265
mxd\BlackCipher\BlackCipher64.aes|55636632|B9789B61DF84563F4677BB97B305D863
mxd\BlackCipher\BlackXchg.aes|8993280|781E2F7FECB15AC1207EF586E53D1042
mxd\BlackCipher\config.bc|17468|8A1C62AA38B2874A082677535EF225E5
mxd\BlackCipher\CrashReporter.dll|1082560|3BF0989CFCC807E5537AFDC79F441DFA
mxd\BlackCipher\CrashReporter_64.dll|1449152|847361131CECDCC59A0E4CC7DB46865E
mxd\Canvas.dll|2092512|0AA82F1815DF84D7B2969486B7FD9455
mxd\CrashReportClient.exe|790600|DB0881ED89184EAC1B8A387F7EE9A316
mxd\CrashReporter.dll|824400|A1EBEEEE95EFD088EADF463462FE3AC7
mxd\Data\Base\Base.ini|15|73158CDE0884B152E85BB12BBBA328E0
mxd\Data\Base\Base.wz|273|02A21289288E2FB5A448E61989265649
mxd\Data\Base\Base_000.wz|4135490|91A38896F0E4DCA8BBD3CE8B843C47E7
mxd\Data\Character\Accessory\Accessory.ini|15|73158CDE0884B152E85BB12BBBA328E0
mxd\Data\Character\Accessory\Accessory.wz|79|160B2544B4BE992F7106C02CBE0B8CF8
mxd\Data\Character\Accessory\Accessory_000.wz|6325512|3702AC6F87E11050A1D70F8A24B88ABE
mxd\Data\Character\Accessory\_Canvas\_Canvas.ini|15|73158CDE0884B152E85BB12BBBA328E0
mxd\Data\Character\Accessory\_Canvas\_Canvas.wz|63|DD47A6AF938484B29346BC5DA4F2DD5D
mxd\Data\Character\Accessory\_Canvas\_Canvas_000.wz|2968029|C2C205E4E73B0D8BED42D501EB63924E
(truncated)
```

In the first line, there are 3 arguments:
```
File Base URL: https://mxdver0.jijiagames.com/v3client/build/5/8848/apppc/961
App ID: 5
Version: 0.0.0.9
```

And in rest of lines, there are 3 arguments.
```
Filename: mxd\Base\Base.wz
File Length (bytes): 273
MD5 Checksum of the File: 02A21289288E2FB5A448E61989265649
```

## How a file is downloaded?
You might think it would request a file from following unsigned URL:
```
https://mxdver0.jijiagames.com/v3client/build/5/8848/apppc/961/mxd/Data/Base/Base.wz
```

But this is wrong. By using Web Debugger, you can see Official Launcher is downloading a file with the name in another MD5 checksum.
```
https://mxdver0.jijiagames.com/v3client/build/5/8848/apppc/961/mxd/Data/Base/2F94FE1EA9EEF0B69951868B14CE5AA6
```

To get the filename, you need to calculate the MD5 checksum of following string in **UTF-16LE encoding**, and make the filename uppercase. You have to keep backslashes here.
```
<AppID>_<Version>_<Filename>
```

In this case:
```
5_0.0.0.9_mxd\Base\Base.wz
```

And you should get following MD5 checksum:
```
2F94FE1EA9EEF0B69951868B14CE5AA6
```

Next, you should get the directory without filename, and in this case, you'll get `mxd/Base/`. Here you also need to replace backslashes with slashes.

Now you can combine it into following unsigned URL:
```
<FileBaseURL>/<directory><MD5_Checksum_of_requested_file>
```

In the downloader you create, you can check if the file you'd like to download exists locally, do size verification and checksum verification before you download when file exists, so it can skip files that's already downloaded.

Repeating process in every files listed in manifest, you'll eventually get a full client.

## Launching
Since v226 update, SQ Games no longer allows you run the game by double-clicking MapleStory.exe.

To launch the game, simply pass switch `--sqLauncher` to MapleStory.exe.

## Summary
This article wraps up how CMS client downloader works. In my opinion, the most critical part is URL signing, which is the biggest obstacle of downloading file.

You can check [cmsdl](https://github.com/HikariCalyx/cmsdl), which is a Rust implementation of CMS client downloader and patcher. It can be used as replacement of official Launcher.

In the next article we'll be looking at the CMS patcher.

## Credits
Thanks to `Deneo` for discovering how Launcher works.

## Change log

- 2026-06-25, v1.0

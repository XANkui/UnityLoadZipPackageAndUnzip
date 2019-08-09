# UnityLoadZipPackageAndUnzip
Unity 中使用 Zip 压缩和解压zip文件

主要内容包括：

1、ZipHelper 脚本 封装了 zip 库对文件的压缩方法 和 对指定文件解压缩的方法

2、包含一个demo，测试 在Android 端，把 StreamingAssets文件夹下的 zip 下载解压到 Application.persistentDataPath 文件夹下，并且可以播放视频

注意实现：

1、Unity 中的文件夹 StreamingAssets 只读文件夹，里面的文件不会被 Unity 压缩；

2、在Unity中使用 ICSharpCode.SharpZipLib.dll时候，可能要与 I18N 系列文件一起使用，不然可能解压不成功，报错 ： System.NotSupportedException: Encoding 437 data could not be found. Make sure you have correct international codeset assembly installed and enabled.

3、I18N 系列文件可以在 Unity 安装路径下的文件夹中找到 ：...\Unity\Editor\Data\Mono\lib\mono\unity

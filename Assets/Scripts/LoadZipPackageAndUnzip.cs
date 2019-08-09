
using UnityEngine;
using System.Collections;
using System;
using System.IO;
using ICSharpCode.SharpZipLib.Zip;


/// <summary>
/// 下载压缩文件，并且解压压缩文件到文件夹
/// </summary>
public class LoadZipPackageAndUnzip : MonoBehaviour
{

    private string sourcesPath = ""; // 暂时没什么作用，用来记录一些文件路径使用
    private string targetPath = "";  // 暂时没什么作用，用来记录一些文件路径使用

    private string filename = "Fuyou";
    private string url;


    
    // 按钮事件，点击按钮开始下载zip与解压zip文件
    public void button()

    {

        sourcesPath = Application.persistentDataPath + "/Zip";
        targetPath = Application.persistentDataPath + "/Resources";
        Debug.Log("sourcesPaht is:" + sourcesPath + "   " + targetPath);

        url = Application.streamingAssetsPath + "/" + "Fuyou.zip";
        StartCoroutine(Wait_LoadDown(filename, url));

    }





    /// <summary>
    /// 下载
    /// </summary>
    /// <param name="ZipID" ZipID的名字，用于存储解压出的每一个Zip文件></param>
    /// <param name="url" Zip下载地址></param>
    /// <returns></returns>
    IEnumerator Wait_LoadDown(string ZipID, string url)
    {
        WWW www = new WWW(url);
        yield return www;
        if (www.isDone)
        {
            if (www.error == null)
            {
                Debug.Log("下载成功");
                string dir = Application.persistentDataPath;
                Debug.Log(dir);
                if (!Directory.Exists(dir))
                    Directory.CreateDirectory(dir);

                yield return new WaitForEndOfFrame();
                //直接使用 将byte转换为Stream，省去先保存到本地在解压的过程
                LoadZipPackageAndUnzip.SaveZip(ZipID, url, www.bytes, null);

            }
            else
            {
                Debug.Log(www.error);
            }
        }

    }

    /// <summary> 
    /// 解压功能(下载后直接解压压缩文件到指定目录) 
    /// </summary> 
    /// <param name="wwwStream">www下载转换而来的Stream</param> 
    /// <param name="zipedFolder">指定解压目标目录(每一个Obj对应一个Folder)</param> 
    /// <param name="password">密码</param> 
    /// <returns>解压结果 true：解压成功</returns> 
    public static bool SaveZip(string ZipID, string url, byte[] ZipByte, string password)
    {
        bool result = true;
        FileStream fs = null;
        ZipInputStream zipStream = null;
        ZipEntry ent = null;
        string fileName;

        ZipID = Application.persistentDataPath + "/" + ZipID;
        Debug.Log("ZipID" + ZipID);
        Debug.Log(ZipID);

        if (!Directory.Exists(ZipID))
        {
            Directory.CreateDirectory(ZipID);
        }
        try
        {
            //直接使用 将byte转换为Stream，省去先保存到本地在解压的过程
            Stream stream = new MemoryStream(ZipByte);
            zipStream = new ZipInputStream(stream);

            if (!string.IsNullOrEmpty(password))
            {
                zipStream.Password = password;
            }
            while ((ent = zipStream.GetNextEntry()) != null)
            {
                if (!string.IsNullOrEmpty(ent.Name))
                {
                    fileName = Path.Combine(ZipID, ent.Name);

                    #region      Android
                    fileName = fileName.Replace('\\', '/');
                    Debug.Log(fileName);
                    if (fileName.EndsWith("/"))
                    {
                        Directory.CreateDirectory(fileName);
                        continue;

                    }
                    #endregion
                    fs = File.Create(fileName);

                    int size = 2048;
                    byte[] data = new byte[size];
                    while (true)
                    {
                        size = zipStream.Read(data, 0, data.Length);
                        if (size > 0)
                        {
                            //fs.Write(data, 0, data.Length);
                            Debug.Log(data.Length);
                            fs.Write(data, 0, size);//解决读取不完整情况
                        }
                        else
                            break;
                    }

                    Debug.Log("解压基本完成...");
                }
            }
        }
        catch (Exception e)
        {
            Debug.Log(e.ToString());
            result = false;
        }
        finally
        {
            if (fs != null)
            {
                fs.Close();
                fs.Dispose();
            }
            if (zipStream != null)
            {
                zipStream.Close();
                zipStream.Dispose();
            }
            if (ent != null)
            {
                ent = null;
            }

            // 垃圾回收
            GC.Collect();
            GC.Collect(1);

            
        }
        return result;
    }
}

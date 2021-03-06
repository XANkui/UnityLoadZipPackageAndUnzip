﻿using ICSharpCode.SharpZipLib.Zip;
using System;
using System.IO;
using UnityEngine;

public class ZipHelper 
{
    /// <summary>  
    /// 功能：解压zip格式的文件。  
    /// </summary>  
    /// <param name="zipFilePath">压缩文件路径 </param>  
    /// <param name="unZipDir">解压文件存放路径,为空时默认与压缩文件同一级目录下，跟压缩文件同名的文件夹</param>  
    /// <param name="password">解压文件的解压密码</param>  
    /// <returns>解压是否成功</returns>  
    public static bool UnZip(string zipFilePath, string unZipDir, string password = null)
    {
        try
        {
            if (zipFilePath == string.Empty)
            {
                throw new Exception("压缩文件不能为空！");
            }
            if (!File.Exists(zipFilePath))
            {
                throw new FileNotFoundException("压缩文件不存在！");
            }
            //解压文件夹为空时默认与压缩文件同一级目录下，跟压缩文件同名的文件夹  
            if (unZipDir == string.Empty)
                unZipDir = zipFilePath.Replace(Path.GetFileName(zipFilePath), Path.GetFileNameWithoutExtension(zipFilePath));
            if (!unZipDir.EndsWith("/"))
                unZipDir += "/";
            if (!Directory.Exists(unZipDir))
                Directory.CreateDirectory(unZipDir);
            using (var s = new ZipInputStream(File.OpenRead(zipFilePath)))
            {
                // 添加密码解压
                if (string.IsNullOrEmpty(password) == false) {
                    s.Password = password;
                }

                ZipEntry theEntry;
                while ((theEntry = s.GetNextEntry()) != null)
                {
                    string directoryName = Path.GetDirectoryName(theEntry.Name);
                    string fileName = Path.GetFileName(theEntry.Name);
                    if (!string.IsNullOrEmpty(directoryName))
                    {
                        Directory.CreateDirectory(unZipDir + directoryName);
                    }
                    if (directoryName != null && !directoryName.EndsWith("/"))
                    {
                    }
                    if (fileName != String.Empty)
                    {
                        using (FileStream streamWriter = File.Create(unZipDir + theEntry.Name))
                        {

                            int size;
                            byte[] data = new byte[2048];
                            while (true)
                            {
                                size = s.Read(data, 0, data.Length);
                                if (size > 0)
                                {
                                    streamWriter.Write(data, 0, size);
                                }
                                else
                                {
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            return true;
        }
        catch (Exception)
        {

            return false;
        }

    }

    /// <summary>
    /// 压缩所有的文件
    /// </summary>
    /// <param name="filesPath">（目录文件夹的路径（不是具体文件））</param>
    /// <param name="zipFilePath">（xxxx.zip）</param>
    /// <param name="password">添加压缩密码</param>
    public static void CreateZipFile(string filesPath, string zipFilePath, string password = null)
    {
        Debug.Log("CreateZipFile");

        if (!Directory.Exists(filesPath))
        {
            return;
        }
        ZipOutputStream stream = new ZipOutputStream(File.Create(zipFilePath));
        stream.SetLevel(0); // 压缩级别 0-9

        // 添加压缩包密码
        if (string.IsNullOrEmpty(password) == false) {
            stream.Password = password;
        }

        byte[] buffer = new byte[4096]; //缓冲区大小
        string[] filenames = Directory.GetFiles(filesPath, "*.*", SearchOption.AllDirectories);
        foreach (string file in filenames)
        {
            ZipEntry entry = new ZipEntry(file.Replace(filesPath, ""));
            entry.DateTime = DateTime.Now;
            stream.PutNextEntry(entry);
            using (FileStream fs = File.OpenRead(file))
            {
                int sourceBytes;
                do
                {
                    sourceBytes = fs.Read(buffer, 0, buffer.Length);
                    stream.Write(buffer, 0, sourceBytes);
                } while (sourceBytes > 0);
            }
        }
        stream.Finish();
        stream.Close();

        Debug.Log("CreateZipFile Success!!!");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestZip : MonoBehaviour
{
    private string createZipSrcFilePath;    // 目录文件夹的路径（不是具体文件）
    private string createSrcFileSaveZipPath; 

    // Start is called before the first frame update
    void Start()
    {
        createZipSrcFilePath = Application.dataPath + "/" + "Video";
        Debug.Log("createZipSrcFilePath:"+ createZipSrcFilePath);
        createSrcFileSaveZipPath = Application.dataPath + "/Fuyou.zip";

        //ZipHelper.CreateZipFile(createZipSrcFilePath, createSrcFileSaveZipPath);

        bool isZipSuccess = ZipHelper.UnZip(createSrcFileSaveZipPath,createZipSrcFilePath);
        if (isZipSuccess == true) {
            Debug.Log("解压成功...");
        }
        else {
            Debug.Log("解压失败...");
        }

        createZipSrcFilePath = Application.dataPath + "/" + "Test";
        createSrcFileSaveZipPath = Application.dataPath + "/TestSlamSlope0809_003.zip";
        isZipSuccess = ZipHelper.UnZip(createSrcFileSaveZipPath, createZipSrcFilePath, "0123");
        if (isZipSuccess == true)
        {
            Debug.Log("解压成功...");
        }
        else
        {
            Debug.Log("解压失败...");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

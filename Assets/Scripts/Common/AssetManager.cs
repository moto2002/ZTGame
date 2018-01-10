﻿using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class AssetManager
{
    private static AssetBundle _assetBundleManifest;
    private static Dictionary<string,AssetBundle> _assetBundleDic;
    /* 
     * @brief 加载资源
     * @param path 资源路径
     * @param callback 回调函数
     */
    public static void LoadAsset(string path, UnityAction<Object, string> callback = null,System.Type type = null)
    {
        // Windows 平台分隔符为 '/', OS 平台 路径分隔符为 '\'， 此处是一个大坑
        if (Application.platform == RuntimePlatform.WindowsEditor)
        {
            path = path.Replace('\\', '/');
        }
       
//#if UNITY_EDITOR
//        //编辑器模式下 资源获取
//        Object obj = null;
//        if (null != type)
//        {
//            obj = UnityEditor.AssetDatabase.LoadAssetAtPath(path,type);
//        }
//        else
//        {
//            obj = UnityEditor.AssetDatabase.LoadMainAssetAtPath(path);
//        }
//        if (null != callback)
//        {
//            callback(obj, path);
//        }
//        return;
//#endif

        string fileName = System.IO.Path.GetFileName(path);
        string fileNameEx = System.IO.Path.GetFileNameWithoutExtension(path);
        string abName = path.Replace(fileName, "").Replace('/', '_');
        abName = abName.Substring(0, abName.Length - 1).ToLower();
        AssetBundle bundle = AssetBundleManager.GetInstance().LoadAssetBundleAndDependencies(abName); ;
        //加载assetBundleManifest文件    
        if (null != bundle)
        {   
            Object obj2 = bundle.LoadAsset(fileNameEx);
            callback(obj2, path);
            return;
        }

        callback(null, path);
    }


    //加载所有资源
    public static void LoadAllAsset(string path, UnityAction<Object[], string> callback = null)
    {
    #if UNITY_EDITOR
        //编辑器模式下 资源获取
        Object[] objs = null;
        objs = UnityEditor.AssetDatabase.LoadAllAssetsAtPath(path);
        if (null != callback)
        {
            callback(objs, path);
        }
        return;
#endif
    }

    public static AssetBundle GetAssetBundle(string abName)
    {
        if (null == _assetBundleDic)
        {
            _assetBundleDic = new Dictionary<string, AssetBundle>();
        }

        if (!_assetBundleDic.ContainsKey(abName))
        {
            _assetBundleDic[abName] = AssetBundle.LoadFromFile(Application.dataPath + "/AssetBundles/" + abName);
        }
        return _assetBundleDic[abName];
    }

    public static void Destroy()
    {
        if(null != _assetBundleManifest){
            _assetBundleManifest.Unload(true);
            _assetBundleManifest = null;
        }
        if (null != _assetBundleDic)
        {
            foreach(string abName in _assetBundleDic.Keys){
                _assetBundleDic[abName].Unload(true);
            }
            _assetBundleDic = null;
        }
    }
}

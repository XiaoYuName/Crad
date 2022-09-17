using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Crad.Assets
{
    public class AssetsBundleManager : Singleton<AssetsBundleManager>
    {
        public AssetMode Mode;
        private GameObject[] game;
        protected override void Awake()
        {
            base.Awake();
            switch (Mode)
            {
                case AssetMode.LocalBundle:
                    LoadAB();
                    // StartCoroutine(AbLoad());
                    break;
            }
        }

        private void LoadAB()
        {
            string path = "AssetBundle/logineui.bundle";
            var ab = AssetBundle.LoadFromMemory(File.ReadAllBytes(path));
            game = ab.LoadAllAssets<GameObject>();
        }

        private IEnumerator AbLoad()
        {
            string path = "AssetBundle/logineui.bundle";
            AssetBundleCreateRequest request = AssetBundle.LoadFromMemoryAsync(File.ReadAllBytes(path));
            yield return request;
            AssetBundle ab = request.assetBundle;
            game = ab.LoadAllAssets<GameObject>();
            StopCoroutine(AbLoad());
        }


        /// <summary>
        /// 获取一个GameObject对象
        /// </summary>
        /// <param name="itemName"></param>
        /// <returns></returns>
        public GameObject GetAbGameObject(string itemName)
        {
            foreach (var item in game)
            {
                if (item.name == itemName)
                {
                    return item;
                }
            }
            return null;
        }
    }
}


public enum AssetMode
{
    /// <summary>
    /// 加载本地路径
    /// </summary>
    LocalAsset,
    /// <summary>
    /// 加载本地Bundle
    /// </summary>
    LocalBundle,
    /// <summary>
    /// 加载服务器路径
    /// </summary>
    UpdateBundle,
}

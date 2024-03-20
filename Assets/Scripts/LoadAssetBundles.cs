using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class LoadAssetBundles : MonoBehaviour
{
    void Start() => StartCoroutine("AsyncStart");
    private IEnumerator AsyncStart()
    {
        string path = Application.dataPath + "/Import";
        foreach (FileInfo file in new DirectoryInfo(path).GetFiles().Where(file => !file.Name.EndsWith(".meta")))
        {
            AssetBundleCreateRequest createRequest = AssetBundle.LoadFromFileAsync(file.FullName);
            yield return createRequest;
            AssetBundle assetBundle = createRequest.assetBundle;
            if (assetBundle == null)
            {
                Debug.Log($"Failed to load {file.Name}!");
                yield break;
            }
            AssetBundleRequest request = assetBundle.LoadAllAssetsAsync();
            yield return request;
            List<GameObject> gameObjects = new List<GameObject>();
            foreach (object obj in request.allAssets)
            {
                GameObject gameObject = obj as GameObject;
                if (gameObject != null) gameObjects.Add(gameObject);
            }
            foreach (GameObject gameObject in gameObjects)
            {
                Instantiate(gameObject, this.gameObject.transform);
            }
        }
        yield break;
    }
}

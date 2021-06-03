using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class EmptySceneForScreen : MonoBehaviour
{
    [SerializeField] private List<GameObject> objToDisable;

    public void PrepareForShareScreen()
    {
        foreach (GameObject obj in objToDisable)
        {
            obj.SetActive(false);
        }
        StartCoroutine(TakeScreenShotAndShare());
    }

    IEnumerator TakeScreenShotAndShare()
    {
        yield return new WaitForEndOfFrame();

        Texture2D tx = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        tx.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        tx.Apply();

        string path = Path.Combine(Application.temporaryCachePath, "sharedImage.png");
        File.WriteAllBytes(path, tx.EncodeToPNG());

        Destroy(tx);

        new NativeShare().AddFile(path).SetSubject("This is my score").SetText("I got this far! Can you do better?").Share();
    }
}

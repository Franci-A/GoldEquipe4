using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    [System.Serializable]
    struct Parallax
    {
        public GameObject gameObjectsInParallax;
        public float speed;
        public Vector3 StartPos;
        public Vector3 EndPos;
    }

    [SerializeField] List<Parallax> parallaxes;

    private void Update()
    {
        foreach (Parallax item in parallaxes)
        {
            item.gameObjectsInParallax.transform.Translate(new Vector3(item.EndPos.x * item.speed * Time.deltaTime, 0, 0));
            if (item.gameObjectsInParallax.transform.position.x >= item.EndPos.x - .2f && item.gameObjectsInParallax.transform.position.x <= item.EndPos.x + .2f)
            {
                item.gameObjectsInParallax.transform.position = new Vector3(item.StartPos.x, item.gameObjectsInParallax.transform.position.y, 0);
            }

        }
    }
}

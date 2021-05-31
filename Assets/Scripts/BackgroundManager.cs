using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundManager : MonoBehaviour
{
    [System.Serializable]
    struct Parallax
    {
        public List<GameObject> gameObjectsInParallax;
        public float speed;
        public Vector3 StartPos;
        public Vector3 EndPos;
    }

    [SerializeField] List<Parallax> parallaxes;

    private void Update()
    {
        foreach (Parallax item in parallaxes)
        {
            foreach (GameObject obj in item.gameObjectsInParallax)
            {
                obj.transform.Translate(item.EndPos * item.speed * Time.deltaTime);
                if (obj.transform.position.x >= item.EndPos.x -.2f && obj.transform.position.x <= item.EndPos.x + .2f)
                {
                    obj.transform.position = item.StartPos;
                }
            }
        }
    }
}

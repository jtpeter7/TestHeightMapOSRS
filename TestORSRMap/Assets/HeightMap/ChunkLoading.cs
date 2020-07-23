using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkLoading : MonoBehaviour
{
    public GameObject MapGenerator;
    public Texture2D heightMap;
    /*public Texture2D textureMap;
    public Texture2D textureMapLeft;
    public Texture2D textureMapRight;*/
    public int lx, ly;
    public int HeightScale;
    public GameObject test;
    void Start()
    {
        Generate();
    }

    public void Generate()
    {
        Texture2D newHM = new Texture2D(65, 65);
        Texture2D newTM = new Texture2D(256, 256);
        for(int x= 0; x < 43; x++)
        {
            for (int y = 0; y < 25; y++)
            {
                Color[] HMBuffer = heightMap.GetPixels(x*64, y*64, 65, 65);
                newHM.SetPixels(HMBuffer);
                newHM.Apply();

                test = Instantiate(MapGenerator, new Vector3(0, 0, 0), Quaternion.identity);
                test.GetComponent<HMTo3DMesh>().heightMap = newHM;
                test.GetComponent<HMTo3DMesh>().heightScale = HeightScale;
                test.transform.parent = transform;
                test.name = "Chunk:" + x + "," + y;
                test.transform.position = new Vector3(x * 64, 0, y * 64);
                test.GetComponent<HMTo3DMesh>().localx = x;
                test.GetComponent<HMTo3DMesh>().localy = y;
                test.GetComponent<HMTo3DMesh>().Generate();
                test.AddComponent<SerializeMesh>();
            }
        }
    }
}

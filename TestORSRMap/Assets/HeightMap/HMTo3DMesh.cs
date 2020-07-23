using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HMTo3DMesh : MonoBehaviour
{
    public Texture2D heightMap;
    public Texture2D textureMap,testTM;/*
    public Texture2D textureMapLeft;
    public Texture2D textureMapRight;*/
    List<Vector3> vertices = new List<Vector3>();
    List<int> triangles = new List<int>();
    public int heightScale;
    public int localx, localy;
    public GameObject Textures;


    public void Start()
    {
        /*int test = localx*25 + (24-localy);
        Textures = GameObject.Find("Resources");
        testTM = Textures.GetComponent<LoadResources>().AllResources[test];*/
    }

    public void Generate()
    {
        int test = localx * 25 + (24 - localy);
        Textures = GameObject.Find("Resources");
        testTM = Textures.GetComponent<LoadResources>().AllResources[test];
        for (int i = 0; i < 65; i++)
        {
            for(int j = 0; j < 65; j++)
            {
                vertices.Add(new Vector3(i,heightMap.GetPixel(i,j).grayscale * heightScale,j));
                if(i==0 || j == 0) { continue; }
                triangles.Add(65 * i + j); //Top right
                triangles.Add(65 * i + j - 1); //Bottom right
                triangles.Add(65 * (i - 1) + j - 1); //Bottom left - First triangle
                triangles.Add(65 * (i - 1) + j - 1); //Bottom left 
                triangles.Add(65 * (i - 1) + j); //Top left
                triangles.Add(65 * i + j); //Top right - Second triangle
            }
        }
        Vector2[] uvs = new Vector2[vertices.Count];
        for (var i = 0; i < uvs.Length; i++) //Give UV coords X,Z world coords
            uvs[i] = new Vector2(vertices[i].x, vertices[i].z);

        GameObject plane = new GameObject("TestPlane"); //Create GO and add necessary components
        plane.AddComponent<MeshFilter>();
        plane.AddComponent<MeshRenderer>();
        Mesh procMesh = new Mesh();
        procMesh.vertices = vertices.ToArray(); //Assign vertices, uvs, and tris to the mesh
        procMesh.uv = uvs;
        procMesh.triangles = triangles.ToArray();
        procMesh.RecalculateNormals(); //Determines which way the triangles are facing
        plane.GetComponent<MeshFilter>().mesh = procMesh; //Assign Mesh object to MeshFilter 
       
        plane.GetComponent<Renderer>().material.mainTexture = testTM;
        plane.GetComponent<Renderer>().material.mainTextureScale = new Vector2(0.015625f, 0.015625f);
        plane.transform.parent = transform.parent;
        plane.name = transform.name;
        plane.transform.position = transform.position;
        //plane.transform.parent = transform;
        Destroy(gameObject);
    }
}

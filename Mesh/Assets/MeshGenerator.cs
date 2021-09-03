using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshGenerator : MonoBehaviour
{
    Mesh mesh;

    Vector3[] vertices;
    int[] triangles; 
    Vector2 [] uvs;
    Color[] colors;

    public int xSize = 20;
    public int zSize = 20;

    public Gradient gradient;

    private float minTerrainHeight;
    private float maxTerrainHeight;

    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;

        //StartCoroutine(CreateShape());
        CreateShape();
        UpdateMesh();
    }

    private void Update()
    {

        //UpdateMesh();
    }

    void CreateShape()
    {
        //Loop to generate all the triangles
        vertices = new Vector3[(xSize + 1) * (zSize + 1)];

        for (int count=0, z=0; z <= zSize; z++)
        {
            for (int x=0; x <= xSize; x++)
            {

                float y = Mathf.PerlinNoise(x * 0.3f, z * 0.3f) * 4f;

                
                vertices[count] = new Vector3(x, y, z);

                if (y > maxTerrainHeight)
                    maxTerrainHeight = y;
                if (y < minTerrainHeight)
                    minTerrainHeight = y;

                count++;
            }
        }

        int vert = 0;
        int tris = 0;
        triangles = new int[xSize*zSize*6];

        //Loop to render all the quads
        for (int z = 0; z < zSize; z++)
        {
            for (int x = 0; x < xSize; x++)
            {
                triangles[tris + 0] = vert + 0;
                triangles[tris + 1] = vert + xSize + 1;
                triangles[tris + 2] = vert + 1;
                triangles[tris + 3] = vert + 1;
                triangles[tris + 4] = vert + xSize + 1;
                triangles[tris + 5] = vert + xSize + 2;

                vert++;
                tris += 6;
                //yield return new WaitForSeconds(0.00001f);
            }
            vert++;
        }

        //uvs = new Vector2[vertices.Length];
        colors = new Color[vertices.Length];
        for (int count = 0, z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {
                //uvs[count] = new Vector2((float)x / xSize, (float)z / zSize);

                float height = Mathf.InverseLerp(minTerrainHeight, maxTerrainHeight, vertices[count].y);
                colors[count] = gradient.Evaluate(height);

                count++;
            }
        }


    }

    void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        mesh.colors = colors;

        mesh.RecalculateNormals();
    }


    private void OnDrawGizmos()
    {
        if (vertices == null)
            return;

        for (int i = 0; i < vertices.Length; i++)
        {
            Gizmos.DrawSphere(vertices[i], 0.1f);
        }
    }
}

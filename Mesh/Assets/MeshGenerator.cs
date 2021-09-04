using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class MeshGenerator : MonoBehaviour
{
    Mesh mesh;

    Vector3[] vertices;
    int[] triangles; 
    Color[] colorMap;
    
    public int xSize = 100;
    public int zSize = 100;

    private float minTerrainHeight;
    private float maxTerrainHeight;

    public float noiseScale = 10f;
    public int octaves = 4;
    public float persistance = 0.5f;
    public float lacunarity = 2f;

    public int seed = 21;
    public Vector2 offset = new Vector2(13.1f, 6.0f);
    
    public float heightMultiplier = 4f;
    public AnimationCurve heightCurve;
    
    
    public TerrainType[] regions;

    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
               
        
    }

    private void Update()
    {
        CreateShape();
        UpdateMesh();
        //To add movement
        //offset.x += 0.05f;

    }

    void CreateShape()
    {
        //Loop to generate all the triangles
        vertices = new Vector3[(xSize + 1) * (zSize + 1)];
        for (int count=0, z=0; z <= zSize; z++)
        {
            for (int x=0; x <= xSize; x++)
            {


                float y = calculateNoise(x, z, noiseScale, seed, octaves, persistance, lacunarity, offset);
                y = heightCurve.Evaluate(y);
                y *= heightMultiplier;

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
            }
            vert++;
        }

        //Loop to find colours by height
        colorMap = new Color[vertices.Length];
        for (int count = 0, z = 0; z <= zSize; z++)
        {
            for (int x = 0; x <= xSize; x++)
            {

                float height = Mathf.InverseLerp(minTerrainHeight, maxTerrainHeight, vertices[count].y);
                
                for (int i = 0; i < regions.Length; i++)
                {
                    if (height <= regions[i].height)
                    {

                        colorMap[count] = regions[i].colour;
                        break;
                    }
                }

                count++;
            }
        }

    }

    float calculateNoise(int x, int z, float scale, int seed, int ocataves, float persistance, float lacunarity, Vector2 offset)
    {
        System.Random prng = new System.Random(seed);
        Vector2[] octaveOffsets = new Vector2[ocataves];
        for(int i =0;i<ocataves;i++)
        {
            float offsetX = prng.Next(-100000, 100000) + offset.x;
            float offsetZ = prng.Next(-100000, 100000) + offset.y;

            octaveOffsets[i] = new Vector2(offsetX, offsetZ);
        }

        float amplitude = 1f;
        float frequency = 1f;
        float noiseHeight = 0;
        for (int i=0; i < ocataves; i++)
        {
            float x_Coord = (x/scale * frequency) + octaveOffsets[i].x;
            float z_Coord = (z/scale * frequency) + octaveOffsets[i].y;
            float perlinValue = Mathf.PerlinNoise(x_Coord, z_Coord) * 2 - 1;

            noiseHeight += perlinValue * amplitude;

            amplitude *= persistance;
            frequency *= lacunarity;
        }
        

        return noiseHeight;
    }
    void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.colors = colorMap;

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

    [System.Serializable]
    public struct TerrainType
    {
        public string name;
        public float height;
        public Color colour;
    }
}

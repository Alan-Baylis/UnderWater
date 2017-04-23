using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Level
{
    public class LevelGenerator : MonoBehaviour
    {
        private readonly int _chunkSize = 16;
        private Vector2 _shift;
        public float Factor = 0.5f;

        public Material LandsMaterial;
        public int Seed;

        private void Start()
        {
            var dx = ((((Seed + 3) << 5) - 4)*(((Seed - 2) >> 2) - 20)) << 1;
            _shift = new Vector2(dx, -dx);
            BuildMapAsync();
        }

        private void BuildMapAsync()
        {
            var mapSize = 2;
            for (var x = -mapSize; x < mapSize; x++)
                for (var y = -mapSize; y < mapSize; y++)
                {
                    var chunk = BuildChunkFor(x, y);
                    BuildFromChunk(chunk);
                }
        }

        private void BuildFromChunk(Chunk chunk)
        {
            var newChunk = new GameObject("chunk");
            newChunk.transform.parent = transform;
            newChunk.transform.position = chunk.GetWorldPosition();

            var renderer = newChunk.AddComponent<MeshRenderer>();
            renderer.material = LandsMaterial;

            var filter = newChunk.AddComponent<MeshFilter>();
            var collider = newChunk.AddComponent<PolygonCollider2D>();

            var mesh = new Mesh {name = "ChunkMesh"};

            var vertices = new List<Vector3>();
            var triangles = new List<int>();
            var uv = new List<Vector2>();

            var counter = 0;
            for (var x = 0; x < chunk.Size; x++)
                for (var y = 0; y < chunk.Size; y++)
                {
                    var block = chunk.GetBlock(x, y);
                    if (block != null)
                    {
                        var currentPosition = new Vector2(x, y);
                        var rawShape = block.GetVertices();
                        var square = rawShape.AddToAll(currentPosition);

                        collider.pathCount = counter + 1;
                        collider.SetPath(counter, square);

                        foreach (var vertex in square)
                            vertices.Add(vertex);


                        var item = counter*4;
                        var blockTris = block.GetTriangles();
                        foreach (var index in blockTris)
                            triangles.Add(item + index);

                        uv.Add(new Vector2(0, 0));
                        uv.Add(new Vector2(0, 1));
                        uv.Add(new Vector2(1, 1));
                        uv.Add(new Vector2(1, 0));

                        counter++;
                    }
                }

            mesh.SetVertices(vertices);
            mesh.SetTriangles(triangles, 0);
            mesh.SetUVs(0, uv);
            filter.mesh = mesh;
        }

        private Chunk BuildChunkFor(int cx, int cy)
        {
            var dx = _chunkSize*cx;
            var dy = _chunkSize*cy;

            var chunk = new Chunk(_chunkSize, cx, cy);
            for (var x = 0; x < _chunkSize; x++)
                for (var y = 0; y < _chunkSize; y++)
                {
                    var isSolid = IsSolid(dx + x, dy + y);
                    if (isSolid)
                        chunk.SetBlock(x, y, new Block());
                }
            return chunk;
        }

        private bool IsSolid(int x, int y)
        {
            var r = 10;
            if (x*x + y*y < r*r)
                return false;

            var value = GetNoiseAtPosition(x, y);
            var decider = Factor;
            return value > decider;
        }

        private float GetNoiseAtPosition(int x, int y)
        {
            var f = 0.05f;
            var xPos = f*x + _shift.x;
            var yPos = f*y + _shift.y;

            var value = Mathf.PerlinNoise(xPos, yPos);
            var value2 = Mathf.PerlinNoise(yPos, x*f);
            return value2*value;
        }
    }
}
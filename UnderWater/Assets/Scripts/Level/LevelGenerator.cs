using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Level
{
    public class LevelGenerator : MonoBehaviour
    {
        public int Seed;
        public GameObject Block;
        public float Factor = 0.5f;
        private int _chunkSize = 32;
        private Vector2 _shift;

        private void Start()
        {
            var dx = ((Seed+3 << 5) -4) * ((Seed-2 >> 2) -20) << 1;
            _shift = new Vector2(dx, -dx);
            var coroutine = BuildMapAsync();
            StartCoroutine(coroutine);
        }

        private IEnumerator BuildMapAsync()
        {
            var mapSize = 4;
            for (int x = -mapSize; x < mapSize; x++)
            {
                for (int y = -mapSize; y < mapSize; y++)
                {
                    var chunk = BuildChunkFor(x, y);
                    BuildFromChunkAt(chunk);
                    Debug.Log(string.Format("chunk {0}/{1}",x, y));
                    yield return null;
                }
            }
        }

        private Chunk BuildChunkFor(int cx, int cy)
        {
            var dx = _chunkSize * cx;
            var dy = _chunkSize * cy;

            var chunk = new Chunk(_chunkSize, cx, cy);
            for (int x = 0; x < _chunkSize; x++)
            {
                for (int y = 0; y < _chunkSize; y++)
                {
                    var isSolid = IsSolid(dx + x, dy + y);
                    if (isSolid)
                    {
                        chunk.SetSolid(x, y);
                    }
                }
            }
            return chunk;
        }

        private void BuildFromChunkAt(Chunk chunk)
        {
            var dx = _chunkSize * chunk.X;
            var dy = _chunkSize * chunk.Y;

            for (int x = 0; x < chunk.Size; x++)
            {
                for (int y = 0; y < chunk.Size; y++)
                {
                    var targetX = dx + x;
                    var targetY = dy + y;
                    if (chunk.GetSolid(x, y))
                    {
                        CreateBlock(targetX, targetY);
                    }
                }
            }
        }

        private void CreateBlock(int x, int y)
        {
            Instantiate(Block, transform);
            var position = new Vector2(x, y);
            Block.transform.position = position;
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
            var xPos = 0.05f * x + _shift.x;
            var yPos = 0.05f * y + _shift.y;

            var value = Mathf.PerlinNoise(xPos, yPos);
            var value2 = Mathf.PerlinNoise(yPos, -xPos*xPos / 10000);
            return value2*value;
        }

        private class Chunk
        {
            public int X { get; private set; }
            public int Y { get; private set; }
            private readonly bool[,] _solidMask;
            public int Size { get; private set; }

            public Chunk(int size, int x, int y)
            {
                X = x;
                Y = y;
                Size = size;
                _solidMask = new bool[size, size];
            }

            public void SetSolid(int x, int y)
            {
                _solidMask[x, y] = true;
            }

            public bool GetSolid(int x, int y)
            {
                return _solidMask[x, y];
            }
        }
    }
}

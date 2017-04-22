using JetBrains.Annotations;
using UnityEngine;

namespace Level
{
    public class Chunk
    {
        private readonly bool[,] _solidMask;
        private IBlock[,] _blocks;
        public Chunk(int size, int x, int y)
        {
            X = x;
            Y = y;
            Size = size;
            _blocks = new IBlock[size,size];
            _solidMask = new bool[size, size];
        }

        public int X { get; private set; }
        public int Y { get; private set; }
        public int Size { get; private set; }

        public void SetSolid(int x, int y)
        {
            _solidMask[x, y] = true;
        }

        public bool GetSolid(int x, int y)
        {
            return _solidMask[x, y];
        }

        public IBlock GetBlock(int x, int y)
        {
            return _blocks[x, y];
        }

        public void SetBlock(int x, int y, IBlock block)
        {
            _blocks[x, y] = block;
        }


        public Vector2 GetWorldPosition()
        {
            return new Vector2(X * Size, Y * Size);
        }
    }
}
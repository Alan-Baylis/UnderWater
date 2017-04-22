using UnityEngine;

namespace Level
{
    public class Chunk
    {
        private readonly bool[,] _solidMask;

        public Chunk(int size, int x, int y)
        {
            X = x;
            Y = y;
            Size = size;
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

        public Vector2 GetWorldPosition()
        {
            return new Vector2(X * Size, Y * Size);
        }
    }
}
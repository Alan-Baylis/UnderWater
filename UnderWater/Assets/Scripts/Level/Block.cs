using UnityEngine;

namespace Level
{
    internal class Block : IBlock
    {
        public Vector2[] GetVertices()
        {
            return new[]
            {
                new Vector2(0, 0),
                new Vector2(0, 1),
                new Vector2(1, 1),
                new Vector2(1, 0)
            };
        }

        public int[] GetTriangles()
        {
            return new[]
                        {
                            0, 1, 2, 2, 3, 0
                        };
        }
    }
}
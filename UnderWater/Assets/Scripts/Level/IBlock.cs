using UnityEngine;

namespace Level
{
    public interface IBlock
    {
        Vector2[] GetVertices();
        int[] GetTriangles();
    }
}
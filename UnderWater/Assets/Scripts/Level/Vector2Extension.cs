using UnityEngine;

namespace Level
{
    static internal class Vector2Extension
    {
        public static Vector2[] AddToAll(this Vector2[] rawShape, Vector2 currentPosition)
        {
            var output = new Vector2[rawShape.Length];
            for (int index = 0; index < rawShape.Length; index++)
            {
                output[index] = currentPosition + rawShape[index];
            }
            return output;
        }
    }
}
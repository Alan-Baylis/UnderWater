using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;



public class LevelGenerator : MonoBehaviour
{
    public GameObject Block;
    public float Factor = 0.5f;
    private Vector2 _shift = new Vector2(-100, -100);

    private void Start()
    {
        var size = 100;
        for (int x = -size; x < size; x++)
        {
            for (int y = -size; y < size; y++)
            {
                var isSolid = IsSolid(x, y);
                if (isSolid)
                {
                    Instantiate(Block, transform);
                    var position = new Vector2(x, y);
                    Block.transform.position = position;
                }
            }
        }
    }

    private bool IsSolid(int x, int y)
    {
        var value = GetNoiseAtPosition(x, y);
        var factorNoise = GetNoiseAtPosition(x * 100, y * 30) - 0.5f;
        var decider = Factor + 0.1f * factorNoise;
        return value > decider;
    }

    private float GetNoiseAtPosition(int x, int y)
    {
        var xPos = 0.05f * x + _shift.x;
        var yPos = 0.05f * y + _shift.y;

        var value = Mathf.PerlinNoise(xPos, yPos);
        return value;
    }
}

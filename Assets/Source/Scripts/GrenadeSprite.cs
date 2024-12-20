using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeSprite : MonoBehaviour
{
    private int random_dir;

    private void Start()
    {
        random_dir = Random.Range(1, 3);
    }

    private void FixedUpdate()
    {
        if (random_dir == 1)
        {
            transform.Rotate(Vector3.forward * 10);
        }
        else
        {
            transform.Rotate(Vector3.back * 10);
        }
    }
}

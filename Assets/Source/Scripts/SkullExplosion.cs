using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkullExplosion : MonoBehaviour
{
    void Update()
    {
        if(this.transform.childCount == 0)
        {
            GameManager.num_enemies_active -= 1;
            if (GameManager.num_enemies_active == 0)
            {
                GameManager.enemies_dead = true;
            }
            Destroy(this.gameObject);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class EffectSpawner : MonoBehaviour
{
    public ObjectPool<GameObject> pool;
    public Vector3 position_to_spawn_effect;
    [SerializeField] GameObject effect_to_spawn;

    private void Start()
    {
        pool = new ObjectPool<GameObject>(CreateEffect, OnTakeEffectFromPool, OnReturnEffectToPool, OnDestroyEffect, true, 40, 200);
    }

    private GameObject CreateEffect()
    {
        GameObject effect = Instantiate(effect_to_spawn, position_to_spawn_effect, Quaternion.identity);
        Effects_Script effect_script = effect.GetComponent<Effects_Script>();
        effect_script.SetPool(pool);
        return effect;
    }

    private void OnTakeEffectFromPool(GameObject effect)
    {
        effect.transform.position = position_to_spawn_effect;
        effect.transform.rotation = Quaternion.identity;
        effect.SetActive(true);
    }

    private void OnReturnEffectToPool(GameObject effect)
    {
        effect.gameObject.SetActive(false);
    }

    private void OnDestroyEffect(GameObject effect)
    {
        Destroy(effect.gameObject);
    }
}

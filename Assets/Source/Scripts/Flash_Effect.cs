using System.Collections;

using UnityEngine;

namespace BarthaSzabolcs.Tutorial_SpriteFlash
{
    public class SimpleFlash : MonoBehaviour
    {
        [SerializeField] private Material flashMaterial;
        [SerializeField] private float duration;
        private SpriteRenderer spriteRenderer;
        private Material originalMaterial;
        private Coroutine flashRoutine;
        private Base_Enemy enemy_script;

        void Start()
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            originalMaterial = spriteRenderer.material;
            enemy_script = GetComponentInParent<Base_Enemy>();
        }

        public void Flash()
        {
            if (flashRoutine != null)
            {
                StopCoroutine(flashRoutine);
            }
            flashRoutine = StartCoroutine(FlashRoutine());
        }

        private IEnumerator FlashRoutine()
        {
            spriteRenderer.material = flashMaterial;
            yield return new WaitForSeconds(duration);
            spriteRenderer.material = originalMaterial;
            flashRoutine = null;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (enemy_script != null)
            {
                if (collision.GetComponent<PlayerController>() != null || GameManager.enemies_spawned == false || !this.enemy_script.enemy_spawned)
                {
                    return;
                }
                else
                {
                    Flash();
                }
            }
            else
            {
                if(collision.gameObject.name.Contains("Key") || collision.gameObject.name.Contains("Chaser_2") || collision.gameObject.name.Contains("BossChaser") || collision.gameObject.name.Contains("Room") || collision.gameObject.name.Contains("Banana") || (GameManager.num_enemies_active == 0 && !collision.gameObject.transform.parent.name.Contains("Skull")) || this.GetComponent<PlayerController>().eating)
                {
                    return;
                }
                else
                {
                    Flash();
                }
            }
        }
    }
}
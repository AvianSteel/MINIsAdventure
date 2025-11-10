///Made by Cristian
/// Makes the enemy flash red when dmg
///
using UnityEngine;

public class EnemyBlinkWhite : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;

    void Start()
    {
        

    }

    /// <summary>
    /// when the enemy is object pooled back amke sure it does not have the red overlay
    /// </summary>
    private void OnEnable()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = Color.white;


    }

    public void FlashRed()
    {
        if (isActiveAndEnabled)
        {
            StartCoroutine(FlashCoroutine());

        }
    }

    private System.Collections.IEnumerator FlashCoroutine()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.color = Color.white;
    }
}

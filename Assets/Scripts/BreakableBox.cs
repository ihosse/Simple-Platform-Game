using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class BreakableBox : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.WasHitByPlayer () &&
            collision.WasBottom())
        {
            Destroy(gameObject);
        }
    }
}

using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float Speed { get; private set; }
    public float maxHealth { get; private set; }
    public float reward { get; private set; }

    public void TakeDamage(float damage)
    {
        maxHealth -= damage;
    }

    public void Death()
    {
        Destroy(gameObject);
    }
}

using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float Speed;
    public float maxHealth;
    public float reward;
    private int wavePointIndex;
    private Transform currentTarget;

    private void Start()
    {
        currentTarget = Waypoints.points[0];
    }

    private void Update()
    {
        if(maxHealth != 0)
        {
            Vector3 direction = currentTarget.position - transform.position;
            transform.Translate(direction.normalized * Speed * Time.deltaTime, Space.World);
        }
        if(Vector3.Distance(transform.position, currentTarget.position) < 0.2f)
        {
            GetNextIndex();
        }
        if (maxHealth <= 0)
        {
            Death();
        }  
    }

    public void TakeDamage(float damage)
    {
        maxHealth -= damage;
    }

    private void Death()
    {
        Destroy(gameObject);
    }

    private void GetNextIndex()
    {
        wavePointIndex++;
        currentTarget = Waypoints.points[wavePointIndex];
    }
}

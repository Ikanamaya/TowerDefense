using UnityEngine;

public class Bullet : MonoBehaviour
{
    Vector3 dir;
    [SerializeField] private float speed;
    public EnemyController enemy;
    public Tower tower;
    
    private void Start()
    {
        tower = GetComponent<Tower>();
        Destroy();
    }

    private void Update()
    {
        if(enemy == null)
        {
            Destroy(gameObject);
        }
        MoveToTarget();
    }

    //public void SetTarget() => enemy = tower.target;

    private void MoveToTarget()
    {
        dir = enemy.transform.position - transform.position;
        float distanceThisFrame = speed * Time.deltaTime;
        transform.Translate(dir * distanceThisFrame, Space.World);
    }

    private void Destroy()
    {
        Destroy(gameObject, 2f);
    }
}


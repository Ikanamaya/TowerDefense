using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField] private float rateOfFire;
    [SerializeField] private float range;
    [SerializeField] private float cooldown;
    [SerializeField] private GameObject BulletPrefab;
    [SerializeField] Transform bulletSpawn;
    [SerializeField] public Vector2Int size = Vector2Int.one;
    public Renderer parentRenderer;
    private Component[] childrenRenderer;
    bool IsInstantiating;
    public List<EnemyController> enemies = new List<EnemyController>();
    public EnemyController target { get; private set; }

    private void Awake()
    {
        childrenRenderer = parentRenderer.GetComponentsInChildren(typeof(Renderer));
    }
    private void Start()
    {
        IsInstantiating = false;
    }

    private void Update()
    {
        if (BulletPrefab == null)
        {
            return;
        }
        TargetUpdate();
        if (target != null && !IsInstantiating)
        {
            InstantiateBullet();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<EnemyController>())
        {
            enemies.Add(other.GetComponent<EnemyController>());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<EnemyController>())
        {
            enemies.Remove(other.GetComponent<EnemyController>());
            ResetTarget();
        }
    }

    private void TargetUpdate()
    {
        if (enemies.Count == 0)
        {
            return;
        }
        float minDistance = Mathf.Infinity;
        for (int i = 0; i < enemies.Count; i++)
        {
            float distance = Vector3.Distance(transform.position, enemies[i].transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                target = enemies[i];
            }
        }
    }

    private IEnumerator CooldownTimer(float time)
    {
        yield return new WaitForSeconds(time);
        IsInstantiating = false;
    }

    private void InstantiateBullet()
    {
        if (BulletPrefab == null)
        {
            return;
        }
        IsInstantiating = true;
        GameObject projectile = Instantiate(BulletPrefab, bulletSpawn.position, Quaternion.identity, bulletSpawn.transform);
        projectile.GetComponent<Bullet>().enemy = target;
        projectile.GetComponent<Bullet>().tower = this;
        StartCoroutine(CooldownTimer(cooldown));
    }

    private void ResetTarget() => target = null;

    public void SetTransparent(bool available)
    {
        if (available && childrenRenderer != null)
        {
            parentRenderer.material.color = Color.green;
            foreach (Renderer child in childrenRenderer)
            {
                child.material.color = Color.green;
            }

        }
        else
        {
            parentRenderer.material.color = Color.red;
            foreach (Renderer child in childrenRenderer)
            {
                child.material.color = Color.red;
            }
        }
    }

    private void OnDrawGizmos()
    {
        for (int x = 0; x < size.x; x++)
        {
            for (int y = 0; y < size.y; y++)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawCube(transform.position + new Vector3(x, 0, y), new Vector3(1, 0.1f, 1));
            }
        }
    }
}

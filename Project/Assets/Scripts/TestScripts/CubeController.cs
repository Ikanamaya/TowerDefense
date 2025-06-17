using UnityEngine;

public class CubeController : MonoBehaviour
{
    [SerializeField] public GameObject target;
    Vector3 dir;
    public float speed;
    float distanceThisFrame;
    public float range;
    public Transform playerPoint;
    public LayerMask targetLayer;
    public Transform targetTransform;
    


    void Start()
    {
        targetTransform = target.GetComponent<Transform>();
        targetTransform.position += new Vector3(10, 0, 0);
        Application.targetFrameRate = 10;
    }
    private void Update()
    {

        if (target == null)
        {
            return;
        }
        distanceThisFrame = speed * Time.deltaTime;
        print(distanceThisFrame);
        dir = target.transform.position - transform.position;

        transform.Translate(dir.normalized * distanceThisFrame, Space.World);

    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }

    private void OnCollisionExit(Collision collision)
    {

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}

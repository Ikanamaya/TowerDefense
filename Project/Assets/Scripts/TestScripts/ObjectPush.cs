using UnityEngine;

public class ObjectPush : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.rigidbody != null)
                {
                    //hit.rigidbody.AddForce(transform.InverseTransformDirection(Vector3.forward * 5), ForceMode.Impulse);
                    hit.rigidbody.AddForce(Vector3.forward * 5, ForceMode.Impulse);
                }
            }
        }
    }
}

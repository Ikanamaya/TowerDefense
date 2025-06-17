using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    private Vector3 lastPosition;
    [SerializeField] private LayerMask placementLayer;

    public Vector3 GetMousePosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = mainCamera.nearClipPlane;
        Ray ray = mainCamera.ScreenPointToRay(mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 100, placementLayer))
        {
            lastPosition = hit.point;
        }
        return lastPosition;
    }
}

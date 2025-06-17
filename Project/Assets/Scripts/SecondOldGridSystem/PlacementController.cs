using UnityEngine;

public class PlacementController : MonoBehaviour
{
    [SerializeField] private InputManager inputManager;
    [SerializeField] private GameObject indicator;
    [SerializeField] private Grid grid;

    private void Update()
    {
        Vector3 mousePosition = inputManager.GetMousePosition();
        Vector3Int gridPosition = grid.WorldToCell(mousePosition);
        indicator.transform.position = grid.CellToWorld(gridPosition);

    }
}

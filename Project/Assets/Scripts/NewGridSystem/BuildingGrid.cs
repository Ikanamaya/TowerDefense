using System.Collections;
using System.Drawing;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class BuildingGrid : MonoBehaviour
{
    [SerializeField] private Vector2Int gridSize = new Vector2Int(10, 10);
    private Building[,] grid;
    private Building flyingBuilding;
    private Camera mainCamera;
    private Plane groundPlane;
    private int x, y;
    private bool isAvailable;

    private void Awake()
    {
        grid = new Building[gridSize.x, gridSize.y];
        mainCamera = Camera.main;
    }

    private void Start()
    {
        groundPlane = new Plane(Vector2.up, Vector3.zero);
        isAvailable = true;
    }

    private void Update()
    {
        if (flyingBuilding != null)
        {
            RoundPosition();
            if (isAvailable && Input.GetMouseButtonDown(0))
            {
                PlaceBuilding(x, y);
            }
        }
    }

    private void SelectBuilding(Building buildingPrefab)
    {
        if (flyingBuilding != null)
        {
            Destroy(flyingBuilding.gameObject);
        }

        flyingBuilding = Instantiate(buildingPrefab);
    }

    private void RoundPosition()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (groundPlane.Raycast(ray, out float enter))
        {
            var worldPosition = ray.GetPoint(enter);
            x = Mathf.RoundToInt(worldPosition.x);
            y = Mathf.RoundToInt(worldPosition.z);

            if ((x < 0 || x > gridSize.x - flyingBuilding.size.x) || (y < 0 || y > gridSize.y - flyingBuilding.size.y))
            {
                isAvailable = false;
            }
            else
            {
                isAvailable = true;
            }
            if (isAvailable && isOccupied(x, y)) isAvailable = false;
            
            flyingBuilding.transform.position = new Vector3(x, 0, y);
            flyingBuilding.SetTransparent(isAvailable);
        }
    }

    private bool isOccupied(int placeX, int placeY)
    {
        for (int x = 0; x < flyingBuilding.size.x; x++)
        {
            for (int y = 0; y < flyingBuilding.size.y; y++)
            {
                if(grid[x + placeX, y + placeY] != null) return true;
            }
        }

        return false;
    }

    private void PlaceBuilding(int placeX, int placeY)
    {
        for (int x = 0; x < flyingBuilding.size.x; x++)
        {
            for (int y = 0; y < flyingBuilding.size.y; y++)
            {
                grid[x + placeX, y + placeY] = flyingBuilding;
            }
        }
        Instantiate(flyingBuilding);
        flyingBuilding.SetNormal();
        flyingBuilding = null;
    }

}

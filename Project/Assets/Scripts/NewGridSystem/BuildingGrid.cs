using UnityEngine;

public class BuildingGrid : MonoBehaviour
{
    [SerializeField] private Vector2Int gridSize = new Vector2Int(30, 30);
    private Tower[,] grid;
    [SerializeField] private Tower tower;
    [SerializeField] private GameObject gridShader;
    private FlyingBuilding flyingTower;
    private Camera mainCamera;
    private Plane groundPlane;
    private int x, y;
    private bool isAvailable;

    private void Awake()
    {
        grid = new Tower[gridSize.x, gridSize.y];
        mainCamera = Camera.main;
    }

    private void Start()
    {
        groundPlane = new Plane(Vector2.up, Vector3.zero);
        isAvailable = true;
        gridShader.SetActive(false);
    }

    private void Update()
    {
        if (flyingTower != null)
        {
            RoundPosition();
            if (isAvailable && Input.GetMouseButtonDown(0))
            {
                PlaceBuilding(x, y);
            }
            if (Input.GetMouseButtonDown(1))
            {
                DestroyBuilding(flyingTower);
            }
        }
    }

    public void SelectBuilding(FlyingBuilding buildingPrefab)
    {
        if (flyingTower != null)
        {
            DestroyBuilding(flyingTower);
        }

        gridShader.SetActive(true);
        flyingTower = Instantiate(buildingPrefab);

    }

    private void RoundPosition()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (groundPlane.Raycast(ray, out float enter))
        {
            var worldPosition = ray.GetPoint(enter);
            x = Mathf.RoundToInt(worldPosition.x);
            y = Mathf.RoundToInt(worldPosition.z);

            if ((x < 0 || x > gridSize.x - flyingTower.size.x) || (y < 0 || y > gridSize.y - flyingTower.size.y) || flyingTower.isColliding)
            {
                isAvailable = false;
            }
            else
            {
                isAvailable = true;
            }
            if (isAvailable && isOccupied(x, y)) isAvailable = false;

            flyingTower.transform.position = new Vector3(x, 0, y);
            flyingTower.SetTransparent(isAvailable); 
        }
    }

    private bool isOccupied(int placeX, int placeY)
    {
        for (int x = 0; x < flyingTower.size.x; x++)
        {
            for (int y = 0; y < flyingTower.size.y; y++)
            {
                if(grid[x + placeX, y + placeY] != null) return true;
            }
        }

        return false;
    }

    private void PlaceBuilding(int placeX, int placeY)
    {
        for (int x = 0; x < flyingTower.size.x; x++)
        {
            for (int y = 0; y < flyingTower.size.y; y++)
            {
                grid[x + placeX, y + placeY] = tower;
            }
        }
        Instantiate(tower, new Vector3(flyingTower.transform.position.x, flyingTower.transform.position.y, flyingTower.transform.position.z), Quaternion.identity);
        DestroyBuilding(flyingTower);
        gridShader.SetActive(false);
        flyingTower = null;
    }

    private void DestroyBuilding(FlyingBuilding buildingPrefab)
    {
        Destroy(buildingPrefab.gameObject);
    }

}

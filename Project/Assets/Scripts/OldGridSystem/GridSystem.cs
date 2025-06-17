using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class GridSystem : MonoBehaviour
{
    [SerializeField] private GameObject objectToPlace;
    [SerializeField] private int gridSize;
    private GameObject ghostObject;
    private HashSet<Vector3> occupiedPositions = new HashSet<Vector3>(); //быстрее чем list

    private void Start()
    {
        CreateGhostObject();
    }

    private void Update()
    {
        UpdateGhostPosition();

        if (Input.GetMouseButtonDown(0))
        {
            PlaceObject();
        }
    }

    private void CreateGhostObject()
    {
        ghostObject = Instantiate(objectToPlace);
        ghostObject.GetComponent<Collider>().enabled = false;

        Renderer[] renderers = ghostObject.GetComponentsInChildren<Renderer>();

        foreach (Renderer renderer in renderers)
        {
            Material mat = renderer.material;
            Color color = mat.color;
            color.a = 0.5f;
            mat.color = color;

            mat.SetFloat("_Mode", 2);
            mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
            mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
            mat.SetInt("_ZWrite", 0);
            mat.DisableKeyword("_ALPHATEST_ON");
            mat.EnableKeyword("_ALPHABLEND_ON");
            mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
            mat.renderQueue = 3000;
        }
    }

    private void UpdateGhostPosition()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            Vector3 point = hit.point;
            Vector3 snappedPosition = new Vector3Int(
            Mathf.RoundToInt(point.x / gridSize) * gridSize,
            Mathf.RoundToInt(point.y / gridSize) * gridSize,
            Mathf.RoundToInt(point.z / gridSize) * gridSize);

            ghostObject.transform.position = snappedPosition;

            if (occupiedPositions.Contains(snappedPosition))
            {
                ghostObject.GetComponent<Material>().color = Color.red;
            }
        }
    }

    private void PlaceObject()
    {
        Vector3 placingPosition = ghostObject.transform.position;
        if (!occupiedPositions.Contains(placingPosition))
        {
            Instantiate(objectToPlace, placingPosition, Quaternion.identity);

            occupiedPositions.Add(placingPosition);
        }
    }
    private void SetGhostColor(Color color)
    {
        return;
    }

}

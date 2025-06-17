using UnityEngine;

public class Building : MonoBehaviour
{
    [SerializeField] public Vector2Int size = Vector2Int.one;
    public Renderer parentRenderer;
    private Component[] childrenRenderer;

    private BuildingGrid grid;

    private void Awake()
    {
        childrenRenderer = parentRenderer.GetComponentsInChildren(typeof(Renderer));
        
    }
    public void SetTransparent(bool available)
    {
        if (available && childrenRenderer != null)
        {
            parentRenderer.material.color = Color.green;
            //Debug.Log(childrenRenderer);
            foreach (Renderer child in childrenRenderer)
            {
                child.material.color = Color.green;
            }

        }
        else
        {
            parentRenderer.material.color = Color.red;
            //Debug.Log(childrenRenderer);
            foreach (Renderer child in childrenRenderer)
            {
                child.material.color = Color.red;
            }
        }
    }

    public void SetNormal()
    {
        parentRenderer.material.color = Color.white;
        foreach (Renderer child in childrenRenderer)
        {
            child.material.color = Color.white;
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

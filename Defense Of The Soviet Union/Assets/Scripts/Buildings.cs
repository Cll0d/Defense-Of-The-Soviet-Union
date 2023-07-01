using Unity.VisualScripting;
using UnityEngine;

public class Buildings : MonoBehaviour
{
    [SerializeField] private Vector2Int _size;
    public Canvas Canvas;
    public bool IsTriiger = true;

    private void OnTriggerEnter(Collider other)
    {
        IsTriiger = true;
    }

    private void OnTriggerStay(Collider other)
    {
        IsTriiger = false;
        Canvas.enabled = true;
    }
    private void OnTriggerExit(Collider other)
    { 
        IsTriiger = true;
        Canvas.enabled = false;
    }
    public Vector2Int Size
    {
        get => _size;
        set => _size = value;
    }

    private void OnDrawGizmosSelected()
    {
        for (int x = 0; x < _size.x; x++)
        {
            for (int y = 0; y < _size.y; y++)
            {
                Gizmos.color = new Color(0.88f, 0f, 1f, 0.4f);
                Gizmos.DrawCube(transform.position + new Vector3(x, 0, y), new Vector3(1, 0.1f, 1));
            }
        }
    }
}

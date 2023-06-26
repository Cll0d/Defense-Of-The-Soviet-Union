using Unity.VisualScripting;
using UnityEngine;

public class Buildings : MonoBehaviour
{
    [SerializeField] private Vector2Int _size;
    [SerializeField] private Renderer _renderer;
    [SerializeField] private Renderer _renderer1;
    [SerializeField] private Renderer _renderer2;

    public void SetTransparent(bool available)
    {
        if (available)
        {
            _renderer.material.color = Color.green;
            _renderer1.material.color = Color.green;
            _renderer2.material.color = Color.green;
        }
        else
        {
            _renderer.material.color = Color.red;
            _renderer1.material.color = Color.red;
            _renderer2.material.color = Color.red;
        }
    }

    public void ResetColor()
    {
        _renderer.material.color = Color.white;
        _renderer1.material.color = Color.white;
        _renderer2.material.color = Color.white;
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
    private void Update()
    {

    }
}

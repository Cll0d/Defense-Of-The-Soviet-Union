using UnityEngine;
using UnityEngine.EventSystems;

public class ArmyPlace : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    private Card _cardSO;
    public Card CardSO
    {
        get => _cardSO;
        set { _cardSO = value; }
    }

    private GameObject _draggInBuilding;
    private Building _building;
    private bool _isBuild;

    public void OnDrag(PointerEventData eventData)
    {
        if (_draggInBuilding != null)
        {
            var groundPlane = new Plane(Vector3.up, Vector3.zero);
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (groundPlane.Raycast(ray, out float pos))
            {
                Vector3 worldPosition = ray.GetPoint(pos);
                float x = worldPosition.x;
                float z = worldPosition.z;
                if (x < 9 || x > 39)
                {
                    _isBuild = false;
                }
                else if (z < -30 || z > 32)
                {
                    _isBuild = false;
                }
                else
                {
                    _isBuild = true;
                }
                _draggInBuilding.transform.position = ray.GetPoint(pos);
            }
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        _draggInBuilding = Instantiate(_cardSO.Prefab);

        _building = _draggInBuilding.GetComponent<Building>();

        var groundPlane = new Plane(Vector3.up, Vector3.zero);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (groundPlane.Raycast(ray, out float pos))
        {
            Vector3 worldPosition = ray.GetPoint(pos);
            float x = worldPosition.x;
            float z = worldPosition.z;
            _draggInBuilding.transform.position = ray.GetPoint(pos);
        }
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if (!_isBuild)
        {
            Destroy(_draggInBuilding);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("pizdec");
        _isBuild = false;
        Destroy(_draggInBuilding);
    }
}
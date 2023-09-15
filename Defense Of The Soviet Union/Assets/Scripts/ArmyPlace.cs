using UnityEngine;
using UnityEngine.EventSystems;

public class ArmyPlace : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{

    private Vector2Int _gridSize = new Vector2Int(27, 60);
    private Card _cardSO;
    private CoinManager _coinManager;
    private bool _isPlace = false;
    public Card CardSO
    {
        get => _cardSO;
        set { _cardSO = value; }
    }
    private GameObject _draggInBuilding;
    private Buildings _buildings;
    private bool _isBuild;
    private Buildings[,] _grid;
    public bool IsAbleToPlant { get; set; }

    private void Awake()
    {
        _coinManager = CoinManager.Instance;
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (IsAbleToPlant)
        {
            if (_draggInBuilding != null)
            {
                var groundPlane = new Plane(Vector3.up, Vector3.zero);
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (groundPlane.Raycast(ray, out float pos))
                {
                    Vector3 worldPosition = ray.GetPoint(pos);
                    int x = Mathf.RoundToInt(worldPosition.x);
                    int z = Mathf.RoundToInt(worldPosition.z);
                    if (x < 9 || x > (_gridSize.x - _buildings.Size.x) + 10)
                    {
                        _buildings.Canvas.enabled = true;
                        _isBuild = false;
                    }
                    else if (z < -30 || z > (_gridSize.y - _buildings.Size.y) - 26)
                    {
                        _buildings.Canvas.enabled = true;
                        _isBuild = false;
                    }
                    else
                    {
                        _buildings.Canvas.enabled = false;
                        _isBuild = true;
                    }
                    _draggInBuilding.transform.position = new Vector3(x, 0, z);
                }
            }
        }
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        if (IsAbleToPlant)
        {
            _draggInBuilding = Instantiate(_cardSO.Prefab);

            _buildings = _draggInBuilding.GetComponent<Buildings>();
            //_buildings.Canvas.enabled = false;
            _draggInBuilding.GetComponent<BoxCollider>().enabled = true;
        }
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if (IsAbleToPlant)
        {
            if (!_isBuild || _buildings.IsTriiger == false)
            {
                _buildings.Canvas.enabled = true;
                Destroy(_draggInBuilding);
            }
            else
            {
                _buildings.Canvas.enabled = false;
                _coinManager.SpendCoins(_cardSO.Ñost);
            }
        }
    }
}
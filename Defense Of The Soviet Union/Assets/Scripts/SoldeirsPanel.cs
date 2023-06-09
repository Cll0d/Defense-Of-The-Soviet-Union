using UnityEngine;

public class SoldeirsPanel : MonoBehaviour
{
    [SerializeField] private GameObject _armyObject;

    public GameObject ArmyObject
    {
        get { return _armyObject; }
    }
}

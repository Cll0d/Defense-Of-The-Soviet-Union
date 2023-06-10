using UnityEngine;

public class ArmyPanel : MonoBehaviour
{
    [SerializeField] private GameObject _armyObject;

    public GameObject ArmyObject
    {
        get { return _armyObject; }
    }
}

using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "Card/New card", fileName = "New card", order = 51)]
public class Card : ScriptableObject
{
    public Sprite Icon;
    public GameObject Prefab;
    public int Ñost;

    void Start()
    {
        Icon.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
    }
}
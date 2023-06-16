using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CardHolderManager : MonoBehaviour
{
    [SerializeField] private Transform _cardHolderPosition;
    [SerializeField] private GameObject _card;
    [SerializeField] private Card[] _cardSO;
    [SerializeField] private GameObject[] _plantedCards;
    private int _cost;
    private Sprite _icon;

    private void Start()
    {
        _plantedCards = new GameObject[_cardSO.Length];

        for (int i = 0; i < _cardSO.Length; i++)
        {
            CreateCard(i);
        }
    }


    private void CreateCard(int i)
    {
        var card = Instantiate(_card, _cardHolderPosition);
        ArmyPlace armyPlace = card.GetComponent<ArmyPlace>();

        armyPlace.CardSO = _cardSO[i];

        _plantedCards[i] = card;

        _icon = _cardSO[i].Icon;
        _cost = _cardSO[i].Cost;

        card.GetComponentInChildren<Image>().sprite = _icon;
        card.GetComponentInChildren<TMP_Text>().text = _cost.ToString();
    }
}
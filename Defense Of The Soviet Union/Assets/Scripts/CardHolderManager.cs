using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CardHolderManager : MonoBehaviour
{
    [SerializeField] private Transform _cardHolderPosition;
    [SerializeField] private GameObject _card;
    [SerializeField] private Card[] _cardSO;
    [SerializeField] private GameObject[] _plantedCards;
    [SerializeField] private CoinManager _coinManager;
    private int _cost;
    private Sprite _icon;
    GameObject Icon;

    private void Update()
    {
        IsTarget();
    }
    private void Start()
    {
        _plantedCards = new GameObject[_cardSO.Length];

        for (int i = 0; i < _cardSO.Length; i++)
        {
            CreateCard(i);
        }
        GameEvents.Instance.onCoinChange += IsTarget;
    }
    private void CreateCard(int i)
    {
        var card = Instantiate(_card, _cardHolderPosition);
        ArmyPlace armyPlace = card.GetComponent<ArmyPlace>();

        armyPlace.CardSO = _cardSO[i];

        _plantedCards[i] = card;

        _icon = _cardSO[i].Icon;
        _cost = _cardSO[i].Ñost;

        card.GetComponentInChildren<Image>().sprite = _icon;
        card.GetComponentInChildren<TMP_Text>().text = _cost.ToString();
    }
    private void IsTarget()
    {
        for(int i = 0; i < _cardSO.Length; i ++)
        {
            if (_cardSO[i].Ñost > _coinManager.Coins)
            {
                _plantedCards[i].transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(132, 132, 132, 255);
                _plantedCards[i].GetComponent<Image>().color = Color.gray;
                _plantedCards[i].GetComponent<ArmyPlace>().IsAbleToPlant = false;
            }
            else
            {
                _plantedCards[i].transform.GetChild(0).GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
                _plantedCards[i].GetComponent<Image>().color = Color.white;
                _plantedCards[i].GetComponent<ArmyPlace>().IsAbleToPlant = true;
            }
        }
    }
}
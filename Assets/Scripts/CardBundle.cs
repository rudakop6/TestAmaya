using UnityEngine;

[CreateAssetMenu(fileName = "New CardBundle", menuName = "Card Bundle", order = 1)]
public class CardBundle : ScriptableObject
{
    [SerializeField] private CardData[] _cardData;
    public CardData[] CardDatas => _cardData;
}

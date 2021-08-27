using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;


[Serializable]
public class MyUnityEvent : UnityEvent<string> { }

public class SlotsGenerator : MonoBehaviour
{
    [SerializeField] private MyAnimations _myAnimations;
    [SerializeField] private RectTransform _parent;
    [SerializeField] private GameObject _slot;
    [SerializeField] private MyUnityEvent FindEvent;
    [SerializeField] private int _numberOfColumns;
    [SerializeField] private int _numberOfStrings;
    [SerializeField] private FindText _findText;
    private int startLevel = 0;

    [Serializable]
    public struct Levels
    {
        public int _numberOfCard;
        public CardBundle _cardBundle;
    }
    [SerializeField] private List<Levels> levels;

    public int numberOflevels => levels.Count;


    private List<CellImage> slots = new List<CellImage>();
    private List<CardData> listExeption = new List<CardData>();
    
    private void Start()
    {
        RectTransform slot = _slot.GetComponent<RectTransform>();
        
        float numberOfColumns = _numberOfColumns;
        float numberOfStrings = _numberOfStrings;

        slot.sizeDelta = new Vector2(_parent.rect.width / numberOfColumns,
                                        _parent.rect.height / numberOfStrings);   

        Vector3 slotPosition = Vector3.zero;

        for (int i = 0; i < numberOfStrings; i++)
        {
            slotPosition.y = -slot.rect.height * i;
            for (int j = 0; j < numberOfColumns; j++)
            {
                slotPosition.x = slot.rect.width * j;
                _slot.SetActive(false);
                
                var slot_clone = Instantiate(_slot, _parent);
                
                slot_clone.transform.localPosition = slotPosition;
                CellImage cellImage = slot_clone.transform.GetComponentInChildren<CellImage>();
                cellImage.Initialization(_findText);
                slots.Add(cellImage);                               
            }
        }
        CreateLevel(startLevel);
    }

    public void CreateLevel(int level)
    {
        if (level == 0 && listExeption.Count != 0)
        {
            listExeption.Clear();
        }

        for(int i = 0; i< _numberOfColumns*_numberOfStrings;i++)
        {
            slots[i].transform.parent.gameObject.SetActive(false);
        }

        List<CardData> listLevel = new List<CardData>();
        listLevel = CreateListCard(level);

        int randomCard = UnityEngine.Random.Range(0, listLevel.Count);
        listExeption.Add(listLevel[randomCard]);
        FindEvent.Invoke(listLevel[randomCard].Identifier); 
        CardData findCard = listLevel[randomCard];

        ListHandler(ref listLevel);
        

        int randomCells = UnityEngine.Random.Range(0, levels[level]._numberOfCard);              
        for (int h = 0; h < levels[level]._numberOfCard; h++)
        {
            if(h == randomCells)
            {
                slots[h].cellImage.sprite = findCard.Sprite;
                _myAnimations.EazyBounce(() => { }, slots[h]);
                slots[h].Identifier = findCard.Identifier;
            }
            else
            {
                randomCard = UnityEngine.Random.Range(0, listLevel.Count);
                slots[h].cellImage.sprite = listLevel[randomCard].Sprite;
                _myAnimations.EazyBounce(() => { }, slots[h]);
                slots[h].Identifier = listLevel[randomCard].Identifier;
            }           
            slots[h].transform.parent.gameObject.SetActive(true);          
        }
    }

    private List<CardData> CreateListCard(int level)
    {
        List<CardData> list = new List<CardData>();
        for (int k = 0; k < levels[level]._cardBundle.CardDatas.Length; k++)
        {
            list.Add(levels[level]._cardBundle.CardDatas[k]);
        }
        return list;
    }

    private void ListHandler(ref List<CardData> list)
    {
        for (int i = 0; i < listExeption.Count; i++)
        {
            for (int j = 0; j < list.Count; j++)
            {
                if (listExeption[i] == list[j])
                {
                    list.RemoveAt(j);
                }
            }
        }
    }
}

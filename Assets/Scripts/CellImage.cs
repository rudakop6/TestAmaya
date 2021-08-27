using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class CellImage : MonoBehaviour, IPointerClickHandler
{
    public string Identifier { get; set; }
    [SerializeField] private Image _cellImage;
    public Image cellImage => _cellImage;

    [SerializeField] private RectTransform _cell;
    [SerializeField] private RectTransform _rectCellImage;
    [SerializeField, Range(1, 100)] private int _percentSize; //процент размера изображени€ относительно размера €чейки
    private FindText _text;

    private void Awake()
    {
        float range = (float)_percentSize / 100;
        _rectCellImage.sizeDelta = new Vector2(_cell.rect.width * range, _cell.rect.height * range);
    }
    public void Initialization(FindText findText)
    {
        _text = findText;        
    }
    public void OnPointerClick(PointerEventData data)
    {
        _text.CheckerCard(Identifier, this);
    }
}

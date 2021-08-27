using UnityEngine;

public class TransformUI : MonoBehaviour
{
    [SerializeField] private RectTransform _cellsField;
    [SerializeField] private RectTransform _findText;
    [SerializeField] private RectTransform _restartButton;

    [SerializeField, Range(1, 100)] private int _percentSizeGrid; //процент размера сетки относительно размера экрана
    [SerializeField, Range(1, 100)] private int _percentSizeButton; //процент размера кнопки относительно размера экрана

    private void Awake()
    {
        CreateTransformSquare();
        CreateTransformText();
    }
    private void CreateTransformSquare()
    {
        float koefSizeGrid = (float)_percentSizeGrid / 100;
        float koefSizeButton = (float)_percentSizeButton / 100;
        float sideOfGrid; //сторона квадрата сетки
        float sideOfButton; //сторона квадрата кнопки

        if (Screen.width > Screen.height)
        {
            sideOfGrid = Screen.height * koefSizeGrid;
            sideOfButton = Screen.height * koefSizeButton;
        }
        else
        {
            sideOfGrid = Screen.width * koefSizeGrid;
            sideOfButton = Screen.width * koefSizeButton;
        }
        _cellsField.sizeDelta = new Vector2(sideOfGrid, sideOfGrid);
        _restartButton.sizeDelta = new Vector2(sideOfButton, sideOfButton);
        
        
        Vector3 value = new Vector3(-_cellsField.rect.width / 2, _cellsField.rect.height / 2, 0f);
        _cellsField.localPosition = value;
    }
    private void CreateTransformText()
    {
        _findText.sizeDelta = new Vector2(_cellsField.rect.width, Screen.height / 2 - _cellsField.rect.height / 2);
        Vector3 value = new Vector3(0f, _cellsField.rect.height / 2 + _findText.rect.height / 2, 0f);
        _findText.localPosition = value;
    }
}

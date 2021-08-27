using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class MyAnimations : MonoBehaviour
{
    [SerializeField] private FindText _findText;
    [SerializeField, Range(0, 5)] private float _durFadeInText;
    [SerializeField, Range(0, 5)] private float _durFadeRestart;
    [SerializeField, Range(0, 5)] private float _durBounce;
    [SerializeField, Range(0, 20)] private float _offsetPosition;
    [SerializeField, Range(0, 5)] private float _durErrorHandler;
    [SerializeField] private Image _restartImage;
    private CanvasGroup canvasGroup;
    
    private void Awake()
    {
        canvasGroup = _findText.GetComponent<CanvasGroup>();
    }
    public void FadeInText()
    {
        Sequence fadeText = DOTween.Sequence();
        canvasGroup.alpha = 0f;
        fadeText.Append(canvasGroup.DOFade(1f, _durFadeInText));
        fadeText.Play();
    }

    public void EazyBounce(TweenCallback action, CellImage cellImage)
    {
        Sequence bounce = DOTween.Sequence();
        cellImage.transform.localScale = new Vector3(0f, 0f, 1);
        bounce.Append(cellImage.transform.DOScale(1f, _durBounce)).SetEase(Ease.InBounce);
        bounce.OnComplete(action);
        bounce.Play();
    }

    public void FadeRestartPanel(TweenCallback action, int fade)
    {
        Sequence fadeIn = DOTween.Sequence();
        fadeIn.Append(_restartImage.DOFade(fade, _durFadeRestart));
        fadeIn.OnComplete(action);
        fadeIn.Play();
    }

    public void ErrorHandlerBounce(CellImage cellImage)
    {
        Sequence sequence = DOTween.Sequence();
        float xPosition = cellImage.transform.localPosition.x;
        cellImage.transform.localPosition = new Vector3(xPosition - _offsetPosition,
                                            cellImage.transform.localPosition.y,
                                            cellImage.transform.localPosition.z);
        sequence.Append(cellImage.transform.
                    DOLocalMoveX(xPosition + _offsetPosition, _durErrorHandler)).
                        SetEase(Ease.InBounce);
        sequence.OnComplete(() => ReturnPosition(cellImage, xPosition));
        sequence.Play();
    }
    private void ReturnPosition(CellImage cellImage, float xPosition)
    {
        cellImage.transform.localPosition = new Vector3(xPosition,
                                            cellImage.transform.localPosition.y,
                                            cellImage.transform.localPosition.z);
    }
}

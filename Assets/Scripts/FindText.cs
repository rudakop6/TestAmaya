using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class FindText : MonoBehaviour
{
    [SerializeField] private Text _text;
    [SerializeField] private UnityEvent<int> NewLevelEvent;
    [SerializeField] private SlotsGenerator _slotsGenerator;
    [SerializeField] private GameObject _restartImage;
    [SerializeField] private GameObject _restartButton;
    [SerializeField] private MyAnimations _myAnimations;
    private int level = 0;
    private string findValue;

    public void FinderCard(string value)
    {
        findValue = value;
        _myAnimations.FadeInText();
        _text.text = "Find" + " " + value;
    }
    public void CheckerCard(string value, CellImage cell)
    {
        if (value == findValue)
        {
            level++;
            if (level == _slotsGenerator.numberOflevels)
            {
                _myAnimations.EazyBounce(() => Restart(true), cell);
            }
            else
            {
                _myAnimations.EazyBounce(() => NextLevel(), cell);
            }
        }
        else
        {
            _myAnimations.ErrorHandlerBounce(cell);
        }
    }
    public void NextLevel()
    {
        NewLevelEvent.Invoke(level);
    }
    public void RestartClickHandler()
    {
        level = 0;
        EnableButton(false);
        _myAnimations.FadeRestartPanel(()=> EnableScreen(false), (int)FadeEnum.FadeOut);
        NewLevelEvent.Invoke(level);
    }
    public void Restart(bool flag)
    {
        EnableScreen(flag);
        _myAnimations.FadeRestartPanel(()=> EnableButton(flag), (int)FadeEnum.FadeIn);
    }
    public void EnableButton(bool flag)
    {
        _restartButton.SetActive(flag);
    }
    public void EnableScreen(bool flag)
    {
        _restartImage.SetActive(flag);
    }
}

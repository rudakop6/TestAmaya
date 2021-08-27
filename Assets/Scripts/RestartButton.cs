using UnityEngine;
using UnityEngine.Events;

public class RestartButton : MonoBehaviour
{
    [SerializeField] private UnityEvent RestartEvent;
    public void OnClick()
    {
        RestartEvent.Invoke();
    }
}

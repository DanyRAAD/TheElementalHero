using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UIButtonSound : MonoBehaviour, IPointerEnterHandler
{
    public AudioClip hoverClip;
    public AudioClip clickClip;

    void Start()
    {
        GetComponent<Button>().onClick.AddListener(PlayClick);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        UIAudioManager.Instance?.PlaySound(hoverClip);
    }

    public void PlayClick()
    {
        UIAudioManager.Instance?.PlaySound(clickClip);
    }
}

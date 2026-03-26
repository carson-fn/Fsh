using UnityEngine;
using UnityEngine.UI;

public class VolumeToggle : MonoBehaviour
{
    public Button button;
    public Image iconImage;
    public Sprite volumeOnSprite;
    public Sprite volumeOffSprite;

    private bool isMuted = false;

    void Start()
    {
        button.onClick.AddListener(OnClick);
        iconImage.sprite = volumeOnSprite;
    }

    void OnClick()
    {
        isMuted = !isMuted;
        iconImage.sprite = isMuted ? volumeOffSprite : volumeOnSprite;
        AudioListener.volume = isMuted ? 0f : 1f;
    }
}
using UnityEngine;
using UnityEngine.UI;

public class VolumeToggle : MonoBehaviour
{
    public Button button;
    public Image iconImage;
    public Sprite volumeOnSprite;
    public Sprite volumeOffSprite;
    public AudioSource music;

    private bool isMuted = false;

    void Start()
    {
        button.onClick.AddListener(OnClick);
        iconImage.sprite = volumeOnSprite;
        music.playOnAwake = true;
    }

    void OnClick()
    {
        isMuted = !isMuted;
        iconImage.sprite = isMuted ? volumeOffSprite : volumeOnSprite;
        AudioListener.volume = isMuted ? 0f : 1f;
        if (isMuted)
        {
            music.Pause();
        }
        else
        {
            music.Play();
        }
    }
}
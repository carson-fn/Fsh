using UnityEngine;
using UnityEngine.EventSystems;

public class EasterEgg : MonoBehaviour, IPointerClickHandler
{
    [Header("Click Settings")]
    public int clicksRequired = 5;

    [Header("Easter Egg Objects")]
    public GameObject millaine;
    public GameObject carson;
    public GameObject liam;
    public GameObject sabrina;
    public GameObject jacob;
    public GameObject harold;

    private int clickCount = 0;

    void Start()
    {
        millaine.SetActive(false);
        carson.SetActive(false);
        liam.SetActive(false);
        sabrina.SetActive(false);
        jacob.SetActive(false);
        harold.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        clickCount++;

        if (clickCount >= clicksRequired)
        {
            millaine.SetActive(true);
            carson.SetActive(true);
            liam.SetActive(true);
            sabrina.SetActive(true);
            jacob.SetActive(true);
            harold.SetActive(true);
        }
    }
}

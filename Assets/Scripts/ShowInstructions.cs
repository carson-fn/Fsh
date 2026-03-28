using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;



public class ShowInstructions : MonoBehaviour, IPointerClickHandler
{

    [Header("Instructions")]
    public GameObject instructions;

    private int toggle = 0; // 0 = OFF, 1 = ON

    void Start()
    {
        instructions.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (toggle == 0) {
            instructions.SetActive(true);
            toggle = 1;
        }
        else
        {
            instructions.SetActive(false);
            toggle = 0;
        }
        

    }
}
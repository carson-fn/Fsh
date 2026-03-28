using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LoginUIController : MonoBehaviour
{
    EventSystem system;
    public Selectable firstInput;
    public Button submitButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        system = EventSystem.current;
        firstInput.Select();
    }

    // Update is called once per frame
    void Update()
    {
        // Shift + Tab to move up
        if (Input.GetKeyDown(KeyCode.Tab) && Input.GetKey(KeyCode.LeftShift))
        {
            if (system.currentSelectedGameObject == null) { return; }

            Selectable next = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnUp();

            if (next != null) { next.Select(); }
        }
        // Tab to move down
        else if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (system.currentSelectedGameObject == null) { return; }

            Selectable next = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown();

            if (next != null) { next.Select(); }
        }
        // Submit on Enter
        else if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            if (system.currentSelectedGameObject == null) { return; }

            if (submitButton != null)
            {
                submitButton.onClick.Invoke();
                Debug.Log("Submit button clicked");
                return;
            }
            else
            {
                Button button = system.currentSelectedGameObject.GetComponent<Button>();

                if (button != null)
                {
                    button.onClick.Invoke();
                    Debug.Log("Button clicked: " + button.name);
                }
            }
        }
    }
}

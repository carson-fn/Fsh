using UnityEngine;

public class ShopTestOpener : MonoBehaviour
{
    [SerializeField] private ShopMenuController shopMenuController;

    public void OpenShop()
    {
        if (shopMenuController != null)
        {
            shopMenuController.OpenShop();
        }
    }

    public void CloseShop()
    {
        if (shopMenuController != null)
        {
            shopMenuController.CloseShop();
        }
    }
}
using UnityEngine;
using UnityEngine.SceneManagement;

public class StarttoSignIn : MonoBehaviour
{
    public void LoadLoadingScene() {
        SceneManager.LoadScene("SignUpScene");
    }
}
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButtonScript : MonoBehaviour
{
    public void LoadLoadingScene() {
        SceneManager.LoadScene("Loading");
    }
}


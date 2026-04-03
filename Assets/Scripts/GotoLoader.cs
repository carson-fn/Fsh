using UnityEngine;
using UnityEngine.SceneManagement;

public class GotoLoader : MonoBehaviour
{
    public void LoadLoadingScene() {
        SceneManager.LoadScene("Loading");
    }
}


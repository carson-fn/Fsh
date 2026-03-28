using UnityEngine;
using UnityEngine.SceneManagement;

public class MaptoLevel : MonoBehaviour
{
    public void LoadLoadingScene() {
        SceneManager.LoadScene("TestStartScreen");
    }
}

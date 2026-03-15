using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButtonScript : MonoBehaviour
{
    public void LoadScene2()
    {
        SceneManager.LoadScene("SampleScene");
    }
}


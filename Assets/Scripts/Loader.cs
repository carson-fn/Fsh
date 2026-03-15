using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class Loader : MonoBehaviour
{
    IEnumerator Start()
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync("GameScene");
        operation.allowSceneActivation = false;

        float timer = 0f;

        while (!operation.isDone)
        {
            timer += Time.deltaTime;

            if (operation.progress >= 0.9f && timer >= 2f)
            {
                operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}

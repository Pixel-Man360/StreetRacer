using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class InitialSceneControl : MonoBehaviour
{
    [SerializeField] private Slider progressSlider;

    void Start()
    {
        SoundManager.instance.PlaySound(3);
        StartCoroutine(ChangeScene(Loader.GetScene()));
    }

    IEnumerator ChangeScene(int nextScene)
    {
        progressSlider.value = 0;

        AsyncOperation op = SceneManager.LoadSceneAsync(nextScene);
        op.allowSceneActivation = false;

        float progress = 0f;

        while (!op.isDone)
        {
            progress = Mathf.MoveTowards(progress, op.progress, Time.deltaTime);
            progressSlider.value = progress;

            if (progress >= 0.9f)
            {
                progressSlider.value = 1f;
                op.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}


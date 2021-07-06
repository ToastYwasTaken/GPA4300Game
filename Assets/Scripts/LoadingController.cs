using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingController : MonoBehaviour
{

    delegate void CompletedOperation();
    public TextMeshProUGUI ProgressText;

    // Start is called before the first frame update
    void Start()
    {



    }

    private void Operation_completed(AsyncOperation obj)
    {
        Debug.Log("loading completed");
    }

    public void LoadGame()
    {

        StartCoroutine(LoadingGame());
    }

    IEnumerator LoadingGame()
    {
        yield return null;
        AsyncOperation operation = SceneManager.LoadSceneAsync(1);
        operation.completed += Operation_completed;

        operation.allowSceneActivation = false;
        //ProgressText.text = $"Fortschritt: {operation.progress * 100} %";

        while (!operation.isDone)
        {
            ProgressText.text = $"Fortschritt: {operation.progress * 100:##0} %";

            if (operation.progress >= 0.9f)
            {
                ProgressText.text = "Press the space bar to continue!";
                break;
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    operation.allowSceneActivation = true;
 
                }
                yield return null;
            }
        }


    }


    // Update is called once per frame
    void Update()
    {

    }
}

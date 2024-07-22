using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitPlayGame : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var button = GetComponent<UnityEngine.UI.Button>();

        button.onClick.AddListener(QuitGame);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void QuitGame()
    {
        SceneManager.LoadSceneAsync(0);
    }
}

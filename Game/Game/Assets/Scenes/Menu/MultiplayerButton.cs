using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MultiplayerButton : MonoBehaviour
{
    [SerializeField]
    Animator transistionAnim;
    void Start()
    {
        var button = GetComponent<Button>();

        button.onClick.AddListener(() => StartCoroutine(LoadLevel()));
    }

    IEnumerator LoadLevel()
    {
        transistionAnim.SetTrigger("End");
        yield return new WaitForSeconds(1);
        SceneManager.LoadSceneAsync(8);
        transistionAnim.SetTrigger("Start");
    }

    void Update()
    {
        
    }
}

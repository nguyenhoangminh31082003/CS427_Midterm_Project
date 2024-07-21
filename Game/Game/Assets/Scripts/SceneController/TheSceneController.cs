using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TheSceneController : MonoBehaviour
{
    public static TheSceneController Instance;
    [SerializeField] Animator transistionAnim;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void ReplayLevel()
    {
        StartCoroutine(Replay());
    }

    public void BackToMenu() 
    {
        StartCoroutine(Menu());
    }

    IEnumerator Menu()
    {
        //transistionAnim.SetTrigger("End");
        yield return new WaitForSeconds(0);
        SceneManager.LoadSceneAsync(0);
        //transistionAnim.SetTrigger("Start");
    }

    IEnumerator Replay()
    {
        transistionAnim.SetTrigger("End");
        yield return new WaitForSeconds(1);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex);
        transistionAnim.SetTrigger("Start");
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}

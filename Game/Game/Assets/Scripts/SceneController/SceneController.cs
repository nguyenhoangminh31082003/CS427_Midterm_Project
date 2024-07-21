using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public static SceneController Instance;
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

    public void NextLevel()
    {
        MainCharacter.Instance.SaveDataToPlayerPrefs();
        StartCoroutine(LoadLevel());
    }

    public void ReplayLevel()
    {
        StartCoroutine(Replay());
    }

    IEnumerator LoadLevel()
    {
        transistionAnim.SetTrigger("End");
        yield return new WaitForSeconds(1);
        SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
        transistionAnim.SetTrigger("Start");
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

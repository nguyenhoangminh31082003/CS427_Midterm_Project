using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game.Assets.Scripts
{
    public class LoadLevelThreeButton : MonoBehaviour
    {
        // Start is called before the first frame update
        [SerializeField]
        Animator transistionAnim;

        void Start()
        {
            var button = GetComponent<Button>();

            button.onClick.AddListener(() => StartCoroutine(LoadLevel()));
        }

        // Update is called once per frame
        void Update() { }

        IEnumerator LoadLevel()
        {
            transistionAnim.SetTrigger("End");
            yield return new WaitForSeconds(1);
            SceneManager.LoadSceneAsync(5);
            transistionAnim.SetTrigger("Start");
        }
    }
}

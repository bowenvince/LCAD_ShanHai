using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;

public class SceneTransitionHandler : MonoBehaviour
{
    public static SceneTransitionHandler _this;

    //[SerializeField]
    private GameObject player;

    public Animator cutSceneAnim;

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        if (_this == null)
        {
            _this = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SceneTransition(string sceneName, int positionIndex, bool isFacingRight) 
    {
        StartCoroutine(LoadLevel(sceneName, positionIndex, isFacingRight));

    }


    private IEnumerator LoadLevel(string sceneName, int positionIndex, bool isFacingRight) 
    {
        cutSceneAnim.SetTrigger("CutScene");
        yield return new WaitForSeconds(2f);

        SceneManager.LoadScene(sceneName);

        yield return new WaitForSeconds(0f);

        cutSceneAnim.SetTrigger("CutSceneDone");

        if (positionIndex >= 0) 
        {
            player = SceneManagerLocal._this.player;

            SceneManagerLocal._this.player.transform.position = SceneManagerLocal._this.position[positionIndex].transform.position;

            Vector3 scale = SceneManagerLocal._this.player.transform.localScale;
            if (isFacingRight)
            {
                scale = new Vector3(Mathf.Abs(scale.x), scale.y, scale.z);
            }
            else
            {
                scale = new Vector3(-1 * Mathf.Abs(scale.x), scale.y, scale.z);
            }
        }
        

        
    }
}

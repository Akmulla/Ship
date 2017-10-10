using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour
{
    bool started=false;
    AsyncOperation loading;
	// Use this for initialization
	void Start ()
    {
        if (GameController.gc != null)
            Destroy(GameController.gc.gameObject);

        if (UIManager.ui != null)
            Destroy(UIManager.ui.gameObject);
    }
	
	public void BeginGame()
    {
        if(!started)
            SoundManager.sm.SingleSound(SoundSample.Button);
        started = true;
        loading=SceneManager.LoadSceneAsync("Lvl_0");
        loading.allowSceneActivation = false;
    }

    void Update()
    {
        if (!started)
            return;

        if (loading.progress > 0.89f)
            loading.allowSceneActivation = true;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScene : MonoBehaviour
{

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
        SceneManager.LoadScene("Lvl_0");
    }
}

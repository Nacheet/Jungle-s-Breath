using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScreen : MonoBehaviour {

	public void LoadSceneWithIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
}

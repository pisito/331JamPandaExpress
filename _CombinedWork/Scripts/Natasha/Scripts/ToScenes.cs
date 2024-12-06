using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToScenes : MonoBehaviour
{
    public void LoadScenes(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader
{
    public static void LoadLevel()
    {
        int buildIndex = SaveManager.Data.level % (SceneManager.sceneCountInBuildSettings - 1) + 1;
        SceneManager.LoadScene(buildIndex);
    }
}

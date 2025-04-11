using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChange : MonoBehaviour
{
    public string sceneName;
    public string spawnPoint;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameManager.Instance.levelManager.ChangeScene(sceneName, spawnPoint);
    }  
}

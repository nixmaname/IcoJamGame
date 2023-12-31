using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{

    public GameObject levelPanel;
    public int nextLevel;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            levelPanel.SetActive(true);
            SceneManager.LoadScene(nextLevel);
        }
    }
}

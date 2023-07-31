using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Manager : MonoBehaviour
{
    public void Play(int index) {
        SceneManager.LoadScene(index);
    }
}

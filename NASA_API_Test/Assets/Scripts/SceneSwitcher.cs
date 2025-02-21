using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneSwitcher : MonoBehaviour
{
    public void loadScene(int i)
    {
        SceneManager.LoadScene(i);
    }
}

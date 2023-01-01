using UnityEngine;
using UnityEngine.SceneManagement;

public class CloseChangeMenuSetting : MonoBehaviour
{
    [SerializeField] private string _sceneName = "MainMenu";

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
            SceneManager.LoadScene(_sceneName);
    }
}

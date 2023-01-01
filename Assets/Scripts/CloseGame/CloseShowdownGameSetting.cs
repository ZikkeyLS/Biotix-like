using UnityEngine;

public class CloseShowdownGameSetting : MonoBehaviour
{
    [SerializeField] private GameObject _acceptCloseMenu;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape) && _acceptCloseMenu.activeSelf == false)
            _acceptCloseMenu.SetActive(true);
    }

    public void HideWindow()
    {
        _acceptCloseMenu.SetActive(false);
    }

    public void QuitApplication()
    {
        Application.Quit();
    }
}

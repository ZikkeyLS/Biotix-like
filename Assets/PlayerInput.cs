using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public bool OnTouchBegan { get; private set; }
    public bool OnTouchExit { get; private set; }

    private void Update()
    {
        OnTouchBegan = (Input.touchCount != 0 && Input.touches[0].phase == TouchPhase.Began) || Input.GetMouseButtonDown(0);
        OnTouchExit = (Input.touchCount != 0 && Input.touches[0].phase == TouchPhase.Ended) || Input.GetMouseButtonUp(0);
    }
}

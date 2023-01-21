using UnityEngine;

public class CameraConstantWidth : MonoBehaviour
{
    [SerializeField] private Camera _camera;

    [SerializeField] private Vector2 _defaultResolution = new(720, 1280);
    [SerializeField] [Range(0f, 1f)] private float _widthOrHeight;

    private float _initialSize;
    private float _targetAspect;

    private float _initialFov;
    private float _horizontalFov = 120f;

    private void Awake()
    {
        _initialSize = _camera.orthographicSize;
        _targetAspect = _defaultResolution.x / _defaultResolution.y;
        _initialFov = _camera.fieldOfView;
        _horizontalFov = CalcVerticalFov(_initialFov, 1 / _targetAspect);
    }

    private void Update()
    {
        if (_camera.orthographic)
        {
            float constantWidthSize = _initialSize * (_targetAspect / _camera.aspect);
            _camera.orthographicSize = Mathf.Lerp(constantWidthSize, _initialSize, _widthOrHeight);
        }
        else
        {
            float constantWidthFov = CalcVerticalFov(_horizontalFov, _camera.aspect);
            _camera.fieldOfView = Mathf.Lerp(constantWidthFov, _initialFov, _widthOrHeight);
        }
    }

    private float CalcVerticalFov(float hFovInDeg, float aspectRatio)
    {
        float hFovInRads = hFovInDeg * Mathf.Deg2Rad;
        float vFovInRads = 2 * Mathf.Atan(Mathf.Tan(hFovInRads / 2) / aspectRatio);

        return vFovInRads * Mathf.Rad2Deg;
    }
}
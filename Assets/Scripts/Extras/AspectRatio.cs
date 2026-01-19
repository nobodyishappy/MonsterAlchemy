using UnityEngine;

public class AspectRatio : MonoBehaviour
{
    [SerializeField] private int targetXValue;
    [SerializeField] private int targetYValue;
    private float targetAspectRatio;

    private Camera mainCamera;
    private float prevWidth;
    private float prevHeight;
    private Rect originalRect;

    private void Start()
    {
        mainCamera = gameObject.GetComponent<Camera>();
        targetAspectRatio = (float) targetXValue / targetYValue;
        originalRect = mainCamera.rect;
    }

    private void OnDestroy()
    {
        mainCamera.rect = originalRect;
    }

    private void Update()
    {
        if (Screen.width != prevWidth | Screen.height != prevHeight)
        {
            UpdateAspectRatio();
        }
    }

    private void UpdateAspectRatio()
    {
        prevWidth = Screen.width;
        prevHeight = Screen.height;

        float currentAspectRatio = (float) Screen.width / Screen.height;
        float aspectScale = currentAspectRatio / targetAspectRatio;

        Rect cameraRect = mainCamera.rect;

        if (aspectScale < 1.0f)
        {
            cameraRect.width = 1f;
            cameraRect.height = aspectScale;
            cameraRect.x = 0f;
            cameraRect.y = (1.0f - aspectScale) / 2.0f;
        } else
        {
            cameraRect.width = 1.0f / aspectScale;
            cameraRect.height = 1f;
            cameraRect.x = -(1.0f - aspectScale) / 2.0f;
            cameraRect.y = 0f;
        }

        mainCamera.rect = cameraRect;
    }

}

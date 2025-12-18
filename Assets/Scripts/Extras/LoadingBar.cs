using UnityEngine;
using UnityEngine.UI;

public class LoadingBar : MonoBehaviour
{
    [SerializeField] private Image loadingBar;

    private void Awake()
    {
        transform.GetComponent<Canvas>().enabled = false;
    }

    public void DisplayLoading()
    {
        transform.GetComponent<Canvas>().enabled = true;
    }

    public void HideLoading()
    {
        transform.GetComponent<Canvas>().enabled = false;
    }

    public void SetLoadingValue(float percentage)
    {
        loadingBar.fillAmount = percentage;
    }
}

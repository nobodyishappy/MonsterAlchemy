using UnityEngine;
using UnityEngine.UI;

public class ItemDisplay : MonoBehaviour
{
    [SerializeField] private Image item;
    [SerializeField] private Image timer;

    public void UpdateDisplay(Sprite itemSprite)
    {
        item.sprite = itemSprite;
    }

    public void UpdatePercentage(float timeRatio)
    {
        timer.fillAmount = timeRatio;
    } 
}

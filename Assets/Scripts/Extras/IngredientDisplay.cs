using UnityEngine;
using UnityEngine.UI;

public class IngredientDisplay : MonoBehaviour
{
    [SerializeField] private Transform[] imageList;
    [SerializeField] private Sprite plusIcon;
    
    private int current;

    private void Start()
    {
        ResetIngredients();
    }

    public bool AddOneIngredient(Sprite ingredientSprite)
    {
        if (current < imageList.Length)
        {
            imageList[current].GetComponent<Image>().sprite = ingredientSprite;
            if (current + 1 < imageList.Length)
            {
                imageList[current + 1].gameObject.SetActive(true);
                imageList[current + 1].GetComponent<Image>().sprite = plusIcon;
            }
            current++;
            return true;
        }
        return false;
    }

    public void RemoveAllIngredients()
    {
        ResetIngredients();
    }

    private void ResetIngredients()
    {
        current = 0;
        foreach(Transform image in imageList)
        {
            image.gameObject.SetActive(false);
        }

        imageList[0].gameObject.SetActive(true);
        imageList[0].GetComponent<Image>().sprite = plusIcon;
    }
}

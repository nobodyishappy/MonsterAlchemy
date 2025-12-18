using UnityEngine;

public class WellInteractable : Interactable
{
    private float currentProgress;
    [SerializeField] private float totalTimeRequired;
    [SerializeField] private LoadingBar loadingBar;

    private void Start()
    {
        if (GetHasItem())
        {
            if (GetItem().TryGetComponent<Bucket>(out var bucket))
            {
                loadingBar.DisplayLoading();
                loadingBar.SetLoadingValue(0f);
            }
        }
    }

    public override Transform TakeItem()
    {   
        Transform temp = GetItem();
        RemoveItem();
        SetHasItem(false);
        currentProgress = 0f;
        loadingBar.HideLoading();
        return temp;
    }

    public override bool PlaceItem(Transform newItem)
    {   
        if (GetHasItem())
        {
            return false;
        } else
        {
            SetItem(newItem);
            currentProgress = 0f;
            if (newItem.TryGetComponent<Bucket>(out var bucket))
            {
                if (!bucket.HasLiquid())
                {
                    loadingBar.DisplayLoading();
                    loadingBar.SetLoadingValue(0f);
                }
            }
            return true;
        }
        
    }

    public override bool ActionOnItem()
    {
        if (GetHasItem())
        {
            return FillingBucket();
        }

        return false;
    }

    private bool FillingBucket()
    {
        if (GetItem().TryGetComponent<Bucket>(out var bucket))
        {
            if (bucket.HasLiquid())
            {
                return false;
            }

            currentProgress += Time.deltaTime;
            loadingBar.SetLoadingValue(currentProgress / totalTimeRequired);

            if (currentProgress >= totalTimeRequired)
            {
                Liquid liquid = ScriptableObject.CreateInstance<Liquid>();
                liquid.SetNewLiquid("Water", 2, new Color(0f, 0.55f, 1f, 0.4f));
                loadingBar.HideLoading();
                return bucket.FillContainer(liquid);
            }
            return true;
        }
        return false;
    }
}

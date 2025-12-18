using UnityEngine;

public class PlantInteractable : Interactable
{
    private int leavesCount;
    [SerializeField] private Transform[] leafGroups;

    private bool leafGrowing = false;
    private float currentLeafGrowthTime;
    [SerializeField] private float totalLeafGrowthTime;
    [SerializeField] private LoadingBar loadingBar;

    private void Start()
    {
        SetIsResource();
        leavesCount = leafGroups.Length;
        currentLeafGrowthTime = 0f;
        loadingBar.HideLoading();
    }

    private void Update()
    {
        if (leafGrowing)
        {
            GrowingLeaves();
        }
    }

    public override Transform TakeItem()
    {   
        if (leavesCount == 0)
        {
            return null;
        }

        Transform temp = Instantiate(GetItem());
        leafGroups[leavesCount - 1].gameObject.SetActive(false);
        leavesCount--;

        if (leavesCount == 0)
        {
            loadingBar.DisplayLoading();
            loadingBar.SetLoadingValue(0f);
        }

        return temp;
    }

    public override bool UseItemOnItem(Transform newItem)
    {
        if(newItem.TryGetComponent<Bucket>(out var bucket))
        {
            if (bucket.HasWater())
            {
                bucket.EmptyContainer();
                leafGrowing = true;
                return true;
            }
        }
        return false;
    }

    private void GrowingLeaves()
    {
        currentLeafGrowthTime += Time.deltaTime;
        loadingBar.SetLoadingValue(currentLeafGrowthTime / totalLeafGrowthTime);

        if (currentLeafGrowthTime >= totalLeafGrowthTime)
        {
            loadingBar.HideLoading();
            ResetLeaves();
        }
    }

    private void ResetLeaves()
    {
        leafGrowing = false;
        foreach (Transform leafGroup in leafGroups)
        {
            leafGroup.gameObject.SetActive(true);
        }
        leavesCount = leafGroups.Length;
        currentLeafGrowthTime = 0f;
    }
}

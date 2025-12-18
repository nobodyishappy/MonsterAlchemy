using UnityEngine;

public class Interactable : MonoBehaviour
{
    private bool hasItem = false;
    private bool isResource = false;
    private Transform item;
    [SerializeField] private Transform placementPoint;

    private void Awake()
    {
        if (placementPoint.childCount > 0)
        {
            item = placementPoint.GetChild(0);
            hasItem = true;
        }
    }

    public void SetIsResource()
    {
        isResource = true;
    }

    public bool GetIsResource()
    {
        return isResource;
    }

    public void SetHasItem(bool newHasItem)
    {
        hasItem = newHasItem;
    }

    public bool GetHasItem()
    {
        return hasItem;
    }

    public void ResetItemPos()
    {
        item.SetLocalPositionAndRotation(new Vector3(), new Quaternion());
    }

    public Transform GetItem()
    {
        return item;
    }

    public void SetItem(Transform newItem)
    {
        item = newItem;
        item.parent = placementPoint;
        hasItem = true;
        ResetItemPos();
    }

    public void RemoveItem()
    {
        hasItem = false;
        item = null;
    }

    public virtual Transform TakeItem()
    {   
        return null;
    }

    public virtual bool PlaceItem(Transform newItem)
    {   
        return false;
    }

    public virtual bool ActionOnItem()
    {
        return false;
    }

    public virtual bool UseItemOnItem(Transform newItem)
    {
        return false;
    }
}

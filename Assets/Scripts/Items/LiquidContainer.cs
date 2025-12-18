using UnityEngine;

public class LiquidContainer : MonoBehaviour
{
    [SerializeField] private Transform liquidMesh;
    private string containerType;
    private Liquid liquid;
    private bool hasLiquid = false;

    private void Awake()
    {
        liquidMesh.gameObject.SetActive(false);
    }

    public bool HasLiquid()
    {
        return hasLiquid;
    }

    public bool HasWater()
    {
        if (liquid != null) {
            return liquid.IsWater();
        } else
        {
            return false;
        }
    }

    public virtual bool FillContainer(Liquid newLiquid)
    {
        if (newLiquid == null)
        {
            return false;
        }
        
        if (!hasLiquid)
        {
            liquid = newLiquid;
            hasLiquid = true;
            liquidMesh.gameObject.SetActive(true);
            liquidMesh.GetComponent<Renderer>().material.color = liquid.GetLiquidColor();
            return true;
        }
        return false;
    }

    public virtual Liquid EmptyContainer()
    {
        if (hasLiquid)
        {
            Liquid temp = liquid;
            liquid = null;
            hasLiquid = false;
            liquidMesh.gameObject.SetActive(false);
            return temp;
        } 
        return null;
    }

    public void SetContainerType(string type)
    {
        containerType = type;
    } 

    

    public string GetLiquidStoredName()
    {
        if (liquid == null)
        {
            return "";
        }
        return liquid.GetBaseLiquid();
    }
}

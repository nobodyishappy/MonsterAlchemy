using UnityEngine;

public class Bottle : LiquidContainer
{
    [SerializeField] private Transform corkMesh;

    private void Start()
    {
        SetContainerType("Bottle");
        corkMesh.gameObject.SetActive(false);
    }

    public override bool FillContainer(Liquid newLiquid)
    {
        if (base.FillContainer(newLiquid))
        {
            corkMesh.gameObject.SetActive(true);
            return true;
        }
        return false;
    }

    public override Liquid EmptyContainer()
    {
        Liquid liquid = base.EmptyContainer();
        corkMesh.gameObject.SetActive(false);
        return liquid;
    }
}

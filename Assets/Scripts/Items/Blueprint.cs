using UnityEngine;

public class Blueprint : MonoBehaviour
{
    [SerializeField] private SkinnedMeshRenderer floorPlan;

    [SerializeField] private float morphSpeed;

    private bool isMorphing = false;
    private float blendWeight = 0;

    private void Update()
    {
        if(isMorphing)
        {
            UpdateFloorPlan();
        }
    }

    private void UpdateFloorPlan()
    {
        if (blendWeight < 100)
        {
            blendWeight += morphSpeed * Time.deltaTime;
            floorPlan.SetBlendShapeWeight(0, blendWeight);
        }
    }

    public void StartMorph()
    {
        isMorphing = true;
        blendWeight = 0;
    }

    public void ResetMorph()
    {
        isMorphing = false;
        blendWeight = 0;
        floorPlan.SetBlendShapeWeight(0, blendWeight);
    }
}

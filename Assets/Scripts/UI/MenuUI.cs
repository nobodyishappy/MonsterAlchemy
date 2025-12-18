using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuUI : MonoBehaviour
{
    
    public void StartMapStage()
    {
        SceneManager.LoadScene("StoreMap", LoadSceneMode.Single);
    }
}

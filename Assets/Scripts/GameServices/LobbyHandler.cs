using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class LobbyHandler : MonoBehaviour
{
    [SerializeField] private Image loadingBar;
    private int playersOnExit;
    private int totalPlayers = 1;
    private bool isReady = false;

    [SerializeField] private Interactable blueprint;
    [SerializeField] private Interactable recipeSheet;

    private float fillPercentage;
    private float waitingTime = 3;

    [SerializeField] private Transform pauseScreen;
    private InputAction pauseAction;
    private bool isPaused;

    private void Start()
    {
        playersOnExit = 0;
        fillPercentage = 0;

        isPaused = false;

        pauseAction = InputSystem.actions.FindAction("Pause");
    }

    private void Update()
    {
        if (pauseAction.WasPressedThisFrame())
        {
            if (isPaused)
            {
                isPaused = false;
                Unpause();
            } else
            {
                isPaused = true;
                Pause();
            }
        }

        if (fillPercentage >= 1)
        {
            SceneManager.LoadScene("StoreMap", LoadSceneMode.Single);
        }

        if (blueprint.GetHasItem() & recipeSheet.GetHasItem())
        {
            isReady = true;
        } else
        {
            isReady = false;
        }
        
        if (isReady & playersOnExit == totalPlayers)
        {
            fillPercentage += Time.deltaTime / waitingTime;
            loadingBar.fillAmount = fillPercentage;
        } else
        {
            fillPercentage = 0;
            loadingBar.fillAmount = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponentInParent<PlayerController>() != null)
        {
            playersOnExit++;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        playersOnExit--;
    }

    public void Pause()
    {
        Time.timeScale = 0;
        isPaused = true;
        pauseScreen.gameObject.SetActive(true);
    }

    public void Unpause()
    {
        Time.timeScale = 1;
        isPaused = false;
        pauseScreen.gameObject.SetActive(false);
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("HomeScreen", LoadSceneMode.Single);
        Time.timeScale = 1;
    }
}

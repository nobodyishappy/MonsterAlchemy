using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;


public class GameHandler : MonoBehaviour
{
    public static GameHandler Instance { get; private set; }
    public IScoreHandler ScoreHandler { get; private set; }
    public float CurrentTime { get; private set; }

    [SerializeField] private float MaxGameTime;
    private GameUI gameUI;
    private CustomerManager customerManager;
    private PlayerController playerController;
    public bool isGameEnded;

    private InputAction interactAction;
    private InputAction actionAction;
    private InputAction pauseAction;

    private bool isPaused;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return; 
        }

        Instance = this;

        gameUI = FindFirstObjectByType<GameUI>();
        customerManager = FindFirstObjectByType<CustomerManager>();
        playerController = FindFirstObjectByType<PlayerController>();

        ScoreHandler = new ScoreHandler();
        CurrentTime = MaxGameTime;
        isGameEnded = false;
        isPaused = false;

        interactAction = InputSystem.actions.FindAction("Interact");
        actionAction = InputSystem.actions.FindAction("Action");
        pauseAction = InputSystem.actions.FindAction("Pause");
    }

    private void Update()
    {
        if (isGameEnded)
        {
            if (interactAction.WasPressedThisFrame())
            {
                SceneManager.LoadScene("LobbyMap", LoadSceneMode.Single);
            }
            return;
        }

        if (pauseAction.WasPressedThisFrame())
        {
            if (isPaused)
            {
                isPaused = false;
                gameUI.UnpauseGame();
            } else
            {
                isPaused = true;
                gameUI.PauseGame();
            }
        }

        if (CurrentTime > 0)
        {
            CurrentTime -= Time.deltaTime;
        } else
        {
            CurrentTime = 0;
            gameUI.EndGame();
            customerManager.EndGame();
            playerController.enabled = false;
            isGameEnded = true;
        }

        gameUI.UpdateTimerUI(CurrentTime);
    }

    public void ReturnToLobby()
    {
        SceneManager.LoadScene("LobbyMap", LoadSceneMode.Single);
        Time.timeScale = 1;
    }
}

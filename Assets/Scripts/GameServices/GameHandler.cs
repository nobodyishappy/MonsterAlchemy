using Unity.VisualScripting;
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
    public bool isGameEnded;

    private InputAction interactAction;
    private InputAction actionAction;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return; 
        }

        Instance = this;

        gameUI = FindFirstObjectByType<GameUI>();

        ScoreHandler = new ScoreHandler();
        CurrentTime = MaxGameTime;
        isGameEnded = false;

        interactAction = InputSystem.actions.FindAction("Interact");
        actionAction = InputSystem.actions.FindAction("Action");
    }

    private void Update()
    {
        if (isGameEnded)
        {
            Time.timeScale = 1f;
            if (interactAction.WasPressedThisFrame())
            {
                SceneManager.LoadScene("HomeScreen", LoadSceneMode.Single);
            }
            return;
        }

        if (CurrentTime > 0)
        {
            CurrentTime -= Time.deltaTime;
        } else
        {
            CurrentTime = 0;
            Time.timeScale = 0f;
            gameUI.EndGame();
            isGameEnded = true;
        }

        gameUI.UpdateTimerUI(CurrentTime);
    }


}

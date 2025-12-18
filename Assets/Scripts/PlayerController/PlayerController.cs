using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private InputAction moveAction;
    private InputAction interactAction;
    private InputAction actionAction;

    private Rigidbody rb;

    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float rotSpeed = 5f;

    private bool isHolding = false;
    private LayerMask mask;
    
    [SerializeField] private Transform holdPoint;
    private Transform item;
    
    private void Awake()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        interactAction = InputSystem.actions.FindAction("Interact");
        actionAction = InputSystem.actions.FindAction("Action");

        rb = GetComponent<Rigidbody>();
        mask = LayerMask.GetMask("Interactable");
    }

    private void Update()
    {
        RaycastHit hit;

        if (Physics.SphereCast(transform.position + new Vector3(0, 0.4f, 0), 0.25f, transform.forward, out hit, 0.5f, mask))
        {
            Debug.DrawRay(transform.position + new Vector3(0, 0.4f, 0), transform.forward, Color.red);
        }

        if (interactAction.WasPressedThisFrame())
        {
            
            if (Physics.SphereCast(transform.position, 0.25f, transform.forward, out hit, 0.5f, mask))
            {
                Interacted(hit);
            }
        }

        if (actionAction.IsPressed())
        {
            if (Physics.SphereCast(transform.position, 0.25f, transform.forward, out hit, 0.5f, mask))
            {
                ActionOnTarget(hit);
            }
        }
    }

    private void Interacted(RaycastHit hit)
    {
        Interactable interactable = hit.collider.GetComponent<Interactable>();
        if (isHolding)
        {
            if (interactable.PlaceItem(item))
            {
                item = null;
                isHolding = false;
            } else if (interactable.UseItemOnItem(item))
            {
                
            }
        } else
        {   
            if (interactable.GetHasItem())
            {
                Transform newItem = interactable.TakeItem();
                if (newItem  == null)
                {
                    return;
                }
                item = newItem;
                item.parent = holdPoint;
                item.SetLocalPositionAndRotation(new Vector3(), new Quaternion());
                isHolding = true;
            }
        }
    }

    private void ActionOnTarget(RaycastHit hit)
    {
        
        if (hit.collider.TryGetComponent<Interactable>(out var interactable))
        {
            if (interactable.ActionOnItem())
            {
                //Player Animations
            }
        }
    }

    private void FixedUpdate()
    {   
        Vector2 moveValue = moveAction.ReadValue<Vector2>();
        
        if (moveValue.x != 0 | moveValue.y != 0)
        {
            Vector3 moveDir = new Vector3(moveValue.x, 0, moveValue.y);
        
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(moveDir), rotSpeed * Time.deltaTime);

            rb.AddForce(moveDir * moveSpeed * Time.deltaTime, ForceMode.VelocityChange);
        }
    }
}

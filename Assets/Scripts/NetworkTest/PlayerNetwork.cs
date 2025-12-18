using Unity.Netcode;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerNetwork : NetworkBehaviour
{
    private InputAction moveAction;
    private InputAction interactAction;

    [SerializeField] private Transform spawnObjectPrefab;

    private Transform spawnObjectTransform;

    private NetworkVariable<int> randomNumber = new NetworkVariable<int>(1, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    public override void OnNetworkSpawn()
    {
        moveAction = InputSystem.actions.FindAction("Move");
        interactAction = InputSystem.actions.FindAction("Interact");

        randomNumber.OnValueChanged += (int previousValue, int newValue) =>
        {
          Debug.Log(OwnerClientId+ "; " + "random number: " + randomNumber.Value);
        };
    }

    private void Update()
    {   
        if (!IsOwner) return;
        
        if (interactAction.WasPressedThisFrame())
        {
            spawnObjectTransform = Instantiate(spawnObjectPrefab, new Vector3(Random.Range(-10,10), 0, Random.Range(-10,10)), new Quaternion(0, 0, 0, 0));
            spawnObjectTransform.GetComponent<NetworkObject>().Spawn(true);
            // randomNumber.Value = Random.Range(0, 100);
        }

        Vector2 moveValue = moveAction.ReadValue<Vector2>();
        
        Vector3 moveDir = new Vector3(moveValue.x, 0, moveValue.y);

        float moveSpeed = 3f;

        transform.position += moveDir * moveSpeed * Time.deltaTime;
    }

    [ServerRpc]
    private void TestServerRpc()
    {
        Debug.Log("TestServerRpc "+ OwnerClientId);
    }

    [ClientRpc]
    private void TestClientRpc()
    {
        Debug.Log("TestClientRpc");
    }
}

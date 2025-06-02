using UnityEngine;

public class FishingTapHandler : MonoBehaviour
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private InputController inputController;

    void Awake()
    {
        inputController = GetComponent<InputController>();
    }

    private void Start()
    {
        inputController.OnTap += HandleTap;
    }

    private void HandleTap(Vector2 screenPosition)
    {
        Vector3 worldPos = mainCamera.ScreenToWorldPoint(screenPosition);
        worldPos.z = 0f;

        //Debug.Log("Casting fishing rod at: " + worldPos);
        // Cast fishing rod logic here
    }

    private void OnDestroy()
    {
        inputController.OnTap -= HandleTap;
    }
}

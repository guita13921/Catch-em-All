using UnityEngine;

public class TensionMeterManager : MonoBehaviour
{
    [Header("Tension Meter Settings")]
    [SerializeField] private float pullForce = 0f;
    [SerializeField] private float tensionSpeed = 2f;
    [SerializeField] private float minY = -3f;
    [SerializeField] private float maxY = 5f;

    [Header("Markers")]
    public Transform fishMarker;
    public Transform fishMarkerFight;

    [Header("Win Settings")]
    public bool playerWon = false;
    FishingController fishingController;

    private float currentY = 0f;

    void Awake()
    {
        fishingController = GetComponent<FishingController>();
    }

    void Start()
    {
        currentY = Mathf.Clamp(fishMarker.position.y, minY, maxY);
        UpdateMarkerVisibility();
    }

    void Update()
    {
        // Move based on pullForce
        currentY += pullForce * tensionSpeed * Time.deltaTime;
        currentY = Mathf.Clamp(currentY, minY, maxY);

        // Update position of both markers
        Vector3 newPos = new Vector3(fishMarker.position.x, currentY, fishMarker.position.z);
        fishMarker.position = newPos;
        fishMarkerFight.position = newPos;

        UpdateMarkerVisibility();

        // Check win condition
        if (currentY >= maxY && !playerWon)
        {
            playerWon = true;
            fishingController.CurrentFishCatched();
            ResetTension();
        }
    }

    private void UpdateMarkerVisibility()
    {
        bool isPullPositive = pullForce > 0;

        if (fishMarker != null) fishMarker.gameObject.SetActive(isPullPositive);
        if (fishMarkerFight != null) fishMarkerFight.gameObject.SetActive(!isPullPositive);
    }

    // Call these to apply force
    public void AddPullForcePlayer(float amount = 1f)
    {
        pullForce += amount;
    }

    public void AddPullForceFish(float amount = 1f)
    {
        pullForce -= amount;
    }

    public void ResetTension()
    {
        pullForce = 0f;
        currentY = minY;
        playerWon = false;

        if (fishMarker != null) fishMarker.position = new Vector3(fishMarker.position.x, currentY, fishMarker.position.z);
        if (fishMarkerFight != null) fishMarkerFight.position = new Vector3(fishMarkerFight.position.x, currentY, fishMarkerFight.position.z);

        UpdateMarkerVisibility();
    }

}

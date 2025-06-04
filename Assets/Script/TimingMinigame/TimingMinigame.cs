using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TimingMinigame : MonoBehaviour
{
    public enum MotionPattern
    {
        SineWave,
        PingPong,
        EaseInOut
    }

    [Header("Ball Settings")]
    public RectTransform ball;             // Reference to the UI ball
    public MotionPattern pattern = MotionPattern.SineWave;
    public float speed = 1f;               // Oscillation speed
    public float amplitude = 1f;           // Controls how far the ball swings from center
    public float centerX = 0f;     // Center X position of the bar

    [Header("Bounds")]
    public float leftLimit = -1.3f;
    public float rightLimit = 1.3f;

    [Header("Scoring")]
    public float successRange = 0.6f; // From -0.6 to 0.6
    public KeyCode hitKey = KeyCode.Space;
    public int score = 0;
    public GameObject successBar;

    private float elapsedTime;

    public void StartGame()
    {
        //Show UI
    }

    void Update()
    {
        if (ball == null) return;

        elapsedTime += Time.deltaTime;
        float t = 0f;

        switch (pattern)
        {
            case MotionPattern.SineWave:
                t = (Mathf.Sin(elapsedTime * speed) + 1f) / 2f; // Sin from 0 to 1
                break;

            case MotionPattern.PingPong:
                t = Mathf.PingPong(elapsedTime * speed, 1f); // Ping-pong from 0 to 1
                break;

            case MotionPattern.EaseInOut:
                float rawT = Mathf.PingPong(elapsedTime * speed, 1f);
                t = Mathf.SmoothStep(0f, 1f, rawT); // Eases in and out
                break;
        }

        float posX = Mathf.Lerp(leftLimit, rightLimit, t);
        Vector2 current = ball.anchoredPosition;
        ball.anchoredPosition = new Vector2(posX, current.y);
    }

    public void PressTab()
    {
        float offsetFromCenter = ball.anchoredPosition.x - centerX;

        if (Mathf.Abs(offsetFromCenter) <= successRange)
        {
            score++;
            Debug.Log("✅ Hit! Score: " + score);
            StartCoroutine(ShowSuccessBar());
        }
        else
        {
            Debug.Log("❌ Miss. Ball was at offset: " + offsetFromCenter.ToString("F2"));
        }
    }

    private System.Collections.IEnumerator ShowSuccessBar()
    {
        successBar.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        successBar.SetActive(false);
    }
}
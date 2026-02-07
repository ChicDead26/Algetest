using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private TMP_Text textDisplay;
    private float currentTime;
    private Coroutine timerCoroutine;

    // Start is called before the first frame update
    void Start()
    {
        UpdateText();
    }

    public void StartTimer()
    {
        if (timerCoroutine == null)
            timerCoroutine = StartCoroutine(TimerCount());
    }

    public void PauseTimer()
    {
        if (timerCoroutine != null)
        {
            StopCoroutine(timerCoroutine);
            timerCoroutine = null;
        }
    }

    public void StopAndResetTimer()
    {
        if (timerCoroutine != null)
        {
            StopCoroutine(timerCoroutine);
            timerCoroutine = null;
        }
        currentTime = 0;
        UpdateText();
    }

    private IEnumerator TimerCount()
    {
        while (true)
        {
            currentTime += Time.deltaTime;
            UpdateText();
            yield return null;
        }
    }

    private void UpdateText()
    {
        textDisplay.text = FormatTime(currentTime);
    }

    public static int SecondsToMinutes(float seconds)
    {
        return (int)(seconds / 60f);
    }

    public static float OnlySecondsAndMilisseconds(float seconds)
    {
        return seconds % 60;
    }

    public static string FormatTime(float seconds)
    {
        return $"{SecondsToMinutes(seconds)}:{OnlySecondsAndMilisseconds(seconds).ToString("00.000", CultureInfo.InvariantCulture)}";
    }
}

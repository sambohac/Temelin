using UnityEngine;
using TMPro;
using TMPro.EditorUtilities;

public class TimeController : MonoBehaviour
{
    [SerializeField]
    TMP_Text timeText;

    private void Start()
    {
    }

    /// <summary>
    /// Parse time and set it as text in format mm:ss:mmm
    /// </summary>
    /// <param name="time"> Time in milliseconds </param>
    public void SetTime(long time)
    {
        long timeRes = time;
        int mins;
        int secs;

        mins = (int) ((timeRes / 1000.0) / 60.0);
        timeRes -= mins * 60 * 1000;

        secs = (int)(timeRes / 1000.0);
        timeRes -= secs * 1000;

        timeText.text = $"{mins}:{secs}:{timeRes}";
    }

}


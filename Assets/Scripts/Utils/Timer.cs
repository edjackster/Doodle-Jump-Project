using System;
using System.Collections;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public event Action TimesUp;

    private float _tick;
    private Coroutine _timer;

    public void SetTime(float duration)
    {
        _timer = StartCoroutine(Countdown(duration));
    }

    public void Stop()
    {
        if (_timer != null)
            StopCoroutine(_timer);
    }

    private IEnumerator Countdown(float duration)
    {
        var wait = new WaitForSeconds(duration);
        yield return wait;
        TimesUp?.Invoke();
    }
}
using System;
using System.Collections;
using ATG.Activator;
using UnityEngine;

public sealed class OutlineWithAnimation : Outline, IActivateable, IDisposable
{
    [SerializeField] private bool isHideOnAwake = true;
    [SerializeField] private float duration;
    [SerializeField] private Color startColor;
    [SerializeField] private Color endColor;

    private IEnumerator _colorAnimation;

    public bool IsActive { get; private set; }

    private void Start()
    {
        if (isHideOnAwake == true) SetActive(false);
    }

    public void SetActive(bool isActive)
    {
        enabled = isActive;

        if (isActive == true)
        {
            StartAnimate();
        }
        else
        {
            Dispose();
        }

        IsActive = isActive;
    }

    [ContextMenu("Activate")] public void Activate() => SetActive(true);

    private void StartAnimate()
    {
        Dispose();
        OutlineColor = startColor;

        _colorAnimation = SwitchColorTo(endColor);
        StartCoroutine(_colorAnimation);
    }

    public void Dispose()
    {
        if (_colorAnimation != null)
            StopCoroutine(_colorAnimation);
        _colorAnimation = null;
    }

    private IEnumerator SwitchColorTo(Color nextColor)
    {
        float curTime = 0f;
        Color currentColor = nextColor;

        while (curTime < 1f)
        {
            OutlineColor = Color.Lerp(OutlineColor, nextColor, curTime);
            curTime += duration * Time.deltaTime;

            yield return null;
        }

        _colorAnimation = SwitchColorTo(GetColorByCurrent(currentColor));
        StartCoroutine(_colorAnimation);
    }

    private Color GetColorByCurrent(Color currentColor)
    {
        if (currentColor == startColor) return endColor;
        else return startColor;
    }
}
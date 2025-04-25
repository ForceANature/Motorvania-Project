using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CustomCursor : MonoBehaviour
{
    public Transform frameTransform;// To collect frame angle
    public RectTransform cursorTransform;// For moving the cursor "parent"
    public Image contour;// Contour image on/off
    public Image angleIndicator;// To make it go RED when facing DOWN
    private RectTransform angleIndicatorTransform;// To rotate the angle indicator image
    public Image whiteFill;// White fill image
    public Image redFill;// Red fill image
    public Image grayBackground;// Gray background image

    //Fill drainage
    public float drainAmount = 0.2f; // how much to drain
    public float drainDuration = 3f; // duration of drain

    //Coroutine stuff
    private Coroutine drainCoroutine;
    private bool isDraining;
    private Coroutine pulseRoutine;
    public float pulseSpeed = 4f;
    public float pulseScaleAmount = 1.3f;


    void Start()
    {
        Cursor.visible = false;// Removing default cursor

        angleIndicatorTransform = angleIndicator.rectTransform;
    }

    void Update()
    {
        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            cursorTransform.parent as RectTransform,
            Input.mousePosition,
            null, // or your camera if Screen Space - Camera
            out pos
        );
        cursorTransform.anchoredPosition = pos;

        AngleIndicatorHandler();
    }

    //---------------------------------------------------------------------------------------------------

    public void AngleIndicatorHandler()
    {
        // Rotate the effect image to match the player's Y rotation (or full 2D angle)
        float angle = frameTransform.eulerAngles.z; // or Z for 2D
        angleIndicatorTransform.localRotation = Quaternion.Euler(0, 0, angle); // for UI rotation

        // Normalize angle to 0–360
        angle = (angle + 360) % 360;

        // Calculate how close it is to 270 (facing down)
        float redFactor = 1f - Mathf.Abs(Mathf.DeltaAngle(angle, 180f)) / 90f;
        redFactor = Mathf.Clamp01(redFactor); // between 0 and 1

        // Apply color (redder as it points down)
        Color baseColor = Color.white;
        baseColor.r = Mathf.Lerp(baseColor.r, 1f, redFactor);
        baseColor.g = Mathf.Lerp(baseColor.g, 0f, redFactor);
        baseColor.b = Mathf.Lerp(baseColor.b, 0f, redFactor);

        angleIndicator.color = baseColor;
    }

    //---------------------------------------------------------------------------------------------------

    //// Changing switch
    //public void SetEffectSprite(Sprite newSprite)
    //{
    //    whiteFill.sprite = newSprite;
    //}

    //// Or for more dynamic control // on/off switch
    //public void SetPartVisibility(int partIndex, bool visible)
    //{
    //    switch (partIndex)
    //    {
    //        case 0: contour.enabled = visible; break;
    //        case 1: whiteFill.enabled = visible; break;
    //        case 2: redFill.enabled = visible; break;
    //    }
    //}

    //---------------------------------------------------------------------------------------------------

    public void StartDrain()
    {
        if (!isDraining)
        {
            drainCoroutine = StartCoroutine(DrainFill());
        }
    }

    IEnumerator DrainFill()
    {
        isDraining = true;

        float startFill = whiteFill.fillAmount;
        float targetFill = Mathf.Max(0f, startFill - drainAmount);
        float elapsed = 0f;

        while (elapsed < drainDuration)
        {
            // If the key is released, skip to the end
            if (!Input.GetButton("Fire1"))
            {
                whiteFill.fillAmount = targetFill;
                redFill.fillAmount = targetFill;
                isDraining = false;
                yield break;
            }

            elapsed += Time.unscaledDeltaTime;
            float t = elapsed / drainDuration;
            whiteFill.fillAmount = Mathf.Lerp(startFill, targetFill, t);
            yield return null;
        }

        whiteFill.fillAmount = targetFill;
        redFill.fillAmount = targetFill;
        isDraining = false;
    }

    public void ReFillCursor()
    {
        whiteFill.fillAmount = 1f;
        redFill.fillAmount = 1f;
    }

    //---------------------------------------------------------------------------------------------------

    public void StartPulse()
    {
        pulseRoutine ??= StartCoroutine(PulseRoutine());
    }

    public void StopPulse()
    {
        if (pulseRoutine != null)
        {
            StopCoroutine(pulseRoutine);
            pulseRoutine = null;

            // Reset scale and colors
            cursorTransform.localScale = Vector3.one;

            grayBackground.color = Color.gray;
        }
    }


    IEnumerator PulseRoutine()
    {

        while (true)
        {
            float t = Mathf.Sin(Time.time * pulseSpeed) * 0.5f + 0.5f; // 0 to 1

            // Scale the whole cursor
            float scale = Mathf.Lerp(1f, pulseScaleAmount, t);
            cursorTransform.localScale = new Vector3(scale, scale, 1f);

            // Change colors to red on non-exempt parts
            Color pulsingColor = Color.Lerp(Color.gray, Color.red, t);

            grayBackground.color = pulsingColor;

            yield return null;
        }
    }

    //---------------------------------------------------------------------------------------------------

}

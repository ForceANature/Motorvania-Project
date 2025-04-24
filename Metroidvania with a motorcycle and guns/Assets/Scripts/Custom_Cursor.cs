using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CustomCursor : MonoBehaviour
{
    public Transform frameTransform;// To collect frame angle
    public RectTransform cursorTransform;// For moving the cursor "parent"
    public Image contour;// Contour image on/off
    public RectTransform angleIndicator;// To rotate the angle indicator image
    public Image whiteFill;// White fill image on/off
    public Image redFill;// Red fill image on/off

    //Fill drainage
    public float drainAmount = 0.2f; // how much to drain
    public float drainDuration = 3f; // duration of drain

    private Coroutine drainCoroutine;
    private bool isDraining;

    void Start()
    {
        Cursor.visible = false;// Removing default cursor
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

        // Rotate the effect image to match the player's Y rotation (or full 2D angle)
        float angle = frameTransform.eulerAngles.y; // or Z for 2D
        angleIndicator.localRotation = Quaternion.Euler(0, 0, angle); // for UI rotation

        if (Input.GetButtonDown("Fire1") && !isDraining)
        {
            drainCoroutine = StartCoroutine(DrainFill());
        }
    }

    // Changing switch
    public void SetEffectSprite(Sprite newSprite)
    {
        whiteFill.sprite = newSprite;
    }

    // Or for more dynamic control // on/off switch
    public void SetPartVisibility(int partIndex, bool visible)
    {
        switch (partIndex)
        {
            case 0: contour.enabled = visible; break;
            case 1: whiteFill.enabled = visible; break;
            case 2: redFill.enabled = visible; break;
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
}

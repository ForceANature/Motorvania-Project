using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Cylinder_UI : MonoBehaviour
{
    public Transform frameTransform;
    public Image cylinderUI;
    private RectTransform cylinderUITransform;

    void Start()
    {
        cylinderUITransform = cylinderUI.rectTransform;
    }

    // Update is called once per frame
    void Update()
    {
        CylinderRotationHandler();
    }

    public void CylinderRotationHandler()
    {
        float angle = frameTransform.eulerAngles.z;
        cylinderUITransform.localRotation = Quaternion.Euler(0, 0, angle);
    }
}

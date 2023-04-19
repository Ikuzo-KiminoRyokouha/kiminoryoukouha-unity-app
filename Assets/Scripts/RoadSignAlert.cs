using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Debug = UnityEngine.Debug;


public class RoadSignAlert : MonoBehaviour
{
    private GameObject WorldOverlayImage;
    private GameObject AlertImage;

    public Texture2D texture;

    private float ImageOpacity = 1.0f;

    private bool IsFadeOut = true;

    // Start is called before the first frame update
    void Start()
    {
        WorldOverlayImage = GameObject.Find("WorldOverlayImage");
        AlertImage = GameObject.Find("AlertImage");
        AlertImage.GetComponent<RawImage>().texture = texture;
    }

    // Update is called once per frame
    void Update()
    {
        FadeOutImage(0.005f);
    }

    void FadeOutImage(float speed)
    {
        if (ImageOpacity >= 1.0f)
        {
            IsFadeOut = true;
        }
        if (ImageOpacity <= 0f)
        {
            IsFadeOut = false;
        }

        if (IsFadeOut)
        {
            ImageOpacity -= speed;
        }
        else
        {
            ImageOpacity += speed;
        }

        AlertImage.GetComponent<RawImage>().color = new Color(1f, 1f, 1f, ImageOpacity);

    }
}

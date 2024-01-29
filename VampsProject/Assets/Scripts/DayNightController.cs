using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Rendering.Universal;

public class DayNightController : MonoBehaviour
{
    public static bool isDay = true;

    [SerializeField] GameObject Astrals;
    [SerializeField] GameObject Sun;
    [SerializeField] GameObject Moon;
    [SerializeField] GameObject Zenith;
    //[SerializeField] Light2D GlobalLight;
    [SerializeField] float daylightRotationSpeed = 1f;

    float luminosityRange = 0.75f;
    float minLuminosity = 0.25f;

    Vector3 zenithPosition;
    Vector3 horizonPosition;
    // Start is called before the first frame update
    void Start()
    {
        horizonPosition = Sun.transform.position;
        zenithPosition = Zenith.transform.position;

        
    }

    // Update is called once per frame
    void Update()
    {
        Astrals.transform.Rotate(Vector3.back * (daylightRotationSpeed * Time.deltaTime));

        float astralAngle = Astrals.transform.localRotation.eulerAngles.z;

        if (astralAngle < 180)
        { 
           isDay = false;
        }
        else
        {
            isDay = true;
        }

        UpdateLight();
    }

    void UpdateLight()
    {// This will range from 0 to 1, 0 when sun/moon is at hozizon, 1 when at zenith
        

        float sunMoonHeightRatio = Mathf.Abs(Sun.transform.position.y -horizonPosition.y) / (zenithPosition.y -horizonPosition.y);
        //    GlobalLight.intensity = minLuminosity + sunMoonHeightRatio * luminosityRange;
        //    GlobalLight.color = Color.Lerp(Color.blue, Color.clear, sunMoonHeightRatio);
    }
}

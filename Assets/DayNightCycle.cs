using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayNightCycle : MonoBehaviour
{
    [SerializeField, Range(0f, 1f)] private float time;
    [SerializeField] private float fullDayLength;
    [SerializeField] private float startTime = 0.4f;
    private float timeRate;
    [SerializeField] private Vector3 noon;

    [Header("Sun")]
    [SerializeField] private Light sun;
    [SerializeField] private Gradient sunColor;
    [SerializeField] private AnimationCurve sunIntensity;

    [Header("Moon")]
    [SerializeField] private Light moon;
    [SerializeField] private Gradient moonColor;
    [SerializeField] private AnimationCurve moonIntensity;

    [Header("Other Light")]
    [SerializeField] private AnimationCurve lightingIntensityMultiplier;
    [SerializeField] private AnimationCurve reflectionsIntensityMultiplier;

    // Start is called before the first frame update
    void Start()
    {
        timeRate = 1f / fullDayLength;
        time = startTime;
    }

    // Update is called once per frame
    void Update()
    {
        // increment time
        time += timeRate * Time.deltaTime;

        /* if (time >= .75f)
        {
            RenderSettings.sun = moon;
        }
        else if (time >= .25f)
        {
            RenderSettings.sun = sun;
        } */

        if (time >= 1f)
            time = 0f;

        if (time > .2f && time < .8f)
            sun.gameObject.SetActive(true);
        else
            sun.gameObject.SetActive(false);

        if (time < .4f || time > .6f)
            moon.gameObject.SetActive(true);
        else
            moon.gameObject.SetActive(false);

        // rotate light
        sun.transform.eulerAngles = (time - 0.25f) * noon * 4.0f;
        moon.transform.eulerAngles = (time - 0.75f) * noon * 4.0f;

        // light intensity
        sun.intensity = sunIntensity.Evaluate(time);
        moon.intensity = moonIntensity.Evaluate(time);

        // light color
        sun.color = sunColor.Evaluate(time);
        moon.color = moonColor.Evaluate(time);

        // enable/disable sun
        /*if (sun.intensity == 0 && sun.gameObject.activeInHierarchy)
            sun.gameObject.SetActive(false);
        else if (sun.intensity > 0 && !sun.gameObject.activeInHierarchy)
            sun.gameObject.SetActive(true);

        if (moon.intensity == 0 && moon.gameObject.activeInHierarchy)
            moon.gameObject.SetActive(false);
        else if (moon.intensity > 0 && !moon.gameObject.activeInHierarchy)
            moon.gameObject.SetActive(true);*/

        // RenderSettings.ambientIntensity = lightingIntensityMultiplier.Evaluate(time);
        // RenderSettings.reflectionIntensity = reflectionsIntensityMultiplier.Evaluate(time);
    }
}

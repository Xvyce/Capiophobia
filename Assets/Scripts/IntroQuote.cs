using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class IntroQuote : MonoBehaviour
{
    public PostProcessVolume vol;
    DepthOfField depthoffield;
    // Start is called before the first frame update
    void Start()
    {
        vol.profile.TryGetSettings(out depthoffield);
        depthoffield.focalLength.value = 300;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (depthoffield.focalLength.value >0)
        depthoffield.focalLength.value -= 1.5f;
    }
}

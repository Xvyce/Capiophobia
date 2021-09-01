using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class TurnZoom : MonoBehaviour
{
    private Transform pos;

    private CameraShakeInstance myShake;

    // Start is called before the first frame update
    void Start()
    {
        pos = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        turn();
        zoom();

        if (Input.GetMouseButtonDown(0))
        {
            //CameraShaker.Instance.ShakeOnce(4f, 4f, .1f, 1f);
            myShake = CameraShaker.Instance.StartShake(3f, 3f, 3f);
            
        }
        if(Input.GetMouseButtonDown(1))
        {
            myShake.StartFadeOut(3);
        }
    }

    private void turn()
    {
        pos.Rotate(0, 0, 0.01f);
    }
    private void zoom()
    {

        Vector3 scaleChange = new Vector3(0.0001f, 0.0001f, 0.0001f);
        if(pos.transform.localScale.x <= 5)
        pos.transform.localScale += scaleChange;
        

    }
}

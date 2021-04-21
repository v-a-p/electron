using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        orginPos = transform.position;
        currentTime = -10;
    }


    public GameObject player;   
    public float shakeInt;
    public float shakeDis;
    public Vector3 shakePos;
    public Vector3 orginPos;
    public AnimationCurve shakeCurve;
    private float currentTime;  //when want to shake, must be zero


    public void Shake()
    {

        currentTime = Time.time ;
    }

    // Update is called once per frame
    void Update()
    {

        var Xpos = (Time.time) * shakeInt + 10;
        var Ypos = (Time.time) * shakeInt + 100;
        shakePos = new Vector3((Mathf.PerlinNoise(x: Xpos, y: 1) - 0.5f) * shakeDis,
            (Mathf.PerlinNoise(x: Ypos, y: 1) - 0.5f) * shakeDis, z: 0)  * shakeCurve.Evaluate(Time.time - currentTime) ;

        transform.position = orginPos + shakePos;


        orginPos = Vector3.Lerp(a: transform.position, b: new Vector3(player.transform.position.x, player.transform.position.y,
            transform.position.z), t: 0.05f);

    }
}

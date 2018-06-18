using UnityEngine;
using System.Collections;

public class SwipeRotationCamera : TouchLogicV2//NOTE: This script has been updated to V2 after video recording
{
    public Transform player;
    public float sensitivityX = 15, sensitivityY = 10;
    public bool invertX = false, invertY = false;
    private float pitch,yaw;
    //cache initial rotation of player so pitch and yaw don't reset to 0 before rotating
    private Vector3 oRotation;

    void Start()
    {
        base.Start();
        //cache original rotation of player so pitch and yaw don't reset to 0 before rotating
        oRotation = player.eulerAngles;
        pitch = oRotation.x;
        yaw = oRotation.y;

    }
    public override void OnTouchMovedAnywhere()
    {
        yaw += Input.GetTouch(touch2Watch).deltaPosition.x * sensitivityX * (invertX? 1:-1) * Time.deltaTime;
        pitch -= Input.GetTouch(touch2Watch).deltaPosition.y * sensitivityY * (invertY? 1:-1) * Time.deltaTime;
        //limit so we dont do backflips
        pitch = Mathf.Clamp(pitch, -89, 89);
        //do the rotations of our camera
        player.eulerAngles = new Vector3 ( pitch, yaw, 0.0f);
    }

    public override void OnTouchEndedAnywhere()
    {
        print("TouchLogicV2.currTouch = "+TouchLogicV2.currTouch+"; touch2Watch = "+touch2Watch+"; Input.touches.Length = "+Input.touches.Length);
        //the || condition is a failsafe
        if(TouchLogicV2.currTouch == touch2Watch || Input.touches.Length <= 0)
            touch2Watch = 64;
        else touch2Watch--;
    }
}
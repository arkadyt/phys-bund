using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;
[RequireComponent(typeof(Image))]
public class TouchLogicV2 : MonoBehaviour {
    public static int currTouch = 0;//so other scripts can know what touch is currently on screen
    [HideInInspector]
    public int touch2Watch = 64;
    private Rect touchZoneRect;
    public virtual void Start() {
        Vector3[] touchZoneCorners = new Vector3[4];
        GetComponent<Image>().rectTransform.GetWorldCorners(touchZoneCorners);
        touchZoneRect = new Rect(touchZoneCorners[0], touchZoneCorners[2] - touchZoneCorners[0]);
    }
    //If your child class uses Update, you must call base.Update(); to get this functionality
    public virtual void Update() {
        //is there a touch on screen?
        if (Input.touches.Length <= 0)
        {
            OnNoTouches();
        }
        else //if there is a touch
        {
            //loop through all the the touches on screen
            foreach (Touch touch in Input.touches) {
                currTouch = touch.fingerId;
                //executes this code for current touch (i) on screen
                if (touchZoneRect.Contains(touch.position))
                {
                    //if current touch hits our guitexture, run this code
                    switch (touch.phase) {
                        case TouchPhase.Began:
                            OnTouchBegan();
                            touch2Watch = currTouch;
                            break;
                        case TouchPhase.Ended:
                            OnTouchEnded();
                            break;
                        case TouchPhase.Moved:
                            OnTouchMoved();
                            break;
                        case TouchPhase.Stationary:
                            OnTouchStayed();
                            break;
                    }
                }
                //outside so it doesn't require the touch to be over the guitexture
                switch (touch.phase) {
                    case TouchPhase.Began:
                    OnTouchBeganAnywhere();
                    break;
                    case TouchPhase.Ended:
                    OnTouchEndedAnywhere();
                    break;
                    case TouchPhase.Moved:
                    OnTouchMovedAnywhere();
                    break;
                    case TouchPhase.Stationary:
                    OnTouchStayedAnywhere();
                    break;
                }
            }
        }
    }
    //the default functions, define what will happen if the child doesn't override these functions
    public virtual void OnNoTouches() {
    }
    public virtual void OnTouchBegan() {
        touch2Watch = TouchLogicV2.currTouch;
    }
    public virtual void OnTouchEnded() {
    }
    public virtual void OnTouchMoved() {
    }
    public virtual void OnTouchStayed() {
    }
    public virtual void OnTouchBeganAnywhere() {
    }
    public virtual void OnTouchEndedAnywhere() {
    }
    public virtual void OnTouchMovedAnywhere() {
    }
    public virtual void OnTouchStayedAnywhere() {
    }
}
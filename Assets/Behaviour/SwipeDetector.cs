using System;

namespace Behaviour
{
    using UnityEngine;
 
    public enum Swipe { None, Up, Down, Left, Right };
 
    public class SwipeDetector : MonoBehaviour
    {
        public float minSwipeLength = 5f;
        Vector2 firstPressPos;
        Vector2 secondPressPos;
        Vector2 currentSwipe;

        public bool isNachujaKamera;

        public static Swipe swipeDirection;
        public static event Action<Swipe> OnSwipe = delegate { };
 
        void Update()
        {
            DetectSwipe();
        }

        private void DetectSwipe ()
        {
            if (Input.touches.Length > 0) {
                var t = Input.GetTouch(0);
 
                if (t.phase == TouchPhase.Began) {
                    firstPressPos = new Vector2(t.position.x, t.position.y);
                }

                if (t.phase == TouchPhase.Ended) {
                    secondPressPos = new Vector2(t.position.x, t.position.y);
                    currentSwipe = new Vector3(secondPressPos.x - firstPressPos.x, secondPressPos.y - firstPressPos.y);
           
                    // Make sure it was a legit swipe, not a tap
                    if (currentSwipe.magnitude < minSwipeLength) {
                        swipeDirection = Swipe.None;
                        return;
                    }
           
                    currentSwipe.Normalize();
 
                    // Swipe up
                    if (currentSwipe.y > 0 &&  currentSwipe.x > -0.5f && currentSwipe.x < 0.5f) {
                        swipeDirection = Swipe.Up;
                        OnSwipe(Swipe.Up);
                        // Swipe down
                    } else if (currentSwipe.y < 0 && currentSwipe.x > -0.5f && currentSwipe.x < 0.5f) {
                        swipeDirection = Swipe.Down;
                        OnSwipe(Swipe.Down);
                        // Swipe right
                    } else if (currentSwipe.x < 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f) {
                        if (isNachujaKamera)
                        {
                            swipeDirection = Swipe.Right;
                            OnSwipe(Swipe.Right);
                        }
                        else
                        {
                            swipeDirection = Swipe.Left;
                            OnSwipe(Swipe.Left);
                        }
                        // Swipe left 
                    } else if (currentSwipe.x > 0 && currentSwipe.y > -0.5f && currentSwipe.y < 0.5f) {
                        if (isNachujaKamera)
                        {
                            swipeDirection = Swipe.Left;
                            OnSwipe(Swipe.Left);
                        }
                        else
                        {
                            swipeDirection = Swipe.Right;
                            OnSwipe(Swipe.Right);
                        }
                    }
                }
            } else {
                swipeDirection = Swipe.None;
            }
        }
    }
}
using System;

namespace Behaviour
{
    using UnityEngine;
 
    public enum Swipe { None, Up, Down, Left, Right };
 
    public class SwipeDetector : MonoBehaviour
    {
        public float minSwipeLength = 5f;
        private Vector2? _firstPressPos;
//        private Vector2 _currentSwipe;
        
        private bool _isSwipeEnabled;

        public delegate void DoSwipe(Swipe dir);
        public static event DoSwipe OnSwipe;

        private void Start()
        {
#if UNITY_ANDROID
        _isSwipeEnabled = true;
#elif UNITY_IOS
        _isSwipeEnabled = true;
#else
            _isSwipeEnabled = false;
#endif
        }

        void Update()
        {
            if (_isSwipeEnabled)
            {
                DetectSwipe();
            }
            else
            { 
                Buttons();
            }
        }

        private void DetectSwipe ()
        {
            if (Input.touches.Length > 0) {
                var t = Input.GetTouch(0);
 
                if (t.phase == TouchPhase.Began) {
                    _firstPressPos = new Vector2(t.position.x, t.position.y);
                }

                if (_firstPressPos != null && t.phase == TouchPhase.Moved)
                {
                    var xDiff = t.position.x - _firstPressPos.Value.x;
                    var yDiff = t.position.y - _firstPressPos.Value.y;
                    
                    var horiz = Mathf.Abs(xDiff);
                    var vertic = Mathf.Abs(yDiff);
                    
                    //_currentSwipe = new Vector2(t.position.x - _firstPressPos.Value.x, t.position.y - _firstPressPos.Value.y);
                
                    /*if (_currentSwipe.magnitude < minSwipeLength) {
                        return;
                    }*/

                    if (vertic < minSwipeLength && horiz < minSwipeLength)
                    {
                        return;
                    }
                    
                    //_currentSwipe.Normalize();
                
                    // Swipe up
                    /*if (_currentSwipe.y > 0 &&  _currentSwipe.x > -0.5f && _currentSwipe.x < 0.5f) {
                        OnSwipe(Swipe.Up);
                        // Swipe down
                    } else if (_currentSwipe.y < 0 && _currentSwipe.x > -0.5f && _currentSwipe.x < 0.5f) {
                        OnSwipe(Swipe.Down);
                        // Swipe right
                    } else if (_currentSwipe.x < 0 && _currentSwipe.y > -0.5f && _currentSwipe.y < 0.5f) {
                        OnSwipe(Swipe.Left);
                        // Swipe left 
                    } else if (_currentSwipe.x > 0 && _currentSwipe.y > -0.5f && _currentSwipe.y < 0.5f) {
                        OnSwipe(Swipe.Right);
                    }*/

                    if (vertic > horiz)
                    {
                        OnSwipe(yDiff > 0 ? Swipe.Up : Swipe.Down);
                    }
                    else
                    {
                        OnSwipe(xDiff > 0 ? Swipe.Right : Swipe.Left);
                    }

                    _firstPressPos = null;
                }
            }
        }
        
        private void Buttons()
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                OnSwipe(Swipe.Up);
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                OnSwipe(Swipe.Down);
            }

            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                OnSwipe(Swipe.Left);
            }
            
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                OnSwipe(Swipe.Right);
            }
        }
    }
}
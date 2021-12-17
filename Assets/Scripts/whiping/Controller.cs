using System.Collections;
using System.Collections.Generic;
using Behaviour;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace whiping
{
    public class Controller : MonoBehaviour
    {
        public GameObject up;
        public GameObject down;
        public GameObject right;
        public GameObject left;
        public Transform arrowParent;
        public Image bar;
        public bool isSwipeEnabled;

        private int _missed;
        private int _boost;
        
        private Queue<string> directions = new Queue<string>();
        private Queue<bool> statuses = new Queue<bool>();

        // Start is called before the first frame update
        void Start()
        {
            bar.fillAmount = 0.3f;
            StartCoroutine(SpawnArrow());
        }

        // Update is called once per frame
        void Update()
        {
            if (bar.fillAmount <= 0)
            {
                //TODO: loose
                StopAllCoroutines();
                SceneManager.LoadScene("Scenes/BaseScene");
            } else if (bar.fillAmount >= 1)
            {
                //TODO: win
                StopAllCoroutines();
                
                var state = GameState.GetInstance();
                state.SaveState(state.GetEggs() + 5, state.GetLives());
                
                SceneManager.LoadScene("Scenes/BaseScene");
            }
            
            string dir;
            
            dir = isSwipeEnabled ? Swipe() : Buttons();

            if (dir == null || directions.Count <= 0) return;
            
            bool status = dir.Equals(directions.Dequeue());

            if (status)
            {
                Hit();
            }
            else
            {
                Missed();
            }
            
            statuses.Enqueue(status);
        }

        private void Missed()
        {
            gameObject.GetComponent<Image>().color = Color.red;
            _missed++;
            _boost = 0;
            bar.fillAmount -= 0.1f;
        }

        private void Hit()
        {
            gameObject.GetComponent<Image>().color = Color.green;
            _boost++;

            GameObject.FindWithTag("Player").GetComponent<PlayerWhiping>().Whip();

            if (_boost >= 3)
            {
                bar.fillAmount += 0.1f;
            }
            else
            {
                bar.fillAmount += 0.05f;
            }
        }

        string Swipe()
        {
            switch (SwipeDetector.swipeDirection)
            {
                case Behaviour.Swipe.Left:
                    return "LEFT";
                case Behaviour.Swipe.Right:
                    return "RIGHT";
                case Behaviour.Swipe.Up:
                    return "UP";
                case Behaviour.Swipe.Down:
                    return "DOWN";
            }

            return null;
        }

        string Buttons()
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                return "RIGHT";
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                return "LEFT";
            }
            else if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                return "UP";
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                return "DOWN";
            }

            return null;
        }


        private IEnumerator SpawnArrow()
        {
            yield return new WaitForSeconds(1);

            int dir = Random.Range(0, 4);

            switch (dir)
            {
                case 0:
                    Instantiate(up, arrowParent);
                    break;
                case 1:
                    Instantiate(down, arrowParent);
                    break;
                case 2:
                    Instantiate(right, arrowParent);
                    break;
                case 3:
                    Instantiate(left, arrowParent);
                    break;
            }

            StartCoroutine(SpawnArrow());
        }

        private void OnTriggerEnter(Collider other)
        {
            string dir = other.gameObject.GetComponent<Arrows>().direction;

            directions.Enqueue(dir);
            gameObject.GetComponent<Image>().color = Color.yellow;
        }

        private void OnTriggerExit(Collider other)
        {
            if (statuses.Count == 0)
            {
                directions.Dequeue();
                Missed();
            }
            else {
                statuses.Dequeue();
            }
        }
    }
}
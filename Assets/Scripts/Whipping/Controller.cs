using System.Collections;
using System.Collections.Generic;
using Behaviour;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace Whipping
{
    public class Controller : MonoBehaviour
    {
        public int startingWhippingReward = 5;
        public int rewardMultiplier = 10;
        public float startingBarAmount = 0.3f;

        public GameObject endText;
        public GameObject up;
        public GameObject down;
        public GameObject right;
        public GameObject left;
        public Transform arrowParent;
        public Image bar;

        private int _missed;
        private int _boost;
        private bool _ended;

        private Queue<Swipe> directions = new Queue<Swipe>();
        private Queue<bool> statuses = new Queue<bool>();

        // Start is called before the first frame update
        void Start()
        {
            SwipeDetector.OnSwipe += OnSwipe;

            bar.fillAmount = startingBarAmount;

            StartCoroutine(SpawnArrow());
        }

        // Update is called once per frame
        void Update()
        {
            if (_ended)
                return;

            if (bar.fillAmount <= 0)
            {
                _ended = true;
                StartCoroutine(EndWhipping(false));
            }
            else if (bar.fillAmount >= 1)
            {
                _ended = true;
                StartCoroutine(EndWhipping(true));
            }
        }
        
        void OnDestroy(){
            SwipeDetector.OnSwipe -= OnSwipe;
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

        void OnSwipe(Swipe dir)
        {
            if (Swipe.None.Equals(dir) || directions.Count <= 0) return;

            var daco = directions.Dequeue();
            
            Debug.Log("Ocakavam: " + daco);
            Debug.Log("Mam: " + dir);
            bool status = dir.Equals(daco);

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

        private IEnumerator EndWhipping(bool win)
        {
            StopCoroutine(SpawnArrow());

            string text = "You lost :(";
            Color color = Color.red;

            if (win)
            {
                GameState state = GameState.GetInstance();
                var prize = startingWhippingReward + rewardMultiplier * state.GetWhippingNumber();

                if (_missed > 0 && _missed <= 3)
                {
                    prize = (prize * 4) / 5;
                }
                else if (_missed > 3)
                {
                    prize = (prize * 3) / 5;
                }

                state.WhippingWin(prize);

                text = "You won :) +" + prize + "p";
                color = Color.green;
            }

            endText.GetComponentInChildren<Text>().text = text;
            endText.GetComponent<Image>().color = color;
            endText.SetActive(true);

            yield return new WaitForSeconds(1);

            SceneManager.LoadScene("Scenes/BaseScene");
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
            var dir = other.gameObject.GetComponent<Arrows>().direction;

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
            else
            {
                statuses.Dequeue();
            }
        }
    }
}
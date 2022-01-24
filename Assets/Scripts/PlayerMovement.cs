using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

namespace ToDyToScAnO
{

    public class PlayerMovement : MonoBehaviour
    {
        public CharacterController2D controller;

        public float runSpeed = 40f;
        public TextMeshProUGUI countText;
        public TextMeshProUGUI TimerText;
        public AudioClip WinMusic;
        public AudioClip LoseMusic;
        public AudioClip coinSound;

        public GameObject LoseText;
        public GameObject WinText;
        public GameObject AnnouncementText;

        public float _timeleft;

        float horizontalMove = 0f;
        bool jump = false;

        public int count;
        private Rigidbody rb;
        private bool _canTimerStart = false;
        private UIManager _uiManager;
        public bool gameOver = false;

        private AudioSource _audioSource;

        // Start is called before the first frame update
        void Start()
        {
           

            _audioSource = GetComponent<AudioSource>();
            rb = GetComponent<Rigidbody>();
            count = 0;

            SetCountText();

            _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
            
        }

        // Update is called once per frame
        void Update()
        {
            StartCoroutine(Timer());
            if (gameOver == false && _canTimerStart == true)
            {
                _timeleft -= Time.deltaTime;
                _uiManager.updateTime(_timeleft);
            }
            horizontalMove = Input.GetAxisRaw("Horizontal") * runSpeed;

            if (Input.GetButtonDown("Jump"))
            {
                jump = true;
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Application.Quit();
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                if (gameOver == true)
                {
                    SceneManager.LoadScene("Main");
                }
            }




            if (_timeleft < 0)
            {
                gameOver = true;
                LoseText.SetActive(true);
                runSpeed = 0f;

                AudioSource.PlayClipAtPoint(LoseMusic, transform.position, 0.8f);
            }
            else
            {

            }

            if (_timeleft <= 0)
            {
                _timeleft = 0;
            }

            if (count >= 5)
            {
                gameOver = true;
                WinText.SetActive(true);
                runSpeed = 0f;

                AudioSource.PlayClipAtPoint(WinMusic, transform.position, 0.5f);
            }
        }

        public IEnumerator Timer()
        {
            AnnouncementText.SetActive(true);

            yield return new WaitForSeconds(2);
            _canTimerStart = true;
        }


        void FixedUpdate()
        {
            //Moves ours character
            controller.Move(horizontalMove * Time.fixedDeltaTime, false, jump);
            jump = false;
        }

        public void PlaySound(AudioClip clip)
        {
            _audioSource.PlayOneShot(clip);
        }




        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.CompareTag("Coin"))
            {
                collision.gameObject.SetActive(false);
                count = count + 1;

                SetCountText();
            }

            if (collision.gameObject.CompareTag("Coin"))
            {
                _audioSource.PlayOneShot(coinSound);
            }
        }


        void SetCountText()
        {
            countText.text = "Count: " + count.ToString();
        }
    }
}

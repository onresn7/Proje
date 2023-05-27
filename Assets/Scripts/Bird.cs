using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Bird : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private float _force;
    [SerializeField] private float _yBound;


    public ScoreManager scoreManager;


    public GameObject spawner;

    public bool dead;

    public AudioSource aSource;
    public AudioSource score;

    public bool isStarted;


    public GameObject start;
    private void Start()
    {
        scoreManager = GameObject.FindObjectOfType<ScoreManager>();
    }
    private void Update()
    {
        if (isStarted == false)
        {
            _rigidbody.constraints = RigidbodyConstraints2D.FreezePositionY;
        }
        if (Input.GetMouseButtonDown(0) && _rigidbody.position.y < _yBound && dead == false)
        {
            start.SetActive(false);
            isStarted = true;
            spawner.GetComponent<Spawner>().isStarted = true;
            _rigidbody.constraints = RigidbodyConstraints2D.None;
            _rigidbody.constraints = RigidbodyConstraints2D.FreezePositionX;
            _rigidbody.constraints = RigidbodyConstraints2D.FreezeRotation;
            Flap();
            aSource.Play();
        }
    }

    private void Flap()
    {
        _rigidbody.velocity = Vector2.zero;
        _rigidbody.AddForce(Vector2.up * _force);

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Game Over");

        dead = true;

        spawner.GetComponent<Spawner>().dead = true;
        int loadedNumber = PlayerPrefs.GetInt("highScore");
        if (scoreManager.score > loadedNumber)
        {
            PlayerPrefs.SetInt("highScore", scoreManager.score);
            scoreManager.bestScore.text = "Highscore: " +  scoreManager.score.ToString();
        }
        else
        {
            scoreManager.bestScore.text = "Highscore: " + loadedNumber.ToString();
        }
        
        scoreManager.gameEnded.SetActive(true);

        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Respawn")
        {
            Debug.Log("Trigger");
            scoreManager.score += 1;
            score.Play();
        }


        if (collision.tag == "Finish")
        {
            dead = true;

            spawner.GetComponent<Spawner>().dead = true;
            int loadedNumber = PlayerPrefs.GetInt("highScore");
            if (scoreManager.score > loadedNumber)
            {
                PlayerPrefs.SetInt("highScore", scoreManager.score);
                scoreManager.bestScore.text = "Highscore: " + scoreManager.score.ToString();
            }
            else
            {
                scoreManager.bestScore.text = "Highscore: " + loadedNumber.ToString();
            }

            scoreManager.gameEnded.SetActive(true);
            _rigidbody.constraints = RigidbodyConstraints2D.FreezeAll;
        }
    }
}

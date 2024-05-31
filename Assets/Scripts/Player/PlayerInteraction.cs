using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerInteraction : MonoBehaviour
{
    public float VelocityBuff = 1;
    public float ObstacleDebuff = 1;
    public Counter GameCounter;
    public TextMeshProUGUI DisplayedResult;
    public RectTransform Counter, ResultDisplay;
    public GameObject Missile;
    public Spawner Spawner;
    public RectTransform NerfText, BuffText;


    private PlayerController _playerController;
    private float _playerYVelocity;
    private string _result;
    private Animator _anim;

    private void Start()
    {
        _anim = GetComponent<Animator>();
        Time.timeScale = 1f;
        _playerController = GetComponent<PlayerController>();
        _playerYVelocity = _playerController.PlayerYVelocity;

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Obstacle"))
        {
            NerfText.GetComponent<Animator>().Play("Appering");
            _playerController.PlayerYVelocity += ObstacleDebuff;
            SoundManager.instance.PlaySfx("Explosion");
            collision.GetComponent<Animator>().Play("Explosion");
            StartCoroutine(SlowDuration());
        }

        if (collision.CompareTag("LimitBreaker"))
        {
            SoundManager.instance.PlaySfx("SpeedUp");
            BuffText.GetComponent<Animator>().Play("Appearing");
            ObstacleDebuff += 2;
            _playerController.PlayerYVelocity -= VelocityBuff;
            _playerYVelocity = _playerController.PlayerYVelocity;
        }

        if (collision.CompareTag("Ground"))
        {
            _anim.Play("Explosion");
            if(_anim.GetCurrentAnimatorStateInfo(0).IsTag("Explosion"))              
            {
                Time.timeScale = 0f;
                _result = string.Format("{0:00}:{1:00}", GameCounter.Seconds, GameCounter.Minutes);
                Counter.gameObject.SetActive(false);
                ResultDisplay.gameObject.SetActive(true);
                DisplayedResult.text = DisplayedResult.text + _result;

            }

        }

        if (collision.CompareTag("MovementChaquel"))
        {
            _playerController.CanMove = true;
        }

        if (collision.CompareTag("FinalLine"))
        {
            Spawner.TurnSpawningOff();
        }
    }

    IEnumerator SlowDuration()
    {
        yield return new WaitForSeconds(1f);
        _playerController.PlayerYVelocity = _playerYVelocity;
    }
}

using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class Fingering : MonoBehaviour
{
    public GameObject finger;
    private Rigidbody2D _rb;
    public float fingerSpeed = 5;
    public GameObject pig;
    public AudioSource audioSource;
    public TextMeshProUGUI scoreText;
    private int _scoreNumber;
    private void Start()
    {
        _rb = finger.GetComponent<Rigidbody2D>();
        //_scoreNumber = PlayerPrefs.GetInt("Score");
        scoreText.text = _scoreNumber.ToString();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _rb.velocity = Vector2.zero;
            _rb.AddForce(Vector2.right * fingerSpeed, ForceMode2D.Impulse);
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Svinka"))
        {
            _scoreNumber++;
            //PlayerPrefs.SetInt("Score", _scoreNumber);
            scoreText.text = _scoreNumber.ToString();
            audioSource.Play();
            StartCoroutine(PigSqueeze());
            StopCoroutine(PigSqueeze());
        }
    }

    private IEnumerator PigSqueeze()
    {
        pig.transform.DOScale(new Vector3(0.75f, 1.25f, 1), 0.07f);
        yield return new WaitForSeconds(0.1f);
        pig.transform.DOScale(new Vector3(1f, 1f, 1), 0.07f);
        yield return null;
    }
}

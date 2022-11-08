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
    public static int ScoreNumber;
    private readonly Vector3 _startSqueezeVector = new Vector3(0.75f, 1.25f, 1);
    private readonly Vector3 _endSqueezeVector = new Vector3(1f, 1f, 1);
    
    private void Awake()
    {
        _rb = finger.GetComponent<Rigidbody2D>();
        ScoreNumber = PlayerPrefs.GetInt("Score");
        scoreText.text = ScoreNumber.ToString();
    }

    private void Update()
    {
        if (MiniGameController.MiniGameIsActive) return;
        if (!Input.GetMouseButtonDown(0)) return;
        _rb.velocity = Vector2.zero;
        _rb.AddForce(Vector2.right * fingerSpeed, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Svinka"))
        {
            ScoreNumber++;
            PlayerPrefs.SetInt("Score", ScoreNumber);
            scoreText.text = ScoreNumber.ToString();
            audioSource.Play();
            StartCoroutine(PigSqueeze());
        }
    }

    private IEnumerator PigSqueeze()
    {
        pig.transform.DOScale(_startSqueezeVector, 0.07f);
        yield return new WaitForSeconds(0.1f);
        pig.transform.DOScale(_endSqueezeVector, 0.07f);
    }
}

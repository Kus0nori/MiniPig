using UnityEngine;

public class CircleController : MonoBehaviour
{
    private MiniGameController _controller;
    private void Awake()
    {
        _controller = GameObject.Find("MiniGameController").GetComponent<MiniGameController>();
    }

    private void OnMouseDown()
    {
        _controller.clickedCirclesCounter++;
        Destroy(gameObject);
    }
}

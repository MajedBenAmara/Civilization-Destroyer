using UnityEngine;

public class CloudMovement : MonoBehaviour
{
    public float CloudSpeed = 4f;
    public int direction = 1;
    public RectTransform TeleportPoint;
    public float PositiveYpos = 4000f, NegativeYpos = -200f;
    private RectTransform _recTransform;
    private Rigidbody2D _rb;

    

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _recTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {

        if (direction > 0)
        {
            if (transform.position.y > PositiveYpos)
                transform.position = new Vector2(TeleportPoint.position.x, TeleportPoint.position.y);
        }
        else
        {
            if (_recTransform.anchoredPosition.y < NegativeYpos)
                transform.position = new Vector2(TeleportPoint.position.x, TeleportPoint.position.y);
        }
    }
    private void FixedUpdate()
    {
        _rb.velocity = new Vector2(0f, direction * CloudSpeed);
    }
}

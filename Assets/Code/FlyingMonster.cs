using UnityEngine;

public class MovingMonster : MonoBehaviour
{
    public float speed = 2.0f;
    public float maxHeight = 0f;
    public float minHeight = -1.0f;
    private int direction = 1;

    public Sprite defaultSprite;
    public Sprite movingUpSprite;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        transform.Translate(0, direction * speed * Time.deltaTime, 0);

        if (transform.position.y > maxHeight || transform.position.y < minHeight)
        {
            direction *= -1;
        }

        if (direction > 0)
        {
            spriteRenderer.sprite = movingUpSprite;
        }
        else
        {
            spriteRenderer.sprite = defaultSprite;
        }
    }
}
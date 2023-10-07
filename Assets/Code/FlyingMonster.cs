using UnityEngine;

public class MovingMonster : MonoBehaviour
{
    public float speed = 2.0f;
    public float range = 1f;
    private int direction = 1;

    public float maxHeight;
    public float minHeight;

    public Sprite defaultSprite;
    public Sprite movingUpSprite;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        maxHeight = transform.position.y + range;
        minHeight = transform.position.y - range;
    }

    void Update()
    {
        transform.Translate(0, direction * speed * Time.deltaTime, 0);

        

        if (transform.position.y > maxHeight)
        {
            direction = -1;
            transform.position = new Vector3(transform.position.x, maxHeight, transform.position.z);
        }
        else if (transform.position.y < minHeight)
        {
            direction = 1;
            transform.position = new Vector3(transform.position.x, minHeight, transform.position.z);
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
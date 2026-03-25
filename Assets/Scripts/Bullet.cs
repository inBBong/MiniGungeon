using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float speed=10;
    public float damage = 1;
    Vector2 direction;
    public Vector2 Direction
    {
        set
        {
            direction = value.normalized;
        }
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag=="wall"|| collision.tag == "enemy")
        {
            gameObject.SetActive(false);
        }
    }
}

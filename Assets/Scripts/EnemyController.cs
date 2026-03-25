using UnityEngine;
using UnityEngine.Rendering;

public class EnemyController : MonoBehaviour
{
    enum State
    {
        Spawning,
        Moving,
        Dying
    }
    public float speed = 2;

    public Material flashMaterial;
    public Material defaultMaterial;


    GameObject target;
    State state;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        target = GameObject.Find("Player");
        state = State.Moving;
    }

    private void FixedUpdate()
    {
        if (state == State.Moving)
        {
            Vector2 direction = target.transform.position - transform.position;
            transform.Translate(direction.normalized * speed * Time.fixedDeltaTime);

            if (direction.x < 0)
            {
                GetComponent<SpriteRenderer>().flipX = true;
            }
            if (direction.x > 0)
            {
                GetComponent<SpriteRenderer>().flipX = false;
            }
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "bullet")
        {
            float d = collision.gameObject.GetComponent<Bullet>().damage;
            if (GetComponent<Character>().Hit(d))
            {
                //살아있을때
                Flash();
            }
            else
            {
                //죽었을때
                Die();
            }
        }
    }
    void Flash()
    {
        GetComponent<SpriteRenderer>().material = flashMaterial;
        Invoke("AfterFlash", 0.5f);
    }    
    void AfterFlash()
    {
        GetComponent<SpriteRenderer>().material = defaultMaterial;
    }
    void Die()
    {
        state = State.Dying;
        GetComponent<Animator>().SetTrigger("Die");
        Invoke("AfterDying", 1.4f);
    }
    void AfterDying()
    {
        Destroy(gameObject);
    }
}

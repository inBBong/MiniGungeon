using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float speed = 3;
    Vector3 move;

    public GameObject bulletPrefab;

    public Material flashMaterial;
    public Material defaultMaterial;

    public AudioClip shotSound;
    public AudioClip hitSound;
    public AudioClip deadSound;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        move = Vector3.zero;
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            move += new Vector3(-1, 0, 0);
        }
        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            move += new Vector3(1, 0, 0);
        }
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            move += new Vector3(0, 1, 0);
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            move += new Vector3(0, -1, 0);
        }
        move = move.normalized;
        if (move.x < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        if (move.x > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        if (move.magnitude > 0) //움직일때
        {
            GetComponent<Animator>().SetTrigger("Move");
        }
        else // 안움직일때
        {
            GetComponent<Animator>().SetTrigger("Stop");
        }
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
        void Shoot()
        {
            GetComponent<AudioSource>().PlayOneShot(shotSound);

            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            worldPosition.z = 0;
            worldPosition -= transform.position + new Vector3(0, -0.5f);

            GameObject newBullet = GetComponent<ObjectPool>().Get();
            if (newBullet != null)
            {
                newBullet.transform.position = transform.position + new Vector3(0, -0.5f);
                newBullet.GetComponent<Bullet>().Direction = worldPosition;
            }
        }

    }
    private void FixedUpdate()
    {
        transform.Translate(move * speed * Time.fixedDeltaTime);

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            if (GetComponent<Character>().Hit(1))
            {
                //살아있다.
                GetComponent<AudioSource>().PlayOneShot(hitSound);
                Flash();
            }
            else
            {
                //죽음
                GetComponent<AudioSource>().PlayOneShot(deadSound);
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
        
        GetComponent<Animator>().SetTrigger("Die");
        Invoke("AfterDying", 0.875f);
    }
    void AfterDying()
    {
        //gameObject.SetActive(false);
        SceneManager.LoadScene("GameOverScene");
    }
}

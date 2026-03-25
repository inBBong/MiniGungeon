using UnityEngine;
using UnityEngine.Pool;

public class PlayerController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float speed = 3;
    Vector3 move;

    public GameObject bulletPrefab;
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
}

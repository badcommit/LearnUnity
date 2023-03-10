using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private float speed;
    private bool hit;
    private float direction;
    private Animator anim;
    private BoxCollider2D boxCollider2D;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        anim = GetComponent<Animator>();
        boxCollider2D = GetComponent<BoxCollider2D>();

    }

    // Update is called once per frame
    void Update()
    {
        if (hit)
        {
            return;
        }
        float movementSpeed = speed * Time.deltaTime * direction ;
        transform.Translate(movementSpeed, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        print("collide");
        hit = true;
        boxCollider2D.enabled = false;
        anim.SetTrigger("explode");
    }

    public void SetDirection(float _direction)
    {
        
        direction = _direction;
        gameObject.SetActive(true);
        hit = false;
        boxCollider2D.enabled = true;

        float localScaleX = transform.localScale.x;
        if(Mathf.Sign(localScaleX) != _direction)
        {
            localScaleX = -localScaleX;
        }
        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}

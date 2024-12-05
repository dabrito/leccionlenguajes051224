using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
  public float playerJumpForce = 20f;
  public float playerSpeed = 3f;
  public Sprite[] walkSprites;
  public Sprite jumpSprite;
  private int walkIndex = 0;


  private Rigidbody2D myrigidbody2D;
  private SpriteRenderer mySpriteRenderer;

  void Start()
  {
    myrigidbody2D = GetComponent<Rigidbody2D>();
    mySpriteRenderer = GetComponent<SpriteRenderer>();
    StartCoroutine(WalkCoroutine());
  }

  void Update()
  {
    if (Input.GetKeyDown(KeyCode.Space))
    {
      myrigidbody2D.linearVelocity = new Vector2(myrigidbody2D.linearVelocity.x, playerJumpForce);
    }
    if (Input.GetKeyDown(KeyCode.RightArrow))
    {
      myrigidbody2D.linearVelocity = new Vector2(playerSpeed, myrigidbody2D.linearVelocity.y);
    }
    if (Input.GetKeyDown(KeyCode.LeftArrow))
    {
      myrigidbody2D.linearVelocity = new Vector2(-playerSpeed, myrigidbody2D.linearVelocity.y);
    }
  }

  IEnumerator WalkCoroutine()
  {
    while (true)
    {
      walkIndex = (walkIndex + 1) % walkSprites.Length;
      mySpriteRenderer.sprite = walkSprites[walkIndex];
      yield return new WaitForSeconds(0.1f);
    }
  }

  void OnTriggerEnter2D(Collider2D collision)
  {
    if (collision.CompareTag("Enemy"))
    {
      Destroy(collision.gameObject);
      PlayerDeath();
    }
    else if (collision.CompareTag("DeathZone"))
    {
      PlayerDeath();
    }
  }

  void PlayerDeath()
  {
    SceneManager.LoadScene("SampleScene");
  }

}
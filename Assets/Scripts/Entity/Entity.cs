using System;
using System.Collections;
using UnityEditor;
using UnityEngine;

public class Entity : MonoBehaviour
{
  public event Action OnFlipped;
  public Animator anim { get; private set; }
  public Rigidbody2D rb { get; private set; }
  protected StateMachine stateMachine;

  private bool facingRight = true;
  public int facingDir { get; private set; } = 1;

  [Header("Collider detection")]
  public LayerMask whatIsGround;
  [SerializeField] private float groundCheckDistance;
  [SerializeField] private float wallCheckDistance;
  [SerializeField] private Transform groundCheck;
  [SerializeField] private Transform primaryWallCheck;
  [SerializeField] private Transform secondaryWallCheck;
  public bool groundDetected { get; private set; }
  public bool wallDetected { get; private set; }


  private Coroutine slowDownCo;
  private Coroutine knockBackCo;
  private bool isKnocked;

  protected virtual void Awake()
  {
    anim = GetComponentInChildren<Animator>();
    rb = GetComponent<Rigidbody2D>();

    stateMachine = new StateMachine();
  }

  protected virtual void Start()
  {

  }

  protected virtual void Update()
  {
    HandleCollisionDetection();
    stateMachine.currentState.Update();
  }

  public virtual void SlowDownEntity(float duration, float slowMultipier, bool canOverrideSlowEffect = false)
  {
    if (slowDownCo != null)
    {
      if (canOverrideSlowEffect)
        StopCoroutine(slowDownCo);
      else
        return;
    }

    slowDownCo = StartCoroutine(SlowDownEntityCo(duration, slowMultipier));
  }

  protected virtual IEnumerator SlowDownEntityCo(float duration, float slowMultiptier)
  {
    yield return null;
  }

  public virtual void StopSlowDown()
  {
    slowDownCo = null;
  }

  public virtual void EntityDeath()
  {

  }

  public void ReciveKnockback(Vector2 knockback, float duration)
  {
    if (knockBackCo != null)
      StopCoroutine(knockBackCo);

    knockBackCo = StartCoroutine(KnockbackCo(knockback, duration));
  }

  private IEnumerator KnockbackCo(Vector2 knockback, float duration)
  {
    isKnocked = true;
    rb.linearVelocity = knockback;

    yield return new WaitForSeconds(duration);

    rb.linearVelocity = Vector2.zero;
    isKnocked = false;
  }

  public void CurrentStateAnimationTrigger()
  {
    stateMachine.currentState.AnimationTrigger();
  }

  public void UpdateActiveState()
  {
    stateMachine.UpdateActiveState();
  }

  public void SetVelocity(float xVelocity, float yVelocity)
  {
    if (isKnocked) return;

    rb.linearVelocity = new Vector2(xVelocity, yVelocity);
    HandleFlip(xVelocity);
  }

  public void HandleFlip(float xVelocity)
  {
    if (xVelocity > 0 && !facingRight)
      Flip();
    else if (xVelocity < 0 && facingRight)
      Flip();
  }

  public void Flip()
  {
    transform.Rotate(0, 180, 0);
    facingRight = !facingRight;
    facingDir *= -1;

    OnFlipped?.Invoke();
  }

  private void HandleCollisionDetection()
  {
    groundDetected = Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);

    if (secondaryWallCheck != null)
    {
      wallDetected = (
          Physics2D.Raycast(primaryWallCheck.position, Vector2.right * facingDir, wallCheckDistance, whatIsGround)
          && Physics2D.Raycast(secondaryWallCheck.position, Vector2.right * facingDir, wallCheckDistance, whatIsGround)
      );
    }
    else
      wallDetected = Physics2D.Raycast(primaryWallCheck.position, Vector2.right * facingDir, wallCheckDistance, whatIsGround);
  }

  protected virtual void OnDrawGizmos()
  {
    Gizmos.color = Color.red;
    Gizmos.DrawLine(groundCheck.position, groundCheck.position + new Vector3(0, -groundCheckDistance));
    Gizmos.DrawLine(primaryWallCheck.position, primaryWallCheck.position + new Vector3(wallCheckDistance * facingDir, 0));

    if (secondaryWallCheck != null)
      Gizmos.DrawLine(secondaryWallCheck.position, secondaryWallCheck.position + new Vector3(wallCheckDistance * facingDir, 0));
  }
}

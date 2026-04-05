using Unity.VisualScripting;
using UnityEngine;

public class SkillObject_DomainExpansion : SkillObject_Base
{
    private Skill_DomainExpansion domainManager;

    private float expandSpeed = 2;
    private float duration;
    private float slowDownPercent = 0.9f;

    private Vector3 targetScale;
    private bool isShrinking;

    public void SetupDomain(Skill_DomainExpansion domainManager)
    {
      this.domainManager = domainManager;

      duration = domainManager.GetDomainDuration();
      float maxSize = domainManager.maxDomainSize;
      slowDownPercent = domainManager.GetSlowPercentage();
      expandSpeed = domainManager.expandSpeed;

      targetScale = Vector3.one * maxSize;
      Invoke(nameof(ShinkDomain), duration);
    }

    private void Update()
    {
      HandleScaling();
    }

    private void HandleScaling()
    {
      float sizeDifference = Mathf.Abs(transform.localScale.x - targetScale.x);
      bool shoundChangeScale = sizeDifference > 0.1f;

      if (shoundChangeScale)
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, expandSpeed * Time.deltaTime);

      if (isShrinking && sizeDifference < .1f)
        Destroy(gameObject);
    }

    private void ShinkDomain()
    {
      targetScale = Vector3.zero;
      isShrinking = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
      Enemy enemy = collision.GetComponent<Enemy>();

      if (!enemy) return;

      enemy.SlowDownEntity(duration, slowDownPercent, true);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
      Enemy enemy = collision.GetComponent<Enemy>();

      if (!enemy) return;

      enemy.StopSlowDown();
    }
}

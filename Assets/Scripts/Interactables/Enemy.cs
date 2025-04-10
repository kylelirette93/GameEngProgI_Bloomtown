using System.Collections;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public ProgressTracker progressTracker;
    protected int health;
    ParticleSystem bloodParticles;
    public QuestManager questManager;

    private void Start()
    {
        progressTracker = GameManager.Instance.progressTracker;
        questManager = GameManager.Instance.questManager;
        bloodParticles = GetComponentInChildren<ParticleSystem>();
        bloodParticles.transform.position = transform.position;
    }
    public virtual void TakeDamage(int dmg)
    {
        health -= dmg;
        if (health < 0)
        {
            Die();
        }
    }

    IEnumerator DoShrinkyThing(float time, Vector3 targetScale)
    {
        bloodParticles.Play();
        Vector3 originalScale = transform.localScale;
        float elapsedTime = 0f;

        while (elapsedTime < time)
        {
            transform.localScale = Vector3.Lerp(originalScale, targetScale, elapsedTime / time);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.localScale = targetScale;
        Destroy(gameObject);
    }
    public virtual void Die()
    {
        StartCoroutine(DoShrinkyThing(1f, new Vector3(0.1f, 0.1f, 0.1f)));    
    }
}

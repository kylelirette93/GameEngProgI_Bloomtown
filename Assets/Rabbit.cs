using UnityEngine;

public class Rabbit : Enemy
{
    public override void Die()
    {
        progressTracker.rabbitsHunted++;
        questManager.CheckQuestStatus();
        base.Die();
    }
}

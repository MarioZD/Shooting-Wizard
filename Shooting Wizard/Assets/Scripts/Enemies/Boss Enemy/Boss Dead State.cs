using UnityEngine;

public class BossDeadState : BossBaseState
{
    BossStateManager currentEnemy;
    public override void EnterState(BossStateManager enemy)
    {
        currentEnemy = enemy;
        Drop(enemy.drops);
        UnityEngine.GameObject.Destroy(enemy.gameObject);
    }
    public override void UpdateState(BossStateManager enemy)
    {

    }
    public override void OnCollisionStay2D(BossStateManager enemy, Collision2D collision)
    {

    }

    void Drop(GameObject[] drops)
    {
        if (drops != null)
        {
                UnityEngine.GameObject.Instantiate(drops[Random.Range(0, drops.Length - 1)], currentEnemy.transform.position, Quaternion.identity);
            

        }
    }
}
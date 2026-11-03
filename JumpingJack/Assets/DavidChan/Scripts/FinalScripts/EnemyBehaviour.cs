using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    public virtual void RecycleToEnemyPool()
    {
        EnemyPool.Instance.RecycleEnemy(this);
    }
}

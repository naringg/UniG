using UnityEngine;
using UnityEngine.Events;

public class ExplosionRange : MonoBehaviour
{
    public UnityEvent explosion;

    bool this_only_runs_once = false;

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.CompareTag("Player"))
        {
            if (!this_only_runs_once)
            {
                this_only_runs_once = true;
                explosion.Invoke();
            }
        }
    }
}

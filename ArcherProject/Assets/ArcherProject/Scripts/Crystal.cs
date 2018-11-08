using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Crystal : MonoBehaviour, IDamagable, IKillable
{

    public Slider HealthBar;

    [SerializeField]
    public bool isDestroyed;

    [SerializeField]
    private float health;

	// Use this for initialization
	void Update ()
    {
        HealthBar.value = health;
    }

    public void Kill()
    {
        foreach(Transform t in transform)
        {
            if (t.GetComponent<Rigidbody>() != null)
            {
                t.parent = null;
                t.GetComponent<Renderer>().enabled = true;
                Vector3 randomDir = new Vector3(Random.Range(-5, 5), 5, Random.Range(-5, 5));
                t.GetComponent<Rigidbody>().isKinematic = false;
                t.GetComponent<Rigidbody>().AddForce(randomDir, ForceMode.Impulse);
            }
        }

        Destroy(gameObject);
    }

    public void TakeDamage(float damage)
    {
        if (!isDestroyed)
        {
            health -= damage;
            if (health <= 0.0f)
            {
                isDestroyed = true;
                Kill();
            }
        }
    }
}

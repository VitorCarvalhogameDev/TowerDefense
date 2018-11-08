using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour , IKillable
{
    public NavMeshAgent navAgent;

    public Animator anim;

    public Transform activeTarget;

    [SerializeField]
    private bool isDead;

    [SerializeField]
    private float health;

    [SerializeField]
    private bool canAttack;
    
    [SerializeField]
    private float timeBetweenAttacks;

    // Use this for initialization
    void Awake () {
        navAgent = GetComponent<NavMeshAgent>();
        anim = GetComponent<Animator>();
    }

    // Use this for initialization
    void Start()
    {

        activeTarget = GameManager.instance.GetRandomCrystalPosition();
        if (activeTarget != null)
            MoveEnemy(activeTarget.position);

    }

    // Update is called once per frame
    void Update ()
    {
        if (activeTarget != null && !isDead)
        {
            float distanceToActiveTarget = Vector3.Distance(transform.position, activeTarget.position);
            
            if (distanceToActiveTarget <= 1.0f)
            {
                Vector3 lookDirection = activeTarget.position - transform.position;
                lookDirection.Normalize();
                transform.rotation = Quaternion.LookRotation(lookDirection, Vector3.up);

                if (!canAttack)
                {
                    canAttack = true;
                    anim.SetBool("Moving", false);
                    StartCoroutine(TimedAttack());
                }
            }
            else
            {
                if(!canAttack)
                {
                    anim.SetBool("Moving", true);
                }
            }
        }
	}

    public IEnumerator TimedAttack()
    {
        Debug.Log("This enemy can now attack!");
        yield return new WaitForSeconds(1);

        if(isDead)
        {
            yield break;
        }

        // get the crystal layer
        int layerMask = 1 << 9;
        
        // raycast from center -> forward
        // just want to check the crystal layer
        Debug.DrawRay(transform.position, transform.forward, Color.green, 10);
        
        RaycastHit[] hits = Physics.RaycastAll(transform.position, transform.forward, 1f, layerMask);
        foreach(RaycastHit hit in hits)
        {
            // check for collisions
            IDamagable DamageableInterface = hit.collider.GetComponent<IDamagable>();
            if (DamageableInterface != null)
            {
                anim.SetTrigger("Attack");
                yield return new WaitForSeconds(0.8f);
                Debug.Log("Hit enemy");
                DamageableInterface.TakeDamage(20f);
            }
        }

        yield return new WaitForSeconds(timeBetweenAttacks);

        if(activeTarget == null)
        {
            activeTarget = GameManager.instance.GetRandomCrystalPosition();
            if (activeTarget != null)
            {
                canAttack = false;
                MoveEnemy(activeTarget.position);
            }
        }
        
        if (!isDead)
        {
            StartCoroutine(TimedAttack());
        }

    }

    public void MoveEnemy(Vector3 movePt)
    {
        navAgent.SetDestination(movePt);
    }

    public void Kill()
    {
        isDead = true;
        anim.enabled = false;
        navAgent.isStopped = true;
        
        foreach (Transform t in transform)
        {
            if (t.GetComponent<Rigidbody>() != null)
            {
                Vector3 randomDir = new Vector3(Random.Range(-5, 5), 5, Random.Range(-5, 5));
                t.GetComponent<Rigidbody>().isKinematic = false;
                t.GetComponent<Rigidbody>().AddForce(randomDir, ForceMode.Impulse);
            }
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        if(health <= 0)
        {
            Kill();
        }
    }
}

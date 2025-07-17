using UnityEngine;

public class Tower : MonoBehaviour
{
    public Scaner_Tower scaner_Tower;
    public Transform firePoint;
    public GameObject center;
    Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void FireTrigger()
    {
        animator.SetTrigger("Fire");
    } 

}

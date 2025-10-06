using UnityEngine;

public class AmmoControler : MonoBehaviour
{
    public GameObject targetToMoveTowards;
    public float bulletDmg; // how much dmg the bullet has, inherited from player controler, and will be accesed by the enemy when it colides
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetToMoveTowards.transform.position, 10 * Time.deltaTime);
    }
}

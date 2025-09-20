using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetZoneControler : MonoBehaviour
{
    public List<GameObject> targets = new List<GameObject>();
    [SerializeField] private GameObject pl;
    [SerializeField] private GameObject ammo;

    private GameObject cloneStorage; // temporary storage for the latest ammo copy     turn into object pooling
    void Start()
    {
       // StartCoroutine(DoISHoot());
    }

    private void OnEnable()
    {
        StartCoroutine(DoISHoot());

    }

    public void OnTriggerEnter2D(Collider2D collision) // if enemy trigers the zones
    {
        if (collision.gameObject.name == "Enemy")
        {
            targets.Add(collision.gameObject);
        }

    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Enemy")
        {
            bool removed = targets.Remove(collision.gameObject);
        }
    }

    
    private void openFire(GameObject enem)
    {
        cloneStorage = Instantiate(ammo, pl.transform.position, Quaternion.identity);
        cloneStorage.name = "Ammo";
        cloneStorage.GetComponent<AmmoControler>().targetToMoveTowards = enem;
    }

    /// <summary>
    /// Courutine that checks if anybody is in the hitlist (targets)
    /// <returns></returns>
    private IEnumerator DoISHoot()
    {
        if (targets.Count>0)
        {
            print(targets);
            openFire(targets[0].gameObject);
        }


        yield return new WaitForSeconds(0.5f);
        repeatDoIShoot();
    }
    /// <summary>
    /// forms a loop with DoISHoot so that it always checks of there is anybody in the list
    /// </summary>
    private void repeatDoIShoot()
    {
        StartCoroutine(DoISHoot());
    }

}

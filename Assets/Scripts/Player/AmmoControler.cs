using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class AmmoControler : MonoBehaviour
{
    public GameObject targetToMoveTowards;
    public float bulletDmg; // how much dmg the bullet has, inherited from player controler, and will be accesed by the enemy when it colides
    public GameObject ZoneHost; // the triger zone that summoned the bullet
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetToMoveTowards.transform.position, 10 * Time.deltaTime);

    }

    private IEnumerator BulletLife()
    {
        yield return new WaitForSeconds(7);
        bulletGetsOld();
    }

    public void bulletGetsOld()
    {
      ZoneHost.GetComponent<TargetZoneControler>().ListOldBullet(gameObject);
      gameObject.SetActive(false);
    }

}

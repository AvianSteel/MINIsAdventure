using UnityEngine;

public class AbilityCooldownUI : MonoBehaviour
{
    public PlayerControler playerControler;
    public GameObject laserRect;
    public GameObject mineRect;
    public GameObject dashRect;

    public bool canLaserLocal;
    public bool canMineLocal;
    public bool canDashLocal;

    private int laserLvlLocal;
    private int mineLvlLocal;
    private int dashLvlLocal;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canLaserLocal = playerControler.canLaser;
        canMineLocal = playerControler.canMine;
        canDashLocal = playerControler.canDash;
    }

    // Update is called once per frame
    void Update()
    {
        canLaserLocal = playerControler.canLaser;
        canMineLocal = playerControler.canMine;
        canDashLocal = playerControler.canDash;

        laserRect.SetActive(!canLaserLocal);
        mineRect.SetActive(!canMineLocal);
        dashRect.SetActive(!canDashLocal);
    }
}

using UnityEngine;

public class AbilityCooldownUI : MonoBehaviour
{
    public PlayerControler playerControler;
    public UnityEngine.UI.Slider laserSlider;
    public UnityEngine.UI.Slider mineSlider;
    public UnityEngine.UI.Slider dashSlider;


    public float laserCooldownLocal;
    public float mineCooldownLocal;
    public float dashCooldownLocal;


    //private int laserLvlLocal;
    //private int mineLvlLocal;
    //private int dashLvlLocal;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        laserCooldownLocal = playerControler.timeRemainLaser;
        mineCooldownLocal = playerControler.timeRemainMine;
        dashCooldownLocal = playerControler.timeRemainDash;
    }

    // Update is called once per frame
    void Update()
    {
        laserCooldownLocal = playerControler.timeRemainLaser;
        mineCooldownLocal = playerControler.timeRemainMine;
        dashCooldownLocal = playerControler.timeRemainDash;

        laserSlider.value = (laserCooldownLocal / 10f) * 100;
        mineSlider.value = (mineCooldownLocal / 5f) * 100;
        dashSlider.value = (dashCooldownLocal / 3f) * 100;
    }
}

using TMPro;
using UnityEngine;

public class StatUI : MonoBehaviour
{
    private PlayerControler playerControler;

    [SerializeField] private TMP_Text laserLvlText;
    [SerializeField] private TMP_Text mineLvlText;
    [SerializeField] private TMP_Text dashLvlText;
    [SerializeField] private TMP_Text speedLvlText;
    [SerializeField] private TMP_Text damageLvlText;
    [SerializeField] private TMP_Text defenseLvlText;
    [SerializeField] private TMP_Text fireRateLvlText;
    [SerializeField] private TMP_Text cooldownLvlText;

    private void Update()
    {
        laserLvlText.text = playerControler.laserLvl.ToString();
        mineLvlText.text = playerControler.mineLvl.ToString();
        dashLvlText.text = playerControler.dashLvl.ToString();
        //speedLvlText.text = playerControler.speedLvl.ToString();
        //damageLvlText.text = playerControler.damageLvl.ToString();
        //defenseLvlText.text = playerControler.defenseLvl.ToString();
        //fireRateLvlText.text = playerControler.fireRateLvl.ToString();
        //cooldownLvlText.text = playerControler.cooldownLvl.ToString();

    }
}

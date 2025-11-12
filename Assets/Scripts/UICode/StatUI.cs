using TMPro;
using UnityEngine;

public class StatUI : MonoBehaviour
{
    public PlayerControler playerControler;

    [SerializeField] private TMP_Text laserLvlText;
    [SerializeField] private TMP_Text mineLvlText;
    [SerializeField] private TMP_Text dashLvlText;
    [SerializeField] private TMP_Text speedLvlText;
    [SerializeField] private TMP_Text defenseLvlText;
    [SerializeField] private TMP_Text fireRateLvlText;
    [SerializeField] private TMP_Text cooldownLvlText;

    private void Update()
    {
        laserLvlText.text = playerControler.laserLvl.ToString();
        mineLvlText.text = playerControler.mineLvl.ToString();
        dashLvlText.text = playerControler.dashLvl.ToString();

        float tempSpeed = 150 - playerControler.PlSpeed;
        speedLvlText.text = "+ " + tempSpeed.ToString();
        defenseLvlText.text = "+ " + playerControler.Pldefense.ToString();
        fireRateLvlText.text = "+ " + playerControler.atackTime.ToString();
        cooldownLvlText.text = "- " + playerControler.abilityTime.ToString() + "sec.";

    }
}

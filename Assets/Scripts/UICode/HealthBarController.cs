using UnityEngine;
using UnityEngine.Rendering;

public class HealthBarController : MonoBehaviour
{
    public GameObject healthBar;
    public RectTransform hBar;
    private int healthBarWidth;
    private int healthBarPercent;
    public int health;
    public PlayerControler playerControler;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = playerControler.hp;
        healthBarPercent = health * 4;
        healthBarWidth = 525;
    }

    // Update is called once per frame
    void Update()
    {
        health = playerControler.hp;
        healthBarPercent = health * 4;
        healthBarWidth = (int)(5.25 * healthBarPercent);
        hBar.sizeDelta = new Vector2(healthBarWidth, 56);
    }
}

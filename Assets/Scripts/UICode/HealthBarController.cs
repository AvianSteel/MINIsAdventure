using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class HealthBarController : MonoBehaviour
{
    public UnityEngine.UI.Slider healthBarSlider;
    public int health;
    public PlayerControler playerControler;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        health = playerControler.hp;
        healthBarSlider.value = health;
    }

    // Update is called once per frame
    void Update()
    {
        health = playerControler.hp;
        healthBarSlider.value = health;
    }
}

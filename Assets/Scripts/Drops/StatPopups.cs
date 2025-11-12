using TMPro;
using UnityEngine;

public class StatPopups : MonoBehaviour
{    
    public TextMeshPro statTxt;
    public SpriteRenderer statPopUp;
    private Color statPopUpColor;

    private float dissapearTimer;
    private Color textColor;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        dissapearTimer = 0.5f;
        statPopUpColor = statPopUp.color;
        statPopUpColor.a = 1f;
        textColor.a = 1f;
    }

    private void Update()
    {
        float moveYSpeed = 2f;
        transform.position += new Vector3(0, moveYSpeed) * Time.deltaTime;

        dissapearTimer -= Time.deltaTime;
        if (dissapearTimer < 0)
        {

            float dissapearSpeed = 3f;
            statPopUpColor.a -= dissapearSpeed * Time.deltaTime;
            textColor.a -= dissapearSpeed * Time.deltaTime;
            statPopUp.color = statPopUpColor;
            statTxt.color = textColor;
            if (textColor.a < 0)
            {
                Destroy(gameObject);
                //TO CRISTIAN:
                //Pretty sure this is where object pooling stuff goes. Don't edit any functions or variables other than the above line
                //If you need to edit stuff, either ask me first, or fix it urself
                // Thanks, Bryson
            }
        }
    }
}

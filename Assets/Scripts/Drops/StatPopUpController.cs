using TMPro;
using UnityEngine;

public class StatPopUpController : MonoBehaviour
{
    private float dissapearTimer;
    public SpriteRenderer statPopup;
    private Color imgAlpha;
    private Color textColor;
    public TextMeshPro statText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        imgAlpha = statPopup.color;
        imgAlpha.a = 1f;
        dissapearTimer = 0.5f;
        textColor = Color.white;
        textColor.a = 1;
    }

    // Update is called once per frame
    void Update()
    {
        float moveYSpeed = 2f;
        transform.position += new Vector3(0, moveYSpeed) * Time.deltaTime;

        dissapearTimer -= Time.deltaTime;
        if (dissapearTimer < 0)
        {

            float dissapearSpeed = 3f;
            imgAlpha.a -= dissapearSpeed * Time.deltaTime;
            statPopup.color = imgAlpha;
            textColor.a -= dissapearSpeed * Time.deltaTime;
            statText.color = textColor;
            if (imgAlpha.a < 0f) 
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

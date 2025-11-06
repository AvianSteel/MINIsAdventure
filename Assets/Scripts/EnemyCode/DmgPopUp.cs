using TMPro;
using UnityEngine;

public class DmgPopUp : MonoBehaviour
{
    
    public DmgPopUp Create(Vector3 position, int dmg)
    {
        Transform dmgPopupTransform = Instantiate(PrefabHolder.I.dmgTxt, position, 
            Quaternion.AngleAxis(0, new Vector3(0, 0, 1)));
        DmgPopUp damage = dmgPopupTransform.GetComponent<DmgPopUp>();
        damage.Setup(dmg);

        return damage;
    }
    private TextMeshPro textMesh;

    private float dissapearTimer;
    private Color textColor;

    private void Awake()
    {
        textMesh = transform.GetComponent<TextMeshPro>();
    }
    public void Setup(int dmgAmount)
    {
        textMesh.text = dmgAmount.ToString();
        dissapearTimer = 0.5f;
        textColor = Color.white;
        textColor.a = 1;
    }
    private void Update()
    {
        float moveYSpeed = 2f;
        transform.position += new Vector3(0, moveYSpeed) * Time.deltaTime;

        dissapearTimer -= Time.deltaTime;
        if(dissapearTimer < 0)
        {
            
            float dissapearSpeed = 3f;
            textColor.a -= dissapearSpeed * Time.deltaTime;
            textMesh.color = textColor;
            if(textColor.a < 0)
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

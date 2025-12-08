using TMPro;
using UnityEngine;

public class DmgPopUp : MonoBehaviour
{
    public Transform dmgPopupTransform;
    public GameObject enemySpawnControler; // that is where the storage for pooling objects is hosted
    public DmgPopUp Create(Vector3 position, int dmg)
    {
         dmgPopupTransform = Instantiate(PrefabHolder.I.dmgTxt, position, 
            Quaternion.AngleAxis(0, new Vector3(0, 0, 1)));
        DmgPopUp damage = dmgPopupTransform.GetComponent<DmgPopUp>();
        damage.Setup(dmg,null);

        return damage;
    }
    private TextMeshPro textMesh;

    private float dissapearTimer;
    private Color textColor;

    private void Awake()
    {
        textMesh = transform.GetComponent<TextMeshPro>();
    }
    public void Setup(int dmgAmount, Transform origPos)
    {

        textColor.a = 1;

        if (origPos != null)
        {
            transform.position = origPos.position;
        }

        textMesh.text = dmgAmount.ToString();
        dissapearTimer = 0.5f;
        if (dmgAmount > 100)
        {
            textMesh.color = Color.yellow;
        } else if (dmgAmount >= 50)
        {
            textMesh.color = Color.red;
        } else if (dmgAmount >= 25)
        {
            textMesh.color = Color.orange;
        }else
        {
            textColor = Color.white;
        }

        //  transform.position = new Vector3(0, 0, 0);

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

                if (enemySpawnControler != null)
                {

                }
                enemySpawnControler.GetComponent<EnemySpawnControler>().dmgList.Add(gameObject);
                gameObject.SetActive(false);
                // gameObject.SetActive(false);
                //TO CRISTIAN:
                //Pretty sure this is where object pooling stuff goes. Don't edit any functions or variables other than the above line
                //If you need to edit stuff, either ask me first, or fix it urself
                // Thanks, Bryson
                // From CRISTIAN:
                //                  NO ;)
                // the code had to be entire eddited to correctly object pool it, a bug is there that needs to be adresed where object pooled 
                // numbers appear with a delay
            }
        }
    }
}

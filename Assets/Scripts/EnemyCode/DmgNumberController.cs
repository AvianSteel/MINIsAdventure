using TMPro;
using UnityEngine;

public class DmgNumberController : MonoBehaviour
{
    [SerializeField] private Transform dmgTxt;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void DamageText()
    {
        Transform dmgPopupTransform = Instantiate(dmgTxt, Vector3.zero, Quaternion.AngleAxis(0, new Vector3(0,0,1)));
        DmgPopUp damage = dmgPopupTransform.GetComponent<DmgPopUp>();
        damage.Setup(2, null);
    }
}

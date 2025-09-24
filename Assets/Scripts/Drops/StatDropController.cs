using UnityEngine;

public class StatDropController : MonoBehaviour
{
    [SerializeField] GameObject drop;
    public void DropStat(Vector3 transform)
    {
        Instantiate(drop, transform, Quaternion.identity);
    }
}

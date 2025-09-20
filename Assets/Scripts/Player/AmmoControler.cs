using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class AmmoControler : MonoBehaviour
{
    public GameObject targetToMoveTowards;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetToMoveTowards.transform.position, 30 * Time.deltaTime);
    }
}

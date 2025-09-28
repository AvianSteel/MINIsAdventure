using UnityEngine;

public class AmmoControler : MonoBehaviour
{
    public GameObject targetToMoveTowards;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetToMoveTowards.transform.position, 10 * Time.deltaTime);
    }
}

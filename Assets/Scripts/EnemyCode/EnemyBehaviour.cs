using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    [SerializeField] private GameObject target;
    [SerializeField] private GameObject drop;
    private StatDropController dropController;
    public float speed;

    private float dropRoll;
    void Start()
    {
        target = GameObject.FindWithTag("Player");
        dropController = GameObject.Find("DropController").GetComponent<StatDropController>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);

    }

    private void OnDisable()
    {
        dropRoll = Random.Range(0f, 10f);
        if (dropRoll >= 9f)
        {
            dropController.DropStat(transform.position);
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Ammo")
        {
            collision.gameObject.SetActive(false);
            gameObject.SetActive(false);
        }
    }

}

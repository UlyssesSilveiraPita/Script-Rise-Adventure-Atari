//using Unity.VisualScripting;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [Header("Scripts")]
    public GameManager gameManager {get; set;}
    //[SerializeField] private GameManager gameManager;

    [Header("Components")]
    [SerializeField] private Rigidbody2D player_rb;
    private GameObject carriedItem;



    [Header("Variaveis")]
    [SerializeField] private float playerSpeed;
    [SerializeField] private bool isCarryingItem = false;
    private float horizontal;
    private float vertical;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameManager = FindAnyObjectByType<GameManager>();


    }

    // Update is called once per frame
    void Update()
    {
        InputManagger();
        
    }

    void InputManagger()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        player_rb.linearVelocity = new Vector2(horizontal, vertical) * playerSpeed;

        if (Input.GetButtonDown("Fire1") && gameManager.isHaveKey == true)
        {
            gameManager.isHaveKey = false;
            gameManager.ketys[0].gameObject.transform.position = new Vector2(transform.localPosition.x - 1f, transform.localPosition.y);
            carriedItem.transform.parent = null; // solta o item
            carriedItem = null;
            isCarryingItem = false;
        }
        else if (Input.GetButtonDown("Fire1") && gameManager.isHaveKey2 == true)
        {
            gameManager.isHaveKey2 = false;
            gameManager.ketys[1].gameObject.transform.position = new Vector2(transform.localPosition.x - 1f, transform.localPosition.y);
            carriedItem.transform.parent = null; // solta o item
            carriedItem = null;
            isCarryingItem = false;
        }
        else if (Input.GetButtonDown("Fire1") && gameManager.isHaveArrow == true)
        {
            gameManager.isHaveArrow = false;
            gameManager.arrow.gameObject.transform.position = new Vector2(transform.localPosition.x - 1f, transform.localPosition.y);
            carriedItem.transform.parent = null; // solta o item
            carriedItem = null;
            isCarryingItem = false;
        }
        else if (Input.GetButtonDown("Fire1") && gameManager.isHaveTesour == true)
        {
            gameManager.isHaveTesour = false;
            gameManager.tesour.gameObject.transform.position = new Vector2(transform.localPosition.x - 1f, transform.localPosition.y);
            carriedItem.transform.parent = null; // solta o item
            carriedItem = null;
            isCarryingItem = false;
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (isCarryingItem) return; // já tem item → não pega outro

        switch (col.gameObject.tag)
        {
            case ("Key"):
                gameManager.isHaveKey = true;
                carriedItem = col.gameObject;
                carriedItem.transform.parent = transform; // item segue player
                gameManager.ketys[0].gameObject.transform.position = new Vector2(transform.position.x, transform.position.y);
                isCarryingItem = true;
                break;

            case ("Key2"):
                gameManager.isHaveKey2 = true;
                carriedItem = col.gameObject;
                carriedItem.transform.parent = transform; // item segue player
                gameManager.ketys[1].gameObject.transform.position = new Vector2(transform.position.x, transform.position.y);
                isCarryingItem = true;
                break;

            case ("Arrow"):
                gameManager.isHaveArrow = true;
                carriedItem = col.gameObject;
                carriedItem.transform.parent = transform; // item segue player
                //gameManager.arrow.gameObject.transform.position = new Vector2(gameManager.arrowPosition.position.x, gameManager.arrowPosition.transform.position.y);
                isCarryingItem = true;
                break;

            case ("Tesour"):
                gameManager.isHaveTesour = true;
                carriedItem = col.gameObject;
                carriedItem.transform.parent = transform; // item segue player
                gameManager.tesour.gameObject.transform.position = new Vector2(transform.position.x, transform.position.y);
                isCarryingItem = true;
                break;

        }

    }
    private void OnTriggerEnter2D(Collider2D col)
    {

        Teleports teleport = col.GetComponent<Teleports>();

        if (teleport != null)
        {
            int index = teleport.targetIndexTeleports;

            if (index >= 0 && index < gameManager.respawPointsTeleports.Length)
            {
                transform.position = gameManager.respawPointsTeleports[index].position;
            }
            
        }


        /*switch (col.gameObject.tag)
        {
            case ("TeleA"):
                gameObject.transform.position = gameManager.respawPointsTeleports[1].transform.position;
                break;

            case ("TeleA (1)"):
                gameObject.transform.position = gameManager.respawPointsTeleports[0].transform.position;
                break;

            case ("TeleB"):
                gameObject.transform.position = gameManager.respawPointsTeleports[3].transform.position;
                break;

            case ("TeleB (1)"):
                gameObject.transform.position = gameManager.respawPointsTeleports[2].transform.position;
                break;

            case ("TeleC"):
                gameObject.transform.position = gameManager.respawPointsTeleports[5].transform.position;
                break;

            case ("TeleC (1)"):
                gameObject.transform.position = gameManager.respawPointsTeleports[4].transform.position;
                break;

            case ("TeleD"):
                gameObject.transform.position = gameManager.respawPointsTeleports[7].transform.position;
                break;

            case ("TeleD (1)"):
                gameObject.transform.position = gameManager.respawPointsTeleports[6].transform.position;
                break;

            case ("TeleK"):
                gameObject.transform.position = gameManager.respawPointsTeleports[9].transform.position;
                break;

            case ("TeleK (1)"):
                gameObject.transform.position = gameManager.respawPointsTeleports[8].transform.position;
                break;

            case ("TeleM"):
                gameObject.transform.position = gameManager.respawPointsTeleports[11].transform.position;
                break;

            case ("TeleM (1)"):
                gameObject.transform.position = gameManager.respawPointsTeleports[10].transform.position;
                break;
        }*/
    }


    private void OnCollisionEnter2D(Collision2D col)
    {

        // pega o ponto real da colisão

        if ((col.gameObject.CompareTag("Door") && gameManager.isHaveKey == true))
        {
            gameManager.doors[0].transform.localPosition = new Vector2(0, -2);
            gameManager.doors[0].GetComponentInParent<BoxCollider2D>().enabled = false;
        }
        else if(col.gameObject.CompareTag("Door2") && gameManager.isHaveKey2 == true)
        {
            gameManager.doors[1].transform.localPosition = new Vector2(0, -2);
            gameManager.doors[1].GetComponentInParent<BoxCollider2D>().enabled = false;
        }




        else if (col.gameObject.CompareTag("Enemy"))
        {
            Vector2 hitPoint = col.contacts[0].point; // armazena o local da colisão

            // dropa item no local da colisão se tiver
            if (carriedItem != null)
            {
                carriedItem.transform.parent = null;
                carriedItem.transform.position = hitPoint; // solta no ponto do impacto
                carriedItem = null;
                isCarryingItem = false;
            }

            // reseta o player
            transform.position = new Vector2(0, 0);

            gameManager.isHaveKey = false;
            gameManager.isHaveKey2 = false;
            gameManager.isHaveArrow = false;
        }
    }

}

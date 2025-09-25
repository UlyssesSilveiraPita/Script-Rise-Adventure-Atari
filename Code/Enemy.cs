//using NUnit.Framework.Constraints;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public GameManager GameManager;

    [Header("Componentes")]
    [SerializeField] private Rigidbody2D enemy_rb;
    [SerializeField] private SpriteRenderer enemey_Sr;
    [SerializeField] private Sprite enemySpriteDead;
    [SerializeField] private Animator enemyAnimator;
    [SerializeField] private BoxCollider2D enemyBoxCollider;    


    [Header("Variaveis")]
    [SerializeField] private float enemySpeed;
    [SerializeField] private float moveDurations;
    [SerializeField] private float waitTime;
    [SerializeField] private bool isEnemyLive;

    private Vector2 targetDirection;


    void Start()
    {
        GameManager = FindAnyObjectByType<GameManager>();

        isEnemyLive = true;
        StartCoroutine(IEmoveEnemyRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        if ((isEnemyLive))
        {
            IEmoveEnemyRoutine();

        }
        
    }

    private IEnumerator IEmoveEnemyRoutine()
    {
        while (isEnemyLive)
        {
            targetDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;

            float timer = 0f;
            while(timer < moveDurations)
            {
                timer += Time.deltaTime; // recebe tempo frame a frame          // destino = direção
                Vector2 newPosition = Vector2.MoveTowards(enemy_rb.position, enemy_rb.position + targetDirection, enemySpeed * Time.deltaTime);

                enemy_rb.MovePosition(newPosition);

                yield return null; // espera 1 frame
            }

            yield return new WaitForSeconds(waitTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Arrow") && GameManager.isHaveArrow == true)
        {
            enemy_rb.linearVelocity = Vector2.zero;
            isEnemyLive = false;
            enemyAnimator.enabled = false;
            enemyBoxCollider.enabled = false;
            enemey_Sr.sprite = enemySpriteDead;

        }
    }

}

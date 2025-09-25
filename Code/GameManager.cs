//using UnityEditor.Rendering;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Scripts")]
    public PlayerManager playerManager { get; set; } // property só leitura

    [Header("Componentes")]
    public Transform[] teleports;
    public Transform[] respawPointsTeleports;
    public GameObject[] doors;
    public Transform arrowPosition;
    public GameObject[] ketys;
    public GameObject arrow;
    public GameObject tesour;
    private Camera mainCamera;

    [Header("Variaveis")]
    public bool isHaveKey;
    public bool isHaveKey2;
    public bool isHaveArrow;
    public bool isHaveTesour;

    private void Awake()
    {
        // Encontra o PlayerManager na cena automaticamente
        playerManager = FindAnyObjectByType<PlayerManager>();

        // Pega a câmera principal automaticamente
        mainCamera = Camera.main;
    }

    private void LateUpdate()
    {
        if (mainCamera != null && playerManager != null)
        {
            mainCamera.transform.position = new Vector3(playerManager.transform.position.x, playerManager.transform.position.y, -10f);

        }
    }

}

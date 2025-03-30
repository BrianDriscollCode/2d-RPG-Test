using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;


public class ReferenceManager : MonoBehaviour
{
    public static ReferenceManager Instance { get; private set; }

    public Player Player { get; private set; }
    public GameStateManager GameStateManager { get; private set; }
    public BattleEngine BattleEngine { get; private set; }

    public CinemachineVirtualCamera cinemachineVirtualCamera { get; private set; }
    public PixelPerfectCamera pixelPerfectCamera { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);

        FindPlayer();
        FindGameStateManager();
        FindBattleEngine();
        FindVirtualCamera();
        FindPixelPerfectCamera();
    }

    private void Update()
    {
        //Debug.Log(pixelPerfectCamera);
    }

    private void FindPlayer()
    {
        GameObject playerObject = GameObject.FindWithTag("Player");
        if (playerObject == null)
        {
            Debug.LogWarning("ReferenceManager::FindPlayer::Error - 'Player object not found in scene'");
        }
        else
        {
            Player = playerObject.GetComponent<Player>();
            if (Player == null)
            {
                Debug.LogError("ReferenceManager::FindPlayer::Error - 'Player component not found on GameObject'");
            }
        }
    }

    private void FindGameStateManager()
    {
        GameObject gameStateManagerObject = GameObject.FindWithTag("GameStateManager");
        if (gameStateManagerObject == null)
        {
            Debug.LogWarning("ReferenceManager::FindGameStateManager::Error - 'GameStateManager object not found in scene'");
        }
        else
        {
            GameStateManager = gameStateManagerObject.GetComponent<GameStateManager>();
            if (GameStateManager == null)
            {
                Debug.LogError("ReferenceManager::FindGameStateManager::Error - 'GameStateManager component not found on GameObject'");
            }
        }
    }

    private void FindBattleEngine()
    {
        GameObject battleEngineObject = GameObject.FindWithTag("BattleEngine");
        if (battleEngineObject == null)
        {
            Debug.LogWarning("ReferenceManager::FindBattleManager::Error - 'BattleManager object not found in scene'");
        }
        else
        {
            BattleEngine = battleEngineObject.GetComponent<BattleEngine>();
          
        }
    }

    private void FindVirtualCamera()
    {
        GameObject cameraObject = GameObject.FindWithTag("PlayerVirtualCamera");

        if (cameraObject != null)
        {
            cinemachineVirtualCamera = cameraObject.GetComponent<CinemachineVirtualCamera>();
            Debug.Log("ReferenceManager:: FindVirtualCamera-Error -  Found Camera");
        }
        else
        {
            Debug.LogError("ReferenceManager:: FindVirtualCamera-Error - No Cinemachine Camera found");
        }
    }

    private void FindPixelPerfectCamera()
    {
        GameObject cameraObject = GameObject.FindWithTag("MainCamera");

        if (cameraObject != null)
        {
            pixelPerfectCamera = cameraObject.GetComponent<PixelPerfectCamera>();
            if (pixelPerfectCamera)
            {
                Debug.Log("ReferenceManager:: FindPixelPerfectCamera -  Found Pixel Perfect Camera" + pixelPerfectCamera);
            }
            else
            {
                Debug.LogError("ReferenceManager:: FindPixelPerfectCamera-Error - Pixel Perfect Camera NOT FOUND on: " + cameraObject);
            }
            
        }
        else
        {
            Debug.LogError("ReferenceManager:: FindPixelPerfectCamera-Error - No Pixel Perfect Camera found");
        }
    }
}




using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.AI;

public class ControlPanel : MonoBehaviour
{
    public List<MonoBehaviour> scripts;
    public List<NavMeshAgent> agents;
    public bool isGamePaused = false;
    public GameObject pauseLabelPanel;

    public PlayerBehaviour player;
    public PlayerDataSO playerData;

    // Start is called before the first frame update
    void Start()
    {
        agents = FindObjectsOfType<NavMeshAgent>().ToList();

        foreach (var enemy in FindObjectsOfType<EnemyBehaviour>())
        {
            scripts.Add(enemy);
        }

        player = FindObjectOfType<PlayerBehaviour>();
        scripts.Add(player);
        scripts.Add(FindObjectOfType<CameraController>());

        LoadFromPrefs();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onPauseButtonToggled()
    {
        isGamePaused = !isGamePaused;
        pauseLabelPanel.SetActive(isGamePaused);

        foreach (var script in scripts)
        {
            script.enabled = !isGamePaused;
        }

        foreach (var agent in agents)
        {
            agent.enabled = !isGamePaused;
        }
    }

    public void onLoadButtonPressed()
    {
        player.controller.enabled = false;
        player.transform.position = playerData.playerPosition;
        player.transform.rotation = playerData.playerRotation;
        player.health = playerData.playerHealth;
        player.controller.enabled = true;
    }

    public void onSaveButtonPressed()
    {
        playerData.playerPosition = player.transform.position;
        playerData.playerRotation = player.transform.rotation;
        playerData.playerHealth = player.health;

        SaveToPrefs();
    }

    public void LoadFromPrefs()
    {
        playerData.playerPosition.x = PlayerPrefs.GetFloat("PlayerPositionX");
        playerData.playerPosition.y = PlayerPrefs.GetFloat("PlayerPositionY");
        playerData.playerPosition.z = PlayerPrefs.GetFloat("PlayerPositionZ");

        playerData.playerRotation.x = PlayerPrefs.GetFloat("PlayerRotationX");
        playerData.playerRotation.y = PlayerPrefs.GetFloat("PlayerRotationY");
        playerData.playerRotation.z = PlayerPrefs.GetFloat("PlayerRotationZ");
        playerData.playerRotation.w = PlayerPrefs.GetFloat("PlayerRotationW");

        playerData.playerHealth = PlayerPrefs.GetInt("PlayerHealth");
    }

    public void SaveToPrefs()
    {
        PlayerPrefs.SetFloat("PlayerPositionX", playerData.playerPosition.x);
        PlayerPrefs.SetFloat("PlayerPositionY", playerData.playerPosition.y);
        PlayerPrefs.SetFloat("PlayerPositionZ", playerData.playerPosition.z);

        PlayerPrefs.SetFloat("PlayerRotationX", playerData.playerRotation.x);
        PlayerPrefs.SetFloat("PlayerRotationY", playerData.playerRotation.y);
        PlayerPrefs.SetFloat("PlayerRotationZ", playerData.playerRotation.z);
        PlayerPrefs.SetFloat("PlayerRotationW", playerData.playerRotation.w);

        PlayerPrefs.SetInt("PlayerHealth", playerData.playerHealth);

        PlayerPrefs.Save();
    }
}

using UnityEngine;
using Unity.Cinemachine;
using UnityEngine.UI;
using System;

public class Gamemanager : MonoBehaviour
{
    public static Gamemanager instance;

    [SerializeField] GameObject Player;
    [SerializeField] CinemachineCamera camera1;
    [SerializeField] CinemachineCamera camera2;
    [SerializeField] public Transform[] RespawnPoints;
    [SerializeField] private GameObject Particle;

    public Button StartButton;

    private void Awake()
    {
        if (instance != null)
            return;

        instance = this;
    }

    public void SetTheSecondCamera()
    {
        camera1.Priority = 0;
        camera2.Priority = 10;
    }

    public void OnClick()
    {
        StartButton.gameObject.SetActive(false);
        Instantiate(Particle, Player.transform);
        Player.SetActive(true);
    }


}

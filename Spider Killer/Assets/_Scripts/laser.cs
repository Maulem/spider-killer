using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.EventSystems;
using Valve.VR.Extras;

public class laser : MonoBehaviour
{
    private SteamVR_LaserPointer steamVrLaserPointer;
    private CharacterController characterController;

    


    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
        steamVrLaserPointer = gameObject.GetComponent<SteamVR_LaserPointer>();
        steamVrLaserPointer.PointerIn += OnPointerIn;
        steamVrLaserPointer.PointerOut += OnPointerOut;
        steamVrLaserPointer.PointerClick += OnPointerClick;
    }

    private void OnPointerClick(object sender, PointerEventArgs e)
    {
        GameObject clickegObject = e.target.gameObject;
        Vector3 clickedPosition = clickegObject.transform.position;

        Debug.Log("objeto clicado com o laser " + e.target.name);
        Debug.Log("Clicked object position: "+ clickedPosition);
    }

    private void OnPointerOut(object sender, PointerEventArgs e)
    {
    Debug.Log("laser saiu do objeto " + e.target.name);
    }

private void OnPointerIn(object sender, PointerEventArgs e)
    {
        Debug.Log("laser entrou do objeto " + e.target.name);
    }
    private void HangOnWeb(Vector3 webPoint)
    {
        // Calcule o vetor de direção para a posição da teia
        Vector3 hangDirection = webPoint - transform.position;
        hangDirection.Normalize();

        float swingForce = 5f;
        characterController.Move(hangDirection * swingForce * Time.deltaTime);

        // // Mova o jogador para a posição da teia
        // characterController.Move(hangDirection);

        // // Mantenha o jogador suspenso no ar para simular o efeito de pendurar na teia
        // characterController.velocity = Vector3.zero;
    }
}

    
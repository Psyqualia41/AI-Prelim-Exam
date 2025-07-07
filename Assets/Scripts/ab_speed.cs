using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ab_speed : MonoBehaviour
{
    public float speedMod;
    public float abilityTime;
    public GameObject objectToDestroy;
    public Component[] MeshRenderers;

    void OnTriggerEnter(Collider cl)
    {
        CharacterController controller = cl.GetComponent<CharacterController>();

        StartCoroutine(speedAbility(controller));
    }

    IEnumerator speedAbility(CharacterController controller)
    {
        Debug.Log("Speed ability active!!!");

        controller.GetComponent<CharController>().alteredSpeed = speedMod;
        objectToDestroy.GetComponent<SphereCollider>().enabled = false;


        MeshRenderers = GetComponentsInChildren<MeshRenderer>();

        foreach(MeshRenderer mr in MeshRenderers )
        {
            mr.enabled = false;
        }



        yield return new WaitForSeconds(abilityTime);
        controller.GetComponent<CharController>().alteredSpeed = 1;
        Destroy(objectToDestroy);
    }




}

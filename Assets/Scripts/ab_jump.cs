using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ab_jump : MonoBehaviour
{
    public float jumpMod;
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
        Debug.Log("Jump ability active!!!");

        controller.GetComponent<CharController>().alteredJump = jumpMod;
        objectToDestroy.GetComponent<SphereCollider>().enabled = false;


        MeshRenderers = GetComponentsInChildren<MeshRenderer>();

        foreach(MeshRenderer mr in MeshRenderers )
        {
            mr.enabled = false;
        }



        yield return new WaitForSeconds(abilityTime);
        controller.GetComponent<CharController>().alteredJump = 1;
        Destroy(objectToDestroy);
    }




}

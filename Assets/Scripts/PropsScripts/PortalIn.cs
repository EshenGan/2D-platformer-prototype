using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalIn : MonoBehaviour
{
    //[SerializeField] private List<Transform> portalout;
    [SerializeField] private Transform portalout;
    [SerializeField] private Player player;
    //[SerializeField] private int numberOfPortalOut;



    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            
            if (player.canUsePortal == true && player.goInPortal == true)
            {
                
                player.transform.position = portalout.transform.position;
                player.canUsePortal = false;

            }
            
        }

    }
}

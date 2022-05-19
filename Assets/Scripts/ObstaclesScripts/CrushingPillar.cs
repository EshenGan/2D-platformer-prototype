using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrushingPillar : MonoBehaviour
{
    [SerializeField] private GameObject pillarprefab;
    //parameters
    [SerializeField]private float spawnrate;
    //[SerializeField] private float minheight;
    //[SerializeField] private float maxheight;

    private void OnEnable()
    {

            InvokeRepeating(nameof(initPillar), spawnrate, spawnrate);


    }

    private void OnDisable()
    {

            CancelInvoke(nameof(initPillar));


    }

    private void initPillar()
    {
        GameObject pillar = Instantiate(pillarprefab, transform.position, Quaternion.identity);
        pillar.transform.position += Vector3.up; //* Random.Range(minheight, maxheight);

    }


}

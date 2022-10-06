using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSystemCheck : MonoBehaviour
{
    [SerializeField]
    private float distance = 5f;
    [SerializeField]
    private GameObject cam;

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;

        Debug.DrawLine(cam.transform.position, cam.transform.forward, Color.red);
        if (Physics.Raycast(cam.transform.position, cam.transform.forward, out hit, distance))
        {

        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, distance);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class MachineController : MonoBehaviour
{
    [InlineEditor]
    public MachinData data;
    
    private void Start()
    {
            StartCoroutine("ProductionItem");
            Debug.Log("productItem va spawn");
    }

    private void Update()
    {

    }

    IEnumerator ProductionItem()
    {
        while (true)
        {
            yield return new WaitForSeconds(2f);

            Instantiate(data.productItem, data.pointSpawn[0].position, transform.rotation);
            Debug.Log("productItem vient de spawn");
        }
    }
}

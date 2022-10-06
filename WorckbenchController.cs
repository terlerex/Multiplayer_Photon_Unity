using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class WorckbenchController : MonoBehaviour
{
    [InlineEditor]
    public WorkbenchData data;

    private void OnCollisionEnter(Collision collision)
    {
        ItemData item = collision.transform.GetComponent<ItemData>();

        if (item != null)
        {
            return;
        }

        if (data.ItemUse[0])
        {

        }
        /*     
        if (data.WorkbenchModel == item.IdItem[0])
        {
            Destroy(collision.gameObject);
            Instantiate(data.productItem, data.pointSpawn[0].position, transform.rotation);
        }
        */
    }
}

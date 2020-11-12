using System.Collections;
using System.ComponentModel;
using TMPro;
using UnityEngine;
public class WindZone : MonoBehaviour
{
    #region wind_variables
    public float strength;
    public Vector3 windDirection;
    private BoxCollider area;
    public float waitInterval;
    public float howLongTheWindLasts;
    #endregion

    #region Unity_functions
    void Start()
    {
        area = GetComponent<BoxCollider>();
        StartCoroutine(WindCoroutine());    
    }

    void OnTriggerStay(Collider col)
    {
        Rigidbody colRigidbody = col.GetComponent<Rigidbody>();
        if (colRigidbody != null)
        {
            colRigidbody.AddForce(windDirection * strength);
        }
    }

    IEnumerator WindCoroutine()
    {
        while(true)
        {
            area.enabled = false;
            yield return new WaitForSeconds(waitInterval);
            area.enabled = true;
            yield return new WaitForSeconds(howLongTheWindLasts);
        }
    }
    #endregion
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoringSystem : MonoBehaviour
{
    public GameObject FeatherText;
    public static int currentFeathers;
    public static int totalFeathers = 10;

    void Update()
    {
        FeatherText.GetComponent<Text>().text = "Feathers Collected: " + currentFeathers + "/" + totalFeathers;
    }
}

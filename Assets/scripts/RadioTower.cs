using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RadioTower : MonoBehaviour
{
    [SerializeField] private Material lightMaterial;
    [SerializeField] private Color inactiveColor;
    [SerializeField] private Color activeColor;
    [SerializeField] private Transform door;


    public short termCount, activeCount;
    private TMP_Text counter;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        foreach (Terminal t in FindObjectsByType<Terminal>(FindObjectsSortMode.None))
        {
            termCount++;
        }

        lightMaterial.color = inactiveColor;
        lightMaterial.SetColor("_EmissionColor", inactiveColor);

        counter = GameObject.FindGameObjectWithTag("TerminalCounter").GetComponent<TMP_Text>();
        counter.text = termCount.ToString();
    }

    void OnTerminalActivate()
    {
        activeCount++;
        counter.text = (termCount - activeCount).ToString();
        if (activeCount == termCount)
        {
            lightMaterial.color = activeColor;
            lightMaterial.SetColor("_EmissionColor", activeColor);
            door.gameObject.SetActive(false);

        }
    }
}

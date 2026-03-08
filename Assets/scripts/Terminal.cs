using UnityEngine;
using UnityEngine.Rendering.Universal;
using TMPro;

public class Terminal : MonoBehaviour
{
    public Material screenMaterial;
    public Transform screen;
    public Color inactiveCol, activeCol;

    private bool isActive= false;
    private RadioTower radioTower;
    private Transform player;
    private Material m2;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        radioTower = FindFirstObjectByType<RadioTower>();

        m2 = new Material(screenMaterial);
        m2.color = activeCol;
        m2.SetColor("_EmissionColor", activeCol);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    void ActivateTerminal()
    {
        if (!isActive) 
        {
            screen.GetComponent<MeshRenderer>().material = m2;
            radioTower.BroadcastMessage("OnTerminalActivate");
            isActive = true;
        }
        
        
    }
}

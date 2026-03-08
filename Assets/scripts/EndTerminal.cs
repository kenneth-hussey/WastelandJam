using UnityEngine;
using UnityEditor;

public class EndTerminal : MonoBehaviour
{
    public Material screenMaterial;
    public Transform screen;
    public Color inactiveCol, activeCol;

    private Transform player;
    private bool isActive=false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void ActivateTerminal()
    {
        if (!isActive)
        {
            Application.Quit();
            //EditorApplication.isPlaying = false;
            isActive= true;
        }
    }
}

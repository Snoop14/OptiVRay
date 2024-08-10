using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFilterScript : MonoBehaviour
{
    private Color filtColor;
    private Renderer filtRenderer;
    private Vector3 rotSpeed;

    [SerializeField]
    private LineRenderer beam;

    // Start is called before the first frame update
    void Start()
    {
        filtColor = Color.black;
        filtColor.a = 175f / 255f;
        filtRenderer = GetComponent<Renderer>();
        filtRenderer.material.color = filtColor;
        rotSpeed = new Vector3(0, 50, 0);
    }
    private void Update()
    {
        transform.Rotate(rotSpeed * Time.deltaTime);
    }

    public void AddRemoveColour(int colIndex)
    {
        if (filtColor[colIndex] == 1f)
        {
            filtColor[colIndex] = 0f;
        }
        else
        {
            filtColor[colIndex] = 1f;
        }

        if (filtColor.r == 1f && filtColor.g == 1f && filtColor.b == 1f)
        {
            filtColor.a = 125f / 255f;
        }
        else
        {
            filtColor.a = 175f / 255f;
        }

        filtRenderer.material.color = filtColor;
        beam.endColor = filtColor;

    }

    public void spawnFilter()
    {

    }
}

using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class SpawnOpticalScrollScript : MonoBehaviour
{
    [SerializeField] RectTransform rectTransform;
    [SerializeField] Transform spawnPosition;
    [SerializeField] List<GameObject> opticalObject;
    

    int index = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void RightPress()
    {
        if(index < (opticalObject.Count - 1))
        {
            index++;
            rectTransform.localPosition += new Vector3(-600, 0);
        }
        else
        {
            index = 0;
            rectTransform.localPosition += new Vector3(600 * (opticalObject.Count - 1), 0);
        }
    }

    public void LeftPress() 
    {
        if(index > 0)
        {
            index--;
            rectTransform.localPosition += new Vector3(600, 0);
        }
        else
        {
            index = (opticalObject.Count - 1);
            rectTransform.localPosition += new Vector3(-600 * (opticalObject.Count - 1), 0);
        }
        
    }

    public void SpawnPress()
    {
        GameObject spawnThisObject = opticalObject[index];

        Instantiate(spawnThisObject, spawnPosition.position, spawnPosition.rotation);
    }
}

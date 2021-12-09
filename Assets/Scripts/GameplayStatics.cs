using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayStatics : MonoBehaviour
{
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static Transform GetPlayer()
    {
        var temp = GameObject.Find("Player");
        if (temp)
        {
            return temp.transform;
        }
        return null;
    }
}

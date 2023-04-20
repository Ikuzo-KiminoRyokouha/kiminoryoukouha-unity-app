using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class ModelBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 origin = gameObject.transform.position;
        if (origin.y < -0.5f)
        {
            gameObject.transform.position = new Vector3(origin.x, origin.y + 0.05f, origin.z);
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var startPos = Camera.main.transform.position;
        // ˆÊ’uŽw’è
        transform.position = new Vector3(startPos.x, startPos.y, 0);
    }

    
}

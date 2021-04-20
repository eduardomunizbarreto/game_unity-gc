using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoverTiro : MonoBehaviour
{
    public float velocidade = 10f;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        transform.Translate(new Vector2(velocidade * Time.deltaTime, 0));
        if (gameObject.transform.position.x > 10 || gameObject.transform.position.x < -10)
            Destroy(gameObject);
    }

}

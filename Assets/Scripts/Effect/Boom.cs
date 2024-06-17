using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : MonoBehaviour
{
    public float lifeTime;
    private void Start()
    {
        Destroy(this.gameObject, lifeTime);
    }
}

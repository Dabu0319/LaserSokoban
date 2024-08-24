using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laseritem : MonoBehaviour
{
private void OnTriggerEnter2D(Collider2D other)
{
    if (other.CompareTag("Box"))
    {
        Box box = other.GetComponent<Box>();
        if (box != null)
        {
            box.DestroyBox();
        }
    }
}
}

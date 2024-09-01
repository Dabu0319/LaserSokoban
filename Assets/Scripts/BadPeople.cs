using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BadPeople : MonoBehaviour
{
    private void Awake(){
        FindObjectOfType<GameManager>().totalBadPeople++;
    }

    // Start is called before the first frame update
    void Start()
    {
    }


    public void DestroyBadPeople()
    {
        FindObjectOfType<GameManager>().finishBadPeople--;
        Destroy(gameObject);
    }

}

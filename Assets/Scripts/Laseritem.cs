using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Laseritem : MonoBehaviour
{
    public End end;
    

    private void Start()
    {
        end = GetComponent<End>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.CompareTag("Box"))
        {
            Box box = other.GetComponent<Box>();
            if (box != null)
            {
                box.DestroyBox();
                // FindObjectOfType<GameManager>().ResetStage();
            }
            FindObjectOfType<GameManager>().Boxdisplay();
            
        }

        if (other.CompareTag("End"))
        {
            Debug.Log("End detected");
            End end = other.GetComponent<End>();
            if (end != null)
            {
                end.SetActiveEnd();
            }
        }

        if (other.CompareTag("Good People"))
        {
            Debug.Log("Good People detected");
            GoodPeople goodPeople = other.GetComponent<GoodPeople>();
            if (goodPeople != null)
            {
                goodPeople.DestroyGoodPeople();

            }
        }

        if (other.CompareTag("BadPeople"))
        {
            Debug.Log("Bad People detected");
            BadPeople badPeople = other.GetComponent<BadPeople>();
            if(badPeople != null)
            {
                badPeople.DestroyBadPeople();
                FindObjectOfType<GameManager>().BadPeoplezero();
                FindObjectOfType<End>().OpenColor();
            }
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodPeople : MonoBehaviour
{
    public void DestroyGoodPeople()
    {
        gameObject.SetActive(false);
        FindObjectOfType<GameManager>().fail=true;
        FindObjectOfType<MenuController>().FailGame();
    }
}

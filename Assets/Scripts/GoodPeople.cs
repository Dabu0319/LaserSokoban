using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoodPeople : MonoBehaviour
{
    public void DestroyGoodPeople()
    {
        Destroy(gameObject, 0.5f); // 1f 表示延迟 1 秒

    }
}

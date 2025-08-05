using UnityEngine;
using System.Collections.Generic;

public class DamageEffectPolling : MonoBehaviour
{
    public GameObject[] prefabs;
    List<GameObject>[] pools;

    private void Awake()
    {
        pools = new List<GameObject>[prefabs.Length];

        for (int index = 0; index < pools.Length; index++)
        {
            pools[index] = new List<GameObject>();
        }


    }

    public GameObject Get(int index)
    {
        GameObject select = null;

        //....선택한 풀의 비활성화 되어있는 게임오브잭트에 접근한다.
        foreach (GameObject item in pools[index])
        {
            if (!item.activeSelf)
            {
                select = item;
                select.SetActive(true);
                break;
            }
        }

        //못찾았으면?
        if (select == null)
        {

            select = Instantiate(prefabs[index], transform);
            pools[index].Add(select);
        }
        return select;
    }
}

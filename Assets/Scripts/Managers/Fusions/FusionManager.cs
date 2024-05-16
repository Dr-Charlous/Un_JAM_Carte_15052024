using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FusionManager : MonoBehaviour
{

    public FusionData[] FusionDatas;

    [SerializeField] GameObject _cardPrefab;

    public void CheckFusion(CardAssign[] cardsAssign)
    {
        bool isValid = false;
        int k = -1;

        for (int i = 0; i < FusionDatas.Length; i++)
        {
            bool isEveryOne = true;

            for (int j = 0; j < cardsAssign.Length; j++)
            {
                if (FusionDatas[i].Recipe[j] == cardsAssign[j].CardData)
                    continue;
                else
                    isEveryOne = false;
            }

            if (isEveryOne)
            {
                isValid = true;
                k = i;
                break;
            }
            else
                continue;
        }

        if (isValid)
        {
            StartCoroutine(WaitSpawn(cardsAssign, FusionDatas[k]));
        }
    }

    IEnumerator WaitSpawn(CardAssign[] cardsAssign, FusionData fusionData)
    {
        yield return new WaitForSeconds(fusionData.Time);

        for (int i = 0; i < cardsAssign.Length; i++)
        {
            Destroy(cardsAssign[i].gameObject);
        }

        for (int i = 0; i < fusionData.Result.Length; i++)
        {
            GameObject obj = Instantiate(_cardPrefab);
            obj.GetComponent<CardAssign>().CardData = fusionData.Result[i];
        }
    }
}

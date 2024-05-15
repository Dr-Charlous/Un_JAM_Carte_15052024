using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FusionManager : MonoBehaviour
{
    [SerializeField] GameObject _cardPrefab;
    public FusionData[] FusionDatas;

    public void CheckFusion(CardData[] cardsData)
    {
        bool isValid = false;
        int k = -1;

        for (int i = 0; i < FusionDatas.Length; i++)
        {
            bool isEveryOne = true;

            for (int j = 0; j < cardsData.Length; j++)
            {
                if (FusionDatas[i].Reciepe[j] == cardsData[j])
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
            StartCoroutine(WaitSpawn(cardsData, FusionDatas[k]));
        }
    }

    IEnumerator WaitSpawn(CardData[] cardsData, FusionData fusionData)
    {
        yield return new WaitForSeconds(fusionData.Time);

        for (int i = 0; i < cardsData.Length; i++)
        {
            Destroy(cardsData[i]);
        }

        for (int i = 0; i < fusionData.Result.Length; i++)
        {
            GameObject obj = Instantiate(_cardPrefab);
            obj.GetComponent<CardAssign>().CardData = fusionData.Result[i];
        }
    }
}

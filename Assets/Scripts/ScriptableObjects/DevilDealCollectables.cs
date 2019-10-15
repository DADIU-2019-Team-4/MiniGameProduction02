using UnityEngine;

[CreateAssetMenu(fileName = "Devil Deal Collectables", menuName = "ScriptableObjects/DevilDealCollectables")]
public class DevilDealCollectables : DevilDeal
{
    public int ToAdd;

    public override void ApplyDevilDeal()
    {
        CollectionItemSpawner collectionItemSpawner = FindObjectOfType<CollectionItemSpawner>();
        collectionItemSpawner.NumberOfItemsToGoal += ToAdd;
        collectionItemSpawner.UpdateText();
    }
}

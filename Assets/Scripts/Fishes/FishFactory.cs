using UnityEngine;

public class FishFactory 
{
    public Fish CreateFish(Fish prefab, FishData data, Vector3 spawnPosition)
    {
        Fish fish = GameObject.Instantiate(prefab, spawnPosition, Quaternion.identity);
        FishVisualsContainer container = fish.GetComponent<FishVisualsContainer>();

        FishView view = new FishView(container);
        FishBehaviour behaviour = new FishBehaviour(data.BehaviourData, fish.transform);
        FishBehaviourController controller = new FishBehaviourController(fish);

        fish.Initialize(data, view, behaviour, controller);
        return fish;
    }
}

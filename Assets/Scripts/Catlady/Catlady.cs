using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Catlady : MonoBehaviour
{
    [SerializeField] GameObject mommyCat;
    [SerializeField] Vector2 backyardSize;
    float timeSinceLastTea;
    [SerializeField] float timeSpentOnTea = 0.2f;
    [SerializeField] float timeBeforePickingUpAnotherCat = 4;

    [SerializeField] int KittensHunting => CatAI.activeCats;
    [SerializeField] int MaxKittensHunting => (int)(Time.timeSinceLevelLoad / timeBeforePickingUpAnotherCat) + 3;

    void Update()
    {
        if (HaveSomeTea()) return;
        ThrowKittyCatAtPlayer();
    }

    bool HaveSomeTea()
    {
        if (KittensHunting < MaxKittensHunting && Time.time - timeSinceLastTea > timeSpentOnTea)
        {
            return false;
        }
        return true;
    }

    void ThrowKittyCatAtPlayer()
    {
        timeSinceLastTea = Time.time;
        var kitten = CareForKitten();
        kitten.transform.position = FindHoleInFence();
    }

    Vector3 FindHoleInFence(){Vector3 randomArea = new Vector3(Random.Range(-backyardSize.x / 2, backyardSize.x / 2), Random.Range(-backyardSize.y / 2, backyardSize.y / 2), 0);if(Mathf.Abs(randomArea.x) > Mathf.Abs(randomArea.y)){if (randomArea.x > 0){randomArea.x = backyardSize.x / 2;}else{randomArea.x = -backyardSize.x / 2;}}else{if (randomArea.y > 0){randomArea.y = backyardSize.y / 2;}else{randomArea.y = -backyardSize.y / 2;}}return randomArea;}

    GameObject CareForKitten()
    {
        var babyKitten = Instantiate(mommyCat);
        return babyKitten;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(new Vector2(-backyardSize.x / 2, backyardSize.y / 2), new Vector2(backyardSize.x / 2, backyardSize.y / 2));
        Gizmos.DrawLine(new Vector2(backyardSize.x / 2, backyardSize.y / 2), new Vector2(backyardSize.x / 2, -backyardSize.y / 2));
        Gizmos.DrawLine(new Vector2(backyardSize.x / 2, -backyardSize.y / 2), new Vector2(-backyardSize.x / 2, -backyardSize.y / 2));
        Gizmos.DrawLine(new Vector2(-backyardSize.x / 2, -backyardSize.y / 2), new Vector2(-backyardSize.x / 2, backyardSize.y / 2));
    }
}

using System.Collections.Generic;
using UnityEngine;

public class NPCQueueManager : MonoBehaviour
{
    [SerializeField] private List<Transform> queuePoints;

    private List<NPC> npcs = new List<NPC>();

    public void AddNPC(NPC npc)
    {
        if (npcs.Contains(npc))
            return;

        npcs.Add(npc);

        UpdateQueue();
    }

    public void RemoveNPC(NPC npc)
    {
        if (!npcs.Contains(npc))
            return;

        npcs.Remove(npc);

        UpdateQueue();
    }

    public bool IsFirst(NPC npc)
    {
        return npcs.Count > 0 && npcs[0] == npc;
    }

    private void UpdateQueue()
    {
        for (int i = 0; i < npcs.Count; i++)
        {
            if (i < queuePoints.Count)
            {
                npcs[i].SetQueuePoint(queuePoints[i]);
            }
        }
    }
}
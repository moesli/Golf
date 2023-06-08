using UnityEngine;

public class AddTreeColliders : MonoBehaviour
{
    public Terrain terrain;

    void Start()
    {
        AddCollidersToTrees();
    }

    void AddCollidersToTrees()
    {
        // Hole alle Bauminstanzen
        TreeInstance[] trees = terrain.terrainData.treeInstances;

        foreach (TreeInstance tree in trees)
        {
            // Erstelle ein neues GameObject für den Collider
            GameObject go = new GameObject("TreeCollider");
            go.transform.position = Vector3.Scale(tree.position, terrain.terrainData.size) + terrain.transform.position;

            // Füge den Collider hinzu
            CapsuleCollider collider = go.AddComponent<CapsuleCollider>();
            collider.radius = 1; // Setze den Radius
            collider.height = 17; // Setze die Höhe

            // Füge das GameObject zum Terrain hinzu
            go.transform.parent = terrain.transform;
        }
    }
}

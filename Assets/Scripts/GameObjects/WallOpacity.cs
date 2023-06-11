using System.Collections.Generic;
using UnityEngine;

namespace GameObjects
{
    public class WallOpacity : MonoBehaviour
{
    public Material[] newMaterials; 
    private Dictionary<Renderer, Material[]> originalMaterialsDict = new Dictionary<Renderer, Material[]>(); 

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Debug.Log("Girdi");
            Transform root = transform.root; 

            ChangeMaterialsRecursively(root, true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Debug.Log("Çıktı");
            Transform root = transform.root; 

            ChangeMaterialsRecursively(root, false);
        }
    }

    private void ChangeMaterialsRecursively(Transform parent, bool changeMaterials)
    {
        Renderer renderer = parent.GetComponent<Renderer>(); 

        if (renderer != null)
        {
            if (changeMaterials)
            {
               
                if (!originalMaterialsDict.ContainsKey(renderer))
                {
                    originalMaterialsDict[renderer] = renderer.materials;
                }

                
                Material[] materials = renderer.materials;
                for (int i = 0; i < materials.Length; i++)
                {
                    if (i < newMaterials.Length)
                    {
                        materials[i] = newMaterials[i];
                    }
                }
                renderer.materials = materials;
            }
            else
            {
                
                if (originalMaterialsDict.TryGetValue(renderer, out Material[] originalMaterials))
                {
                    renderer.materials = originalMaterials;
                    originalMaterialsDict.Remove(renderer);
                }
            }
        }

        
        for (int i = 0; i < parent.childCount; i++)
        {
            Transform child = parent.GetChild(i);
            ChangeMaterialsRecursively(child, changeMaterials);
        }
    }
}
}


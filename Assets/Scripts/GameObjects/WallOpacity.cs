using System.Collections.Generic;
using UnityEngine;

namespace GameObjects
{
    public class WallOpacity : MonoBehaviour
{
    public Material[] newMaterials; // Birden fazla materyeli değiştirmek için bir dizi materyal
    private Dictionary<Renderer, Material[]> originalMaterialsDict = new Dictionary<Renderer, Material[]>(); // Orijinal materyalleri saklamak için bir sözlük

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Debug.Log("Girdi");
            Transform root = transform.root; // Trigger'a giren objenin kök nesnesini alır

            ChangeMaterialsRecursively(root, true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Debug.Log("Çıktı");
            Transform root = transform.root; // Trigger'dan çıkan objenin kök nesnesini alır

            ChangeMaterialsRecursively(root, false);
        }
    }

    private void ChangeMaterialsRecursively(Transform parent, bool changeMaterials)
    {
        Renderer renderer = parent.GetComponent<Renderer>(); // Child nesnedeki Renderer bileşenini alır

        if (renderer != null)
        {
            if (changeMaterials)
            {
                // Orijinal materyalleri saklar
                if (!originalMaterialsDict.ContainsKey(renderer))
                {
                    originalMaterialsDict[renderer] = renderer.materials;
                }

                // Yeni materyalleri atar
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
                // Orijinal materyalleri geri yükler
                if (originalMaterialsDict.TryGetValue(renderer, out Material[] originalMaterials))
                {
                    renderer.materials = originalMaterials;
                    originalMaterialsDict.Remove(renderer);
                }
            }
        }

        // Child nesneleri kontrol eder
        for (int i = 0; i < parent.childCount; i++)
        {
            Transform child = parent.GetChild(i);
            ChangeMaterialsRecursively(child, changeMaterials);
        }
    }
}
}


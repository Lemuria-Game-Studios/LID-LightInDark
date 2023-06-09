using System.Collections;
using UnityEngine;

namespace GameObjects
{
    public class WallOpacity : MonoBehaviour
    {
        [SerializeField] private float reduceSpeed;
        [SerializeField] private float _target = 255;
        private Material[] Materialsk;



        //[SerializeField] private LayerMask mask;
        private void OnEnable()
        {
            _target = 255;
        }

        /*private void Update()
        {
            Color32 materialColor = _renderer.material.color;
                var moveTowards = Mathf.MoveTowards(materialColor.a, _target, reduceSpeed * Time.deltaTime);
                materialColor.a = (byte)moveTowards;
                //_renderer.material.color = materialColor;
                foreach (Material material in materials)
                {
                    material.color = materialColor;
                }
        }*/


        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                Renderer[] renderers = other.GetComponentsInChildren<Renderer>();

                foreach (Renderer renderer in renderers)
                {
                    Material[] materials = renderer.materials;

                    for (int i = 0; i < materials.Length; i++)
                    {
                        materials[i].SetFloat("_Surface", 1f); // Surface Type'ı transparan yapmak için "_Surface" değerini 1 yapar.

                        Color color = materials[i].color;
                        color.a *= 0.5f; // Transparanlığı yarıya indirmek için alpha değerini 0.5 ile çarpar.
                        materials[i].color = color;
                    }

                    renderer.materials = materials;
                }
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
            {
                Debug.Log("Cıktı");
                _target = 255;

                //materialColor.a = 255;
                GameObject[] childTransforms = GetComponentsInChildren<GameObject>(true);

                foreach (GameObject childTransform in childTransforms)
                {
                    if (childTransform != gameObject) // Kendi transformunu atla
                    {
                        Color32 materialColor = childTransform.GetComponent<Renderer>().material.color;

                        foreach (Material material in Materialsk)
                        {
                            Material newMaterial = new Material(Shader.Find("Universal Render Pipeline/Lit"));

                            // Yüzey tipini şeffaf olarak ayarlayın
                            newMaterial.SetFloat("_Surface", 0f); // 1f, şeffaf; 0f, opak

                            // Materyali nesneye uygulayın
                            childTransform.GetComponent<Renderer>().material = newMaterial;
                            materialColor.a = 255;
                            childTransform.GetComponent<Renderer>().material.color = materialColor;
                        }
                    }
                }
            }
        }

        private void UpdateMaterialColor()
        {
            Renderer[] childTransforms = GetComponentsInChildren<Renderer>(true);

            foreach (Renderer childTransform in childTransforms)
            {
                if (childTransform.gameObject != gameObject) // Kendi transformunu atla
                {
                    Color32 materialColor = childTransform.GetComponent<Renderer>().material.color;


                    foreach (Material material in Materialsk)
                    {
                        Material newMaterial = new Material(Shader.Find("Universal Render Pipeline/Lit"));

                        // Yüzey tipini şeffaf olarak ayarlayın
                        newMaterial.SetFloat("_Surface", 1f); // 1f, şeffaf; 0f, opak

                        // Materyali nesneye uygulayın
                        childTransform.GetComponent<Renderer>().material = newMaterial;
                        material.color = materialColor;
                        childTransform.GetComponent<Renderer>().material.color = materialColor;
                    }

                    childTransform.GetComponent<Renderer>().material.color = materialColor;

                }


            }
        }
    }
}


using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameObjects
{
    public class ObjectOpacity : MonoBehaviour
    {
        private Renderer _renderer;
        [SerializeField] private float reduceSpeed;
        [SerializeField] private float _target = 255;
        private Material[] materials;

        private void Awake()
        {
            _renderer = GetComponent<Renderer>();
        }

        //[SerializeField] private LayerMask mask;
        private void OnEnable()
        {
            _target = 255;
            _renderer = GetComponent<Renderer>();
            materials = _renderer.materials;
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
            if (other.gameObject.layer ==  LayerMask.NameToLayer("Player"))
            {
                Debug.Log("Girdi");
                //Color32 materialColor = _renderer.material.color;
                _target = 0;
                StartCoroutine(UpdateMaterialColor());
                // materialColor.a = 0; 
                //_renderer.material.color = materialColor;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.layer ==  LayerMask.NameToLayer("Player"))
            {
                Debug.Log("Cıktı");
                Color32 materialColor = _renderer.material.color;
                _target = 255;
                
                materialColor.a = 255;
                foreach (Material material in materials)
                {
                    material.color = materialColor;
                }
                _renderer.material.color = materialColor;
                
            }
        }
        private IEnumerator UpdateMaterialColor()
        {
            
            Color32 materialColor = _renderer.material.color;
                    while (Mathf.Abs(_renderer.material.color.a - _target) > 0.01f)
                    {
                        
                        float newAlpha = Mathf.MoveTowards(materialColor.a, _target, reduceSpeed * Time.deltaTime);
                        materialColor.a = (byte)newAlpha;
        
                        foreach (Material material in materials)
                        {
                            material.color = materialColor;
                        }

                        _renderer.material.color = materialColor;
                        yield return null;
                    }
                    
                
            
            
        }
    }
}

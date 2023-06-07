using System;
using UnityEngine;

namespace GameObjects
{
    public class ObjectOpacity : MonoBehaviour
    {
        private Renderer _renderer;
        [SerializeField] private float reduceSpeed;
        private float _target;

        private void Awake()
        {
            _renderer = GetComponent<Renderer>();
        }

        //[SerializeField] private LayerMask mask;
       
        /*private void Update()
        {
            if (_target != null)
            {
                float newAlpha = Mathf.MoveTowards(_renderer.material.color.a, _target, reduceSpeed * Time.deltaTime);
                var material = _renderer.material;
                var materialColor = _renderer.material.color;
                materialColor.a = newAlpha;
                material.color = materialColor;
            }
                
        }*/
        

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer ==  LayerMask.NameToLayer("Player"))
            {
                Debug.Log("Girdi");
                Color32 materialColor = _renderer.material.color;
                //_target = 128;
                materialColor.a = 128; 
                _renderer.material.color = materialColor;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.gameObject.layer ==  LayerMask.NameToLayer("Player"))
            {
                var renderer = gameObject.GetComponent<Renderer>();
                Color32 materialColor = renderer.material.color;
                materialColor.a = 255; // 0.5f * 255 (alpha değerini 0-255 aralığında belirtmek için)
                renderer.material.color = materialColor;
                
            }
        }
    }
}

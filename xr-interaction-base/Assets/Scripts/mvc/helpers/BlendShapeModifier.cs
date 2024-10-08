using System;
using UnityEngine;

namespace ATG.MVC.Helpers
{
    public interface IBlendShapeModifier
    {
        float GetWeightRateByName(string name);
        void SetWeightRateByName(string name, float rate);
    }

    [RequireComponent(typeof(SkinnedMeshRenderer))]
    public sealed class BlendShapeModifier: MonoBehaviour, IBlendShapeModifier
    {
        [SerializeField] private BlendShapeModifierData[] blendShapes;

        private SkinnedMeshRenderer _renderer;

        private void Awake() => _renderer = GetComponent<SkinnedMeshRenderer>();
        
        public void SetWeightRateByName(string name, float rate)
        {
            var selected = Array.Find(blendShapes, b => b.Name == name);
            if(selected == null)
                throw new NullReferenceException($"Blendshape with name={name} not exist!");
            
            rate = Mathf.Clamp01(rate);
            _renderer.SetBlendShapeWeight(selected.Index, rate * selected.MaxValue);
        }

        public float GetWeightRateByName(string name)
        {
            var selected = Array.Find(blendShapes, b => b.Name == name);
            if(selected == null)
                throw new NullReferenceException($"Blendshape with name={name} not exist!");

            return _renderer.GetBlendShapeWeight(selected.Index) / selected.MaxValue;
        }

        [Serializable]
        private class BlendShapeModifierData
        {
            [field: SerializeField] public int Index { get; private set;}
            [field: SerializeField] public string Name { get; private set; }
            [field: SerializeField] public float MaxValue {get; private set;}
        }
    }
}
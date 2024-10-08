using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ATG.UI
{
    [Serializable]
    public class ColoredSkinSet
    {
        [SerializeField] private SkinType type;
        [SerializeField] private ColoredSkin[] skinsSet;

        [SerializeField] private UnityEvent OnSelect;

        public SkinType SkinType => type;

        public void SetupSkin()
        {
            foreach (var skin in skinsSet)
            {
                skin.SetupColor();
            }

            OnSelect?.Invoke();
        }

        [Serializable]
        private class ColoredSkin
        {
            [SerializeField] private Graphic graphic;
            [SerializeField] private Color color;

            public void SetupColor() => graphic.color = color;
        }
    }

    public class ColoredSkinSwitcher : MonoBehaviour, ISkinSwitcher
    {
        [SerializeField] private ColoredSkinSet[] skinSet;

        private IReadOnlyDictionary<SkinType, ColoredSkinSet> _setsDic;

        private void Awake()
        {
            _setsDic = new Dictionary<SkinType, ColoredSkinSet>
            (
                skinSet.Select(s => new KeyValuePair<SkinType, ColoredSkinSet>(s.SkinType, s))
            );
        }

        public void SwitchToSkin(SkinType skinType)
        {
            if(_setsDic.ContainsKey(skinType) == false)
                throw new NullReferenceException($"can't find skin = {skinType} for {transform.name}");

            _setsDic[skinType].SetupSkin();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ATG.UI
{
    [Serializable]
    public sealed class UIViewSetup
    {
        [field: SerializeField] public UIView Prefab { get; private set; }
        [field: SerializeField, Range(0, 100)] public int SortOrder { get; private set; } = 0;
    }

    [CreateAssetMenu(menuName = "configs/new ui views set config", fileName = "new_ui_view_set_config")]
    public sealed class UIViewSetData : ScriptableObject
    {
        [SerializeField] private UIViewSetup[] viewPrefabSets;

        public IEnumerable<KeyValuePair<UIElementType, UIViewSetup>> ViewPrefabs => 
            viewPrefabSets.Select(setup => new KeyValuePair<UIElementType, UIViewSetup>(setup.Prefab.ViewType, setup));
    }
}
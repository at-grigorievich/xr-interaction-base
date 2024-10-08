using System;
using UnityEngine;

namespace ATG.Animator
{
	[CreateAssetMenu(menuName = "configs/new animator config", fileName = "new_animator_config")]
	public class AnimateableServiceData: ScriptableObject
	{
		[SerializeField] private AnimatorTagData[] tagsData;

        public void PlayCrossFade(UnityEngine.Animator animator, AnimatorEnum needTag, 
            object value = null, float startTime = 0f)
		{
			string tagName;
		
			foreach (var tag in tagsData) 
			{
				if(tag.TryGetNameByTag(needTag,out tagName))
				{
					switch(tag.TagType)
					{
						case AnimatorTagType.Crossfade:
							animator.CrossFade(tagName,tag.CrossFade,tag.Layer,startTime);
							break;
						case AnimatorTagType.Trigger:
							animator.SetTrigger(tagName);
							break;
						case AnimatorTagType.Bool:
							animator.SetBool(tagName, (bool)value);
							break;
						case AnimatorTagType.Float:
							animator.SetFloat(tagName,(float)value);
							break;
                        case AnimatorTagType.Layer:
                            animator.SetLayerWeight(Int32.Parse(tagName), (float)value);
                            break;
					}
					return;
				}
			}
			//Debug.LogError(needTag + " not founded");
		}

		private enum AnimatorTagType
		{
			Crossfade,
			Trigger,
			Bool,
			Float,
            Layer
		}

		[Serializable]
		private sealed class AnimatorTagData
		{
			[SerializeField] private AnimatorEnum tag;
			[SerializeField] private string name;
			[SerializeField,Range(-1,100)] private int layer = -1;
            [Space(5)]
			[SerializeField] private AnimatorTagType tagType = AnimatorTagType.Crossfade;
			[SerializeField] private float crossFade = 0.2f;
            [Space(5)]
            [SerializeField] private bool isLockedSpeed;

			public float CrossFade { get { return crossFade; }}
			public AnimatorTagType TagType { get { return tagType; }}
			public int Layer { get { return layer; }}

            public bool IsLockedSpeed { get { return isLockedSpeed; } }

			public bool TryGetNameByTag(AnimatorEnum t, out string tagName)
			{
				if (tag == t) 
				{
					tagName = name;
					return true;
				}

				tagName = string.Empty;
				return false;
			}
		}
	}
}


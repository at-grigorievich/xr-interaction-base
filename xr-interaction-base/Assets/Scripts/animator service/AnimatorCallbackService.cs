using System;
using UnityEngine;

namespace ATG.Animator
{
	[RequireComponent(typeof(UnityEngine.Animator))]
	public sealed class AnimatorCallbackService: MonoBehaviour, IAnimateableCallback
	{
		public const string DealDamage = "DealDamage";
		public const string EndAttack = "EndAttack";
        public const string Break = "Break";
        public const string EndBreak = "EndBreak";
        public const string StartAttack = "StartAttack";
        public const string StopAttack = "StopAttack";
		public const string StopDrink = "StopDrink";

		public event EventHandler<AnimatorEventArgs> OnAnimatorReceived;
		
		public void AnimatorReceive(string obj)
		{
#if UNITY_EDITOR
			//Debug.Log("animator event received " + obj);
#endif
			if(enabled == false) return;
			if (OnAnimatorReceived == null) return;

			OnAnimatorReceived.Invoke(this,new AnimatorEventArgs(obj));
		}
	}
}


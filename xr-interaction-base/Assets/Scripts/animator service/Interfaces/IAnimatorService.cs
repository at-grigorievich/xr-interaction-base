using ATG.Activator;
using UnityEngine;

namespace ATG.Animator
{
	public interface IAnimatorService: IActivateable
	{
		UnityEngine.Animator Animator { get; }
		IAnimateableCallback CallbackService { get; }

        AnimatorEnum LastCrossfadeTag { get; }

		Transform RigParent { get; }

        void UpdateAnimatorConfig(AnimateableServiceData animatorConfig);

        void SetAnimatorSpeed(float speed);
		void SetAnimatorLayerWeight(int layer, float weight);

		void CrossFadeAnimate(AnimatorEnum tag, object value = null, float startTime = 0f, bool ignoreRepeat = false);

        AnimatorStateInfo GetCurrentStateInfo(int layer);

		void RunAnimate();
		void IdleAnimate();
		void WalkAnimate();

	}
}


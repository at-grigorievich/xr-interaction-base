using UnityEngine;

namespace ATG.Animator
{
	public class AnimatorService: IAnimatorService
	{
		public const float DefaultAnimatorSpeed = 1f;

		private UnityEngine.Animator _animator;
		private Transform _rigParent;

		private IAnimateableCallback _callbackService;

		private AnimateableServiceData _currentConfig;

		public UnityEngine.Animator Animator { get { return _animator; } }
		public IAnimateableCallback CallbackService { get { return _callbackService; } }

		public Transform RigParent { get { return _rigParent; } }

        public AnimatorEnum LastCrossfadeTag { get; private set; }

        public bool IsActive {get; private set;}

        public AnimatorService(UnityEngine.Animator animator, Transform rigParent, 
		                       IAnimateableCallback callback,
		                       AnimateableServiceData animatorConfig)
		{
			_currentConfig = animatorConfig;
			
			_animator = animator;
			_rigParent = rigParent;

            _callbackService = callback;
		}
        
        public void UpdateAnimatorConfig(AnimateableServiceData animatorConfig)
        {
            _currentConfig = animatorConfig;
        }

        public void SetAnimatorSpeed(float speed)
        {
            _animator.speed = speed;
        }

		public void SetAnimatorLayerWeight(int layer, float weight)
		{
			_animator.SetLayerWeight (layer, weight);
		}

        public void CrossFadeAnimate(AnimatorEnum tag, object value = null, float startTime = 0f, bool ignoreRepeat = false)
		{
			if (_currentConfig == null) 
			{
				throw new System.NullReferenceException("AnimateableServiceData is NULL !");
			}

			if(tag == LastCrossfadeTag && ignoreRepeat == true) return;

			if (_animator != null)
			{
				_currentConfig.PlayCrossFade(_animator, tag, value, startTime);

				LastCrossfadeTag = tag;
			}
		}

        public AnimatorStateInfo GetCurrentStateInfo(int layer)
        {
            if (layer > _animator.layerCount)
                throw new System.IndexOutOfRangeException("Animator hasn't " + layer + " layer");
            return _animator.GetCurrentAnimatorStateInfo(layer);
        }

		public void RunAnimate()
		{
            _currentConfig.PlayCrossFade(_animator, AnimatorEnum.Idle, false);
            _currentConfig.PlayCrossFade(_animator, AnimatorEnum.Run, true);
            _currentConfig.PlayCrossFade(_animator, AnimatorEnum.Walk, false);
		}

		public void IdleAnimate()
		{
			_currentConfig.PlayCrossFade(_animator, AnimatorEnum.Idle, true);
			_currentConfig.PlayCrossFade(_animator, AnimatorEnum.Run, false);
			_currentConfig.PlayCrossFade (_animator, AnimatorEnum.Walk, false);
		}

		public void WalkAnimate()
		{
            _currentConfig.PlayCrossFade(_animator, AnimatorEnum.Idle, false);
            _currentConfig.PlayCrossFade(_animator, AnimatorEnum.Run, false);
            _currentConfig.PlayCrossFade(_animator, AnimatorEnum.Walk, true);
		}

        public void SetActive(bool isActive)
        {
			IsActive = isActive;

			if (_animator != null)
			{
				_animator.enabled = isActive;
			}
        }
    }
}

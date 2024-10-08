using System;

namespace ATG.Animator
{
	public class AnimatorEventArgs: EventArgs
	{
		public readonly string EventName;

		public AnimatorEventArgs(string data)
		{
			EventName = data;
		}
	}

	public interface IAnimateableCallback
	{
		event EventHandler<AnimatorEventArgs> OnAnimatorReceived;

		void AnimatorReceive(string obj);
	}
}


using System;
using UnityEngine;

namespace ATG.Quiz
{
    [Serializable]
    public class QuizAnswerCreator
    {
        [SerializeField, TextArea] private string answerText;
        [SerializeField] private bool isTrueAnswer;

        public QuizAnswer Create(byte index) => new(index, answerText, isTrueAnswer);
    }

    public readonly struct QuizAnswerResult
    {
        public int Index {get;}
        public bool AnswerVariant {get;}

        public QuizAnswerResult(int index, bool answer)
        {
            Index = index;
            AnswerVariant = answer;
        }
    }

    

    public readonly struct QuizAnswer
    {
        public byte Index { get; }
        public string Text { get; }

        public bool IsTrue { get; }

        public QuizAnswer(byte index, string text, bool isTrue)
        {
            Index = index;
            Text = text;

            IsTrue = isTrue;
        }
    }
}
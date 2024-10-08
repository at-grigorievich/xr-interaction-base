using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ATG.Quiz
{
    [Serializable]
    public sealed class QuizQuestionCreator
    {
        [SerializeField, TextArea] private string questionText;
        [SerializeField] private float points = 1f; // очков за верный ответ
        [Space(10)]
        [SerializeField] private QuizAnswerCreator[] answers;

        public QuizQuestion Create(byte index)
        {
            HashSet<QuizAnswer> answersSet = new();

            for(byte i = 0; i < answers.Length; i++)
            {
                answersSet.Add(answers[i].Create(i));
            }

            return new QuizQuestion(index, questionText, answersSet, points);
        }
    }
        
    public readonly struct QuizQuestion
    {
        public byte Index {get;}
        public string Text {get;}
        public float Points {get;} // очков за верный ответ
        
        public HashSet<QuizAnswer> Answers {get;}

        public int TrueAnswersCount => Answers.Count(answer => answer.IsTrue == true);

        public QuizQuestion(byte index, string text, HashSet<QuizAnswer> quizAnswers, float points)
        {
            Index = index;
            Text = text;

            Answers = quizAnswers;

            Points = points;
        }

        public static QuizQuestion None => new QuizQuestion(0, string.Empty, null, 0);
    }
}
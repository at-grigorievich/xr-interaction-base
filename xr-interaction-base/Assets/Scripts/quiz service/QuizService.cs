using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace ATG.Quiz
{
    public sealed class QuizService : IQuizService
    {
        private readonly QuizData _config;

        private readonly int _needQuestsCount;

        private readonly List<float> _rateByQuests;

        private Queue<QuizQuestion> _questionsQueue;

        private QuizQuestion? _currentQuest;

        private HashSet<QuizQuestion> _questions;
        
        public event Action OnCompleteQuest;

        public int CurrentQuestIndex => _currentQuest.HasValue ? _currentQuest.Value.Index : 0;

        public int TotalQuestCount => _needQuestsCount;

        public int TrueQuestionsCount { get; private set; }
        public int TotalTrueQuestionsCount => _questions.Sum(question => question.TrueAnswersCount);

        public bool IsActive { get; private set; }

        public QuizService(QuizData config, int needQuestsCount)
        {
            _config = config;
            _needQuestsCount = needQuestsCount;

            _rateByQuests = new();
        }

        public void SetActive(bool isActive)
        {
            IsActive = isActive;

            if(IsActive == true)
            {
                _questions = new HashSet<QuizQuestion>(_config.GetRandomQuests(_needQuestsCount));
                _questionsQueue = new Queue<QuizQuestion>(_questions);
            }
            else
            {
                TrueQuestionsCount = 0;
            }
        }

        public bool TryGetNextQuestion(out QuizQuestion question)
        {
           if(IsActive == false)
           {
                question = QuizQuestion.None;
                return false;
           }
           
           if(_questionsQueue.Count <= 0)
           {
                question = QuizQuestion.None;
                OnCompleteQuest?.Invoke();
                return false;
           }

           _currentQuest = _questionsQueue.Dequeue();
           question = _currentQuest.Value;

           return true;
        }

        public IReadOnlyCollection<QuizAnswerResult> CheckQuestionAnswers(IReadOnlyCollection<QuizAnswerResult> userResults)
        {
            if(_currentQuest.HasValue == false)
                throw new NullReferenceException("no currect quest");
            var quest = _currentQuest.Value;

            if(userResults.Count != quest.Answers.Count)
                throw new ArgumentOutOfRangeException("difference between user results count and question answers count");

            QuizAnswerResult[] userResultsArr = userResults.ToArray();
            QuizAnswerResult[] resposeResults = new QuizAnswerResult[quest.Answers.Count];

            float rateByOne = 1f / quest.TrueAnswersCount;
            float rateForQuestion = 0f;

            foreach(var answer in quest.Answers)
            {
                int index = answer.Index;

                var userAnswer = userResultsArr[index];

                rateForQuestion += userAnswer.AnswerVariant == true && answer.IsTrue ? rateByOne : 0;
                TrueQuestionsCount += userAnswer.AnswerVariant == true && answer.IsTrue ? 1 : 0;

                resposeResults[index] = new QuizAnswerResult(index, answer.IsTrue);
            }

            //Debug.Log(rateForQuestion);
            _rateByQuests.Add(rateForQuestion);

            return resposeResults;
        }

        public float CalculateGraduate()
        {
            float graduate = Mathf.Clamp(_rateByQuests.Sum(), 0f, 5f);

            return (float)Math.Round(graduate, MidpointRounding.AwayFromZero);
        }
    }
}
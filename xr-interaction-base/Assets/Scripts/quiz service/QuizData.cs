using System.Collections.Generic;
using UnityEngine;

namespace ATG.Quiz
{
    [CreateAssetMenu(menuName = "configs/quiz", fileName = "new_quiz_config")]
    public sealed class QuizData: ScriptableObject
    {
        [SerializeField] private QuizQuestionCreator[] questions;

        public IReadOnlyCollection<QuizQuestion> GetRandomQuests(int count)
        {
            HashSet<QuizQuestion> result = new HashSet<QuizQuestion>(count);
            
            var creators = GetRandomQuestionContainers(count);

            byte counter = 1;
            
            foreach(var creator in creators)
            {
                result.Add(creator.Create(counter));
                counter++;
            }

            return result;
        }

        public IReadOnlyCollection<QuizQuestionCreator> GetRandomQuestionContainers(int count)
        {
            HashSet<QuizQuestionCreator> creators = new HashSet<QuizQuestionCreator>();

            while(creators.Count < count)
            {
                QuizQuestionCreator res = GetRandomQuestionContainer();

                if(creators.Contains(res) == false)
                {
                    creators.Add(res);
                }
            }

            return creators;
        }

        private QuizQuestionCreator GetRandomQuestionContainer()
        {
            int rnd = UnityEngine.Random.Range(0, questions.Length);

            return questions[rnd];
        }
    }
}
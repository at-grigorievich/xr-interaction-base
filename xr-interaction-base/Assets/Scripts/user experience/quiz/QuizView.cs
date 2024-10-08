using System;
using System.Collections.Generic;
using System.Linq;
using ATG.Quiz;
using TMPro;
using UnityEngine;
using VContainer;

namespace ATG.UI
{
    public sealed class QuizView : UIView
    {
        public const string AnswerText = "Ответить";
        public const string NextText = "Далее";
        
        public const string ResultOutput = "Вы верно ответили на <color=green>{0}/{1}</color> вопросов.";

        public const string MoreAnswers = "Выберите несколько ответов";
        public const string OneAnswer = "Выберите только один ответ";

        [SerializeField] private GameObject questionPlaceholder;
        [SerializeField] private GameObject answersPlaceholder;
        [SerializeField] private GameObject resultPlaceholder;
        [SerializeField] private TextMeshProUGUI questionNumberOutput;
        [SerializeField] private TextMeshProUGUI questionAnswerCountOutput;
        [SerializeField] private TextMeshProUGUI questionTextOutput;
        [Space(5)]
        [SerializeField] private AnswerElementCreator answerElementCreator;
        [SerializeField] private ScaleClickButton nextButton;
        [Space(5)]
        [SerializeField] private TextMeshProUGUI graduateOutput;
        [Space(10)]
        [SerializeField] private bool isHideOnAwake;

        [Inject] private IQuizService _quizService;

        private QuizQuestion _currentQuestion;

        private List<AnswerElement> _answerInstances;

        public override UIElementType ViewType => UIElementType.Quiz;

        public event Action OnComplete;

        private void Start()
        {
            if(isHideOnAwake == true)
                Hide();
        }

        public override void Show(object sender, object data)
        {
            base.Show(sender, data);
            _quizService.SetActive(true);

            UpdateQuestion();
        }

        public override void Hide()
        {
            base.Hide();

            _quizService.SetActive(false);

            nextButton.Hide();

            if(_answerInstances != null)
            {
                foreach(var answerElement in _answerInstances)
                {
                    answerElement.OnSelected -= OnAnswerUserSelected;
                    answerElement.Hide();
                }
            }
        }

        private void UpdateQuestion()
        {
            if(_quizService.TryGetNextQuestion(out _currentQuestion) == true)
            {
                questionPlaceholder.SetActive(true);
                answersPlaceholder.SetActive(true);
                resultPlaceholder.SetActive(false);

                UpdateQuiestionNumberOutput();
                UpdateQuestionTextOutput();

                UpdateAnswers();
                
                Action userSelectAnswer = OnUserSelectAnswerHandler;

                nextButton.UpdateText(AnswerText);
                nextButton.Show(this, userSelectAnswer);
            }
            else ShowResult();
        }

        private void ShowResult()
        {
            nextButton.Hide();
            
            questionPlaceholder.SetActive(false);
            answersPlaceholder.SetActive(false);
            resultPlaceholder.SetActive(true);

            graduateOutput.text = _quizService.CalculateGraduate().ToString();

            string result = string.Format(ResultOutput, _quizService.TrueQuestionsCount,
                 _quizService.TotalTrueQuestionsCount);

            OnComplete?.Invoke();
        }

        private void OnUserSelectAnswerHandler()
        {
            if(Array.TrueForAll(_answerInstances.ToArray(), a => a.IsSelected == false) == true)
            {
                return;
            }

            nextButton.Hide();

            if(_answerInstances == null) return;
            
            foreach(var answer in _answerInstances)
            {
                answer.OnSelected -= OnAnswerUserSelected;
            }

            QuizAnswerResult[] userResults = new QuizAnswerResult[_answerInstances.Count];

            for(int i = 0; i < _answerInstances.Count; i++)
            {
                userResults[i] = _answerInstances[i].GetUserAnswer();
            }

            QuizAnswerResult[] requestResult = _quizService.CheckQuestionAnswers(userResults).ToArray();   
            ShowQuestionResult(requestResult);
        }

        private void ShowQuestionResult(QuizAnswerResult[] requestResults)
        {
            int counter = 0;

            foreach(var answerInstance in _answerInstances)
            {
                answerInstance.ShowResult(requestResults[counter]);
                counter++;
            }

            Action showNextQuestion = UpdateQuestion;

            nextButton.UpdateText(NextText);
            nextButton.Show(this, showNextQuestion);
        }

        private void UpdateAnswers()
        {
            int needAnswerElements = _currentQuestion.Answers.Count;

            int TrueAnswersCount = _currentQuestion.TrueAnswersCount;

            _answerInstances = new List<AnswerElement>(answerElementCreator.GetAnswers(needAnswerElements));

            int count = 0;

            foreach(var answer in _currentQuestion.Answers)
            {
                _answerInstances[count].Show(this, answer);

                // Если можно выбрать только один верный ответ, то остальные должны автоматически отключиться
                if(TrueAnswersCount == 1)
                {
                    _answerInstances[count].OnSelected += OnAnswerUserSelected;
                }

                count++;
            }
        }

        private void UpdateQuiestionNumberOutput()
        {
            int currentIndex = _currentQuestion.Index;
            int totalIndex = _quizService.TotalQuestCount;

            questionNumberOutput.text = $"Вопрос {currentIndex} / {totalIndex}";

            if (_currentQuestion.TrueAnswersCount == 0)
                throw new IndexOutOfRangeException("Answers must be more than 0!");

            questionAnswerCountOutput.text = _currentQuestion.TrueAnswersCount > 1 ? MoreAnswers : OneAnswer;
        }

        private void UpdateQuestionTextOutput()
        {
            questionTextOutput.text = _currentQuestion.Text;
        }

        private void OnAnswerUserSelected(AnswerElement selected)
        {
            foreach(var answer in _answerInstances)
            {
                if(ReferenceEquals(answer, selected) == true) continue;

                answer.Unselect();
            }
        }
    }
}
using System;
using System.Collections.Generic;
using ATG.Quiz;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace ATG.UI
{
    [Serializable]
    public class AnswerElementCreator
    {
        [SerializeField] private AnswerElement prefab;
        [SerializeField] private RectTransform root;

        public IReadOnlyCollection<AnswerElement> GetAnswers(int count)
        {
            AnswerElement[] activeElements = root.GetComponentsInChildren<AnswerElement>();
            
            if(activeElements.Length == count) return activeElements;

            List<AnswerElement> result = new (count);

            int counter = 0;

            while(result.Count < count)
            {
                if(counter < activeElements.Length)
                {
                    result.Add(activeElements[counter]);
                }
                else
                {
                    result.Add(Create());
                }
                counter++;
            }

            if(counter < activeElements.Length)
            {
                for(int i = counter; i < activeElements.Length; i++)
                {
                    activeElements[i].Hide();
                }
            }

            return result;
        }

        private AnswerElement Create()
        {
            AnswerElement instance = GameObject.Instantiate(prefab, root);
            return instance;  
        }
    }

    [RequireComponent(typeof(ISkinSwitcher))]
    public class AnswerElement : UIElement, IPointerEnterHandler, IPointerExitHandler,
        IPointerClickHandler
    {
        [SerializeField] private TextMeshProUGUI textOutput;
        
        [SerializeField] private GameObject trueInfoPanel;
        [SerializeField] private GameObject falseInfoPanel;

        private ISkinSwitcher _skinSwitcher;

        private QuizAnswer? _currentAnswer;

        public bool IsSelected { get; private set; }

        public event Action<AnswerElement> OnSelected;

        private void Awake()
        {
            _skinSwitcher = GetComponent<ISkinSwitcher>();;
        }

        private void Start()
        {
            _skinSwitcher.SwitchToSkin(SkinType.None);
        }

        public override void SetActive(bool isActive)
        {
            base.SetActive(isActive);
            gameObject.SetActive(isActive);

        }

        public override void Show(object sender, object data)
        {
            base.Show(sender, data);

            Unselect();

            if(data is QuizAnswer answer)
            {
                _currentAnswer = answer;
                textOutput.text = _currentAnswer.Value.Text;
            }
            else throw new ArgumentOutOfRangeException("data is not quiz answer !"); 
        }

        public override void Hide()
        {
            base.Hide();

            _currentAnswer = null;
            IsSelected = false;
        }

        public QuizAnswerResult GetUserAnswer()
        {
            if(_currentAnswer.HasValue == false)
                throw new NullReferenceException("No question for answering");

            base.SetActive(false);
            return new QuizAnswerResult(_currentAnswer.Value.Index, IsSelected);
        }
        
        public void ShowResult(QuizAnswerResult requestResult)
        {
            if(_currentAnswer.HasValue == false)
                throw new NullReferenceException("No answer !");

            if(_currentAnswer.Value.Index != requestResult.Index)
                throw new ArgumentOutOfRangeException($"Difference between answer and result outputs!");
            
            SkinType nextSkin = SkinType.None;
            
            trueInfoPanel.SetActive(_currentAnswer.Value.IsTrue == true);
            falseInfoPanel.SetActive(_currentAnswer.Value.IsTrue == false);

            if(IsSelected == true)
            {
                nextSkin = requestResult.AnswerVariant == true ? SkinType.Third : SkinType.Fourth;
            }
            else if (requestResult.AnswerVariant == true)
            {
                nextSkin = SkinType.Fifth;
            }

            _skinSwitcher.SwitchToSkin(nextSkin);
        }

        public void Unselect()
        {
            IsSelected = false;
            _skinSwitcher.SwitchToSkin(SkinType.None);
            
            trueInfoPanel.SetActive(false);
            falseInfoPanel.SetActive(false);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            if (IsActive == false) return;

            if (IsSelected == true) return;
            _skinSwitcher.SwitchToSkin(SkinType.First);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (IsActive == false) return;

            if (IsSelected == true) return;
            _skinSwitcher.SwitchToSkin(SkinType.None);
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (IsActive == false) return;

            IsSelected = !IsSelected;

            if (IsSelected == true)
            {
                OnSelected?.Invoke(this);
                _skinSwitcher.SwitchToSkin(SkinType.Second);
            }
            else
            {
                _skinSwitcher.SwitchToSkin(SkinType.First);
            }
        }
    }
}
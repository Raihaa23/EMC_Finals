using Events;
using Managers;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GridManagement
{
    public class GridSquare : Selectable, IPointerClickHandler, ISubmitHandler, IPointerUpHandler,IPointerExitHandler
    {

        [SerializeField] private TMP_Text number_text;
        private int number_ = 0;
        private int correct_number_ = 0;

        private bool selected_ = false;
        private int square_index_ = -1;
        private bool has_default_value_ = false;
        private bool has_wrong_value_ = false;

        public bool HasWrongValue() { return has_default_value_; }
        public void SetHasDefaultValue(bool has_default) { has_default_value_ = has_default; }
        public bool GetHasDefaultValue() { return has_default_value_; }

        public bool IsSelected() { return selected_; } 
        public void SetSquareIndex(int index)
        {
            square_index_ = index;
        }

        public void SetCorrectNumber(int index)
        {
            correct_number_ = index;
            
            has_wrong_value_ = false;
        }

        public void DisplayText()
        {
            if (number_ <= 0)
                number_text.GetComponent<TMP_Text>().text = " ";
            else
                number_text.GetComponent<TMP_Text>().text = number_.ToString();
        }

        public void SetNumber(int number)
        {
            number_ = number;
            DisplayText();
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            selected_ = true;
            GameEvents.SquareSelecedMethod(square_index_);
            
        }

        public void OnSubmit(BaseEventData eventData)
        {

        }

        private void OnEnable()
        {
            GameEvents.OnUpdateSquareNumber += OnSetNumber;
            GameEvents.OnSquareSelected += OnSquareSelected;
            GameEvents.OnGetHint += GetHint;
        }

        private void OnDisable()
        {
            GameEvents.OnUpdateSquareNumber -= OnSetNumber;
            GameEvents.OnSquareSelected -= OnSquareSelected;
            GameEvents.OnGetHint -= GetHint;
        }

        public void OnSetNumber(int number)
        {
            if (selected_ && has_default_value_ == false)
            {
                SetNumber(number);
                if (number != correct_number_)
                {
                    has_wrong_value_ = true;
                    var colors = this.colors;
                    colors.normalColor = Color.red;
                    this.colors = colors;
                    ScoreManager.Instance.reduceScore();
                }
                else
                {
                    has_wrong_value_ = false;
                    has_default_value_ = true;
                    var colors = this.colors;
                    colors.normalColor = Color.white;
                    this.colors = colors;
                    ScoreManager.Instance.AddScore();
                    GameEvents.OnCorrectMethod();
                }
            }
        }

        public void GetHint()
        {
            if (selected_ && has_default_value_ == false)
            {
                SetNumber(correct_number_);
                has_wrong_value_ = false;
                has_default_value_ = true;
                var colors = this.colors;
                colors.normalColor = Color.white;
                this.colors = colors;
                GameEvents.OnCorrectMethod();
                GameEvents.OnReduceHintMethod();
            }
        }

        public void OnSquareSelected(int square_index)
        {
            if(square_index_ != square_index)
            {
                selected_ = false;
            }
        }

        public void SetSquareColour(Color col)
        {
            var colors = this.colors;
            colors.normalColor = col;
            this.colors = colors;
        }
    }
}


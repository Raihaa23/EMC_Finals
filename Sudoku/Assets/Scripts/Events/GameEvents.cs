using UnityEngine;

namespace Events
{
    public class GameEvents : MonoBehaviour
    {
        public delegate void UpdateSquareNumber(int number);
        public static event UpdateSquareNumber OnUpdateSquareNumber;

        public static void UpdateSquareNumberMethod(int number)
        {
            if (OnUpdateSquareNumber != null)
                OnUpdateSquareNumber(number);
        }

        public delegate void SquareSelected(int square_index);
        public static event SquareSelected OnSquareSelected;

        public static void SquareSelecedMethod(int square_index)
        {
            if (OnSquareSelected != null)
                OnSquareSelected(square_index);
        }

        public delegate void GetHint();

        public static event GetHint OnGetHint;

        public static void OnGetHintMethod()
        {
            if (OnGetHint != null)
            {
                OnGetHint();
            }
        }
        
        public delegate void ReduceHintCount();

        public static event ReduceHintCount OnReduceHint;

        public static void OnReduceHintMethod()
        {
            if (OnReduceHint != null)
            {
                OnReduceHint();
            }
        }
    
        public delegate void CorrectNumber(int squareIndex);
        public static event CorrectNumber OnCorrectNumber;

        public static void OnCorrectMethod(int squareIndex)
        {
            if (OnCorrectNumber != null)
            {
                OnCorrectNumber(squareIndex);
            }
        }

        public delegate void Victory();
        public static event Victory OnVictory;

        public static void OnVictoryMethod()
        {
            if (OnVictory != null)
            {
                OnVictory();
            }
        }
    }
}

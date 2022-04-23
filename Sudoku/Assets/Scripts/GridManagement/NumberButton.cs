using Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GridManagement
{
    public class NumberButton : Selectable, IPointerClickHandler, ISubmitHandler, IPointerUpHandler, IPointerExitHandler
    {
        public int value = 0;

        public void OnPointerClick(PointerEventData eventData)
        {
            GameEvents.UpdateSquareNumberMethod(value);
        }

        public void OnSubmit(BaseEventData eventData)
        {

        }
    }
}

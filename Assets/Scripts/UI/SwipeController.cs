using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
    public class SwipeController : MonoBehaviour, IEndDragHandler
    {

        [SerializeField] int maxPage;
        int currentPage;
        Vector3 targetPos;
        [SerializeField] Vector3 pageStep;
        [SerializeField] RectTransform characterPagesRect;

        [SerializeField] float tweenTime;
        [SerializeField] LeanTweenType tweenType;
        float dragThreshold;

        private void Awake()
        {
            currentPage = 1;
            targetPos = characterPagesRect.localPosition;
            dragThreshold = Screen.width / 15;
        }
        public void Next()
        {
            if (currentPage < maxPage)
            {
                currentPage++;
                targetPos += pageStep;
                MovePage();
            }
        }

        public void Previous()
        {
            if (currentPage > 1)
            {
                currentPage--;
                targetPos -= pageStep;
                MovePage();
            }
        }

        void MovePage()
        {

            characterPagesRect.LeanMoveLocal(targetPos, tweenTime).setEase(tweenType);
        }

        public void OnEndDrag(PointerEventData eventData)
        {

            if (Mathf.Abs(eventData.position.x - eventData.pressPosition.x) > dragThreshold)
            {
                if (eventData.position.x > eventData.pressPosition.x) Previous();
                else Next();
            }
            else
            {
                MovePage();
            }
        }
    }
}
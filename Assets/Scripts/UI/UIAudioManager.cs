using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI; // Required when using Event data.

namespace Managers.Audio
{
    public class UIAudioManager : MonoBehaviour, ISelectHandler, ISubmitHandler, IPointerEnterHandler, IPointerDownHandler, IMoveHandler, IEndDragHandler
    {
        public void Start()
        {
            if (CompareTag("sfxSlider") || CompareTag("musicSlider")) GetComponent<Slider>().onValueChanged.AddListener (delegate {SetVolume();});
            
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            EventSystem.current.SetSelectedGameObject(gameObject);
        }

        public void OnSelect(BaseEventData eventData)
        {
        }
        
        public void  OnMove(AxisEventData eventData)
        {
            if (CompareTag("sfxSlider")) SoundManager.Instance.PlaySound("event:/SFX/Dash", 4f);
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if(GetComponent<Button>() != null)
            {
                SoundManager.Instance.PlaySound("event:/SFX/Jump", 4f);
            }
            if (CompareTag("sfxSlider")) SoundManager.Instance.PlaySound("event:/SFX/Dash", 4f);
        }
        
        public void OnSubmit(BaseEventData eventData)
        {
            if(GetComponent<Button>() != null)
            {
                SoundManager.Instance.PlaySound("event:/SFX/Jump", 4f);
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (CompareTag("sfxSlider")) SoundManager.Instance.PlaySound("event:/SFX/Dash", 4f);
        }
        
        private void SetVolume()
        {
            if (CompareTag("sfxSlider")) SoundManager.Instance.sfxVolume = GetComponent<Slider>().value;
            if (CompareTag("musicSlider")) SoundManager.Instance.SetMusicVolume(GetComponent<Slider>().value);
        }
    }
}

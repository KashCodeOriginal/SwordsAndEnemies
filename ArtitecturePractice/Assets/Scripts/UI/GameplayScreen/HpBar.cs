using UnityEngine;
using UnityEngine.UI;

namespace UI.GameplayScreen
{
    public class HpBar : MonoBehaviour
    {
        [SerializeField] private Image _barImage;

        public void SetValue(float current, float max)
        {
            _barImage.fillAmount = current / max;
        }
    }
}
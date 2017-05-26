using UnityEngine;
using UnityEngine.UI;
namespace I2.Loc
{
	[AddComponentMenu("I2/Localization/SetLanguage")]
	public class SetLanguage : MonoBehaviour 
	{
        
		public string _Language;

#if UNITY_EDITOR
		public LanguageSource mSource;
#endif

        void OnClick()
		{
			ApplyLanguage();
        }

		public void ApplyLanguage()
		{
			if( LocalizationManager.HasLanguage(_Language))
			{
				LocalizationManager.CurrentLanguage = _Language;
			}
		}

        public void ChangeLanguage(int a)
        {
            switch(a)
            {
                case 0:
                    _Language = "English";
                    break;
                case 1:
                    _Language = "Vietnamese";
                    break;
            }
            
            ApplyLanguage();
        }
    }
}
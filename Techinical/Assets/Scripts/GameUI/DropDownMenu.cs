using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using I2;
public class DropDownMenu : MonoBehaviour
{
    public Dropdown dd;
    public I2.Loc.SetLanguage setLanguage;
	// Use this for initialization

    public void ChangeLanguage()
    {
        setLanguage.ChangeLanguage(dd.value);
    }
    
}

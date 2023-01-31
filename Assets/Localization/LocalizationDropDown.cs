using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Localization;
using UnityEngine.Localization.Settings;
using UnityEngine.UI;
public class LocalizationDropDown : MonoBehaviour
{
    public Dropdown myDropdown;

    // Start is called before the first frame update
    
    public void ChangeLanguage()
    {
       LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.Locales[myDropdown.value];

    }


}

using System.Collections.Generic;
using UnityEngine;

namespace CitrioN.SettingsMenuCreator
{
  [CreateAssetMenu(fileName = "SettingsSaver_Xml_",
                   menuName = "CitrioN/Settings Menu Creator/Settings Saver/XML",
                   order = 23)]
  public class SettingsSaver_Xml : SettingsSaver_File
  {
    protected override string FileExtension => "xml";

    protected override string GetSaveString(SettingsCollection collection, Dictionary<string, object> settingValues)
      => XmlUtility_Settings.GetSaveString(collection, settingValues);

    protected override Dictionary<string, object> LoadFromText(string text)
      => XmlUtility_Settings.LoadFromText(text);
  }
}
using CitrioN.Common;
using UnityEngine;

namespace CitrioN.SettingsMenuCreator
{
  [MenuPath("Display")]
  public class Setting_Fullscreen : Setting_Screen<bool>
  {
    public override string PropertyName => nameof(Screen.fullScreen);

    public Setting_Fullscreen()
    {
      defaultValue = true;
    }
  }

  [MenuPath("Display")]
  public class Setting_FullScreenMode : Setting_Screen<FullScreenMode>
  {
    public override string PropertyName => nameof(Screen.fullScreenMode);

    public Setting_FullScreenMode()
    {
      defaultValue = FullScreenMode.FullScreenWindow;
    }
  }

  [MenuPath("Display")]
  public class Setting_ScreenSleepTimeout : Setting_Screen<int>
  {
    public override string PropertyName => nameof(Screen.sleepTimeout);

    public Setting_ScreenSleepTimeout()
    {
      options.Add(new StringToStringRelation("-1", "Never"));
      options.Add(new StringToStringRelation("-2", "System Setting"));

      defaultValue = -1;
    }
  }

  #region Mobile Settings
  // TODO Enable once tested

  // TODO Only works on iOS?
  //[MenuPath("Display")]
  //public class Setting_Brightness : Setting_Screen<float>
  //{
  //  public override string PropertyName => nameof(Screen.brightness);

  //  public Setting_Brightness()
  //  {
  //    options.AddMinMaxRangeValues("0", "1");

  //    assignUnityValueAsDefault = true;
  //  }
  //}

  //[MenuPath("Display")]
  //public class Setting_ScreenOrientation : Setting_Screen<ScreenOrientation>
  //{
  //  public override string PropertyName => nameof(Screen.orientation);

  //  public Setting_ScreenOrientation()
  //  {
  //    assignUnityValueAsDefault = true;
  //  }
  //}

  //[MenuPath("Display")]
  //public class Setting_AutorotateToLandscapeLeft : Setting_Screen<bool>
  //{
  //  public override string PropertyName => nameof(Screen.autorotateToLandscapeLeft);

  //  public Setting_AutorotateToLandscapeLeft()
  //  {
  //    assignUnityValueAsDefault = true;
  //  }
  //}

  //[MenuPath("Display")]
  //public class Setting_AutorotateToLandscapeRight : Setting_Screen<bool>
  //{
  //  public override string PropertyName => nameof(Screen.autorotateToLandscapeRight);

  //  public Setting_AutorotateToLandscapeRight()
  //  {
  //    assignUnityValueAsDefault = true;
  //  }
  //}

  //[MenuPath("Display")]
  //public class Setting_AutorotateToPortrait : Setting_Screen<bool>
  //{
  //  public override string PropertyName => nameof(Screen.autorotateToPortrait);

  //  public Setting_AutorotateToPortrait()
  //  {
  //    assignUnityValueAsDefault = true;
  //  }
  //}

  //[MenuPath("Display")]
  //public class Setting_AutorotateToPortraitUpsideDown : Setting_Screen<bool>
  //{
  //  public override string PropertyName => nameof(Screen.autorotateToPortraitUpsideDown);

  //  public Setting_AutorotateToPortraitUpsideDown()
  //  {
  //    assignUnityValueAsDefault = true;
  //  }
  //}
  #endregion
}
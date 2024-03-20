using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dart
{
  public static class CForm
  {
    public static frmMain frmMain = null;
    public static frm_SystemSetting frmSystemSetting = null;
    public static UC_Dartboard ucDartBoard = null;

    public static string ProgramVer = "20180109";
    public static string LastModifier = "Hue";

    public static void Init(frmMain main)
    {
      frmMain = main;
      frmSystemSetting = new frm_SystemSetting();
    }
  }
}

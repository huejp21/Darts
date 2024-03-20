using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Runtime.InteropServices;

namespace Dart
{
  #region Parameter Type Class
  [Serializable]
  public class ClassSystemPara
  {
    public ClassSystemPara GetValues()
    {
      using (System.IO.MemoryStream stream = new System.IO.MemoryStream())
      {
        System.Runtime.Serialization.Formatters.Binary.BinaryFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
        formatter.Serialize(stream, this);
        stream.Position = 0;
        return (ClassSystemPara)formatter.Deserialize(stream);
      }
    } // Deep Copy
    public string Path_Sound_D_Bull { get; set; }
    public string Path_Sound_S_Bull { get; set; }
    public string Path_Sound_Single { get; set; }
    public string Path_Sound_Double { get; set; }
    public string Path_Sound_Triple { get; set; }
    public string Path_Sound_Miss { get; set; }

    public int Color_D_Bull { get; set; }
    public int Color_S_Bull { get; set; }
    public int Color_S_Odd { get; set; }
    public int Color_SP_Odd { get; set; }
    public int Color_S_Even { get; set; }
    public int Color_SP_Even { get; set; }
    public int Color_Miss { get; set; }
  }

  [Serializable]
  public class ClassUserPara
  {
    public ClassUserPara GetValues()
    {
      using (System.IO.MemoryStream stream = new System.IO.MemoryStream())
      {
        System.Runtime.Serialization.Formatters.Binary.BinaryFormatter formatter = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
        formatter.Serialize(stream, this);
        stream.Position = 0;
        return (ClassUserPara)formatter.Deserialize(stream);
      }
    } // Deep Copy
  }
  #endregion

  public static class CParamMgr
  {
    [DllImport("kernel32")]
    private static extern long WritePrivateProfileString(string section, string key, string val, string filePath);
    [DllImport("kernel32")]
    private static extern long GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

    public static ClassSystemPara cParaSys = new ClassSystemPara();
    public static ClassUserPara cParaUser = new ClassUserPara();

    #region Path
    public static string PATH_ROOT = "";

    public static string PATH_PARA_SYSTEM = "";
    public static string PATH_PARA_NAME_SYSTEM = "";

    public static string PATH_PARA_USER = "";
    public static string PATH_PARA_NAME_USER = "";

    #endregion

    public static void Init()// Initial
    {
      string[] temp = System.Windows.Forms.Application.StartupPath.Split('\\');
      for (int i = 0; i < temp.Length - 3; i++)
      {
        PATH_ROOT += (temp[i] + "\\");
      }

      PATH_PARA_SYSTEM = PATH_ROOT + "Parameter\\";
      PATH_PARA_NAME_SYSTEM = "System.ini";

      PATH_PARA_USER = PATH_ROOT + "Parameter\\";
      PATH_PARA_NAME_USER = "User.ini";

      cParaSys.Path_Sound_D_Bull = PATH_ROOT + "Sound\\" + "BullsEye.mp3";
      cParaSys.Path_Sound_S_Bull = PATH_ROOT + "Sound\\" + "BullsEye.mp3";
      cParaSys.Path_Sound_Single = PATH_ROOT + "Sound\\" + "HitCan.mp3";
      cParaSys.Path_Sound_Double = PATH_ROOT + "Sound\\" + "ElectricBoom.mp3";
      cParaSys.Path_Sound_Triple = PATH_ROOT + "Sound\\" + "Boom.mp3";
      cParaSys.Path_Sound_Miss = PATH_ROOT + "Sound\\" + "Miss.mp3";

      cParaSys.Color_D_Bull = System.Drawing.Color.Black.ToArgb();
      cParaSys.Color_S_Bull = System.Drawing.Color.Red.ToArgb();
      cParaSys.Color_S_Odd = System.Drawing.Color.White.ToArgb();
      cParaSys.Color_SP_Odd = System.Drawing.Color.Lime.ToArgb();
      cParaSys.Color_S_Even = System.Drawing.Color.Black.ToArgb();
      cParaSys.Color_SP_Even = System.Drawing.Color.Red.ToArgb();
      cParaSys.Color_Miss = System.Drawing.Color.Gray.ToArgb();

      Load_System();
      Load_User();
    }

    public static bool Save_System() // Save Parameter System
    {
      string strPath = PATH_PARA_SYSTEM + PATH_PARA_NAME_SYSTEM;
      string strSection = "";
      string strKey = "";

      if (!System.IO.Directory.Exists(PATH_PARA_SYSTEM))
      {
        System.IO.Directory.CreateDirectory(PATH_PARA_SYSTEM);
      }

      var properties = cParaSys.GetType().GetProperties();
      foreach (System.Reflection.PropertyInfo property in properties)
      {
        try
        {
          strKey = property.Name;
          var val = property.GetValue(cParaSys, null);
          if (property.PropertyType.IsArray == true)
          {
            string strData = "";
            Array arr = property.GetValue(cParaSys, null) as Array;
            for (int i = 0; i < arr.Length; i++)
            {
              if (i == 0)
              {
                strData += arr.GetValue(i).ToString();
              }
              else
              {
                strData += ("," + arr.GetValue(i).ToString());
              }
            }
            if (0 == WritePrivateProfileString(strSection, strKey, strData, strPath)) { return false; };
          }
          else
          {
            if (0 == WritePrivateProfileString(strSection, strKey, val.ToString(), strPath)) { return false; };
          }
        }
        catch (Exception ex)
        {
          //?????Log
          return false;
        }
      }
      return true;
    }
    public static bool Load_System() // Load Parameter System
    {
      string strPath = PATH_PARA_SYSTEM + PATH_PARA_NAME_SYSTEM;
      string strSection = "";
      string strKey = "";
      int iSize = 32768;
      StringBuilder sbBuffer = new StringBuilder(iSize);

      if (!System.IO.Directory.Exists(PATH_PARA_SYSTEM))
      {
        System.IO.Directory.CreateDirectory(PATH_PARA_SYSTEM);
      }
      if (!System.IO.File.Exists(strPath))
      {
        return Save_System();
      }

      var properties = cParaSys.GetType().GetProperties();
      foreach (System.Reflection.PropertyInfo property in properties)
      {
        try
        {
          strKey = property.Name;
          sbBuffer.Clear();
          GetPrivateProfileString(strSection, strKey, "(NONE)", sbBuffer, iSize, strPath);
          if (sbBuffer.ToString().CompareTo("(NONE)") != 0)
          {
            if (property.PropertyType.IsArray == true)
            {
              var val = sbBuffer.ToString().Split(',');
              Array arr = property.GetValue(cParaSys, null) as Array;
              if (val.Length < arr.Length)
              {
                return false;
              }
              for (int i = 0; i < arr.Length; i++)
              {
                arr.SetValue(Convert.ChangeType(val[i], arr.GetType().GetElementType()), i);
              }
              property.SetValue(cParaSys, arr, null);
            }
            else
            {
              var value = Convert.ChangeType(sbBuffer.ToString(), property.PropertyType);
              property.SetValue(cParaSys, value, null);
            }
          }
        }
        catch (Exception ex)
        {
          //?????Log
          return false;
        }
      }
      return true;
    }

    public static bool Save_User() // Save Parameter System
    {
      string strPath = PATH_PARA_USER + PATH_PARA_NAME_USER;
      string strSection = "";
      string strKey = "";

      if (!System.IO.Directory.Exists(PATH_PARA_USER))
      {
        System.IO.Directory.CreateDirectory(PATH_PARA_USER);
      }

      var properties = cParaUser.GetType().GetProperties();
      foreach (System.Reflection.PropertyInfo property in properties)
      {
        try
        {
          strKey = property.Name;
          var val = property.GetValue(cParaUser, null);
          if (property.PropertyType.IsArray == true)
          {
            string strData = "";
            Array arr = property.GetValue(cParaUser, null) as Array;
            for (int i = 0; i < arr.Length; i++)
            {
              if (i == 0)
              {
                strData += arr.GetValue(i).ToString();
              }
              else
              {
                strData += ("," + arr.GetValue(i).ToString());
              }
            }
            if (0 == WritePrivateProfileString(strSection, strKey, strData, strPath)) { return false; };
          }
          else
          {
            if (0 == WritePrivateProfileString(strSection, strKey, val.ToString(), strPath)) { return false; };
          }
        }
        catch (Exception ex)
        {
          //?????Log
          return false;
        }
      }
      return true;
    }
    public static bool Load_User() // Load Parameter System
    {
      string strPath = PATH_PARA_USER + PATH_PARA_NAME_USER;
      string strSection = "";
      string strKey = "";
      int iSize = 32768;
      StringBuilder sbBuffer = new StringBuilder(iSize);

      if (!System.IO.Directory.Exists(PATH_PARA_USER))
      {
        System.IO.Directory.CreateDirectory(PATH_PARA_USER);
      }
      if (!System.IO.File.Exists(strPath))
      {
        return Save_User();
      }

      var properties = cParaUser.GetType().GetProperties();
      foreach (System.Reflection.PropertyInfo property in properties)
      {
        try
        {
          strKey = property.Name;
          sbBuffer.Clear();
          GetPrivateProfileString(strSection, strKey, "(NONE)", sbBuffer, iSize, strPath);
          if (sbBuffer.ToString().CompareTo("(NONE)") != 0)
          {
            if (property.PropertyType.IsArray == true)
            {
              var val = sbBuffer.ToString().Split(',');
              Array arr = property.GetValue(cParaUser, null) as Array;
              if (val.Length < arr.Length)
              {
                return false;
              }
              for (int i = 0; i < arr.Length; i++)
              {
                arr.SetValue(Convert.ChangeType(val[i], arr.GetType().GetElementType()), i);
              }
              property.SetValue(cParaUser, arr, null);
            }
            else
            {
              var value = Convert.ChangeType(sbBuffer.ToString(), property.PropertyType);
              property.SetValue(cParaUser, value, null);
            }
          }
        }
        catch (Exception ex)
        {
          //?????Log
          return false;
        }
      }
      return true;
    }


  }
}

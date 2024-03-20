using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dart
{
  public partial class frm_SystemSetting : Form
  {
    private ClassSystemPara cTempParaSys = CParamMgr.cParaSys.GetValues();
    private ClassUserPara cTempParaUser = CParamMgr.cParaUser.GetValues();

    private System.Threading.Thread th = null;
    private bool bThread = false;
    private bool bSoundOn = false;
    private string strSoundPath = "";

    public void Thread_Start()
    {
      if (th == null)
      {
        bThread = true;
        th = new System.Threading.Thread(Run);
        th.IsBackground = true;
        th.Start();
      }
    } // Start and Check Thread

    public void Thread_Abort()
    {
      th.Abort();
      bThread = false;
      th = null;
    } // Kill All Thread

    private void Run()
    {
      while (bThread)
      {
        lock (this)
        {
          System.Threading.Thread.Sleep(1);
          if (bSoundOn)
          {
            bSoundOn = false;
            try
            {
              MediaPlayer.MediaPlayerClass mpc = new MediaPlayer.MediaPlayerClass();
              mpc.FileName = strSoundPath;
              mpc.Play();
              //System.Media.SoundPlayer sd = new System.Media.SoundPlayer(strSoundPath);
              //sd.Play();
            }
            catch (Exception ex)
            {
              continue;
            }
          }
        }
      }
    }

    public frm_SystemSetting()
    {
      InitializeComponent();
    }

    private void frm_SystemSetting_Shown(object sender, EventArgs e)
    {
      Thread_Start();

      cTempParaSys = CParamMgr.cParaSys.GetValues();
      cTempParaUser = CParamMgr.cParaUser.GetValues();

      Update_Color();
      Update_Sound();
    }

    private void Update_Color()
    {
      btn_Color_D_Bull.BackColor = Color.FromArgb(cTempParaSys.Color_D_Bull);
      btn_Color_S_Bull.BackColor = Color.FromArgb(cTempParaSys.Color_S_Bull);
      btn_Color_S_Odd.BackColor = Color.FromArgb(cTempParaSys.Color_S_Odd);
      btn_Color_SP_Odd.BackColor = Color.FromArgb(cTempParaSys.Color_SP_Odd);
      btn_Color_S_Even.BackColor = Color.FromArgb(cTempParaSys.Color_S_Even);
      btn_Color_SP_Even.BackColor = Color.FromArgb(cTempParaSys.Color_SP_Even);
      btn_Color_Miss.BackColor = Color.FromArgb(cTempParaSys.Color_Miss);

      lbl_Color_D_Bull.Text = string.Format("{0},{1},{2}", Color.FromArgb(cTempParaSys.Color_D_Bull).R, Color.FromArgb(cTempParaSys.Color_D_Bull).G, Color.FromArgb(cTempParaSys.Color_D_Bull).B);
      lbl_Color_S_Bull.Text = string.Format("{0},{1},{2}", Color.FromArgb(cTempParaSys.Color_S_Bull).R, Color.FromArgb(cTempParaSys.Color_S_Bull).G, Color.FromArgb(cTempParaSys.Color_S_Bull).B);
      lbl_Color_S_Odd.Text = string.Format("{0},{1},{2}", Color.FromArgb(cTempParaSys.Color_S_Odd).R, Color.FromArgb(cTempParaSys.Color_S_Odd).G, Color.FromArgb(cTempParaSys.Color_S_Odd).B);
      lbl_Color_SP_Odd.Text = string.Format("{0},{1},{2}", Color.FromArgb(cTempParaSys.Color_SP_Odd).R, Color.FromArgb(cTempParaSys.Color_SP_Odd).G, Color.FromArgb(cTempParaSys.Color_SP_Odd).B);
      lbl_Color_S_Even.Text = string.Format("{0},{1},{2}", Color.FromArgb(cTempParaSys.Color_S_Even).R, Color.FromArgb(cTempParaSys.Color_S_Even).G, Color.FromArgb(cTempParaSys.Color_S_Even).B);
      lbl_Color_SP_Even.Text = string.Format("{0},{1},{2}", Color.FromArgb(cTempParaSys.Color_SP_Even).R, Color.FromArgb(cTempParaSys.Color_SP_Even).G, Color.FromArgb(cTempParaSys.Color_SP_Even).B);
      lbl_Color_Miss.Text = string.Format("{0},{1},{2}", Color.FromArgb(cTempParaSys.Color_Miss).R, Color.FromArgb(cTempParaSys.Color_Miss).G, Color.FromArgb(cTempParaSys.Color_Miss).B);
    }

    private void Update_Sound()
    {
      txt_Sound_D_Bull.Text = cTempParaSys.Path_Sound_D_Bull;
      txt_Sound_S_Bull.Text = cTempParaSys.Path_Sound_S_Bull;
      txt_Sound_Single.Text = cTempParaSys.Path_Sound_Single;
      txt_Sound_Double.Text = cTempParaSys.Path_Sound_Double;
      txt_Sound_Triple.Text = cTempParaSys.Path_Sound_Triple;
      txt_Sound_Miss.Text = cTempParaSys.Path_Sound_Miss;
    }

    private void btn_Sound_Browse_Click(object sender, EventArgs e)
    {
      Control btn = (Control)sender;
      string name = btn.Name;

      Control txt = null;
      string Path = "";
      switch (name)
      {
        case "btn_Sound_D_Bull": Path = cTempParaSys.Path_Sound_D_Bull; txt = txt_Sound_D_Bull; break;
        case "btn_Sound_S_Bull": Path = cTempParaSys.Path_Sound_S_Bull; txt = txt_Sound_S_Bull; break;
        case "btn_Sound_Single": Path = cTempParaSys.Path_Sound_Single; txt = txt_Sound_Single; break;
        case "btn_Sound_Double": Path = cTempParaSys.Path_Sound_Double; txt = txt_Sound_Double; break;
        case "btn_Sound_Triple": Path = cTempParaSys.Path_Sound_Triple; txt = txt_Sound_Triple; break;
        case "btn_Sound_Miss": Path = cTempParaSys.Path_Sound_Miss; txt = txt_Sound_Miss; break;
        default:
          return;
      }

      try
      {
        OpenFileDialog ofDlg = new OpenFileDialog();
        ofDlg.InitialDirectory = System.IO.Directory.GetParent(Path).ToString();
        ofDlg.Filter = "mp3 files (*.mp3)|*.txt|All files (*.*)|*.*";
        ofDlg.FilterIndex = 2;
        ofDlg.RestoreDirectory = true;
        if (ofDlg.ShowDialog() == DialogResult.OK)
        {
          Path = ofDlg.FileName;
          txt.Text = Path;
        }
      }
      catch (Exception ex)
      {
        //????? Log
      }

      switch (name)
      {
        case "btn_Sound_D_Bull": cTempParaSys.Path_Sound_D_Bull= Path; break;
        case "btn_Sound_S_Bull": cTempParaSys.Path_Sound_S_Bull= Path; break;
        case "btn_Sound_Single": cTempParaSys.Path_Sound_Single= Path; break;
        case "btn_Sound_Double": cTempParaSys.Path_Sound_Double= Path; break;
        case "btn_Sound_Triple": cTempParaSys.Path_Sound_Triple= Path; break;
        case "btn_Sound_Miss": cTempParaSys.Path_Sound_Miss = Path; break;
        default:
          return;
      }

    }

    private void lbl_Sound_Title_Click(object sender, EventArgs e)
    {
      Control btn = (Control)sender;
      string name = btn.Name;

      string Path = "";
      switch (name)
      {
        case "lbl_Sound_D_Bull": Path = cTempParaSys.Path_Sound_D_Bull; break;
        case "lbl_Sound_S_Bull": Path = cTempParaSys.Path_Sound_S_Bull; break;
        case "lbl_Sound_Single": Path = cTempParaSys.Path_Sound_Single; break;
        case "lbl_Sound_Double": Path = cTempParaSys.Path_Sound_Double; break;
        case "lbl_Sound_Triple": Path = cTempParaSys.Path_Sound_Triple; break;
        case "lbl_Sound_Miss": Path = cTempParaSys.Path_Sound_Miss; break;
        default:
          return;
      }

      strSoundPath = Path;
      bSoundOn = true;
    }

    private void btn_Color_Click(object sender, EventArgs e)
    {
      Control btn = (Control)sender;
      string name = btn.Name;

      Control lbl = null;
      Color color = Color.Transparent;
      switch (name)
      {
        case "btn_Color_D_Bull": color = Color.FromArgb(cTempParaSys.Color_D_Bull); lbl = lbl_Color_D_Bull; break;
        case "btn_Color_S_Bull": color = Color.FromArgb(cTempParaSys.Color_S_Bull); lbl = lbl_Color_S_Bull; break;
        case "btn_Color_S_Odd": color = Color.FromArgb(cTempParaSys.Color_S_Odd); lbl = lbl_Color_S_Odd; break;
        case "btn_Color_SP_Odd": color = Color.FromArgb(cTempParaSys.Color_SP_Odd); lbl = lbl_Color_SP_Odd; break;
        case "btn_Color_S_Even": color = Color.FromArgb(cTempParaSys.Color_S_Even); lbl = lbl_Color_S_Even; break;
        case "btn_Color_SP_Even": color = Color.FromArgb(cTempParaSys.Color_SP_Even); lbl = lbl_Color_SP_Even; break;
        case "btn_Color_Miss": color = Color.FromArgb(cTempParaSys.Color_Miss); lbl = lbl_Sound_Miss; break;
        default:
          return;
      }
      try
      {
        ColorDialog cDlg = new ColorDialog();
        cDlg.Color = color;
        cDlg.AllowFullOpen = false;
        cDlg.ShowHelp = true;
        if (cDlg.ShowDialog() == DialogResult.OK)
        {
          color = cDlg.Color;
          lbl.BackColor = color;
        }
      }
      catch (Exception ex)
      {
        //????? Log
      }
      
      switch (name)
      {
        case "btn_Color_D_Bull": cTempParaSys.Color_D_Bull = color.ToArgb(); break;
        case "btn_Color_S_Bull": cTempParaSys.Color_S_Bull = color.ToArgb(); break;
        case "btn_Color_S_Odd": cTempParaSys.Color_S_Odd = color.ToArgb(); break;
        case "btn_Color_SP_Odd": cTempParaSys.Color_SP_Odd = color.ToArgb(); break;
        case "btn_Color_S_Even": cTempParaSys.Color_S_Even = color.ToArgb(); break;
        case "btn_Color_SP_Even": cTempParaSys.Color_SP_Even = color.ToArgb(); break;
        case "btn_Color_Miss": cTempParaSys.Color_Miss = color.ToArgb(); break;
        default:
          return;
      }
    }

    private void btn_Close_Click(object sender, EventArgs e)
    {
      this.Hide();
    }

    private void btn_Save_Click(object sender, EventArgs e)
    {
      if (MessageBox.Show(this, "Are you sure save?", "Information", MessageBoxButtons.OKCancel) != System.Windows.Forms.DialogResult.OK)
      { 
        return; 
      }
      CParamMgr.cParaSys = cTempParaSys.GetValues();
      if (CParamMgr.Save_System())
      {
        MessageBox.Show("OK");
      }
      else
      {
        MessageBox.Show("Fail");
      }
      
    }





  }
}

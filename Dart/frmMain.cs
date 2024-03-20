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
  public partial class frmMain : Form
  {
    private int blinkCount = 0;

    private System.Windows.Forms.Label[] Cricket_Player1_Label = new System.Windows.Forms.Label[7];
    private System.Windows.Forms.Label[] Cricket_Player2_Label = new System.Windows.Forms.Label[7];
    private System.Windows.Forms.Label[] Cricket_Player3_Label = new System.Windows.Forms.Label[7];
    private System.Windows.Forms.Label[] Cricket_Player4_Label = new System.Windows.Forms.Label[7];

    private System.Windows.Forms.Button[] Cricket_Player1_Button = new System.Windows.Forms.Button[7];
    private System.Windows.Forms.Button[] Cricket_Player2_Button = new System.Windows.Forms.Button[7];
    private System.Windows.Forms.Button[] Cricket_Player3_Button = new System.Windows.Forms.Button[7];
    private System.Windows.Forms.Button[] Cricket_Player4_Button = new System.Windows.Forms.Button[7];



    public frmMain()
    {
      InitializeComponent();

      CParamMgr.Init();
      CForm.Init(this);

#region For DartBoard
      Color color_D_Bull = Color.FromArgb(CParamMgr.cParaSys.Color_D_Bull);
      Color color_S_Bull = Color.FromArgb(CParamMgr.cParaSys.Color_S_Bull);
      Color color_Odd = Color.White;
      Color color_Even = Color.Black;
      Color color_Odd_SP = Color.Lime;
      Color color_Even_SP = Color.Red;
      Color color_Miss = Color.Gray;
      Point center = new Point(250, 250);
      int size = 500;

      CForm.ucDartBoard = new UC_Dartboard
        (
        center, 
        size, 
        new Point(0,0), 
        Color.FromArgb(CParamMgr.cParaSys.Color_D_Bull),
        Color.FromArgb(CParamMgr.cParaSys.Color_S_Bull),
        Color.FromArgb(CParamMgr.cParaSys.Color_S_Odd),
        Color.FromArgb(CParamMgr.cParaSys.Color_S_Even),
        Color.FromArgb(CParamMgr.cParaSys.Color_SP_Odd),
        Color.FromArgb(CParamMgr.cParaSys.Color_SP_Even), 
        Color.FromArgb(CParamMgr.cParaSys.Color_Miss)
        );

      this.Controls.Add(CForm.ucDartBoard);
#endregion


      Cricket_Player1_Label[0] = lbl_15_Player1;
      Cricket_Player1_Label[1] = lbl_16_Player1;
      Cricket_Player1_Label[2] = lbl_17_Player1;
      Cricket_Player1_Label[3] = lbl_18_Player1;
      Cricket_Player1_Label[4] = lbl_19_Player1;
      Cricket_Player1_Label[5] = lbl_20_Player1;
      Cricket_Player1_Label[6] = lbl_Bull_Player1;

      Cricket_Player2_Label[0] = lbl_15_Player2;
      Cricket_Player2_Label[1] = lbl_16_Player2;
      Cricket_Player2_Label[2] = lbl_17_Player2;
      Cricket_Player2_Label[3] = lbl_18_Player2;
      Cricket_Player2_Label[4] = lbl_19_Player2;
      Cricket_Player2_Label[5] = lbl_20_Player2;
      Cricket_Player2_Label[6] = lbl_Bull_Player2;

      Cricket_Player3_Label[0] = lbl_15_Player3;
      Cricket_Player3_Label[1] = lbl_16_Player3;
      Cricket_Player3_Label[2] = lbl_17_Player3;
      Cricket_Player3_Label[3] = lbl_18_Player3;
      Cricket_Player3_Label[4] = lbl_19_Player3;
      Cricket_Player3_Label[5] = lbl_20_Player3;
      Cricket_Player3_Label[6] = lbl_Bull_Player3;

      Cricket_Player4_Label[0] = lbl_15_Player4;
      Cricket_Player4_Label[1] = lbl_16_Player4;
      Cricket_Player4_Label[2] = lbl_17_Player4;
      Cricket_Player4_Label[3] = lbl_18_Player4;
      Cricket_Player4_Label[4] = lbl_19_Player4;
      Cricket_Player4_Label[5] = lbl_20_Player4;
      Cricket_Player4_Label[6] = lbl_Bull_Player4;

      Cricket_Player1_Button[0] = btn_15_Player1;
      Cricket_Player1_Button[1] = btn_16_Player1;
      Cricket_Player1_Button[2] = btn_17_Player1;
      Cricket_Player1_Button[3] = btn_18_Player1;
      Cricket_Player1_Button[4] = btn_19_Player1;
      Cricket_Player1_Button[5] = btn_20_Player1;
      Cricket_Player1_Button[6] = btn_Bull_Player1;

      Cricket_Player2_Button[0] = btn_15_Player2;
      Cricket_Player2_Button[1] = btn_16_Player2;
      Cricket_Player2_Button[2] = btn_17_Player2;
      Cricket_Player2_Button[3] = btn_18_Player2;
      Cricket_Player2_Button[4] = btn_19_Player2;
      Cricket_Player2_Button[5] = btn_20_Player2;
      Cricket_Player2_Button[6] = btn_Bull_Player2;

      Cricket_Player3_Button[0] = btn_15_Player3;
      Cricket_Player3_Button[1] = btn_16_Player3;
      Cricket_Player3_Button[2] = btn_17_Player3;
      Cricket_Player3_Button[3] = btn_18_Player3;
      Cricket_Player3_Button[4] = btn_19_Player3;
      Cricket_Player3_Button[5] = btn_20_Player3;
      Cricket_Player3_Button[6] = btn_Bull_Player3;

      Cricket_Player4_Button[0] = btn_15_Player4;
      Cricket_Player4_Button[1] = btn_16_Player4;
      Cricket_Player4_Button[2] = btn_17_Player4;
      Cricket_Player4_Button[3] = btn_18_Player4;
      Cricket_Player4_Button[4] = btn_19_Player4;
      Cricket_Player4_Button[5] = btn_20_Player4;
      Cricket_Player4_Button[6] = btn_Bull_Player4;

      CMaster.cGameMgr.Init_Data();
      CMaster.Start_All();
    }

    private void tmr_Monitor_Tick(object sender, EventArgs e)
    {
      tmr_Monitor.Enabled = false;
      blinkCount++;
      if (blinkCount <= 100)
      {
        blinkCount = 0;
      }
      lbl_Point_X.Text = string.Format("X:{0}", CForm.ucDartBoard.GetMouseLocation().X);
      lbl_Point_Y.Text = string.Format("Y:{0}", CForm.ucDartBoard.GetMouseLocation().Y);
      lbl_Current_Point.Text = CForm.ucDartBoard.Get_CurrentPoint().ToString();
      lbl_Current_Type.Text = CForm.ucDartBoard.Get_CurrentType().ToString();
      lbl_CurrerntRound.Text = CMaster.cGameMgr.GetRound().ToString();


      switch (CMaster.cGameMgr.GetPlayerType())
      {
        case PlayerType.VS_ONE:
          group_Player1.Visible = true;
          group_Player2.Visible = false;
          group_Player3.Visible = false;
          group_Player4.Visible = false;
          break;
        case PlayerType.VS_TWO:
          group_Player1.Visible = true;
          group_Player2.Visible = true;
          group_Player3.Visible = false;
          group_Player4.Visible = false;
          break;
        case PlayerType.VS_THR:
          group_Player1.Visible = true;
          group_Player2.Visible = true;
          group_Player3.Visible = true;
          group_Player4.Visible = false;
          break;
        case PlayerType.VS_FOU:
          group_Player1.Visible = true;
          group_Player2.Visible = true;
          group_Player3.Visible = true;
          group_Player4.Visible = true;
          break;
        case PlayerType.DOUBLE:
          group_Player1.Visible = true;
          group_Player2.Visible = true;
          group_Player3.Visible = true;
          group_Player4.Visible = true;
          break;
        default:
          break;
      }

      try
      {
        for (int i = 0; i < 7; i++)
        {
          Cricket_Player1_Button[i].ImageIndex = CMaster.cGameMgr.GetMark(Player.Player1, i);
          Cricket_Player2_Button[i].ImageIndex = CMaster.cGameMgr.GetMark(Player.Player2, i);
          Cricket_Player3_Button[i].ImageIndex = CMaster.cGameMgr.GetMark(Player.Player3, i);
          Cricket_Player4_Button[i].ImageIndex = CMaster.cGameMgr.GetMark(Player.Player4, i);
        }
      }
      catch (Exception ex)
      {
        string strMsg = string.Format("{0}\r\n{1}\r\n{2}", System.Reflection.MethodBase.GetCurrentMethod().Name, new System.Diagnostics.StackTrace(ex, true), ex);
        MessageBox.Show(strMsg);
      }

      switch (CMaster.cGameMgr.GetThrow())
      {
        case 0:
          lbl_throw1.ImageIndex = 0;
          lbl_throw2.ImageIndex = 0;
          lbl_throw3.ImageIndex = 0;
          break;
        case 1:
          lbl_throw1.ImageIndex = -1;
          lbl_throw2.ImageIndex = 0;
          lbl_throw3.ImageIndex = 0;
          break;
        case 2:
          lbl_throw1.ImageIndex = -1;
          lbl_throw2.ImageIndex = -1;
          lbl_throw3.ImageIndex = 0;
          break;
        case 3:
          lbl_throw1.ImageIndex = -1;
          lbl_throw2.ImageIndex = -1;
          lbl_throw3.ImageIndex = -1;
          break;
        default:
          lbl_throw1.ImageIndex = 0;
          lbl_throw2.ImageIndex = 0;
          lbl_throw3.ImageIndex = 0;
          break;
      }

      //lbl_throw1.ImageIndex = ;

      lbl_score_P1.Text = CMaster.cGameMgr.GetPoint(Player.Player1).ToString();
      lbl_score_P2.Text = CMaster.cGameMgr.GetPoint(Player.Player2).ToString();
      lbl_score_P3.Text = CMaster.cGameMgr.GetPoint(Player.Player3).ToString();
      lbl_score_P4.Text = CMaster.cGameMgr.GetPoint(Player.Player4).ToString();

      Player player = CMaster.cGameMgr.GetPlayer();
      switch (player)
      {
        case Player.Player1:
          group_Player1.BackColor = Color.Lime;
          group_Player2.BackColor = Color.Transparent;
          group_Player3.BackColor = Color.Transparent;
          group_Player4.BackColor = Color.Transparent;
          break;
        case Player.Player2:
          group_Player1.BackColor = Color.Transparent;
          group_Player2.BackColor = Color.Lime;
          group_Player3.BackColor = Color.Transparent;
          group_Player4.BackColor = Color.Transparent;
          break;
        case Player.Player3:
          group_Player1.BackColor = Color.Transparent;
          group_Player2.BackColor = Color.Transparent;
          group_Player3.BackColor = Color.Lime;
          group_Player4.BackColor = Color.Transparent;
          break;
        case Player.Player4:
          group_Player1.BackColor = Color.Transparent;
          group_Player2.BackColor = Color.Transparent;
          group_Player3.BackColor = Color.Transparent;
          group_Player4.BackColor = Color.Lime;
          break;
        default:
          break;
      }

      lbl_GameResult.Text = CMaster.cGameMgr.GetAlart();
      tmr_Monitor.Enabled = true;
    }

    private void Form1_Load(object sender, EventArgs e)
    {
      lbl_ver.Text = string.Format("Ver.{0} {1}", CForm.ProgramVer, CForm.LastModifier);
      listBox_GameType.Items.Clear();
      for (int i = 0; i < Enum.GetNames(typeof(GameType)).Length; i++)
      {
        listBox_GameType.Items.Add(((GameType)i).ToString());
      }
      listBox_GameType.SelectedIndex = 0;

      listBox_Player.Items.Clear();
      for (int i = 0; i < Enum.GetNames(typeof(PlayerType)).Length; i++)
      {
        switch ((PlayerType)i)
        {
          case PlayerType.VS_ONE: listBox_Player.Items.Add("Alone"); break;
          case PlayerType.VS_TWO: listBox_Player.Items.Add("1:1"); break;
          case PlayerType.VS_THR: listBox_Player.Items.Add("1:1:1"); break;
          case PlayerType.VS_FOU: listBox_Player.Items.Add("1:1:1:1"); break;
          case PlayerType.DOUBLE: listBox_Player.Items.Add("2:2"); break;
          default: break;
        }
      }
      listBox_Player.SelectedIndex = 0;

      listBox_Round.Items.Clear();
      for (int i = 0; i < Enum.GetNames(typeof(RoundType)).Length; i++)
      {
        listBox_Round.Items.Add(((RoundType)i).ToString());
      }
      listBox_Round.SelectedIndex = 0;

      CMaster.cGameMgr.Set_Information(
        (GameType)listBox_GameType.SelectedIndex,
        (PlayerType)listBox_Player.SelectedIndex,
        (RoundType)listBox_Round.SelectedIndex);
      CMaster.cGameMgr.Init_Data();
    }

    private void btn_SystemSetting_Click(object sender, EventArgs e)
    {
      CForm.frmSystemSetting.ShowDialog();
    }

    private void btn_OK_MouseDown(object sender, MouseEventArgs e)
    {
      btn_OK.BackColor = Color.Red;
    }

    private void btn_OK_MouseUp(object sender, MouseEventArgs e)
    {
      btn_OK.BackColor = Color.Maroon;
    }

    private void btn_GameSetting_Click(object sender, EventArgs e)
    {
      CMaster.cGameMgr.Init_Data();
      CMaster.cGameMgr.Set_Information(
        (GameType)listBox_GameType.SelectedIndex,
        (PlayerType)listBox_Player.SelectedIndex,
        (RoundType)listBox_Round.SelectedIndex);
    }

    private void btn_OK_Click(object sender, EventArgs e)
    {
      CMaster.cGameMgr.SetGameFinished();
      CMaster.cGameMgr.ChangePlayer();
    }




  }
}

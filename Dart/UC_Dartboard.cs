using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Windows.Forms;
using System.Drawing;

namespace Dart
{
  public class UC_Dartboard: UserControl
  {
    // Realtime Value
    private Point MouseLocation = new Point(0, 0);
    private int CurrentPoint;
    private Button_Type_Dart type = Button_Type_Dart.MISS;

    private int R_D_Bull = 0;
    private int R_S_Bull = 0;
    private int R_I_Trip = 0;
    private int R_O_Trip = 0;
    private int R_I_Doub = 0;
    private int R_O_Doub = 0;

    
    private Point center = new Point(100, 100);
    private int R = 100;
    private Color color_D_Bull = Color.Transparent;
    private Color color_S_Bull = Color.Transparent;
    private Color color_Odd = Color.Transparent;
    private Color color_Even = Color.Transparent;
    private Color color_Odd_SP = Color.Transparent;
    private Color color_Even_SP = Color.Transparent;
    private Color color_Miss = Color.Transparent;

    private static System.Threading.Thread th = null;
    private static bool bThread = true;


        //    center, 
        //size, 
        //new Point(0,0), 
        //Color.FromArgb(CParamMgr.cParaSys.Color_D_Bull),
        //Color.FromArgb(CParamMgr.cParaSys.Color_S_Bull),
        //Color.FromArgb(CParamMgr.cParaSys.Color_S_Odd),
        //Color.FromArgb(CParamMgr.cParaSys.Color_S_Even),
        //Color.FromArgb(CParamMgr.cParaSys.Color_SP_Odd),
        //Color.FromArgb(CParamMgr.cParaSys.Color_SP_Even), 
        //Color.FromArgb(CParamMgr.cParaSys.Color_Miss), 

    public UC_Dartboard(
      Point center, 
      int r, 
      Point location,
      Color color_D_Bull,
      Color color_S_Bull,
      Color color_Odd,
      Color color_Even,
      Color color_Odd_SP,
      Color color_Even_SP,
      Color color_Miss
      )
    {
      this.center = center;
      this.R = r;
      this.color_D_Bull = color_D_Bull;
      this.color_S_Bull = color_S_Bull;
      this.color_Odd = color_Odd;
      this.color_Even = color_Even;
      this.color_Even_SP = color_Even_SP;
      this.color_Odd_SP = color_Odd_SP;
      this.color_Miss = color_Miss;

      R_D_Bull = (int)(r * 0.07); // 7%
      R_S_Bull = (int)(r * 0.11); // 11%
      R_I_Trip = (int)(r * 0.53); // 53%
      R_O_Trip = (int)(r * 0.63); // 63%
      R_I_Doub = (int)(r * 0.9);  // 90%
      R_O_Doub = (int)(r * 1.0);  // 100%

      this.Location = location;
      this.Size = new System.Drawing.Size(r, r);
      EventHandler Event_Click = new EventHandler(btn_Click);
      this.Click += Event_Click;

      MouseEventHandler Event_Move = new MouseEventHandler(btn_MouseMove);
      this.MouseMove += Event_Move;

      // Thread Start
      Thread_Start();
    }

    #region event
    private void btn_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
    {
      MouseLocation = this.PointToClient(new Point(Control.MousePosition.X, Control.MousePosition.Y));
    }

    private void btn_Click(Object sender, System.EventArgs e)
    {
      if (CMaster.cGameMgr.SetPoint(type, CurrentPoint))
      {
        // 넥스트 버튼 누르세요.
      }
      if (CMaster.cGameMgr.Check_Game_Finished())
      {
        // 게임 끝
      }

    }


    protected override void OnPaint(PaintEventArgs e)
    {
      Graphics graphics = e.Graphics;
      Bitmap back_img = new Bitmap(this.Width, this.Height); // 버퍼 사이즈 수정(패턴 트레인 이미지 버그생김)
      Graphics backgroundGraphic = Graphics.FromImage(back_img);

      Pen pen_D_Bull = new Pen(color_D_Bull, 2);
      Pen pen_S_Bull = new Pen(color_S_Bull,2);
      Pen pen_Odd = new Pen(color_Odd, 2);
      Pen pen_Even = new Pen(color_Even, 2);
      Pen pen_Odd_SP = new Pen(color_Odd_SP, 2);
      Pen pen_Even_SP = new Pen(color_Even_SP, 2);
      Pen pen_Miss = new Pen(color_Miss, 2);

      for (int i = R_D_Bull; i > 0; i--)
      {
          backgroundGraphic.DrawEllipse(pen_D_Bull, Get_Location_Circle(i).X, Get_Location_Circle(i).Y, i, i);
      }
      for (int i = R_S_Bull; i > R_D_Bull; i--)
      {
          backgroundGraphic.DrawEllipse(pen_S_Bull, Get_Location_Circle(i).X, Get_Location_Circle(i).Y, i, i);
      }
      for (int i = R_I_Trip; i > R_S_Bull; i--)
      {
          backgroundGraphic.DrawArc(pen_Odd, Get_Location_Circle(i).X, Get_Location_Circle(i).Y, i, i, Get_S_Angle(Button_Type_Dart.S_1, false), 360 / 20);
          backgroundGraphic.DrawArc(pen_Even, Get_Location_Circle(i).X, Get_Location_Circle(i).Y, i, i, Get_S_Angle(Button_Type_Dart.S_2, false), 360 / 20);
          backgroundGraphic.DrawArc(pen_Even, Get_Location_Circle(i).X, Get_Location_Circle(i).Y, i, i, Get_S_Angle(Button_Type_Dart.S_3, false), 360 / 20);
          backgroundGraphic.DrawArc(pen_Odd, Get_Location_Circle(i).X, Get_Location_Circle(i).Y, i, i, Get_S_Angle(Button_Type_Dart.S_4, false), 360 / 20);
          backgroundGraphic.DrawArc(pen_Odd, Get_Location_Circle(i).X, Get_Location_Circle(i).Y, i, i, Get_S_Angle(Button_Type_Dart.S_5, false), 360 / 20);
          backgroundGraphic.DrawArc(pen_Odd, Get_Location_Circle(i).X, Get_Location_Circle(i).Y, i, i, Get_S_Angle(Button_Type_Dart.S_6, false), 360 / 20);
          backgroundGraphic.DrawArc(pen_Even, Get_Location_Circle(i).X, Get_Location_Circle(i).Y, i, i, Get_S_Angle(Button_Type_Dart.S_7, false), 360 / 20);
          backgroundGraphic.DrawArc(pen_Even, Get_Location_Circle(i).X, Get_Location_Circle(i).Y, i, i, Get_S_Angle(Button_Type_Dart.S_8, false), 360 / 20);
          backgroundGraphic.DrawArc(pen_Odd, Get_Location_Circle(i).X, Get_Location_Circle(i).Y, i, i, Get_S_Angle(Button_Type_Dart.S_9, false), 360 / 20);
          backgroundGraphic.DrawArc(pen_Even, Get_Location_Circle(i).X, Get_Location_Circle(i).Y, i, i, Get_S_Angle(Button_Type_Dart.S_10, false), 360 / 20);
          backgroundGraphic.DrawArc(pen_Odd, Get_Location_Circle(i).X, Get_Location_Circle(i).Y, i, i, Get_S_Angle(Button_Type_Dart.S_11, false), 360 / 20);
          backgroundGraphic.DrawArc(pen_Even, Get_Location_Circle(i).X, Get_Location_Circle(i).Y, i, i, Get_S_Angle(Button_Type_Dart.S_12, false), 360 / 20);
          backgroundGraphic.DrawArc(pen_Even, Get_Location_Circle(i).X, Get_Location_Circle(i).Y, i, i, Get_S_Angle(Button_Type_Dart.S_13, false), 360 / 20);
          backgroundGraphic.DrawArc(pen_Even, Get_Location_Circle(i).X, Get_Location_Circle(i).Y, i, i, Get_S_Angle(Button_Type_Dart.S_14, false), 360 / 20);
          backgroundGraphic.DrawArc(pen_Odd, Get_Location_Circle(i).X, Get_Location_Circle(i).Y, i, i, Get_S_Angle(Button_Type_Dart.S_15, false), 360 / 20);
          backgroundGraphic.DrawArc(pen_Odd, Get_Location_Circle(i).X, Get_Location_Circle(i).Y, i, i, Get_S_Angle(Button_Type_Dart.S_16, false), 360 / 20);
          backgroundGraphic.DrawArc(pen_Odd, Get_Location_Circle(i).X, Get_Location_Circle(i).Y, i, i, Get_S_Angle(Button_Type_Dart.S_17, false), 360 / 20);
          backgroundGraphic.DrawArc(pen_Even, Get_Location_Circle(i).X, Get_Location_Circle(i).Y, i, i, Get_S_Angle(Button_Type_Dart.S_18, false), 360 / 20);
          backgroundGraphic.DrawArc(pen_Odd, Get_Location_Circle(i).X, Get_Location_Circle(i).Y, i, i, Get_S_Angle(Button_Type_Dart.S_19, false), 360 / 20);
          backgroundGraphic.DrawArc(pen_Even, Get_Location_Circle(i).X, Get_Location_Circle(i).Y, i, i, Get_S_Angle(Button_Type_Dart.S_20, false), 360 / 20);
      }
      for (int i = R_O_Trip; i > R_I_Trip; i--)
      {
          backgroundGraphic.DrawArc(pen_Odd_SP, Get_Location_Circle(i).X, Get_Location_Circle(i).Y, i, i, Get_S_Angle(Button_Type_Dart.S_1, false), 360 / 20);
          backgroundGraphic.DrawArc(pen_Even_SP, Get_Location_Circle(i).X, Get_Location_Circle(i).Y, i, i, Get_S_Angle(Button_Type_Dart.S_2, false), 360 / 20);
          backgroundGraphic.DrawArc(pen_Even_SP, Get_Location_Circle(i).X, Get_Location_Circle(i).Y, i, i, Get_S_Angle(Button_Type_Dart.S_3, false), 360 / 20);
          backgroundGraphic.DrawArc(pen_Odd_SP, Get_Location_Circle(i).X, Get_Location_Circle(i).Y, i, i, Get_S_Angle(Button_Type_Dart.S_4, false), 360 / 20);
          backgroundGraphic.DrawArc(pen_Odd_SP, Get_Location_Circle(i).X, Get_Location_Circle(i).Y, i, i, Get_S_Angle(Button_Type_Dart.S_5, false), 360 / 20);
          backgroundGraphic.DrawArc(pen_Odd_SP, Get_Location_Circle(i).X, Get_Location_Circle(i).Y, i, i, Get_S_Angle(Button_Type_Dart.S_6, false), 360 / 20);
          backgroundGraphic.DrawArc(pen_Even_SP, Get_Location_Circle(i).X, Get_Location_Circle(i).Y, i, i, Get_S_Angle(Button_Type_Dart.S_7, false), 360 / 20);
          backgroundGraphic.DrawArc(pen_Even_SP, Get_Location_Circle(i).X, Get_Location_Circle(i).Y, i, i, Get_S_Angle(Button_Type_Dart.S_8, false), 360 / 20);
          backgroundGraphic.DrawArc(pen_Odd_SP, Get_Location_Circle(i).X, Get_Location_Circle(i).Y, i, i, Get_S_Angle(Button_Type_Dart.S_9, false), 360 / 20);
          backgroundGraphic.DrawArc(pen_Even_SP, Get_Location_Circle(i).X, Get_Location_Circle(i).Y, i, i, Get_S_Angle(Button_Type_Dart.S_10, false), 360 / 20);
          backgroundGraphic.DrawArc(pen_Odd_SP, Get_Location_Circle(i).X, Get_Location_Circle(i).Y, i, i, Get_S_Angle(Button_Type_Dart.S_11, false), 360 / 20);
          backgroundGraphic.DrawArc(pen_Even_SP, Get_Location_Circle(i).X, Get_Location_Circle(i).Y, i, i, Get_S_Angle(Button_Type_Dart.S_12, false), 360 / 20);
          backgroundGraphic.DrawArc(pen_Even_SP, Get_Location_Circle(i).X, Get_Location_Circle(i).Y, i, i, Get_S_Angle(Button_Type_Dart.S_13, false), 360 / 20);
          backgroundGraphic.DrawArc(pen_Even_SP, Get_Location_Circle(i).X, Get_Location_Circle(i).Y, i, i, Get_S_Angle(Button_Type_Dart.S_14, false), 360 / 20);
          backgroundGraphic.DrawArc(pen_Odd_SP, Get_Location_Circle(i).X, Get_Location_Circle(i).Y, i, i, Get_S_Angle(Button_Type_Dart.S_15, false), 360 / 20);
          backgroundGraphic.DrawArc(pen_Odd_SP, Get_Location_Circle(i).X, Get_Location_Circle(i).Y, i, i, Get_S_Angle(Button_Type_Dart.S_16, false), 360 / 20);
          backgroundGraphic.DrawArc(pen_Odd_SP, Get_Location_Circle(i).X, Get_Location_Circle(i).Y, i, i, Get_S_Angle(Button_Type_Dart.S_17, false), 360 / 20);
          backgroundGraphic.DrawArc(pen_Even_SP, Get_Location_Circle(i).X, Get_Location_Circle(i).Y, i, i, Get_S_Angle(Button_Type_Dart.S_18, false), 360 / 20);
          backgroundGraphic.DrawArc(pen_Odd_SP, Get_Location_Circle(i).X, Get_Location_Circle(i).Y, i, i, Get_S_Angle(Button_Type_Dart.S_19, false), 360 / 20);
          backgroundGraphic.DrawArc(pen_Even_SP, Get_Location_Circle(i).X, Get_Location_Circle(i).Y, i, i, Get_S_Angle(Button_Type_Dart.S_20, false), 360 / 20);
      }
      for (int i = R_I_Doub; i > R_O_Trip; i--)
      {
          backgroundGraphic.DrawArc(pen_Odd, Get_Location_Circle(i).X, Get_Location_Circle(i).Y, i, i, Get_S_Angle(Button_Type_Dart.S_1, false), 360 / 20);
          backgroundGraphic.DrawArc(pen_Even, Get_Location_Circle(i).X, Get_Location_Circle(i).Y, i, i, Get_S_Angle(Button_Type_Dart.S_2, false), 360 / 20);
          backgroundGraphic.DrawArc(pen_Even, Get_Location_Circle(i).X, Get_Location_Circle(i).Y, i, i, Get_S_Angle(Button_Type_Dart.S_3, false), 360 / 20);
          backgroundGraphic.DrawArc(pen_Odd, Get_Location_Circle(i).X, Get_Location_Circle(i).Y, i, i, Get_S_Angle(Button_Type_Dart.S_4, false), 360 / 20);
          backgroundGraphic.DrawArc(pen_Odd, Get_Location_Circle(i).X, Get_Location_Circle(i).Y, i, i, Get_S_Angle(Button_Type_Dart.S_5, false), 360 / 20);
          backgroundGraphic.DrawArc(pen_Odd, Get_Location_Circle(i).X, Get_Location_Circle(i).Y, i, i, Get_S_Angle(Button_Type_Dart.S_6, false), 360 / 20);
          backgroundGraphic.DrawArc(pen_Even, Get_Location_Circle(i).X, Get_Location_Circle(i).Y, i, i, Get_S_Angle(Button_Type_Dart.S_7, false), 360 / 20);
          backgroundGraphic.DrawArc(pen_Even, Get_Location_Circle(i).X, Get_Location_Circle(i).Y, i, i, Get_S_Angle(Button_Type_Dart.S_8, false), 360 / 20);
          backgroundGraphic.DrawArc(pen_Odd, Get_Location_Circle(i).X, Get_Location_Circle(i).Y, i, i, Get_S_Angle(Button_Type_Dart.S_9, false), 360 / 20);
          backgroundGraphic.DrawArc(pen_Even, Get_Location_Circle(i).X, Get_Location_Circle(i).Y, i, i, Get_S_Angle(Button_Type_Dart.S_10, false), 360 / 20);
          backgroundGraphic.DrawArc(pen_Odd, Get_Location_Circle(i).X, Get_Location_Circle(i).Y, i, i, Get_S_Angle(Button_Type_Dart.S_11, false), 360 / 20);
          backgroundGraphic.DrawArc(pen_Even, Get_Location_Circle(i).X, Get_Location_Circle(i).Y, i, i, Get_S_Angle(Button_Type_Dart.S_12, false), 360 / 20);
          backgroundGraphic.DrawArc(pen_Even, Get_Location_Circle(i).X, Get_Location_Circle(i).Y, i, i, Get_S_Angle(Button_Type_Dart.S_13, false), 360 / 20);
          backgroundGraphic.DrawArc(pen_Even, Get_Location_Circle(i).X, Get_Location_Circle(i).Y, i, i, Get_S_Angle(Button_Type_Dart.S_14, false), 360 / 20);
          backgroundGraphic.DrawArc(pen_Odd, Get_Location_Circle(i).X, Get_Location_Circle(i).Y, i, i, Get_S_Angle(Button_Type_Dart.S_15, false), 360 / 20);
          backgroundGraphic.DrawArc(pen_Odd, Get_Location_Circle(i).X, Get_Location_Circle(i).Y, i, i, Get_S_Angle(Button_Type_Dart.S_16, false), 360 / 20);
          backgroundGraphic.DrawArc(pen_Odd, Get_Location_Circle(i).X, Get_Location_Circle(i).Y, i, i, Get_S_Angle(Button_Type_Dart.S_17, false), 360 / 20);
          backgroundGraphic.DrawArc(pen_Even, Get_Location_Circle(i).X, Get_Location_Circle(i).Y, i, i, Get_S_Angle(Button_Type_Dart.S_18, false), 360 / 20);
          backgroundGraphic.DrawArc(pen_Odd, Get_Location_Circle(i).X, Get_Location_Circle(i).Y, i, i, Get_S_Angle(Button_Type_Dart.S_19, false), 360 / 20);
          backgroundGraphic.DrawArc(pen_Even, Get_Location_Circle(i).X, Get_Location_Circle(i).Y, i, i, Get_S_Angle(Button_Type_Dart.S_20, false), 360 / 20);
      }
      for (int i = R_O_Doub; i > R_I_Doub; i--)
      {
          backgroundGraphic.DrawArc(pen_Odd_SP, Get_Location_Circle(i).X, Get_Location_Circle(i).Y, i, i, Get_S_Angle(Button_Type_Dart.S_1, false), 360 / 20);
          backgroundGraphic.DrawArc(pen_Even_SP, Get_Location_Circle(i).X, Get_Location_Circle(i).Y, i, i, Get_S_Angle(Button_Type_Dart.S_2, false), 360 / 20);
          backgroundGraphic.DrawArc(pen_Even_SP, Get_Location_Circle(i).X, Get_Location_Circle(i).Y, i, i, Get_S_Angle(Button_Type_Dart.S_3, false), 360 / 20);
          backgroundGraphic.DrawArc(pen_Odd_SP, Get_Location_Circle(i).X, Get_Location_Circle(i).Y, i, i, Get_S_Angle(Button_Type_Dart.S_4, false), 360 / 20);
          backgroundGraphic.DrawArc(pen_Odd_SP, Get_Location_Circle(i).X, Get_Location_Circle(i).Y, i, i, Get_S_Angle(Button_Type_Dart.S_5, false), 360 / 20);
          backgroundGraphic.DrawArc(pen_Odd_SP, Get_Location_Circle(i).X, Get_Location_Circle(i).Y, i, i, Get_S_Angle(Button_Type_Dart.S_6, false), 360 / 20);
          backgroundGraphic.DrawArc(pen_Even_SP, Get_Location_Circle(i).X, Get_Location_Circle(i).Y, i, i, Get_S_Angle(Button_Type_Dart.S_7, false), 360 / 20);
          backgroundGraphic.DrawArc(pen_Even_SP, Get_Location_Circle(i).X, Get_Location_Circle(i).Y, i, i, Get_S_Angle(Button_Type_Dart.S_8, false), 360 / 20);
          backgroundGraphic.DrawArc(pen_Odd_SP, Get_Location_Circle(i).X, Get_Location_Circle(i).Y, i, i, Get_S_Angle(Button_Type_Dart.S_9, false), 360 / 20);
          backgroundGraphic.DrawArc(pen_Even_SP, Get_Location_Circle(i).X, Get_Location_Circle(i).Y, i, i, Get_S_Angle(Button_Type_Dart.S_10, false), 360 / 20);
          backgroundGraphic.DrawArc(pen_Odd_SP, Get_Location_Circle(i).X, Get_Location_Circle(i).Y, i, i, Get_S_Angle(Button_Type_Dart.S_11, false), 360 / 20);
          backgroundGraphic.DrawArc(pen_Even_SP, Get_Location_Circle(i).X, Get_Location_Circle(i).Y, i, i, Get_S_Angle(Button_Type_Dart.S_12, false), 360 / 20);
          backgroundGraphic.DrawArc(pen_Even_SP, Get_Location_Circle(i).X, Get_Location_Circle(i).Y, i, i, Get_S_Angle(Button_Type_Dart.S_13, false), 360 / 20);
          backgroundGraphic.DrawArc(pen_Even_SP, Get_Location_Circle(i).X, Get_Location_Circle(i).Y, i, i, Get_S_Angle(Button_Type_Dart.S_14, false), 360 / 20);
          backgroundGraphic.DrawArc(pen_Odd_SP, Get_Location_Circle(i).X, Get_Location_Circle(i).Y, i, i, Get_S_Angle(Button_Type_Dart.S_15, false), 360 / 20);
          backgroundGraphic.DrawArc(pen_Odd_SP, Get_Location_Circle(i).X, Get_Location_Circle(i).Y, i, i, Get_S_Angle(Button_Type_Dart.S_16, false), 360 / 20);
          backgroundGraphic.DrawArc(pen_Odd_SP, Get_Location_Circle(i).X, Get_Location_Circle(i).Y, i, i, Get_S_Angle(Button_Type_Dart.S_17, false), 360 / 20);
          backgroundGraphic.DrawArc(pen_Even_SP, Get_Location_Circle(i).X, Get_Location_Circle(i).Y, i, i, Get_S_Angle(Button_Type_Dart.S_18, false), 360 / 20);
          backgroundGraphic.DrawArc(pen_Odd_SP, Get_Location_Circle(i).X, Get_Location_Circle(i).Y, i, i, Get_S_Angle(Button_Type_Dart.S_19, false), 360 / 20);
          backgroundGraphic.DrawArc(pen_Even_SP, Get_Location_Circle(i).X, Get_Location_Circle(i).Y, i, i, Get_S_Angle(Button_Type_Dart.S_20, false), 360 / 20);
      }


      // 숫자 넣기 결과 먼가 꼬여 잇음


      //int distanceFromCenter = (R_O_Trip + (R_I_Doub - R_O_Trip)) / 2;
      //int Text_Font_Size = R_D_Bull;

      //System.Drawing.SolidBrush solBR_Num = new System.Drawing.SolidBrush(color_Miss);

      //for (int i = (int)Button_Type_Dart.S_1; i <= (int)Button_Type_Dart.S_20; i++)
      //{
      //  int deg = Get_S_Angle((Button_Type_Dart)i, true);// -((360 / 20) / 2);
      //  int Text_X = (int)(center.X + (Math.Cos(deg) * distanceFromCenter));
      //  int Text_Y = (int)(center.Y + (Math.Sin(deg) * distanceFromCenter));
      //  graphics.DrawString(i.ToString("00"), new Font("Arial", Text_Font_Size), solBR_Num, new Point((int)(Text_X/* - Text_Font_Size*/), (int)(Text_Y/* - Text_Font_Size*/)));

      //}

      //// 텍스트 폰트 크기 = 싱글불 크기
      //// 텍스트 위치 = 트리플아웃과 더블 인 사이에 해당 각도에 폰트 크기만큼 가로세로 뺌


      graphics.DrawImage(back_img, 0, 0);




      pen_D_Bull.Dispose();
      pen_S_Bull.Dispose(); 
      pen_Odd.Dispose();
      pen_Even.Dispose();
      pen_Odd_SP.Dispose();
      pen_Even_SP.Dispose();
      pen_Miss.Dispose();

    }
    #endregion

    #region Thread
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
          int result = 0; // point * zone
          int point = 0; // 0 ~ 20, 25, 50
          int zone = 1; // single = 1, double = 2, triple = 3

          Point mousePoint = new Point(MouseLocation.X, MouseLocation.Y);

          int dx = mousePoint.X - center.X;
          int dy = mousePoint.Y - center.Y;
          double angle = ((Math.Atan2(dx, dy) * 180) / Math.PI) + 270;
          if (angle > 360)
          {
            angle -= 360;
          }
          angle = Math.Abs(angle - 360);

          int lineLength = (int)Math.Sqrt(Math.Pow(Math.Abs(center.X - mousePoint.X), 2) + Math.Pow(Math.Abs(center.Y - mousePoint.Y), 2));

          string strData = "";
          if (lineLength > (R / 2))
          {
            strData = string.Format("Out: {0}", angle);
            point = 0;
            zone = 1;
            type = Button_Type_Dart.MISS;
          }
          else
          {
            if (lineLength <= (R_D_Bull / 2))
            {
              strData = string.Format("DBull: {0}", angle);
              point = 25;
              zone = 2;
              type = Button_Type_Dart.D_BULL;
            }
            else if (lineLength > (R_D_Bull / 2) && lineLength <= (R_S_Bull / 2))
            {
              strData = string.Format("SBull: {0}", angle);
              point = 25;
              zone = 1;
              type = Button_Type_Dart.S_BULL;
            }
            else
            {
              if (lineLength >= (R_I_Trip / 2) && lineLength <= (R_O_Trip / 2))
              {
                strData = string.Format("Triple: {0}", angle);
                zone = 3;
                point = Get_Area(angle);
                type = (Button_Type_Dart)(20 * (zone - 1)) + point;
              }
              else if (lineLength >= (R_I_Doub / 2) && lineLength <= (R_O_Doub / 2))
              {
                strData = string.Format("Double: {0}", angle);
                zone = 2;
                point = Get_Area(angle);
                type = (Button_Type_Dart)(20 * (zone - 1)) + point;
              }
              else
              {
                strData = string.Format("Single: {0}", angle);
                zone = 1;
                point = Get_Area(angle);
                type = (Button_Type_Dart)(20 * (zone - 1)) + point;
              }

            }
          }
          //MessageBox.Show(strData);
          if (zone == 25)
          {
            CurrentPoint = 50;
          }
          else
          {
            CurrentPoint = zone * point;
          }
        }
      }
    }
    #endregion
    #region Math
    private int Get_S_Angle(Button_Type_Dart type, bool end) // Arc Angle false : start true : end
    {
      int result = -1;

      switch (type)
      {
        case Button_Type_Dart.S_1:
        case Button_Type_Dart.D_1:
        case Button_Type_Dart.T_1:
          result = 279;
          break;
        case Button_Type_Dart.S_2:
        case Button_Type_Dart.D_2:
        case Button_Type_Dart.T_2:
          result = 45;
          break;
        case Button_Type_Dart.S_3:
        case Button_Type_Dart.D_3:
        case Button_Type_Dart.T_3:
          result = 81;
          break;
        case Button_Type_Dart.S_4:
        case Button_Type_Dart.D_4:
        case Button_Type_Dart.T_4:
          result = 315;
          break;
        case Button_Type_Dart.S_5:
        case Button_Type_Dart.D_5:
        case Button_Type_Dart.T_5:
          result = 243;
          break;
        case Button_Type_Dart.S_6:
        case Button_Type_Dart.D_6:
        case Button_Type_Dart.T_6:
          result = 351;
          break;
        case Button_Type_Dart.S_7:
        case Button_Type_Dart.D_7:
        case Button_Type_Dart.T_7:
          result = 117;
          break;
        case Button_Type_Dart.S_8:
        case Button_Type_Dart.D_8:
        case Button_Type_Dart.T_8:
          result = 153;
          break;
        case Button_Type_Dart.S_9:
        case Button_Type_Dart.D_9:
        case Button_Type_Dart.T_9:
          result = 207;
          break;
        case Button_Type_Dart.S_10:
        case Button_Type_Dart.D_10:
        case Button_Type_Dart.T_10:
          result = 9;
          break;
        case Button_Type_Dart.S_11:
        case Button_Type_Dart.D_11:
        case Button_Type_Dart.T_11:
          result = 171;
          break;
        case Button_Type_Dart.S_12:
        case Button_Type_Dart.D_12:
        case Button_Type_Dart.T_12:
          result = 225;
          break;
        case Button_Type_Dart.S_13:
        case Button_Type_Dart.D_13:
        case Button_Type_Dart.T_13:
          result = 333;
          break;
        case Button_Type_Dart.S_14:
        case Button_Type_Dart.D_14:
        case Button_Type_Dart.T_14:
          result = 189;
          break;
        case Button_Type_Dart.S_15:
        case Button_Type_Dart.D_15:
        case Button_Type_Dart.T_15:
          result = 27;
          break;
        case Button_Type_Dart.S_16:
        case Button_Type_Dart.D_16:
        case Button_Type_Dart.T_16:
          result = 135;
          break;
        case Button_Type_Dart.S_17:
        case Button_Type_Dart.D_17:
        case Button_Type_Dart.T_17:
          result = 63;
          break;
        case Button_Type_Dart.S_18:
        case Button_Type_Dart.D_18:
        case Button_Type_Dart.T_18:
          result = 297;
          break;
        case Button_Type_Dart.S_19:
        case Button_Type_Dart.D_19:
        case Button_Type_Dart.T_19:
          result = 99;
          break;
        case Button_Type_Dart.S_20:
        case Button_Type_Dart.D_20:
        case Button_Type_Dart.T_20:
          result = 261;
          break;
        case Button_Type_Dart.S_BULL:
        case Button_Type_Dart.D_BULL:
        case Button_Type_Dart.MISS:
        default:
          return result;
      }
      if (end)
      {
        result += 18;
      }
      if (result > 360)
      {
        result -= 360;
      }
      return result;
    }
    private int Get_Area(double angle)
    {
      int start = (int)Button_Type_Dart.S_1;
      int end = (int)Button_Type_Dart.S_20;

      int result = 0;
      for (int i = start; i <= end; i++)
      {
        if ((Button_Type_Dart)i == Button_Type_Dart.S_6)
        {
          if ((351 <= angle && angle < 360)
            || (0 <= angle && angle < 9))
          {
            result = (int)Button_Type_Dart.S_6;
          }
        }
        else
        {
          if (Get_S_Angle((Button_Type_Dart)i, false) <= angle && Get_S_Angle((Button_Type_Dart)i, true) > angle)
          {
            result = i;
            break;
          }
        }
      }
      return result;
    }
    private Point Get_Location_Circle(int R) // Circle Location
    {
      Point leftTop = new Point();
      leftTop.X = (int)(center.X - (R/2));
      leftTop.Y = (int)(center.Y - (R/2));
      return leftTop;
    }
    #endregion

    #region Control Function
    public Point GetMouseLocation()
    {
      return MouseLocation;
    }
    public int Get_CurrentPoint()
    {
      return CurrentPoint;
    }
    public Button_Type_Dart Get_CurrentType()
    {
      return type;
    }
    #endregion
  }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Threading;

namespace Dart
{
  public class ClassGameMgr
  {
    private static ClassGameMgr cInstatnce;
    private static object syncLock = new object();

    private static System.Threading.Thread th = null;
    private static bool bThread = true;

    private static GameData[] gmData = new GameData[Enum.GetNames(typeof(Player)).Length];
    private static GameType Type_Game;
    private static PlayerType Type_Player;
    private static RoundType Type_Round;

    private static int CurrentRound = 1;
    private static Player CurrentPlayer = Player.Player1;
    private static int CurrentThrow = 0;
    private static string Alart = "";
    private static bool GameOver = false;
    private static int ZeroOneOldPoint = 0;

    private static bool bSoundOn = false;
    private static string strSoundPath = "";

    protected ClassGameMgr()
    {
      Check_Thread();
    }

    public static ClassGameMgr Get_Instance()
    {
      if (cInstatnce == null)
      {
        lock (syncLock)
        {
          if (cInstatnce == null)
          {
            cInstatnce = new ClassGameMgr();
          }
        }
      }
      return cInstatnce;
    }

    public void Check_Thread()
    {
      if (th == null)
      {
        bThread = true;
        th = new Thread(Run);
        th.IsBackground = true;
        th.Start();
      }
    }

    public void Abort_Thread()
    {
      th.Abort();
      bThread = false;
      th = null;
    } // Kill All Thread

    #region Control Function
    public void Init_Data()
    {
      for (int i = 0; i < gmData.Length; i++)
      {
        gmData[i].score = 0;
        gmData[i].Mark = new int[7];
        for (int j = 0; j < 7; j++)
        {
          gmData[i].Mark[j] = 0;
        }
      }
      CurrentRound = 1;
      CurrentPlayer = Player.Player1;
      CurrentThrow = 0;
      Alart = "";
      GameOver = false;
    }

    public void Set_Information(GameType gtype, PlayerType ptype, RoundType rtype)
    {
      Init_Data();
      Type_Game = gtype;
      Type_Player = ptype;
      Type_Round = rtype;
      switch (Type_Game)
      {
        case GameType.Count_Up:
          for (int i = 0; i < Enum.GetNames(typeof(Player)).Length; i++)
          {
            gmData[i].score = 0;
          }
          break;
        case GameType.ZeroOne_501:
          for (int i = 0; i < Enum.GetNames(typeof(Player)).Length; i++)
          {
            gmData[i].score = 501;
          }
          break;
        case GameType.Cricket_Standard:
          for (int i = 0; i < Enum.GetNames(typeof(Player)).Length; i++)
          {
            gmData[i].score = 0;
          }
          break;
        default:
          break;
      }

    }

    public int GetRound()
    {
      return CurrentRound;
    }

    public Player GetPlayer()
    {
      return CurrentPlayer;
    }

    public int GetPoint(Player pl)
    {
      return gmData[(int)pl].score;
    }

    public GameType GetGameType()
    {
      return Type_Game;
    }

    public PlayerType GetPlayerType()
    {
      return Type_Player;
    }

    public RoundType GetRoundType()
    {
      return Type_Round;
    }

    public bool SetPoint(Button_Type_Dart hit, int point)
    {
      if (GameOver)
      {
        return false;
      }
      Player lastPlayer = Player.Player1;
      switch (Type_Player)
      {
        case PlayerType.VS_ONE: lastPlayer = Player.Player1; break;
        case PlayerType.VS_TWO: lastPlayer = Player.Player2; break;
        case PlayerType.VS_THR: lastPlayer = Player.Player3; break;
        case PlayerType.VS_FOU: lastPlayer = Player.Player4; break;
        case PlayerType.DOUBLE: lastPlayer = Player.Player4; break;
        default: lastPlayer = Player.Player1; break;
      }
      int Team = -1;
      if (PlayerType.DOUBLE == Type_Player)
      {
        switch (CurrentPlayer)
        {
          case Player.Player1: Team = (int)Player.Player3; break;
          case Player.Player2: Team = (int)Player.Player4; break;
          case Player.Player3: Team = (int)Player.Player1; break;
          case Player.Player4: Team = (int)Player.Player2; break;
          default: Team = -1; break;
        }
      }
      HitArea hitArea = HitArea.Miss;
      switch (hit)
      {

        case Button_Type_Dart.S_1:
        case Button_Type_Dart.S_2:
        case Button_Type_Dart.S_3:
        case Button_Type_Dart.S_4:
        case Button_Type_Dart.S_5:
        case Button_Type_Dart.S_6:
        case Button_Type_Dart.S_7:
        case Button_Type_Dart.S_8:
        case Button_Type_Dart.S_9:
        case Button_Type_Dart.S_10:
        case Button_Type_Dart.S_11:
        case Button_Type_Dart.S_12:
        case Button_Type_Dart.S_13:
        case Button_Type_Dart.S_14:
        case Button_Type_Dart.S_15:
        case Button_Type_Dart.S_16:
        case Button_Type_Dart.S_17:
        case Button_Type_Dart.S_18:
        case Button_Type_Dart.S_19:
        case Button_Type_Dart.S_20:
          hitArea = HitArea.Single;
          break;
        case Button_Type_Dart.D_1:
        case Button_Type_Dart.D_2:
        case Button_Type_Dart.D_3:
        case Button_Type_Dart.D_4:
        case Button_Type_Dart.D_5:
        case Button_Type_Dart.D_6:
        case Button_Type_Dart.D_7:
        case Button_Type_Dart.D_8:
        case Button_Type_Dart.D_9:
        case Button_Type_Dart.D_10:
        case Button_Type_Dart.D_11:
        case Button_Type_Dart.D_12:
        case Button_Type_Dart.D_13:
        case Button_Type_Dart.D_14:
        case Button_Type_Dart.D_15:
        case Button_Type_Dart.D_16:
        case Button_Type_Dart.D_17:
        case Button_Type_Dart.D_18:
        case Button_Type_Dart.D_19:
        case Button_Type_Dart.D_20:
          hitArea = HitArea.Double;
          break;
        case Button_Type_Dart.T_1:
        case Button_Type_Dart.T_2:
        case Button_Type_Dart.T_3:
        case Button_Type_Dart.T_4:
        case Button_Type_Dart.T_5:
        case Button_Type_Dart.T_6:
        case Button_Type_Dart.T_7:
        case Button_Type_Dart.T_8:
        case Button_Type_Dart.T_9:
        case Button_Type_Dart.T_10:
        case Button_Type_Dart.T_11:
        case Button_Type_Dart.T_12:
        case Button_Type_Dart.T_13:
        case Button_Type_Dart.T_14:
        case Button_Type_Dart.T_15:
        case Button_Type_Dart.T_16:
        case Button_Type_Dart.T_17:
        case Button_Type_Dart.T_18:
        case Button_Type_Dart.T_19:
        case Button_Type_Dart.T_20:
          hitArea = HitArea.Triple;
          break;
        case Button_Type_Dart.S_BULL:
          hitArea = HitArea.S_Bull;
          break;
        case Button_Type_Dart.D_BULL:
          hitArea = HitArea.D_Bull;
          break;
        case Button_Type_Dart.MISS:
        default:
          hitArea = HitArea.Miss;
          break;
      }

      switch (Type_Game)
      {
        case GameType.Count_Up:
          if (CurrentThrow >= 3)
          {
            Alart = "Next Player";
            return false;
          }
          CurrentThrow++;
          gmData[(int)CurrentPlayer].score += point;
          if (Team != -1)
          {
            gmData[Team].score += point;
          }
          PlaySound(hitArea);
          break;
        case GameType.ZeroOne_501:
          if (CurrentThrow >= 3)
          {
            Alart = "Next Player";
            return false;
          }
          if (CurrentThrow == 0)
          {
            ZeroOneOldPoint = gmData[(int)CurrentPlayer].score;
          }
          CurrentThrow++;
          gmData[(int)CurrentPlayer].score -= point;
          if (gmData[(int)CurrentPlayer].score < 0)
          {
            gmData[(int)CurrentPlayer].score = ZeroOneOldPoint;
            PlaySound(HitArea.Miss);
          }
          else
          {
            PlaySound(hitArea);
          }
          if (Team != -1)
          {
            gmData[Team].score = gmData[(int)CurrentPlayer].score;
          }
          break;
        case GameType.Cricket_Standard:
          int CricketPoint = 0;
          int MarkIndex = 0;
          int OldMark = 0;
          if (CurrentThrow >= 3)
          {
            Alart = "Next Player";
            return false;
          }
          CurrentThrow++;
          switch (hit)
          {
            case Button_Type_Dart.S_15:
              MarkIndex = 0;
              OldMark = gmData[(int)CurrentPlayer].Mark[MarkIndex];
              switch (gmData[(int)CurrentPlayer].Mark[0])
              {
                case 0:
                case 1:
                case 2:
                  gmData[(int)CurrentPlayer].Mark[0] += 1;
                  if (Team != -1)
                  {
                    gmData[Team].Mark[0] += 1;
                  }
                  break;
                case 3:
                  CricketPoint = 15;
                  break;
                case 4:
                default:
                  break;
              }
              break;
            case Button_Type_Dart.S_16:
              MarkIndex = 1;
              OldMark = gmData[(int)CurrentPlayer].Mark[MarkIndex];
              switch (gmData[(int)CurrentPlayer].Mark[1])
              {
                case 0:
                case 1:
                case 2:
                  gmData[(int)CurrentPlayer].Mark[1] += 1;
                  if (Team != -1)
                  {
                    gmData[Team].Mark[1] += 1;
                  }
                  break;
                case 3:
                  CricketPoint = 16;
                  break;
                case 4:
                default:
                  break;
              }
              break;
            case Button_Type_Dart.S_17:
              MarkIndex = 2;
              OldMark = gmData[(int)CurrentPlayer].Mark[MarkIndex];
              switch (gmData[(int)CurrentPlayer].Mark[2])
              {
                case 0:
                case 1:
                case 2:
                  gmData[(int)CurrentPlayer].Mark[2] += 1;
                  if (Team != -1)
                  {
                    gmData[Team].Mark[2] += 1;
                  }
                  break;
                case 3:
                  CricketPoint = 17;
                  break;
                case 4:
                default:
                  break;
              }
              break;
            case Button_Type_Dart.S_18:
              MarkIndex = 3;
              OldMark = gmData[(int)CurrentPlayer].Mark[MarkIndex];
              switch (gmData[(int)CurrentPlayer].Mark[3])
              {
                case 0:
                case 1:
                case 2:
                  gmData[(int)CurrentPlayer].Mark[3] += 1;
                  if (Team != -1)
                  {
                    gmData[Team].Mark[3] += 1;
                  }
                  break;
                case 3:
                  CricketPoint = 18;
                  break;
                case 4:
                default:
                  break;
              }
              break;
            case Button_Type_Dart.S_19:
              MarkIndex = 4;
              OldMark = gmData[(int)CurrentPlayer].Mark[MarkIndex];
              switch (gmData[(int)CurrentPlayer].Mark[4])
              {
                case 0:
                case 1:
                case 2:
                  gmData[(int)CurrentPlayer].Mark[4] += 1;
                  if (Team != -1)
                  {
                    gmData[Team].Mark[4] += 1;
                  }
                  break;
                case 3:
                  CricketPoint = 19;
                  break;
                case 4:
                default:
                  break;
              }
              break;
            case Button_Type_Dart.S_20:
              MarkIndex = 5;
              OldMark = gmData[(int)CurrentPlayer].Mark[MarkIndex];
              switch (gmData[(int)CurrentPlayer].Mark[5])
              {
                case 0:
                case 1:
                case 2:
                  gmData[(int)CurrentPlayer].Mark[5] += 1;
                  if (Team != -1)
                  {
                    gmData[Team].Mark[5] += 1;
                  }
                  break;
                case 3:
                  CricketPoint = 20;
                  break;
                case 4:
                default:
                  break;
              }
              break;
            case Button_Type_Dart.D_15:
              MarkIndex = 0;
              OldMark = gmData[(int)CurrentPlayer].Mark[MarkIndex];
              switch (gmData[(int)CurrentPlayer].Mark[0])
              {
                case 0:
                case 1:
                  gmData[(int)CurrentPlayer].Mark[0] += 2;
                  if (Team != -1)
                  {
                    gmData[Team].Mark[0] += 2;
                  }
                  break;
                case 2:
                  gmData[(int)CurrentPlayer].Mark[0] += 1;
                  if (Team != -1)
                  {
                    gmData[Team].Mark[0] += 1;
                  }
                  CricketPoint = 15;
                  break;
                case 3:
                  CricketPoint = 30;
                  break;
                case 4:
                default:
                  break;
              }
              break;
            case Button_Type_Dart.D_16:
              MarkIndex = 1;
              OldMark = gmData[(int)CurrentPlayer].Mark[MarkIndex];
              switch (gmData[(int)CurrentPlayer].Mark[1])
              {
                case 0:
                case 1:
                  gmData[(int)CurrentPlayer].Mark[1] += 2;
                  if (Team != -1)
                  {
                    gmData[Team].Mark[1] += 2;
                  }
                  break;
                case 2:
                  gmData[(int)CurrentPlayer].Mark[1] += 1;
                  if (Team != -1)
                  {
                    gmData[Team].Mark[1] += 1;
                  }
                  CricketPoint = 16;
                  break;
                case 3:
                  CricketPoint = 32;
                  break;
                case 4:
                default:
                  break;
              }
              break;
            case Button_Type_Dart.D_17:
              MarkIndex = 2;
              OldMark = gmData[(int)CurrentPlayer].Mark[MarkIndex];
              switch (gmData[(int)CurrentPlayer].Mark[2])
              {
                case 0:
                case 1:
                  gmData[(int)CurrentPlayer].Mark[2] += 2;
                  if (Team != -1)
                  {
                    gmData[Team].Mark[2] += 2;
                  }
                  break;
                case 2:
                  gmData[(int)CurrentPlayer].Mark[2] += 1;
                  if (Team != -1)
                  {
                    gmData[Team].Mark[2] += 1;
                  }
                  CricketPoint = 17;
                  break;
                case 3:
                  CricketPoint = 34;
                  break;
                case 4:
                default:
                  break;
              }
              break;
            case Button_Type_Dart.D_18:
              MarkIndex = 3;
              OldMark = gmData[(int)CurrentPlayer].Mark[MarkIndex];
              switch (gmData[(int)CurrentPlayer].Mark[3])
              {
                case 0:
                case 1:
                  gmData[(int)CurrentPlayer].Mark[3] += 2;
                  if (Team != -1)
                  {
                    gmData[Team].Mark[3] += 2;
                  }
                  break;
                case 2:
                  gmData[(int)CurrentPlayer].Mark[3] += 1;
                  if (Team != -1)
                  {
                    gmData[Team].Mark[3] += 1;
                  }
                  CricketPoint = 18;
                  break;
                case 3:
                  CricketPoint = 36;
                  break;
                case 4:
                default:
                  break;
              }
              break;
            case Button_Type_Dart.D_19:
              MarkIndex = 4;
              OldMark = gmData[(int)CurrentPlayer].Mark[MarkIndex];
              switch (gmData[(int)CurrentPlayer].Mark[4])
              {
                case 0:
                case 1:
                  gmData[(int)CurrentPlayer].Mark[4] += 2;
                  if (Team != -1)
                  {
                    gmData[Team].Mark[4] += 2;
                  }
                  break;
                case 2:
                  gmData[(int)CurrentPlayer].Mark[4] += 1;
                  if (Team != -1)
                  {
                    gmData[Team].Mark[4] += 1;
                  }
                  CricketPoint = 19;
                  break;
                case 3:
                  CricketPoint = 38;
                  break;
                case 4:
                default:
                  break;
              }
              break;
            case Button_Type_Dart.D_20:
              MarkIndex = 5;
              OldMark = gmData[(int)CurrentPlayer].Mark[MarkIndex];
              switch (gmData[(int)CurrentPlayer].Mark[5])
              {
                case 0:
                case 1:
                  gmData[(int)CurrentPlayer].Mark[5] += 2;
                  if (Team != -1)
                  {
                    gmData[Team].Mark[5] += 2;
                  }
                  break;
                case 2:
                  gmData[(int)CurrentPlayer].Mark[5] += 1;
                  if (Team != -1)
                  {
                    gmData[Team].Mark[5] += 1;
                  }
                  CricketPoint = 20;
                  break;
                case 3:
                  CricketPoint = 40;
                  break;
                case 4:
                default:
                  break;
              }
              break;
            case Button_Type_Dart.T_15:
              MarkIndex = 0;
              OldMark = gmData[(int)CurrentPlayer].Mark[MarkIndex];
              switch (gmData[(int)CurrentPlayer].Mark[0])
              {
                case 0:
                  gmData[(int)CurrentPlayer].Mark[0] += 3;
                  if (Team != -1)
                  {
                    gmData[Team].Mark[0] += 3;
                  }
                  break;
                case 1:
                  gmData[(int)CurrentPlayer].Mark[0] += 2;
                  if (Team != -1)
                  {
                    gmData[Team].Mark[0] += 2;
                  }
                  CricketPoint = 15;
                  break;
                case 2:
                  gmData[(int)CurrentPlayer].Mark[0] += 1;
                  if (Team != -1)
                  {
                    gmData[Team].Mark[0] += 1;
                  }
                  CricketPoint = 30;
                  break;
                case 3:
                  CricketPoint = 45;
                  break;
                case 4:
                default:
                  break;
              }
              break;
            case Button_Type_Dart.T_16:
              MarkIndex = 1;
              OldMark = gmData[(int)CurrentPlayer].Mark[MarkIndex];
              switch (gmData[(int)CurrentPlayer].Mark[1])
              {
                case 0:
                  gmData[(int)CurrentPlayer].Mark[1] += 3;
                  if (Team != -1)
                  {
                    gmData[Team].Mark[1] += 3;
                  }
                  break;
                case 1:
                  gmData[(int)CurrentPlayer].Mark[1] += 2;
                  if (Team != -1)
                  {
                    gmData[Team].Mark[1] += 2;
                  }
                  CricketPoint = 16;
                  break;
                case 2:
                  gmData[(int)CurrentPlayer].Mark[1] += 1;
                  if (Team != -1)
                  {
                    gmData[Team].Mark[1] += 1;
                  }
                  CricketPoint = 32;
                  break;
                case 3:
                  CricketPoint = 48;
                  break;
                case 4:
                default:
                  break;
              }
              break;
            case Button_Type_Dart.T_17:
              MarkIndex = 2;
              OldMark = gmData[(int)CurrentPlayer].Mark[MarkIndex];
              switch (gmData[(int)CurrentPlayer].Mark[2])
              {
                case 0:
                  gmData[(int)CurrentPlayer].Mark[2] += 3;
                  if (Team != -1)
                  {
                    gmData[Team].Mark[2] += 3;
                  }
                  break;
                case 1:
                  gmData[(int)CurrentPlayer].Mark[2] += 2;
                  if (Team != -1)
                  {
                    gmData[Team].Mark[2] += 2;
                  }
                  CricketPoint = 17;
                  break;
                case 2:
                  gmData[(int)CurrentPlayer].Mark[2] += 1;
                  if (Team != -1)
                  {
                    gmData[Team].Mark[2] += 1;
                  }
                  CricketPoint = 34;
                  break;
                case 3:
                  CricketPoint = 51;
                  break;
                case 4:
                default:
                  break;
              }
              break;
            case Button_Type_Dart.T_18:
              MarkIndex = 3;
              OldMark = gmData[(int)CurrentPlayer].Mark[MarkIndex];
              switch (gmData[(int)CurrentPlayer].Mark[3])
              {
                case 0:
                  gmData[(int)CurrentPlayer].Mark[3] += 3;
                  if (Team != -1)
                  {
                    gmData[Team].Mark[3] += 3;
                  }
                  break;
                case 1:
                  gmData[(int)CurrentPlayer].Mark[3] += 2;
                  if (Team != -1)
                  {
                    gmData[Team].Mark[3] += 2;
                  }
                  CricketPoint = 18;
                  break;
                case 2:
                  gmData[(int)CurrentPlayer].Mark[3] += 1;
                  if (Team != -1)
                  {
                    gmData[Team].Mark[3] += 1;
                  }
                  CricketPoint = 36;
                  break;
                case 3:
                  CricketPoint = 54;
                  break;
                case 4:
                default:
                  break;
              }
              break;
            case Button_Type_Dart.T_19:
              MarkIndex = 4;
              OldMark = gmData[(int)CurrentPlayer].Mark[MarkIndex];
              switch (gmData[(int)CurrentPlayer].Mark[4])
              {
                case 0:
                  gmData[(int)CurrentPlayer].Mark[4] += 3;
                  if (Team != -1)
                  {
                    gmData[Team].Mark[4] += 3;
                  }
                  break;
                case 1:
                  gmData[(int)CurrentPlayer].Mark[4] += 2;
                  if (Team != -1)
                  {
                    gmData[Team].Mark[4] += 2;
                  }
                  CricketPoint = 19;
                  break;
                case 2:
                  gmData[(int)CurrentPlayer].Mark[4] += 1;
                  if (Team != -1)
                  {
                    gmData[Team].Mark[4] += 1;
                  }
                  CricketPoint = 38;
                  break;
                case 3:
                  CricketPoint = 57;
                  break;
                case 4:
                default:
                  break;
              }
              break;
            case Button_Type_Dart.T_20:
              MarkIndex = 5;
              OldMark = gmData[(int)CurrentPlayer].Mark[MarkIndex];
              switch (gmData[(int)CurrentPlayer].Mark[5])
              {
                case 0:
                  gmData[(int)CurrentPlayer].Mark[5] += 3;
                  if (Team != -1)
                  {
                    gmData[Team].Mark[5] += 3;
                  }
                  break;
                case 1:
                  gmData[(int)CurrentPlayer].Mark[5] += 2;
                  if (Team != -1)
                  {
                    gmData[Team].Mark[5] += 2;
                  }
                  CricketPoint = 20;
                  break;
                case 2:
                  gmData[(int)CurrentPlayer].Mark[5] += 1;
                  if (Team != -1)
                  {
                    gmData[Team].Mark[5] += 1;
                  }
                  CricketPoint = 40;
                  break;
                case 3:
                  CricketPoint = 60;
                  break;
                case 4:
                default:
                  break;
              }
              break;
            case Button_Type_Dart.S_BULL:
              MarkIndex = 6;
              OldMark = gmData[(int)CurrentPlayer].Mark[MarkIndex];
              switch (gmData[(int)CurrentPlayer].Mark[6])
              {
                case 0:
                case 1:
                case 2:
                  gmData[(int)CurrentPlayer].Mark[6] += 1;
                  if (Team != -1)
                  {
                    gmData[Team].Mark[6] += 1;
                  }
                  break;
                case 3:
                  CricketPoint = 50;
                  break;
                case 4:
                default:
                  break;
              }
              break;
            case Button_Type_Dart.D_BULL:
              MarkIndex = 6;
              OldMark = gmData[(int)CurrentPlayer].Mark[MarkIndex];
              switch (gmData[(int)CurrentPlayer].Mark[6])
              {
                case 0:
                case 1:
                  gmData[(int)CurrentPlayer].Mark[6] += 2;
                  if (Team != -1)
                  {
                    gmData[Team].Mark[6] += 2;
                  }
                  break;
                case 2:
                  gmData[(int)CurrentPlayer].Mark[6] += 1;
                  if (Team != -1)
                  {
                    gmData[Team].Mark[6] += 1;
                  }
                  CricketPoint = 50;
                  break;
                case 3:
                  CricketPoint = 50;
                  break;
                case 4:
                default:
                  break;
              }
              break;
            default:
              MarkIndex = -1;
              OldMark = -1;
              PlaySound(HitArea.Miss);
              break;
          }

          if (MarkIndex >= 0)
          {
            switch (Type_Player)
            {
              case PlayerType.VS_ONE:
                gmData[(int)CurrentPlayer].score += CricketPoint;
                break;
              case PlayerType.VS_TWO:
                if (gmData[(int)Player.Player1].Mark[MarkIndex] >= 3
                 && gmData[(int)Player.Player2].Mark[MarkIndex] >= 3)
                {
                  gmData[(int)Player.Player1].Mark[MarkIndex] = 4;
                  gmData[(int)Player.Player2].Mark[MarkIndex] = 4;
                }
                else
                {
                  gmData[(int)CurrentPlayer].score += CricketPoint;
                }
                break;
              case PlayerType.VS_THR:
                if (gmData[(int)Player.Player1].Mark[MarkIndex] >= 3
                 && gmData[(int)Player.Player2].Mark[MarkIndex] >= 3
                 && gmData[(int)Player.Player3].Mark[MarkIndex] >= 3)
                {
                  gmData[(int)Player.Player1].Mark[MarkIndex] = 4;
                  gmData[(int)Player.Player2].Mark[MarkIndex] = 4;
                  gmData[(int)Player.Player3].Mark[MarkIndex] = 4;
                }
                else
                {
                  gmData[(int)CurrentPlayer].score += CricketPoint;
                }
                break;
              case PlayerType.VS_FOU:
                if (gmData[(int)Player.Player1].Mark[MarkIndex] >= 3
                 && gmData[(int)Player.Player2].Mark[MarkIndex] >= 3
                 && gmData[(int)Player.Player3].Mark[MarkIndex] >= 3
                 && gmData[(int)Player.Player4].Mark[MarkIndex] >= 3)
                {
                  gmData[(int)Player.Player1].Mark[MarkIndex] = 4;
                  gmData[(int)Player.Player2].Mark[MarkIndex] = 4;
                  gmData[(int)Player.Player3].Mark[MarkIndex] = 4;
                  gmData[(int)Player.Player4].Mark[MarkIndex] = 4;
                }
                else
                {
                  gmData[(int)CurrentPlayer].score += CricketPoint;
                }
                break;
              case PlayerType.DOUBLE:
                if (gmData[(int)Player.Player1].Mark[MarkIndex] >= 3
                 && gmData[(int)Player.Player2].Mark[MarkIndex] >= 3
                 && gmData[(int)Player.Player3].Mark[MarkIndex] >= 3
                 && gmData[(int)Player.Player4].Mark[MarkIndex] >= 3)
                {
                  gmData[(int)Player.Player1].Mark[MarkIndex] = 4;
                  gmData[(int)Player.Player2].Mark[MarkIndex] = 4;
                  gmData[(int)Player.Player3].Mark[MarkIndex] = 4;
                  gmData[(int)Player.Player4].Mark[MarkIndex] = 4;
                }
                else
                {
                  gmData[(int)CurrentPlayer].score += CricketPoint;
                  gmData[Team].score += CricketPoint;
                }
                break;
              default:
                break;
            }
            if (gmData[(int)CurrentPlayer].Mark[MarkIndex] < 4)
            {
              if (gmData[(int)CurrentPlayer].Mark[MarkIndex] != OldMark
               || gmData[(int)CurrentPlayer].Mark[MarkIndex] == 3)
              {
                PlaySound(hitArea);
              }
              else
              {
                PlaySound(HitArea.Miss);
              }
            }
            else
            {
              if (gmData[(int)CurrentPlayer].Mark[MarkIndex] != OldMark)
              {
                PlaySound(hitArea);
              }
              else
              {
                PlaySound(HitArea.Miss);
              }
            }
          }
          break;
        default:
          if (CurrentThrow >= 3)
          {
            Alart = "Next Player";
            return false;
          }
          CurrentThrow++;
          break;
      }
      return true;
    }

    public void ChangePlayer()
    {
      Player lastPlayer = Player.Player1;
      switch (Type_Player)
      {
        case PlayerType.VS_ONE: lastPlayer = Player.Player1; break;
        case PlayerType.VS_TWO: lastPlayer = Player.Player2; break;
        case PlayerType.VS_THR: lastPlayer = Player.Player3; break;
        case PlayerType.VS_FOU: lastPlayer = Player.Player4; break;
        case PlayerType.DOUBLE: lastPlayer = Player.Player4; break;
        default: lastPlayer = Player.Player1; break;
      }
      int round = 0;
      switch (Type_Round)
      {
        case RoundType.Round_5: round = 5; break;
        case RoundType.Round_8: round = 8; break;
        case RoundType.Round_10: round = 10; break;
        case RoundType.Round_15: round = 15; break;
        case RoundType.Round_20: round = 20; break;
        default: round = 0; break;
      }
      if (CurrentPlayer == lastPlayer
       && CurrentRound >= round)
      {
        return;
      }
      int nextPlayer = (int)CurrentPlayer + 1;
      switch (Type_Player)
      {
        case PlayerType.VS_ONE:
          nextPlayer = 0;
          break;
        case PlayerType.VS_TWO:
          nextPlayer = nextPlayer % 2;
          break;
        case PlayerType.VS_THR:
          nextPlayer = nextPlayer % 3;
          break;
        case PlayerType.VS_FOU:
          nextPlayer = nextPlayer % 4;
          break;
        case PlayerType.DOUBLE:
          nextPlayer = nextPlayer % 4;
          break;
        default:
          break;
      }
      CurrentPlayer = (Player)nextPlayer;
      CurrentThrow = 0;
      if (CurrentPlayer == Player.Player1)
      {
        CurrentRound++;
      }
      Alart = "";
    }

    public bool Check_Game_Finished()
    {
      int round = 0;
      switch (Type_Round)
      {
        case RoundType.Round_5: round = 5; break;
        case RoundType.Round_8: round = 8; break;
        case RoundType.Round_10: round = 10; break;
        case RoundType.Round_15: round = 15; break;
        case RoundType.Round_20: round = 20; break;
        default: round = 0; break;
      }
      Player lastPlayer = Player.Player1;
      int PlayingPlayer = 1;
      switch (Type_Player)
      {
        case PlayerType.VS_ONE: lastPlayer = Player.Player1; PlayingPlayer = 1; break;
        case PlayerType.VS_TWO: lastPlayer = Player.Player2; PlayingPlayer = 2; break;
        case PlayerType.VS_THR: lastPlayer = Player.Player3; PlayingPlayer = 3; break;
        case PlayerType.VS_FOU: lastPlayer = Player.Player4; PlayingPlayer = 4; break;
        case PlayerType.DOUBLE: lastPlayer = Player.Player4; PlayingPlayer = 4; break;
        default: lastPlayer = Player.Player1; PlayingPlayer = 1; break;
      }

      if (3 <= CurrentThrow
       && round <= CurrentRound
       && CurrentPlayer == lastPlayer)
      {
        Alart = "Game Over";
        GameOver = true;
        return true;
      }

      switch (Type_Game)
      {
        case GameType.Count_Up:
          break;
        case GameType.ZeroOne_501:
          for (int i = 0; i < PlayingPlayer; i++)
          {
            if (gmData[i].score == 0)
            {
              Alart = "Game Over";
              GameOver = true;
              return true;
            }
          }
          break;
        case GameType.Cricket_Standard:
          for (int i = 0; i < Enum.GetNames(typeof(Player)).Length; i++)
          {
            bool CheckClose = true;
            for (int j = 0; j < gmData[i].Mark.Length; j++)
            {
              if (gmData[i].Mark[j] < 3)
              {
                CheckClose = false;
                break;
              }
            }
            if (CheckClose)
            {
              bool HighScore = true;
              for (int j = 0; j < Enum.GetNames(typeof(Player)).Length; j++)
              {
                if (gmData[i].score < gmData[j].score)
                {
                  HighScore = false;
                  break;
                }
              }
              if (HighScore)
              {
                Alart = "Game Over";
                GameOver = true;
                return true;
              }
            }
          }
          break;
        default:
          break;
      }
      return false;
    }

    public void SetGameFinished()
    {
      int round = 0;
      switch (Type_Round)
      {
        case RoundType.Round_5: round = 5; break;
        case RoundType.Round_8: round = 8; break;
        case RoundType.Round_10: round = 10; break;
        case RoundType.Round_15: round = 15; break;
        case RoundType.Round_20: round = 20; break;
        default: round = 0; break;
      }
      Player lastPlayer = Player.Player1;
      switch (Type_Player)
      {
        case PlayerType.VS_ONE: lastPlayer = Player.Player1; break;
        case PlayerType.VS_TWO: lastPlayer = Player.Player2; break;
        case PlayerType.VS_THR: lastPlayer = Player.Player3; break;
        case PlayerType.VS_FOU: lastPlayer = Player.Player4; break;
        case PlayerType.DOUBLE: lastPlayer = Player.Player4; break;
        default: lastPlayer = Player.Player1; break;
      }

      if (round <= CurrentRound
       && CurrentPlayer == lastPlayer)
      {
        Alart = "Game Over";
        GameOver = true;
      }
    }

    public string GetAlart()
    {
      return Alart;
    }

    public int GetMark(Player pl, int index/*0~6 -> 15~Bull*/)
    {
      try
      {
        return gmData[(int)pl].Mark[index];
      }
      catch (Exception ex)
      {
        string strMsg = string.Format("{0}\r\n{1}\r\n{2}", System.Reflection.MethodBase.GetCurrentMethod().Name, new System.Diagnostics.StackTrace(ex, true), ex);
        System.Windows.Forms.MessageBox.Show(strMsg);
        return -1;
      }

    }

    public int GetThrow()
    {
      return CurrentThrow;
    }

    public void PlaySound(HitArea area)
    {
      switch (area)
      {
        case HitArea.Single:
          strSoundPath = CParamMgr.cParaSys.Path_Sound_Single;
          bSoundOn = true;
          break;
        case HitArea.Double:
          strSoundPath = CParamMgr.cParaSys.Path_Sound_Double;
          bSoundOn = true;
          break;
        case HitArea.Triple:
          strSoundPath = CParamMgr.cParaSys.Path_Sound_Triple;
          bSoundOn = true;
          break;
        case HitArea.S_Bull:
          strSoundPath = CParamMgr.cParaSys.Path_Sound_S_Bull;
          bSoundOn = true;
          break;
        case HitArea.D_Bull:
          strSoundPath = CParamMgr.cParaSys.Path_Sound_D_Bull;
          bSoundOn = true;
          break;
        case HitArea.Miss:
        default:
          strSoundPath = CParamMgr.cParaSys.Path_Sound_Miss;
          bSoundOn = true;
          break;
      }
    }

    #endregion

    private void Run()
    {
      while (bThread)
      {
        lock (this)
        {
          Thread.Sleep(1);
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
  }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dart
{
  public enum Button_Type_Dart
  {
    MISS = 0,
    S_1, S_2, S_3, S_4, S_5, S_6, S_7, S_8, S_9, S_10, S_11, S_12, S_13, S_14, S_15, S_16, S_17, S_18, S_19, S_20,
    D_1, D_2, D_3, D_4, D_5, D_6, D_7, D_8, D_9, D_10, D_11, D_12, D_13, D_14, D_15, D_16, D_17, D_18, D_19, D_20,
    T_1, T_2, T_3, T_4, T_5, T_6, T_7, T_8, T_9, T_10, T_11, T_12, T_13, T_14, T_15, T_16, T_17, T_18, T_19, T_20,
    S_BULL,
    D_BULL,
  };

  public enum HitArea
  { 
    Miss = 0,
    Single,
    Double,
    Triple,
    S_Bull,
    D_Bull,
  }

  public enum GameType
  {
    Count_Up, // Count UP
    ZeroOne_501, // Zero One 501
    Cricket_Standard, // Cricket Standard
  }

  public enum PlayerType
  {
    VS_ONE, // Alone
    VS_TWO, // 1:1
    VS_THR, // 1:1:1
    VS_FOU, // 1:1:1:1
    DOUBLE  // 2:2
  }

  public enum Player
  { 
    Player1 = 0,
    Player2,
    Player3,
    Player4,
  }

  public enum RoundType
  { 
    Round_5,
    Round_8,
    Round_10,
    Round_15,
    Round_20,
  }

  public struct GameData
  {
    public int score;
    public int[] Mark;
    // 0:20;
    // 1:19;
    // 2:18;
    // 3:17;
    // 4:16;
    // 5:15;
    // 6:Bull;
  }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dart
{
  public static class CMaster
  {
    public static ClassGameMgr cGameMgr = ClassGameMgr.Get_Instance();

    public static void Start_All()
    {
      cGameMgr.Check_Thread();
    }

    public static void Abort_All()
    {
      cGameMgr.Abort_Thread();
    }

  }
}

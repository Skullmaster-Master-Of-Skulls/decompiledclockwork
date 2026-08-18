using System;
using System.Data.Common;
using System.Threading;

namespace System.Data.Odbc
{
	// Token: 0x02000299 RID: 665
	internal sealed class OdbcEnvironment
	{
		// Token: 0x060028CA RID: 10442 RVA: 0x00110848 File Offset: 0x0010FC48
		private OdbcEnvironment()
		{
		}

		// Token: 0x060028CB RID: 10443 RVA: 0x0011085C File Offset: 0x0010FC5C
		internal static OdbcEnvironmentHandle GetGlobalEnvironmentHandle()
		{
			OdbcEnvironmentHandle odbcEnvironmentHandle = OdbcEnvironment._globalEnvironmentHandle as OdbcEnvironmentHandle;
			if (odbcEnvironmentHandle == null)
			{
				ADP.CheckVersionMDAC(true);
				object globalEnvironmentHandleLock = OdbcEnvironment._globalEnvironmentHandleLock;
				lock (globalEnvironmentHandleLock)
				{
					odbcEnvironmentHandle = (OdbcEnvironment._globalEnvironmentHandle as OdbcEnvironmentHandle);
					if (odbcEnvironmentHandle == null)
					{
						odbcEnvironmentHandle = new OdbcEnvironmentHandle();
						OdbcEnvironment._globalEnvironmentHandle = odbcEnvironmentHandle;
					}
				}
			}
			return odbcEnvironmentHandle;
		}

		// Token: 0x060028CC RID: 10444 RVA: 0x001108D0 File Offset: 0x0010FCD0
		internal static void ReleaseObjectPool()
		{
			object obj = Interlocked.Exchange(ref OdbcEnvironment._globalEnvironmentHandle, null);
			if (obj != null)
			{
				(obj as OdbcEnvironmentHandle).Dispose();
			}
		}

		// Token: 0x04001AA1 RID: 6817
		private static object _globalEnvironmentHandle;

		// Token: 0x04001AA2 RID: 6818
		private static object _globalEnvironmentHandleLock = new object();
	}
}

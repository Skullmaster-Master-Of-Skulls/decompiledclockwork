using System;
using System.Data.Common;
using System.Threading;

namespace System.Data.Odbc
{
	// Token: 0x020001E9 RID: 489
	internal sealed class OdbcEnvironment
	{
		// Token: 0x06001B6A RID: 7018 RVA: 0x00263558 File Offset: 0x00262958
		private OdbcEnvironment()
		{
		}

		// Token: 0x06001B6B RID: 7019 RVA: 0x00263578 File Offset: 0x00262978
		internal static OdbcEnvironmentHandle GetGlobalEnvironmentHandle()
		{
			OdbcEnvironmentHandle odbcEnvironmentHandle = OdbcEnvironment._globalEnvironmentHandle as OdbcEnvironmentHandle;
			if (odbcEnvironmentHandle == null)
			{
				ADP.CheckVersionMDAC(true);
				lock (OdbcEnvironment._globalEnvironmentHandleLock)
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

		// Token: 0x06001B6C RID: 7020 RVA: 0x002635E8 File Offset: 0x002629E8
		internal static void ReleaseObjectPool()
		{
			object obj = Interlocked.Exchange(ref OdbcEnvironment._globalEnvironmentHandle, null);
			if (obj != null)
			{
				(obj as OdbcEnvironmentHandle).Dispose();
			}
		}

		// Token: 0x04001015 RID: 4117
		private static object _globalEnvironmentHandle;

		// Token: 0x04001016 RID: 4118
		private static object _globalEnvironmentHandleLock = new object();
	}
}

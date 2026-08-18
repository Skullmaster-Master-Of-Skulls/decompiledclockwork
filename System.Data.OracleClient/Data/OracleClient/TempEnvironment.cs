using System;

namespace System.Data.OracleClient
{
	// Token: 0x02000081 RID: 129
	internal sealed class TempEnvironment
	{
		// Token: 0x060006F0 RID: 1776 RVA: 0x00073954 File Offset: 0x00072D54
		private TempEnvironment()
		{
		}

		// Token: 0x060006F1 RID: 1777 RVA: 0x00073974 File Offset: 0x00072D74
		private static void Initialize()
		{
			lock (TempEnvironment.locked)
			{
				if (!TempEnvironment.isInitialized)
				{
					bool unicode = false;
					OCI.MODE environmentMode = OCI.MODE.OCI_THREADED | OCI.MODE.OCI_OBJECT;
					OCI.DetermineClientVersion();
					TempEnvironment.environmentHandle = new OciEnvironmentHandle(environmentMode, unicode);
					TempEnvironment.availableErrorHandle = new OciErrorHandle(TempEnvironment.environmentHandle);
					TempEnvironment.isInitialized = true;
				}
			}
		}

		// Token: 0x060006F2 RID: 1778 RVA: 0x000739F4 File Offset: 0x00072DF4
		internal static OciErrorHandle GetErrorHandle()
		{
			if (!TempEnvironment.isInitialized)
			{
				TempEnvironment.Initialize();
			}
			return TempEnvironment.availableErrorHandle;
		}

		// Token: 0x040004ED RID: 1261
		private static OciEnvironmentHandle environmentHandle;

		// Token: 0x040004EE RID: 1262
		private static OciErrorHandle availableErrorHandle;

		// Token: 0x040004EF RID: 1263
		private static volatile bool isInitialized;

		// Token: 0x040004F0 RID: 1264
		private static object locked = new object();
	}
}

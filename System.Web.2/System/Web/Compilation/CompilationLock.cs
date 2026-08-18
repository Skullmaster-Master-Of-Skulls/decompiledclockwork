using System;
using System.Globalization;
using System.Threading;
using System.Web.Util;

namespace System.Web.Compilation
{
	// Token: 0x02000833 RID: 2099
	internal static class CompilationLock
	{
		// Token: 0x06006417 RID: 25623 RVA: 0x0015F140 File Offset: 0x0015D340
		internal static void GetLock(ref bool gotLock)
		{
			try
			{
			}
			finally
			{
				Monitor.Enter(BuildManager.TheBuildManager);
				CompilationLock._mutex.WaitOne();
				gotLock = true;
			}
		}

		// Token: 0x06006418 RID: 25624 RVA: 0x0015F178 File Offset: 0x0015D378
		internal static void ReleaseLock()
		{
			CompilationLock._mutex.ReleaseMutex();
			Monitor.Exit(BuildManager.TheBuildManager);
		}

		// Token: 0x040033D6 RID: 13270
		private static CompilationMutex _mutex = new CompilationMutex("CL" + StringUtil.GetNonRandomizedHashCode("CompilationLock" + HttpRuntime.AppDomainAppId.ToLower(CultureInfo.InvariantCulture), false).ToString("x", CultureInfo.InvariantCulture), "CompilationLock for " + HttpRuntime.AppDomainAppVirtualPath);
	}
}

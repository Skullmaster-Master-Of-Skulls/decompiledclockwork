using System;
using System.Reflection;
using System.Security.Permissions;
using System.Threading;

namespace Oracle.DataAccess.Client
{
	// Token: 0x0200003B RID: 59
	internal class OracleInit
	{
		// Token: 0x0600026D RID: 621 RVA: 0x0001D45C File Offset: 0x0001C45C
		internal static string GetAssemblyVersion()
		{
			Assembly assembly = Assembly.GetAssembly(typeof(OracleConnection));
			string fullName = assembly.FullName;
			int num = fullName.IndexOf("Version=") + 8;
			int num2 = fullName.IndexOf(",", num);
			if (num2 > num && num > 0)
			{
				return fullName.Substring(num, num2 - num);
			}
			return null;
		}

		// Token: 0x0600026E RID: 622 RVA: 0x0001D4AF File Offset: 0x0001C4AF
		private static void TimerCallbackFunc(object state)
		{
		}

		// Token: 0x0600026F RID: 623 RVA: 0x0001D4B4 File Offset: 0x0001C4B4
		[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
		public static void Initialize()
		{
			RegAndConfigRdr.ReadEntriesForRegistryAndConfig();
			if ((OracleInit.m_nMajorVer >= 5 && OracleInit.m_nMinorVer > 0) || OracleInit.m_nMajorVer >= 6)
			{
				lock (OracleInit.s_lockObj)
				{
					if (!OracleInit.bSetDllDirectoryInvoked)
					{
						string oraOpsDllPath = OraTrace.m_oraOpsDllPath;
						if (oraOpsDllPath != null && oraOpsDllPath != string.Empty)
						{
							try
							{
								string fileName = oraOpsDllPath + "\\oci.dll";
								int num = OpsInit.GetFileAttributes(fileName);
								if (num == -1)
								{
									fileName = oraOpsDllPath + "\\..\\OCI.DLL";
									num = OpsInit.GetFileAttributes(fileName);
									if (num != -1)
									{
										OpsInit.LoadLibrary(fileName);
									}
								}
								num = OpsInit.SetDllDirectory(oraOpsDllPath);
							}
							catch
							{
							}
						}
					}
				}
			}
			OracleInit.m_version = OracleInit.GetAssemblyVersion();
			OracleInit.bSetDllDirectoryInvoked = true;
			try
			{
				int num = OpsInit.CheckVersionCompatibility(OracleInit.m_version);
				if (num != 0)
				{
					throw new OracleException(num);
				}
			}
			catch
			{
				throw new OracleException(ErrRes.INIT_DLL_VERSION_MISMATCH);
			}
			OpsTrace.SyncInfo(OraTrace.m_traceFileName, OraTrace.m_checkConStatus, OraTrace.m_dynamicEnlist, OraTrace.m_FetchSize, OraTrace.m_ociEvents, (int)OraTrace.m_PerformanceCounters, OraTrace.m_PSPE, OraTrace.m_StmtCacheSize, OraTrace.m_StmtCacheSize, OraTrace.m_threadPoolMaxSize, OraTrace.m_TraceLevel, OraTrace.m_TraceOption, OraTrace.m_udtCacheSize, OraTrace.m_fetchArrayPooling);
			RegAndConfigRdr.TraceRegistryAndConfigValues();
			string text = " (%s) (ThreadPoolMaxSize : %s [Original: %s; Set: %s; Post-Set: %s])\n";
			string text2;
			if (RegAndConfigRdr.s_bFromConfigThreadPoolMaxSize)
			{
				text2 = "CONFIG";
			}
			else
			{
				text2 = "REGISTRY";
			}
			uint num2 = 0U;
			uint maxIOCompletionThreads = 0U;
			CThreadPool.GetMaxThreads(out num2, out maxIOCompletionThreads);
			if (OraTrace.m_threadPoolMaxSize > 0 && (long)OraTrace.m_threadPoolMaxSize != (long)((ulong)num2))
			{
				CThreadPool.SetMaxThreads((uint)OraTrace.m_threadPoolMaxSize, maxIOCompletionThreads);
			}
			uint num3 = 0U;
			CThreadPool.GetMaxThreads(out num3, out maxIOCompletionThreads);
			OraTrace.Trace(1U, new string[]
			{
				text,
				text2,
				OraTrace.m_threadPoolMaxSize.ToString(),
				num2.ToString(),
				OraTrace.m_threadPoolMaxSize.ToString(),
				num3.ToString()
			});
			try
			{
				TimerCallback callback = new TimerCallback(OracleInit.TimerCallbackFunc);
				uint num4 = 147766294U;
				OracleInit.m_timer = new Timer(callback, null, num4, num4);
			}
			catch
			{
			}
		}

		// Token: 0x040001F6 RID: 502
		public static bool bSetDllDirectoryInvoked = false;

		// Token: 0x040001F7 RID: 503
		private static OperatingSystem os = Environment.OSVersion;

		// Token: 0x040001F8 RID: 504
		private static int m_nMajorVer = OracleInit.os.Version.Major;

		// Token: 0x040001F9 RID: 505
		private static int m_nMinorVer = OracleInit.os.Version.Minor;

		// Token: 0x040001FA RID: 506
		private static object s_lockObj = new object();

		// Token: 0x040001FB RID: 507
		private static Timer m_timer;

		// Token: 0x040001FC RID: 508
		internal static string m_version;
	}
}

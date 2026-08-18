using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Threading;

namespace Oracle.DataAccess.Client
{
	// Token: 0x02000039 RID: 57
	internal class OraTrace
	{
		// Token: 0x06000264 RID: 612 RVA: 0x0001CE38 File Offset: 0x0001BE38
		internal OraTrace()
		{
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000265 RID: 613 RVA: 0x0001CE40 File Offset: 0x0001BE40
		internal static int MaxStatementCacheSize
		{
			get
			{
				return OraTrace.m_maxStatementCacheSize;
			}
		}

		// Token: 0x06000266 RID: 614 RVA: 0x0001CE48 File Offset: 0x0001BE48
		internal static void SetMaxStatementCacheSize(int newMaxStatementCacheSize)
		{
			if (newMaxStatementCacheSize < OraTrace.m_maxStatementCacheSize)
			{
				lock (OraTrace.m_maxStatementCacheSizeLock)
				{
					if (newMaxStatementCacheSize < OraTrace.m_maxStatementCacheSize)
					{
						if (OraTrace.m_TraceLevel != 0U)
						{
							OraTrace.Trace(64U, new string[]
							{
								string.Concat(new object[]
								{
									" (TUNING) OraTrace::SetMaxStatementCacheSize(): Max Statement Cache Size changed from ",
									OraTrace.m_maxStatementCacheSize,
									" to ",
									newMaxStatementCacheSize,
									"\n"
								})
							});
						}
						OraTrace.m_maxStatementCacheSize = newMaxStatementCacheSize;
					}
				}
			}
		}

		// Token: 0x06000267 RID: 615 RVA: 0x0001CEEC File Offset: 0x0001BEEC
		internal static void Trace(uint TraceLevel, params string[] args)
		{
			if ((TraceLevel & OraTrace.m_TraceLevel) == TraceLevel)
			{
				try
				{
					OpsTrace.Trace(TraceLevel, args);
				}
				catch (Exception)
				{
					throw;
				}
			}
		}

		// Token: 0x06000268 RID: 616 RVA: 0x0001CF20 File Offset: 0x0001BF20
		internal static void TraceExceptionInfo(Exception ex)
		{
			OraTrace.TraceExceptionInfo(ex, true);
		}

		// Token: 0x06000269 RID: 617 RVA: 0x0001CF2C File Offset: 0x0001BF2C
		[SecurityPermission(SecurityAction.Assert, Flags = SecurityPermissionFlag.UnmanagedCode)]
		internal static void TraceExceptionInfo(Exception ex, bool bCreateMiniDump)
		{
			if (ex is ThreadAbortException)
			{
				bCreateMiniDump = false;
			}
			int num = 0;
			int num2 = 0;
			if (bCreateMiniDump)
			{
				try
				{
					OpsTrace.GetLastErrorCode(out num);
				}
				catch (Exception ex2)
				{
					OraTrace.Trace(1U, new string[]
					{
						string.Concat(new string[]
						{
							" (ERROR) GetLastErrorCode: ",
							ex2.GetType().ToString(),
							": ",
							ex2.ToString(),
							"\n"
						})
					});
				}
				try
				{
					num2 = Marshal.GetExceptionCode();
				}
				catch (Exception ex3)
				{
					OraTrace.Trace(1U, new string[]
					{
						string.Concat(new string[]
						{
							" (ERROR) Marshal.GetExceptionCode: ",
							ex3.GetType().ToString(),
							": ",
							ex3.ToString(),
							"\n"
						})
					});
				}
				MiniDumpInfo miniDumpInfo = new MiniDumpInfo();
				miniDumpInfo.threadId = AppDomain.GetCurrentThreadId();
				miniDumpInfo.pExPtrs = Marshal.GetExceptionPointers();
				ThreadPool.QueueUserWorkItem(new WaitCallback(OraTrace.CreateMiniDump), miniDumpInfo);
				miniDumpInfo.evt.WaitOne();
				OraTrace.Trace(1U, new string[]
				{
					string.Concat(new string[]
					{
						" (EXCPT) Lvl0: (Type=",
						ex.GetType().ToString(),
						") (Msg=",
						ex.Message,
						") (Win32Err=",
						num.ToString(),
						") (Code=",
						num2.ToString("x"),
						") (Stack=",
						ex.StackTrace,
						")\n"
					})
				});
			}
			else
			{
				OraTrace.Trace(1U, new string[]
				{
					string.Concat(new string[]
					{
						" (EXCPT) Lvl0: (Type=",
						ex.GetType().ToString(),
						") (Msg=",
						ex.Message,
						") (Stack=",
						ex.StackTrace,
						")\n"
					})
				});
			}
			Exception innerException = ex.InnerException;
			int num3 = 1;
			while (innerException != null)
			{
				if (num3 > 9)
				{
					return;
				}
				if (bCreateMiniDump)
				{
					OraTrace.Trace(1U, new string[]
					{
						string.Concat(new string[]
						{
							" (EXCPT) Lvl",
							num3.ToString(),
							": (Type=",
							ex.GetType().ToString(),
							") (Msg=",
							ex.Message,
							") (Win32Err=",
							num.ToString(),
							") (Code=",
							num2.ToString("x"),
							") (Stack=",
							ex.StackTrace,
							")\n"
						})
					});
				}
				else
				{
					OraTrace.Trace(1U, new string[]
					{
						string.Concat(new string[]
						{
							" (EXCPT) Lvl",
							num3.ToString(),
							": (Type=",
							ex.GetType().ToString(),
							") (Msg=",
							ex.Message,
							") (Stack=",
							ex.StackTrace,
							")\n"
						})
					});
				}
				innerException = innerException.InnerException;
				num3++;
			}
		}

		// Token: 0x0600026A RID: 618 RVA: 0x0001D2DC File Offset: 0x0001C2DC
		[MethodImpl(MethodImplOptions.Synchronized)]
		internal static void CreateMiniDump(object state)
		{
			MiniDumpInfo miniDumpInfo = (MiniDumpInfo)state;
			try
			{
				OpsTrace.CreateMiniDump(miniDumpInfo.threadId, miniDumpInfo.pExPtrs);
			}
			catch (Exception ex)
			{
				OraTrace.Trace(1U, new string[]
				{
					string.Concat(new string[]
					{
						" (ERROR) CreateMiniDump: ",
						ex.GetType().ToString(),
						": ",
						ex.Message,
						"\n"
					})
				});
			}
			miniDumpInfo.evt.Set();
		}

		// Token: 0x040001C8 RID: 456
		internal const int DEFAULT_STMT_CACHE_SIZE = 0;

		// Token: 0x040001C9 RID: 457
		internal const uint LEVEL_NONE = 0U;

		// Token: 0x040001CA RID: 458
		internal const uint LEVEL_ENTRY = 1U;

		// Token: 0x040001CB RID: 459
		internal const uint LEVEL_EXIT = 1U;

		// Token: 0x040001CC RID: 460
		internal const uint LEVEL_SQL = 1U;

		// Token: 0x040001CD RID: 461
		internal const uint LEVEL_CONPOOL = 2U;

		// Token: 0x040001CE RID: 462
		internal const uint LEVEL_MTS = 4U;

		// Token: 0x040001CF RID: 463
		internal const uint LEVEL_MINIDUMP = 8U;

		// Token: 0x040001D0 RID: 464
		internal const uint LEVEL_GRID_CR = 16U;

		// Token: 0x040001D1 RID: 465
		internal const uint LEVEL_GRID_RLB = 32U;

		// Token: 0x040001D2 RID: 466
		internal const uint LEVEL_TUNING = 64U;

		// Token: 0x040001D3 RID: 467
		internal static bool m_RegistryRead;

		// Token: 0x040001D4 RID: 468
		internal static string m_oraOpsDllPath = string.Empty;

		// Token: 0x040001D5 RID: 469
		internal static uint m_TraceLevel = 0U;

		// Token: 0x040001D6 RID: 470
		internal static uint m_TraceOption = 0U;

		// Token: 0x040001D7 RID: 471
		internal static uint m_udtCacheSize = 4096U;

		// Token: 0x040001D8 RID: 472
		internal static int m_StmtCacheSize = 0;

		// Token: 0x040001D9 RID: 473
		internal static uint m_checkConStatus = 1U;

		// Token: 0x040001DA RID: 474
		internal static uint m_dynamicEnlist = 0U;

		// Token: 0x040001DB RID: 475
		internal static int m_FetchSize = 131072;

		// Token: 0x040001DC RID: 476
		internal static int m_ociEvents = 0;

		// Token: 0x040001DD RID: 477
		internal static int m_stmtCacheWithUdts = 1;

		// Token: 0x040001DE RID: 478
		internal static int m_PSPE = 1;

		// Token: 0x040001DF RID: 479
		internal static int m_MetadataPooling = 1;

		// Token: 0x040001E0 RID: 480
		internal static int m_DBNotificationPort = -1;

		// Token: 0x040001E1 RID: 481
		internal static PerfCounterLevel m_PerformanceCounters;

		// Token: 0x040001E2 RID: 482
		internal static int m_threadPoolMaxSize = -1;

		// Token: 0x040001E3 RID: 483
		internal static int m_DBNotificationRegInterval = 0;

		// Token: 0x040001E4 RID: 484
		internal static int m_demandOrclPermission = 0;

		// Token: 0x040001E5 RID: 485
		internal static string m_traceFileName = "";

		// Token: 0x040001E6 RID: 486
		internal static int m_CPThreadPrioritization = 1;

		// Token: 0x040001E7 RID: 487
		internal static bool m_NoPSPESupport = false;

		// Token: 0x040001E8 RID: 488
		internal static int m_InitialLOBFetchSize = -1;

		// Token: 0x040001E9 RID: 489
		internal static int m_InitialLONGFetchSize = -1;

		// Token: 0x040001EA RID: 490
		internal static bool m_selfTuning = true;

		// Token: 0x040001EB RID: 491
		internal static int m_maxStatementCacheSize = 100;

		// Token: 0x040001EC RID: 492
		private static object m_maxStatementCacheSizeLock = new object();

		// Token: 0x040001ED RID: 493
		internal static string m_appEdition = "";

		// Token: 0x040001EE RID: 494
		internal static string m_MetaDataXml = null;

		// Token: 0x040001EF RID: 495
		internal static int m_RevertBUErrHandling = 0;

		// Token: 0x040001F0 RID: 496
		internal static int m_fetchArrayPooling = 1;

		// Token: 0x040001F1 RID: 497
		internal static bool m_configSectionRead;

		// Token: 0x040001F2 RID: 498
		internal static object m_regReadSync = new object();
	}
}

using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.ConstrainedExecution;

namespace System.Net
{
	// Token: 0x020004EC RID: 1260
	internal static class GlobalLog
	{
		// Token: 0x0600273E RID: 10046 RVA: 0x000A2506 File Offset: 0x000A1506
		[ReliabilityContract(Consistency.MayCorruptAppDomain, Cer.None)]
		private static BaseLoggingObject LoggingInitialize()
		{
			return new BaseLoggingObject();
		}

		// Token: 0x17000822 RID: 2082
		// (get) Token: 0x0600273F RID: 10047 RVA: 0x000A250D File Offset: 0x000A150D
		internal static ThreadKinds CurrentThreadKind
		{
			get
			{
				return ThreadKinds.Unknown;
			}
		}

		// Token: 0x06002740 RID: 10048 RVA: 0x000A2510 File Offset: 0x000A1510
		[ReliabilityContract(Consistency.MayCorruptAppDomain, Cer.None)]
		[Conditional("DEBUG")]
		internal static void SetThreadSource(ThreadKinds source)
		{
		}

		// Token: 0x06002741 RID: 10049 RVA: 0x000A2512 File Offset: 0x000A1512
		[ReliabilityContract(Consistency.MayCorruptAppDomain, Cer.None)]
		[Conditional("DEBUG")]
		internal static void ThreadContract(ThreadKinds kind, string errorMsg)
		{
		}

		// Token: 0x06002742 RID: 10050 RVA: 0x000A2514 File Offset: 0x000A1514
		[ReliabilityContract(Consistency.MayCorruptAppDomain, Cer.None)]
		[Conditional("DEBUG")]
		internal static void ThreadContract(ThreadKinds kind, ThreadKinds allowedSources, string errorMsg)
		{
			if ((kind & ThreadKinds.SourceMask) != ThreadKinds.Unknown || (allowedSources & ThreadKinds.SourceMask) != allowedSources)
			{
				throw new InternalException();
			}
			ThreadKinds currentThreadKind = GlobalLog.CurrentThreadKind;
		}

		// Token: 0x06002743 RID: 10051 RVA: 0x000A2535 File Offset: 0x000A1535
		[Conditional("TRAVE")]
		public static void AddToArray(string msg)
		{
		}

		// Token: 0x06002744 RID: 10052 RVA: 0x000A2537 File Offset: 0x000A1537
		[Conditional("TRAVE")]
		public static void Ignore(object msg)
		{
		}

		// Token: 0x06002745 RID: 10053 RVA: 0x000A2539 File Offset: 0x000A1539
		[ReliabilityContract(Consistency.MayCorruptAppDomain, Cer.None)]
		[Conditional("TRAVE")]
		public static void Print(string msg)
		{
		}

		// Token: 0x06002746 RID: 10054 RVA: 0x000A253B File Offset: 0x000A153B
		[Conditional("TRAVE")]
		public static void PrintHex(string msg, object value)
		{
		}

		// Token: 0x06002747 RID: 10055 RVA: 0x000A253D File Offset: 0x000A153D
		[Conditional("TRAVE")]
		public static void Enter(string func)
		{
		}

		// Token: 0x06002748 RID: 10056 RVA: 0x000A253F File Offset: 0x000A153F
		[Conditional("TRAVE")]
		public static void Enter(string func, string parms)
		{
		}

		// Token: 0x06002749 RID: 10057 RVA: 0x000A2544 File Offset: 0x000A1544
		[Conditional("DEBUG")]
		[Conditional("_FORCE_ASSERTS")]
		[ReliabilityContract(Consistency.MayCorruptAppDomain, Cer.None)]
		public static void Assert(bool condition, string messageFormat, params object[] data)
		{
			if (!condition)
			{
				string text = string.Format(CultureInfo.InvariantCulture, messageFormat, data);
				int num = text.IndexOf('|');
				if (num == -1)
				{
					return;
				}
				int length = text.Length;
			}
		}

		// Token: 0x0600274A RID: 10058 RVA: 0x000A2576 File Offset: 0x000A1576
		[ReliabilityContract(Consistency.MayCorruptAppDomain, Cer.None)]
		[Conditional("_FORCE_ASSERTS")]
		[Conditional("DEBUG")]
		public static void Assert(string message)
		{
		}

		// Token: 0x0600274B RID: 10059 RVA: 0x000A2578 File Offset: 0x000A1578
		[Conditional("_FORCE_ASSERTS")]
		[ReliabilityContract(Consistency.MayCorruptAppDomain, Cer.None)]
		[Conditional("DEBUG")]
		public static void Assert(string message, string detailMessage)
		{
			try
			{
				GlobalLog.Logobject.DumpArray(false);
			}
			finally
			{
				UnsafeNclNativeMethods.DebugBreak();
				Debugger.Break();
			}
		}

		// Token: 0x0600274C RID: 10060 RVA: 0x000A25B0 File Offset: 0x000A15B0
		[Conditional("TRAVE")]
		public static void LeaveException(string func, Exception exception)
		{
		}

		// Token: 0x0600274D RID: 10061 RVA: 0x000A25B2 File Offset: 0x000A15B2
		[Conditional("TRAVE")]
		public static void Leave(string func)
		{
		}

		// Token: 0x0600274E RID: 10062 RVA: 0x000A25B4 File Offset: 0x000A15B4
		[Conditional("TRAVE")]
		public static void Leave(string func, string result)
		{
		}

		// Token: 0x0600274F RID: 10063 RVA: 0x000A25B6 File Offset: 0x000A15B6
		[Conditional("TRAVE")]
		public static void Leave(string func, int returnval)
		{
		}

		// Token: 0x06002750 RID: 10064 RVA: 0x000A25B8 File Offset: 0x000A15B8
		[Conditional("TRAVE")]
		public static void Leave(string func, bool returnval)
		{
		}

		// Token: 0x06002751 RID: 10065 RVA: 0x000A25BA File Offset: 0x000A15BA
		[Conditional("TRAVE")]
		public static void DumpArray()
		{
		}

		// Token: 0x06002752 RID: 10066 RVA: 0x000A25BC File Offset: 0x000A15BC
		[Conditional("TRAVE")]
		public static void Dump(byte[] buffer)
		{
		}

		// Token: 0x06002753 RID: 10067 RVA: 0x000A25BE File Offset: 0x000A15BE
		[Conditional("TRAVE")]
		public static void Dump(byte[] buffer, int length)
		{
		}

		// Token: 0x06002754 RID: 10068 RVA: 0x000A25C0 File Offset: 0x000A15C0
		[Conditional("TRAVE")]
		public static void Dump(byte[] buffer, int offset, int length)
		{
		}

		// Token: 0x06002755 RID: 10069 RVA: 0x000A25C2 File Offset: 0x000A15C2
		[Conditional("TRAVE")]
		public static void Dump(IntPtr buffer, int offset, int length)
		{
		}

		// Token: 0x06002756 RID: 10070 RVA: 0x000A25C4 File Offset: 0x000A15C4
		[Conditional("DEBUG")]
		internal static void DebugAddRequest(HttpWebRequest request, Connection connection, int flags)
		{
		}

		// Token: 0x06002757 RID: 10071 RVA: 0x000A25C6 File Offset: 0x000A15C6
		[Conditional("DEBUG")]
		internal static void DebugRemoveRequest(HttpWebRequest request)
		{
		}

		// Token: 0x06002758 RID: 10072 RVA: 0x000A25C8 File Offset: 0x000A15C8
		[Conditional("DEBUG")]
		internal static void DebugUpdateRequest(HttpWebRequest request, Connection connection, int flags)
		{
		}

		// Token: 0x040026C0 RID: 9920
		private static BaseLoggingObject Logobject = GlobalLog.LoggingInitialize();
	}
}

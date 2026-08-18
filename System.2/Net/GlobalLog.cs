using System;
using System.Diagnostics;
using System.Globalization;
using System.Runtime.ConstrainedExecution;

namespace System.Net
{
	// Token: 0x020001C3 RID: 451
	internal static class GlobalLog
	{
		// Token: 0x060011C3 RID: 4547 RVA: 0x0006033A File Offset: 0x0005E53A
		[ReliabilityContract(Consistency.MayCorruptAppDomain, Cer.None)]
		private static BaseLoggingObject LoggingInitialize()
		{
			return new BaseLoggingObject();
		}

		// Token: 0x170003E2 RID: 994
		// (get) Token: 0x060011C4 RID: 4548 RVA: 0x00060341 File Offset: 0x0005E541
		internal static ThreadKinds CurrentThreadKind
		{
			get
			{
				return ThreadKinds.Unknown;
			}
		}

		// Token: 0x060011C5 RID: 4549 RVA: 0x00060344 File Offset: 0x0005E544
		[Conditional("DEBUG")]
		[ReliabilityContract(Consistency.MayCorruptAppDomain, Cer.None)]
		internal static void SetThreadSource(ThreadKinds source)
		{
		}

		// Token: 0x060011C6 RID: 4550 RVA: 0x00060346 File Offset: 0x0005E546
		[Conditional("DEBUG")]
		[ReliabilityContract(Consistency.MayCorruptAppDomain, Cer.None)]
		internal static void ThreadContract(ThreadKinds kind, string errorMsg)
		{
		}

		// Token: 0x060011C7 RID: 4551 RVA: 0x00060348 File Offset: 0x0005E548
		[Conditional("DEBUG")]
		[ReliabilityContract(Consistency.MayCorruptAppDomain, Cer.None)]
		internal static void ThreadContract(ThreadKinds kind, ThreadKinds allowedSources, string errorMsg)
		{
			if ((kind & ThreadKinds.SourceMask) != ThreadKinds.Unknown || (allowedSources & ThreadKinds.SourceMask) != allowedSources)
			{
				throw new InternalException();
			}
			ThreadKinds currentThreadKind = GlobalLog.CurrentThreadKind;
		}

		// Token: 0x060011C8 RID: 4552 RVA: 0x00060374 File Offset: 0x0005E574
		[Conditional("TRAVE")]
		public static void AddToArray(string msg)
		{
		}

		// Token: 0x060011C9 RID: 4553 RVA: 0x00060376 File Offset: 0x0005E576
		[Conditional("TRAVE")]
		public static void Ignore(object msg)
		{
		}

		// Token: 0x060011CA RID: 4554 RVA: 0x00060378 File Offset: 0x0005E578
		[Conditional("TRAVE")]
		[ReliabilityContract(Consistency.MayCorruptAppDomain, Cer.None)]
		public static void Print(string msg)
		{
		}

		// Token: 0x060011CB RID: 4555 RVA: 0x0006037A File Offset: 0x0005E57A
		[Conditional("TRAVE")]
		public static void PrintHex(string msg, object value)
		{
		}

		// Token: 0x060011CC RID: 4556 RVA: 0x0006037C File Offset: 0x0005E57C
		[Conditional("TRAVE")]
		public static void Enter(string func)
		{
		}

		// Token: 0x060011CD RID: 4557 RVA: 0x0006037E File Offset: 0x0005E57E
		[Conditional("TRAVE")]
		public static void Enter(string func, string parms)
		{
		}

		// Token: 0x060011CE RID: 4558 RVA: 0x00060380 File Offset: 0x0005E580
		[Conditional("DEBUG")]
		[Conditional("_FORCE_ASSERTS")]
		[ReliabilityContract(Consistency.MayCorruptAppDomain, Cer.None)]
		public static void Assert(bool condition, string messageFormat, params object[] data)
		{
			if (!condition)
			{
				string text = string.Format(CultureInfo.InvariantCulture, messageFormat, data);
				int num = text.IndexOf('|');
				if (num != -1)
				{
					int num2 = text.Length - num - 1;
				}
			}
		}

		// Token: 0x060011CF RID: 4559 RVA: 0x000603B5 File Offset: 0x0005E5B5
		[Conditional("DEBUG")]
		[Conditional("_FORCE_ASSERTS")]
		[ReliabilityContract(Consistency.MayCorruptAppDomain, Cer.None)]
		public static void Assert(string message)
		{
		}

		// Token: 0x060011D0 RID: 4560 RVA: 0x000603B8 File Offset: 0x0005E5B8
		[Conditional("DEBUG")]
		[Conditional("_FORCE_ASSERTS")]
		[ReliabilityContract(Consistency.MayCorruptAppDomain, Cer.None)]
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

		// Token: 0x060011D1 RID: 4561 RVA: 0x000603F0 File Offset: 0x0005E5F0
		[Conditional("TRAVE")]
		public static void LeaveException(string func, Exception exception)
		{
		}

		// Token: 0x060011D2 RID: 4562 RVA: 0x000603F2 File Offset: 0x0005E5F2
		[Conditional("TRAVE")]
		public static void Leave(string func)
		{
		}

		// Token: 0x060011D3 RID: 4563 RVA: 0x000603F4 File Offset: 0x0005E5F4
		[Conditional("TRAVE")]
		public static void Leave(string func, string result)
		{
		}

		// Token: 0x060011D4 RID: 4564 RVA: 0x000603F6 File Offset: 0x0005E5F6
		[Conditional("TRAVE")]
		public static void Leave(string func, int returnval)
		{
		}

		// Token: 0x060011D5 RID: 4565 RVA: 0x000603F8 File Offset: 0x0005E5F8
		[Conditional("TRAVE")]
		public static void Leave(string func, bool returnval)
		{
		}

		// Token: 0x060011D6 RID: 4566 RVA: 0x000603FA File Offset: 0x0005E5FA
		[Conditional("TRAVE")]
		public static void DumpArray()
		{
		}

		// Token: 0x060011D7 RID: 4567 RVA: 0x000603FC File Offset: 0x0005E5FC
		[Conditional("TRAVE")]
		public static void Dump(byte[] buffer)
		{
		}

		// Token: 0x060011D8 RID: 4568 RVA: 0x000603FE File Offset: 0x0005E5FE
		[Conditional("TRAVE")]
		public static void Dump(byte[] buffer, int length)
		{
		}

		// Token: 0x060011D9 RID: 4569 RVA: 0x00060400 File Offset: 0x0005E600
		[Conditional("TRAVE")]
		public static void Dump(byte[] buffer, int offset, int length)
		{
		}

		// Token: 0x060011DA RID: 4570 RVA: 0x00060402 File Offset: 0x0005E602
		[Conditional("TRAVE")]
		public static void Dump(IntPtr buffer, int offset, int length)
		{
		}

		// Token: 0x060011DB RID: 4571 RVA: 0x00060404 File Offset: 0x0005E604
		[Conditional("DEBUG")]
		internal static void DebugAddRequest(HttpWebRequest request, Connection connection, int flags)
		{
		}

		// Token: 0x060011DC RID: 4572 RVA: 0x00060406 File Offset: 0x0005E606
		[Conditional("DEBUG")]
		internal static void DebugRemoveRequest(HttpWebRequest request)
		{
		}

		// Token: 0x060011DD RID: 4573 RVA: 0x00060408 File Offset: 0x0005E608
		[Conditional("DEBUG")]
		internal static void DebugUpdateRequest(HttpWebRequest request, Connection connection, int flags)
		{
		}

		// Token: 0x0400147D RID: 5245
		private static BaseLoggingObject Logobject = GlobalLog.LoggingInitialize();
	}
}

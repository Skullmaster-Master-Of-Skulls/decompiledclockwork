using System;
using System.Collections;
using System.Globalization;
using System.Security.Permissions;
using System.Threading;

namespace System.Diagnostics
{
	// Token: 0x020004AE RID: 1198
	public class TraceEventCache
	{
		// Token: 0x17000ACA RID: 2762
		// (get) Token: 0x06002C88 RID: 11400 RVA: 0x000C80B3 File Offset: 0x000C62B3
		internal Guid ActivityId
		{
			get
			{
				return Trace.CorrelationManager.ActivityId;
			}
		}

		// Token: 0x17000ACB RID: 2763
		// (get) Token: 0x06002C89 RID: 11401 RVA: 0x000C80BF File Offset: 0x000C62BF
		public string Callstack
		{
			get
			{
				if (this.stackTrace == null)
				{
					this.stackTrace = Environment.StackTrace;
				}
				else
				{
					new EnvironmentPermission(PermissionState.Unrestricted).Demand();
				}
				return this.stackTrace;
			}
		}

		// Token: 0x17000ACC RID: 2764
		// (get) Token: 0x06002C8A RID: 11402 RVA: 0x000C80E7 File Offset: 0x000C62E7
		public Stack LogicalOperationStack
		{
			get
			{
				return Trace.CorrelationManager.LogicalOperationStack;
			}
		}

		// Token: 0x17000ACD RID: 2765
		// (get) Token: 0x06002C8B RID: 11403 RVA: 0x000C80F3 File Offset: 0x000C62F3
		public DateTime DateTime
		{
			get
			{
				if (this.dateTime == DateTime.MinValue)
				{
					this.dateTime = DateTime.UtcNow;
				}
				return this.dateTime;
			}
		}

		// Token: 0x17000ACE RID: 2766
		// (get) Token: 0x06002C8C RID: 11404 RVA: 0x000C8118 File Offset: 0x000C6318
		public int ProcessId
		{
			get
			{
				return TraceEventCache.GetProcessId();
			}
		}

		// Token: 0x17000ACF RID: 2767
		// (get) Token: 0x06002C8D RID: 11405 RVA: 0x000C8120 File Offset: 0x000C6320
		public string ThreadId
		{
			get
			{
				return TraceEventCache.GetThreadId().ToString(CultureInfo.InvariantCulture);
			}
		}

		// Token: 0x17000AD0 RID: 2768
		// (get) Token: 0x06002C8E RID: 11406 RVA: 0x000C813F File Offset: 0x000C633F
		public long Timestamp
		{
			get
			{
				if (this.timeStamp == -1L)
				{
					this.timeStamp = Stopwatch.GetTimestamp();
				}
				return this.timeStamp;
			}
		}

		// Token: 0x06002C8F RID: 11407 RVA: 0x000C815C File Offset: 0x000C635C
		private static void InitProcessInfo()
		{
			new SecurityPermission(SecurityPermissionFlag.UnmanagedCode).Demand();
			if (TraceEventCache.processName == null)
			{
				Process currentProcess = Process.GetCurrentProcess();
				try
				{
					TraceEventCache.processId = currentProcess.Id;
					TraceEventCache.processName = currentProcess.ProcessName;
				}
				finally
				{
					currentProcess.Dispose();
				}
			}
		}

		// Token: 0x06002C90 RID: 11408 RVA: 0x000C81B8 File Offset: 0x000C63B8
		internal static int GetProcessId()
		{
			TraceEventCache.InitProcessInfo();
			return TraceEventCache.processId;
		}

		// Token: 0x06002C91 RID: 11409 RVA: 0x000C81C6 File Offset: 0x000C63C6
		internal static string GetProcessName()
		{
			TraceEventCache.InitProcessInfo();
			return TraceEventCache.processName;
		}

		// Token: 0x06002C92 RID: 11410 RVA: 0x000C81D4 File Offset: 0x000C63D4
		internal static int GetThreadId()
		{
			return Thread.CurrentThread.ManagedThreadId;
		}

		// Token: 0x040026D1 RID: 9937
		private static volatile int processId;

		// Token: 0x040026D2 RID: 9938
		private static volatile string processName;

		// Token: 0x040026D3 RID: 9939
		private long timeStamp = -1L;

		// Token: 0x040026D4 RID: 9940
		private DateTime dateTime = DateTime.MinValue;

		// Token: 0x040026D5 RID: 9941
		private string stackTrace;
	}
}

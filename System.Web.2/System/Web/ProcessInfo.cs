using System;

namespace System.Web
{
	// Token: 0x020000EA RID: 234
	public class ProcessInfo
	{
		// Token: 0x170004F9 RID: 1273
		// (get) Token: 0x06000E5B RID: 3675 RVA: 0x00028CE4 File Offset: 0x00026EE4
		public DateTime StartTime
		{
			get
			{
				return this._StartTime;
			}
		}

		// Token: 0x170004FA RID: 1274
		// (get) Token: 0x06000E5C RID: 3676 RVA: 0x00028CEC File Offset: 0x00026EEC
		public TimeSpan Age
		{
			get
			{
				return this._Age;
			}
		}

		// Token: 0x170004FB RID: 1275
		// (get) Token: 0x06000E5D RID: 3677 RVA: 0x00028CF4 File Offset: 0x00026EF4
		public int ProcessID
		{
			get
			{
				return this._ProcessID;
			}
		}

		// Token: 0x170004FC RID: 1276
		// (get) Token: 0x06000E5E RID: 3678 RVA: 0x00028CFC File Offset: 0x00026EFC
		public int RequestCount
		{
			get
			{
				return this._RequestCount;
			}
		}

		// Token: 0x170004FD RID: 1277
		// (get) Token: 0x06000E5F RID: 3679 RVA: 0x00028D04 File Offset: 0x00026F04
		public ProcessStatus Status
		{
			get
			{
				return this._Status;
			}
		}

		// Token: 0x170004FE RID: 1278
		// (get) Token: 0x06000E60 RID: 3680 RVA: 0x00028D0C File Offset: 0x00026F0C
		public ProcessShutdownReason ShutdownReason
		{
			get
			{
				return this._ShutdownReason;
			}
		}

		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x06000E61 RID: 3681 RVA: 0x00028D14 File Offset: 0x00026F14
		public int PeakMemoryUsed
		{
			get
			{
				return this._PeakMemoryUsed;
			}
		}

		// Token: 0x06000E62 RID: 3682 RVA: 0x00028D1C File Offset: 0x00026F1C
		public void SetAll(DateTime startTime, TimeSpan age, int processID, int requestCount, ProcessStatus status, ProcessShutdownReason shutdownReason, int peakMemoryUsed)
		{
			this._StartTime = startTime;
			this._Age = age;
			this._ProcessID = processID;
			this._RequestCount = requestCount;
			this._Status = status;
			this._ShutdownReason = shutdownReason;
			this._PeakMemoryUsed = peakMemoryUsed;
		}

		// Token: 0x06000E63 RID: 3683 RVA: 0x00028D53 File Offset: 0x00026F53
		public ProcessInfo(DateTime startTime, TimeSpan age, int processID, int requestCount, ProcessStatus status, ProcessShutdownReason shutdownReason, int peakMemoryUsed)
		{
			this._StartTime = startTime;
			this._Age = age;
			this._ProcessID = processID;
			this._RequestCount = requestCount;
			this._Status = status;
			this._ShutdownReason = shutdownReason;
			this._PeakMemoryUsed = peakMemoryUsed;
		}

		// Token: 0x06000E64 RID: 3684 RVA: 0x000030B5 File Offset: 0x000012B5
		public ProcessInfo()
		{
		}

		// Token: 0x0400056F RID: 1391
		private DateTime _StartTime;

		// Token: 0x04000570 RID: 1392
		private TimeSpan _Age;

		// Token: 0x04000571 RID: 1393
		private int _ProcessID;

		// Token: 0x04000572 RID: 1394
		private int _RequestCount;

		// Token: 0x04000573 RID: 1395
		private ProcessStatus _Status;

		// Token: 0x04000574 RID: 1396
		private ProcessShutdownReason _ShutdownReason;

		// Token: 0x04000575 RID: 1397
		private int _PeakMemoryUsed;
	}
}

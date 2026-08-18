using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;

namespace System.Diagnostics
{
	// Token: 0x02000500 RID: 1280
	[Designer("System.Diagnostics.Design.ProcessThreadDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[HostProtection(SecurityAction.LinkDemand, SelfAffectingProcessMgmt = true, SelfAffectingThreading = true)]
	public class ProcessThread : Component
	{
		// Token: 0x0600309D RID: 12445 RVA: 0x000DBF4B File Offset: 0x000DA14B
		internal ProcessThread(bool isRemoteMachine, ThreadInfo threadInfo)
		{
			this.isRemoteMachine = isRemoteMachine;
			this.threadInfo = threadInfo;
			GC.SuppressFinalize(this);
		}

		// Token: 0x17000BEB RID: 3051
		// (get) Token: 0x0600309E RID: 12446 RVA: 0x000DBF67 File Offset: 0x000DA167
		[MonitoringDescription("ThreadBasePriority")]
		public int BasePriority
		{
			get
			{
				return this.threadInfo.basePriority;
			}
		}

		// Token: 0x17000BEC RID: 3052
		// (get) Token: 0x0600309F RID: 12447 RVA: 0x000DBF74 File Offset: 0x000DA174
		[MonitoringDescription("ThreadCurrentPriority")]
		public int CurrentPriority
		{
			get
			{
				return this.threadInfo.currentPriority;
			}
		}

		// Token: 0x17000BED RID: 3053
		// (get) Token: 0x060030A0 RID: 12448 RVA: 0x000DBF81 File Offset: 0x000DA181
		[MonitoringDescription("ThreadId")]
		public int Id
		{
			get
			{
				return this.threadInfo.threadId;
			}
		}

		// Token: 0x17000BEE RID: 3054
		// (set) Token: 0x060030A1 RID: 12449 RVA: 0x000DBF90 File Offset: 0x000DA190
		[Browsable(false)]
		public int IdealProcessor
		{
			set
			{
				SafeThreadHandle handle = null;
				try
				{
					handle = this.OpenThreadHandle(32);
					if (NativeMethods.SetThreadIdealProcessor(handle, value) < 0)
					{
						throw new Win32Exception();
					}
				}
				finally
				{
					ProcessThread.CloseThreadHandle(handle);
				}
			}
		}

		// Token: 0x17000BEF RID: 3055
		// (get) Token: 0x060030A2 RID: 12450 RVA: 0x000DBFD4 File Offset: 0x000DA1D4
		// (set) Token: 0x060030A3 RID: 12451 RVA: 0x000DC038 File Offset: 0x000DA238
		[MonitoringDescription("ThreadPriorityBoostEnabled")]
		public bool PriorityBoostEnabled
		{
			get
			{
				if (!this.havePriorityBoostEnabled)
				{
					SafeThreadHandle handle = null;
					try
					{
						handle = this.OpenThreadHandle(64);
						bool flag = false;
						if (!NativeMethods.GetThreadPriorityBoost(handle, out flag))
						{
							throw new Win32Exception();
						}
						this.priorityBoostEnabled = !flag;
						this.havePriorityBoostEnabled = true;
					}
					finally
					{
						ProcessThread.CloseThreadHandle(handle);
					}
				}
				return this.priorityBoostEnabled;
			}
			set
			{
				SafeThreadHandle handle = null;
				try
				{
					handle = this.OpenThreadHandle(32);
					if (!NativeMethods.SetThreadPriorityBoost(handle, !value))
					{
						throw new Win32Exception();
					}
					this.priorityBoostEnabled = value;
					this.havePriorityBoostEnabled = true;
				}
				finally
				{
					ProcessThread.CloseThreadHandle(handle);
				}
			}
		}

		// Token: 0x17000BF0 RID: 3056
		// (get) Token: 0x060030A4 RID: 12452 RVA: 0x000DC08C File Offset: 0x000DA28C
		// (set) Token: 0x060030A5 RID: 12453 RVA: 0x000DC0F0 File Offset: 0x000DA2F0
		[MonitoringDescription("ThreadPriorityLevel")]
		public ThreadPriorityLevel PriorityLevel
		{
			get
			{
				if (!this.havePriorityLevel)
				{
					SafeThreadHandle handle = null;
					try
					{
						handle = this.OpenThreadHandle(64);
						int threadPriority = NativeMethods.GetThreadPriority(handle);
						if (threadPriority == 2147483647)
						{
							throw new Win32Exception();
						}
						this.priorityLevel = (ThreadPriorityLevel)threadPriority;
						this.havePriorityLevel = true;
					}
					finally
					{
						ProcessThread.CloseThreadHandle(handle);
					}
				}
				return this.priorityLevel;
			}
			set
			{
				SafeThreadHandle handle = null;
				try
				{
					handle = this.OpenThreadHandle(32);
					if (!NativeMethods.SetThreadPriority(handle, (int)value))
					{
						throw new Win32Exception();
					}
					this.priorityLevel = value;
				}
				finally
				{
					ProcessThread.CloseThreadHandle(handle);
				}
			}
		}

		// Token: 0x17000BF1 RID: 3057
		// (get) Token: 0x060030A6 RID: 12454 RVA: 0x000DC138 File Offset: 0x000DA338
		[MonitoringDescription("ThreadPrivilegedProcessorTime")]
		public TimeSpan PrivilegedProcessorTime
		{
			get
			{
				this.EnsureState(ProcessThread.State.IsNt);
				return this.GetThreadTimes().PrivilegedProcessorTime;
			}
		}

		// Token: 0x17000BF2 RID: 3058
		// (get) Token: 0x060030A7 RID: 12455 RVA: 0x000DC14C File Offset: 0x000DA34C
		[MonitoringDescription("ThreadStartAddress")]
		public IntPtr StartAddress
		{
			get
			{
				this.EnsureState(ProcessThread.State.IsNt);
				return this.threadInfo.startAddress;
			}
		}

		// Token: 0x17000BF3 RID: 3059
		// (get) Token: 0x060030A8 RID: 12456 RVA: 0x000DC160 File Offset: 0x000DA360
		[MonitoringDescription("ThreadStartTime")]
		public DateTime StartTime
		{
			get
			{
				this.EnsureState(ProcessThread.State.IsNt);
				return this.GetThreadTimes().StartTime;
			}
		}

		// Token: 0x17000BF4 RID: 3060
		// (get) Token: 0x060030A9 RID: 12457 RVA: 0x000DC174 File Offset: 0x000DA374
		[MonitoringDescription("ThreadThreadState")]
		public ThreadState ThreadState
		{
			get
			{
				this.EnsureState(ProcessThread.State.IsNt);
				return this.threadInfo.threadState;
			}
		}

		// Token: 0x17000BF5 RID: 3061
		// (get) Token: 0x060030AA RID: 12458 RVA: 0x000DC188 File Offset: 0x000DA388
		[MonitoringDescription("ThreadTotalProcessorTime")]
		public TimeSpan TotalProcessorTime
		{
			get
			{
				this.EnsureState(ProcessThread.State.IsNt);
				return this.GetThreadTimes().TotalProcessorTime;
			}
		}

		// Token: 0x17000BF6 RID: 3062
		// (get) Token: 0x060030AB RID: 12459 RVA: 0x000DC19C File Offset: 0x000DA39C
		[MonitoringDescription("ThreadUserProcessorTime")]
		public TimeSpan UserProcessorTime
		{
			get
			{
				this.EnsureState(ProcessThread.State.IsNt);
				return this.GetThreadTimes().UserProcessorTime;
			}
		}

		// Token: 0x17000BF7 RID: 3063
		// (get) Token: 0x060030AC RID: 12460 RVA: 0x000DC1B0 File Offset: 0x000DA3B0
		[MonitoringDescription("ThreadWaitReason")]
		public ThreadWaitReason WaitReason
		{
			get
			{
				this.EnsureState(ProcessThread.State.IsNt);
				if (this.threadInfo.threadState != ThreadState.Wait)
				{
					throw new InvalidOperationException(SR.GetString("WaitReasonUnavailable"));
				}
				return this.threadInfo.threadWaitReason;
			}
		}

		// Token: 0x060030AD RID: 12461 RVA: 0x000DC1E2 File Offset: 0x000DA3E2
		private static void CloseThreadHandle(SafeThreadHandle handle)
		{
			if (handle != null)
			{
				handle.Close();
			}
		}

		// Token: 0x060030AE RID: 12462 RVA: 0x000DC1F0 File Offset: 0x000DA3F0
		private void EnsureState(ProcessThread.State state)
		{
			if ((state & ProcessThread.State.IsLocal) != (ProcessThread.State)0 && this.isRemoteMachine)
			{
				throw new NotSupportedException(SR.GetString("NotSupportedRemoteThread"));
			}
			if ((state & ProcessThread.State.IsNt) != (ProcessThread.State)0 && Environment.OSVersion.Platform != PlatformID.Win32NT)
			{
				throw new PlatformNotSupportedException(SR.GetString("WinNTRequired"));
			}
		}

		// Token: 0x060030AF RID: 12463 RVA: 0x000DC23C File Offset: 0x000DA43C
		private SafeThreadHandle OpenThreadHandle(int access)
		{
			this.EnsureState(ProcessThread.State.IsLocal);
			return ProcessManager.OpenThread(this.threadInfo.threadId, access);
		}

		// Token: 0x060030B0 RID: 12464 RVA: 0x000DC256 File Offset: 0x000DA456
		public void ResetIdealProcessor()
		{
			this.IdealProcessor = 32;
		}

		// Token: 0x17000BF8 RID: 3064
		// (set) Token: 0x060030B1 RID: 12465 RVA: 0x000DC260 File Offset: 0x000DA460
		[Browsable(false)]
		public IntPtr ProcessorAffinity
		{
			set
			{
				SafeThreadHandle handle = null;
				try
				{
					handle = this.OpenThreadHandle(96);
					if (NativeMethods.SetThreadAffinityMask(handle, new HandleRef(this, value)) == IntPtr.Zero)
					{
						throw new Win32Exception();
					}
				}
				finally
				{
					ProcessThread.CloseThreadHandle(handle);
				}
			}
		}

		// Token: 0x060030B2 RID: 12466 RVA: 0x000DC2B0 File Offset: 0x000DA4B0
		private ProcessThreadTimes GetThreadTimes()
		{
			ProcessThreadTimes processThreadTimes = new ProcessThreadTimes();
			SafeThreadHandle handle = null;
			try
			{
				handle = this.OpenThreadHandle(64);
				if (!NativeMethods.GetThreadTimes(handle, out processThreadTimes.create, out processThreadTimes.exit, out processThreadTimes.kernel, out processThreadTimes.user))
				{
					throw new Win32Exception();
				}
			}
			finally
			{
				ProcessThread.CloseThreadHandle(handle);
			}
			return processThreadTimes;
		}

		// Token: 0x040028B5 RID: 10421
		private ThreadInfo threadInfo;

		// Token: 0x040028B6 RID: 10422
		private bool isRemoteMachine;

		// Token: 0x040028B7 RID: 10423
		private bool priorityBoostEnabled;

		// Token: 0x040028B8 RID: 10424
		private bool havePriorityBoostEnabled;

		// Token: 0x040028B9 RID: 10425
		private ThreadPriorityLevel priorityLevel;

		// Token: 0x040028BA RID: 10426
		private bool havePriorityLevel;

		// Token: 0x02000886 RID: 2182
		private enum State
		{
			// Token: 0x0400379C RID: 14236
			IsLocal = 2,
			// Token: 0x0400379D RID: 14237
			IsNt = 4
		}
	}
}

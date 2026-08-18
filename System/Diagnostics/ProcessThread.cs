using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;

namespace System.Diagnostics
{
	// Token: 0x0200078B RID: 1931
	[Designer("System.Diagnostics.Design.ProcessThreadDesigner, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[HostProtection(SecurityAction.LinkDemand, SelfAffectingProcessMgmt = true, SelfAffectingThreading = true)]
	public class ProcessThread : Component
	{
		// Token: 0x06003BB5 RID: 15285 RVA: 0x000FE32A File Offset: 0x000FD32A
		internal ProcessThread(bool isRemoteMachine, ThreadInfo threadInfo)
		{
			this.isRemoteMachine = isRemoteMachine;
			this.threadInfo = threadInfo;
			GC.SuppressFinalize(this);
		}

		// Token: 0x17000E09 RID: 3593
		// (get) Token: 0x06003BB6 RID: 15286 RVA: 0x000FE346 File Offset: 0x000FD346
		[MonitoringDescription("ThreadBasePriority")]
		public int BasePriority
		{
			get
			{
				return this.threadInfo.basePriority;
			}
		}

		// Token: 0x17000E0A RID: 3594
		// (get) Token: 0x06003BB7 RID: 15287 RVA: 0x000FE353 File Offset: 0x000FD353
		[MonitoringDescription("ThreadCurrentPriority")]
		public int CurrentPriority
		{
			get
			{
				return this.threadInfo.currentPriority;
			}
		}

		// Token: 0x17000E0B RID: 3595
		// (get) Token: 0x06003BB8 RID: 15288 RVA: 0x000FE360 File Offset: 0x000FD360
		[MonitoringDescription("ThreadId")]
		public int Id
		{
			get
			{
				return this.threadInfo.threadId;
			}
		}

		// Token: 0x17000E0C RID: 3596
		// (set) Token: 0x06003BB9 RID: 15289 RVA: 0x000FE370 File Offset: 0x000FD370
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

		// Token: 0x17000E0D RID: 3597
		// (get) Token: 0x06003BBA RID: 15290 RVA: 0x000FE3B4 File Offset: 0x000FD3B4
		// (set) Token: 0x06003BBB RID: 15291 RVA: 0x000FE418 File Offset: 0x000FD418
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

		// Token: 0x17000E0E RID: 3598
		// (get) Token: 0x06003BBC RID: 15292 RVA: 0x000FE46C File Offset: 0x000FD46C
		// (set) Token: 0x06003BBD RID: 15293 RVA: 0x000FE4D0 File Offset: 0x000FD4D0
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

		// Token: 0x17000E0F RID: 3599
		// (get) Token: 0x06003BBE RID: 15294 RVA: 0x000FE518 File Offset: 0x000FD518
		[MonitoringDescription("ThreadPrivilegedProcessorTime")]
		public TimeSpan PrivilegedProcessorTime
		{
			get
			{
				this.EnsureState(ProcessThread.State.IsNt);
				return this.GetThreadTimes().PrivilegedProcessorTime;
			}
		}

		// Token: 0x17000E10 RID: 3600
		// (get) Token: 0x06003BBF RID: 15295 RVA: 0x000FE52C File Offset: 0x000FD52C
		[MonitoringDescription("ThreadStartAddress")]
		public IntPtr StartAddress
		{
			get
			{
				this.EnsureState(ProcessThread.State.IsNt);
				return this.threadInfo.startAddress;
			}
		}

		// Token: 0x17000E11 RID: 3601
		// (get) Token: 0x06003BC0 RID: 15296 RVA: 0x000FE540 File Offset: 0x000FD540
		[MonitoringDescription("ThreadStartTime")]
		public DateTime StartTime
		{
			get
			{
				this.EnsureState(ProcessThread.State.IsNt);
				return this.GetThreadTimes().StartTime;
			}
		}

		// Token: 0x17000E12 RID: 3602
		// (get) Token: 0x06003BC1 RID: 15297 RVA: 0x000FE554 File Offset: 0x000FD554
		[MonitoringDescription("ThreadThreadState")]
		public ThreadState ThreadState
		{
			get
			{
				this.EnsureState(ProcessThread.State.IsNt);
				return this.threadInfo.threadState;
			}
		}

		// Token: 0x17000E13 RID: 3603
		// (get) Token: 0x06003BC2 RID: 15298 RVA: 0x000FE568 File Offset: 0x000FD568
		[MonitoringDescription("ThreadTotalProcessorTime")]
		public TimeSpan TotalProcessorTime
		{
			get
			{
				this.EnsureState(ProcessThread.State.IsNt);
				return this.GetThreadTimes().TotalProcessorTime;
			}
		}

		// Token: 0x17000E14 RID: 3604
		// (get) Token: 0x06003BC3 RID: 15299 RVA: 0x000FE57C File Offset: 0x000FD57C
		[MonitoringDescription("ThreadUserProcessorTime")]
		public TimeSpan UserProcessorTime
		{
			get
			{
				this.EnsureState(ProcessThread.State.IsNt);
				return this.GetThreadTimes().UserProcessorTime;
			}
		}

		// Token: 0x17000E15 RID: 3605
		// (get) Token: 0x06003BC4 RID: 15300 RVA: 0x000FE590 File Offset: 0x000FD590
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

		// Token: 0x06003BC5 RID: 15301 RVA: 0x000FE5C2 File Offset: 0x000FD5C2
		private static void CloseThreadHandle(SafeThreadHandle handle)
		{
			if (handle != null)
			{
				handle.Close();
			}
		}

		// Token: 0x06003BC6 RID: 15302 RVA: 0x000FE5D0 File Offset: 0x000FD5D0
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

		// Token: 0x06003BC7 RID: 15303 RVA: 0x000FE61C File Offset: 0x000FD61C
		private SafeThreadHandle OpenThreadHandle(int access)
		{
			this.EnsureState(ProcessThread.State.IsLocal);
			return ProcessManager.OpenThread(this.threadInfo.threadId, access);
		}

		// Token: 0x06003BC8 RID: 15304 RVA: 0x000FE636 File Offset: 0x000FD636
		public void ResetIdealProcessor()
		{
			this.IdealProcessor = 32;
		}

		// Token: 0x17000E16 RID: 3606
		// (set) Token: 0x06003BC9 RID: 15305 RVA: 0x000FE640 File Offset: 0x000FD640
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

		// Token: 0x06003BCA RID: 15306 RVA: 0x000FE690 File Offset: 0x000FD690
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

		// Token: 0x04003455 RID: 13397
		private ThreadInfo threadInfo;

		// Token: 0x04003456 RID: 13398
		private bool isRemoteMachine;

		// Token: 0x04003457 RID: 13399
		private bool priorityBoostEnabled;

		// Token: 0x04003458 RID: 13400
		private bool havePriorityBoostEnabled;

		// Token: 0x04003459 RID: 13401
		private ThreadPriorityLevel priorityLevel;

		// Token: 0x0400345A RID: 13402
		private bool havePriorityLevel;

		// Token: 0x0200078C RID: 1932
		private enum State
		{
			// Token: 0x0400345C RID: 13404
			IsLocal = 2,
			// Token: 0x0400345D RID: 13405
			IsNt = 4
		}
	}
}

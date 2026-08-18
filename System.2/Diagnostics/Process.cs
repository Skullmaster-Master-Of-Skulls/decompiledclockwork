using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Globalization;
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Threading;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;

namespace System.Diagnostics
{
	// Token: 0x020004EF RID: 1263
	[MonitoringDescription("ProcessDesc")]
	[DefaultEvent("Exited")]
	[DefaultProperty("StartInfo")]
	[Designer("System.Diagnostics.Design.ProcessDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	[HostProtection(SecurityAction.LinkDemand, SharedState = true, Synchronization = true, ExternalProcessMgmt = true, SelfAffectingProcessMgmt = true)]
	[PermissionSet(SecurityAction.InheritanceDemand, Name = "FullTrust")]
	public class Process : Component
	{
		// Token: 0x14000042 RID: 66
		// (add) Token: 0x06002FAA RID: 12202 RVA: 0x000D746C File Offset: 0x000D566C
		// (remove) Token: 0x06002FAB RID: 12203 RVA: 0x000D74A4 File Offset: 0x000D56A4
		[Browsable(true)]
		[MonitoringDescription("ProcessAssociated")]
		public event DataReceivedEventHandler OutputDataReceived;

		// Token: 0x14000043 RID: 67
		// (add) Token: 0x06002FAC RID: 12204 RVA: 0x000D74DC File Offset: 0x000D56DC
		// (remove) Token: 0x06002FAD RID: 12205 RVA: 0x000D7514 File Offset: 0x000D5714
		[Browsable(true)]
		[MonitoringDescription("ProcessAssociated")]
		public event DataReceivedEventHandler ErrorDataReceived;

		// Token: 0x06002FAE RID: 12206 RVA: 0x000D7549 File Offset: 0x000D5749
		public Process()
		{
			this.machineName = ".";
			this.outputStreamReadMode = Process.StreamReadMode.undefined;
			this.errorStreamReadMode = Process.StreamReadMode.undefined;
			this.m_processAccess = 2035711;
		}

		// Token: 0x06002FAF RID: 12207 RVA: 0x000D7578 File Offset: 0x000D5778
		private Process(string machineName, bool isRemoteMachine, int processId, ProcessInfo processInfo)
		{
			this.processInfo = processInfo;
			this.machineName = machineName;
			this.isRemoteMachine = isRemoteMachine;
			this.processId = processId;
			this.haveProcessId = true;
			this.outputStreamReadMode = Process.StreamReadMode.undefined;
			this.errorStreamReadMode = Process.StreamReadMode.undefined;
			this.m_processAccess = 2035711;
		}

		// Token: 0x17000B91 RID: 2961
		// (get) Token: 0x06002FB0 RID: 12208 RVA: 0x000D75C8 File Offset: 0x000D57C8
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("ProcessAssociated")]
		private bool Associated
		{
			get
			{
				return this.haveProcessId || this.haveProcessHandle;
			}
		}

		// Token: 0x17000B92 RID: 2962
		// (get) Token: 0x06002FB1 RID: 12209 RVA: 0x000D75DA File Offset: 0x000D57DA
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("ProcessBasePriority")]
		public int BasePriority
		{
			get
			{
				this.EnsureState(Process.State.HaveProcessInfo);
				return this.processInfo.basePriority;
			}
		}

		// Token: 0x17000B93 RID: 2963
		// (get) Token: 0x06002FB2 RID: 12210 RVA: 0x000D75EE File Offset: 0x000D57EE
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("ProcessExitCode")]
		public int ExitCode
		{
			get
			{
				this.EnsureState(Process.State.Exited);
				return this.exitCode;
			}
		}

		// Token: 0x17000B94 RID: 2964
		// (get) Token: 0x06002FB3 RID: 12211 RVA: 0x000D7600 File Offset: 0x000D5800
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("ProcessTerminated")]
		public bool HasExited
		{
			get
			{
				if (!this.exited)
				{
					this.EnsureState(Process.State.Associated);
					SafeProcessHandle safeProcessHandle = null;
					try
					{
						safeProcessHandle = this.GetProcessHandle(1049600, false);
						int num;
						if (safeProcessHandle.IsInvalid)
						{
							this.exited = true;
						}
						else if (NativeMethods.GetExitCodeProcess(safeProcessHandle, out num) && num != 259)
						{
							this.exited = true;
							this.exitCode = num;
						}
						else
						{
							if (!this.signaled)
							{
								ProcessWaitHandle processWaitHandle = null;
								try
								{
									processWaitHandle = new ProcessWaitHandle(safeProcessHandle);
									this.signaled = processWaitHandle.WaitOne(0, false);
								}
								finally
								{
									if (processWaitHandle != null)
									{
										processWaitHandle.Close();
									}
								}
							}
							if (this.signaled)
							{
								if (!NativeMethods.GetExitCodeProcess(safeProcessHandle, out num))
								{
									throw new Win32Exception();
								}
								this.exited = true;
								this.exitCode = num;
							}
						}
					}
					finally
					{
						this.ReleaseProcessHandle(safeProcessHandle);
					}
					if (this.exited)
					{
						this.RaiseOnExited();
					}
				}
				return this.exited;
			}
		}

		// Token: 0x06002FB4 RID: 12212 RVA: 0x000D76F0 File Offset: 0x000D58F0
		private ProcessThreadTimes GetProcessTimes()
		{
			ProcessThreadTimes processThreadTimes = new ProcessThreadTimes();
			SafeProcessHandle safeProcessHandle = null;
			try
			{
				int access = 1024;
				if (EnvironmentHelpers.IsWindowsVistaOrAbove())
				{
					access = 4096;
				}
				safeProcessHandle = this.GetProcessHandle(access, false);
				if (safeProcessHandle.IsInvalid)
				{
					throw new InvalidOperationException(SR.GetString("ProcessHasExited", new object[]
					{
						this.processId.ToString(CultureInfo.CurrentCulture)
					}));
				}
				if (!NativeMethods.GetProcessTimes(safeProcessHandle, out processThreadTimes.create, out processThreadTimes.exit, out processThreadTimes.kernel, out processThreadTimes.user))
				{
					throw new Win32Exception();
				}
			}
			finally
			{
				this.ReleaseProcessHandle(safeProcessHandle);
			}
			return processThreadTimes;
		}

		// Token: 0x17000B95 RID: 2965
		// (get) Token: 0x06002FB5 RID: 12213 RVA: 0x000D7794 File Offset: 0x000D5994
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("ProcessExitTime")]
		public DateTime ExitTime
		{
			get
			{
				if (!this.haveExitTime)
				{
					this.EnsureState((Process.State)20);
					this.exitTime = this.GetProcessTimes().ExitTime;
					this.haveExitTime = true;
				}
				return this.exitTime;
			}
		}

		// Token: 0x17000B96 RID: 2966
		// (get) Token: 0x06002FB6 RID: 12214 RVA: 0x000D77C4 File Offset: 0x000D59C4
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("ProcessHandle")]
		public IntPtr Handle
		{
			get
			{
				this.EnsureState(Process.State.Associated);
				return this.OpenProcessHandle(this.m_processAccess).DangerousGetHandle();
			}
		}

		// Token: 0x17000B97 RID: 2967
		// (get) Token: 0x06002FB7 RID: 12215 RVA: 0x000D77DF File Offset: 0x000D59DF
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public SafeProcessHandle SafeHandle
		{
			get
			{
				this.EnsureState(Process.State.Associated);
				return this.OpenProcessHandle(this.m_processAccess);
			}
		}

		// Token: 0x17000B98 RID: 2968
		// (get) Token: 0x06002FB8 RID: 12216 RVA: 0x000D77F5 File Offset: 0x000D59F5
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("ProcessHandleCount")]
		public int HandleCount
		{
			get
			{
				this.EnsureState(Process.State.HaveProcessInfo);
				return this.processInfo.handleCount;
			}
		}

		// Token: 0x17000B99 RID: 2969
		// (get) Token: 0x06002FB9 RID: 12217 RVA: 0x000D7809 File Offset: 0x000D5A09
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("ProcessId")]
		public int Id
		{
			get
			{
				this.EnsureState(Process.State.HaveId);
				return this.processId;
			}
		}

		// Token: 0x17000B9A RID: 2970
		// (get) Token: 0x06002FBA RID: 12218 RVA: 0x000D7818 File Offset: 0x000D5A18
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("ProcessMachineName")]
		public string MachineName
		{
			get
			{
				this.EnsureState(Process.State.Associated);
				return this.machineName;
			}
		}

		// Token: 0x17000B9B RID: 2971
		// (get) Token: 0x06002FBB RID: 12219 RVA: 0x000D7828 File Offset: 0x000D5A28
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("ProcessMainWindowHandle")]
		public IntPtr MainWindowHandle
		{
			get
			{
				if (!this.haveMainWindow)
				{
					this.EnsureState((Process.State)3);
					this.mainWindowHandle = ProcessManager.GetMainWindowHandle(this.processId);
					if (this.mainWindowHandle != (IntPtr)0)
					{
						this.haveMainWindow = true;
					}
					else
					{
						this.EnsureState(Process.State.HaveProcessInfo);
					}
				}
				return this.mainWindowHandle;
			}
		}

		// Token: 0x17000B9C RID: 2972
		// (get) Token: 0x06002FBC RID: 12220 RVA: 0x000D7880 File Offset: 0x000D5A80
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("ProcessMainWindowTitle")]
		public string MainWindowTitle
		{
			get
			{
				if (this.mainWindowTitle == null)
				{
					IntPtr intPtr = this.MainWindowHandle;
					if (intPtr == (IntPtr)0)
					{
						this.mainWindowTitle = string.Empty;
					}
					else
					{
						int capacity = NativeMethods.GetWindowTextLength(new HandleRef(this, intPtr)) * 2;
						StringBuilder stringBuilder = new StringBuilder(capacity);
						NativeMethods.GetWindowText(new HandleRef(this, intPtr), stringBuilder, stringBuilder.Capacity);
						this.mainWindowTitle = stringBuilder.ToString();
					}
				}
				return this.mainWindowTitle;
			}
		}

		// Token: 0x17000B9D RID: 2973
		// (get) Token: 0x06002FBD RID: 12221 RVA: 0x000D78F4 File Offset: 0x000D5AF4
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("ProcessMainModule")]
		public ProcessModule MainModule
		{
			get
			{
				if (this.OperatingSystem.Platform == PlatformID.Win32NT)
				{
					this.EnsureState((Process.State)3);
					ModuleInfo firstModuleInfo = NtProcessManager.GetFirstModuleInfo(this.processId);
					return new ProcessModule(firstModuleInfo);
				}
				ProcessModuleCollection processModuleCollection = this.Modules;
				this.EnsureState(Process.State.HaveProcessInfo);
				foreach (object obj in processModuleCollection)
				{
					ProcessModule processModule = (ProcessModule)obj;
					if (processModule.moduleInfo.Id == this.processInfo.mainModuleId)
					{
						return processModule;
					}
				}
				return null;
			}
		}

		// Token: 0x17000B9E RID: 2974
		// (get) Token: 0x06002FBE RID: 12222 RVA: 0x000D799C File Offset: 0x000D5B9C
		// (set) Token: 0x06002FBF RID: 12223 RVA: 0x000D79AA File Offset: 0x000D5BAA
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("ProcessMaxWorkingSet")]
		public IntPtr MaxWorkingSet
		{
			get
			{
				this.EnsureWorkingSetLimits();
				return this.maxWorkingSet;
			}
			set
			{
				this.SetWorkingSetLimits(null, value);
			}
		}

		// Token: 0x17000B9F RID: 2975
		// (get) Token: 0x06002FC0 RID: 12224 RVA: 0x000D79B9 File Offset: 0x000D5BB9
		// (set) Token: 0x06002FC1 RID: 12225 RVA: 0x000D79C7 File Offset: 0x000D5BC7
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("ProcessMinWorkingSet")]
		public IntPtr MinWorkingSet
		{
			get
			{
				this.EnsureWorkingSetLimits();
				return this.minWorkingSet;
			}
			set
			{
				this.SetWorkingSetLimits(value, null);
			}
		}

		// Token: 0x17000BA0 RID: 2976
		// (get) Token: 0x06002FC2 RID: 12226 RVA: 0x000D79D8 File Offset: 0x000D5BD8
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("ProcessModules")]
		public ProcessModuleCollection Modules
		{
			get
			{
				if (this.modules == null)
				{
					this.EnsureState((Process.State)3);
					ModuleInfo[] moduleInfos = ProcessManager.GetModuleInfos(this.processId);
					ProcessModule[] array = new ProcessModule[moduleInfos.Length];
					for (int i = 0; i < moduleInfos.Length; i++)
					{
						array[i] = new ProcessModule(moduleInfos[i]);
					}
					ProcessModuleCollection processModuleCollection = new ProcessModuleCollection(array);
					this.modules = processModuleCollection;
				}
				return this.modules;
			}
		}

		// Token: 0x17000BA1 RID: 2977
		// (get) Token: 0x06002FC3 RID: 12227 RVA: 0x000D7A36 File Offset: 0x000D5C36
		[Obsolete("This property has been deprecated.  Please use System.Diagnostics.Process.NonpagedSystemMemorySize64 instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("ProcessNonpagedSystemMemorySize")]
		public int NonpagedSystemMemorySize
		{
			get
			{
				this.EnsureState(Process.State.HaveNtProcessInfo);
				return (int)this.processInfo.poolNonpagedBytes;
			}
		}

		// Token: 0x17000BA2 RID: 2978
		// (get) Token: 0x06002FC4 RID: 12228 RVA: 0x000D7A4C File Offset: 0x000D5C4C
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("ProcessNonpagedSystemMemorySize")]
		[ComVisible(false)]
		public long NonpagedSystemMemorySize64
		{
			get
			{
				this.EnsureState(Process.State.HaveNtProcessInfo);
				return this.processInfo.poolNonpagedBytes;
			}
		}

		// Token: 0x17000BA3 RID: 2979
		// (get) Token: 0x06002FC5 RID: 12229 RVA: 0x000D7A61 File Offset: 0x000D5C61
		[Obsolete("This property has been deprecated.  Please use System.Diagnostics.Process.PagedMemorySize64 instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("ProcessPagedMemorySize")]
		public int PagedMemorySize
		{
			get
			{
				this.EnsureState(Process.State.HaveNtProcessInfo);
				return (int)this.processInfo.pageFileBytes;
			}
		}

		// Token: 0x17000BA4 RID: 2980
		// (get) Token: 0x06002FC6 RID: 12230 RVA: 0x000D7A77 File Offset: 0x000D5C77
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("ProcessPagedMemorySize")]
		[ComVisible(false)]
		public long PagedMemorySize64
		{
			get
			{
				this.EnsureState(Process.State.HaveNtProcessInfo);
				return this.processInfo.pageFileBytes;
			}
		}

		// Token: 0x17000BA5 RID: 2981
		// (get) Token: 0x06002FC7 RID: 12231 RVA: 0x000D7A8C File Offset: 0x000D5C8C
		[Obsolete("This property has been deprecated.  Please use System.Diagnostics.Process.PagedSystemMemorySize64 instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("ProcessPagedSystemMemorySize")]
		public int PagedSystemMemorySize
		{
			get
			{
				this.EnsureState(Process.State.HaveNtProcessInfo);
				return (int)this.processInfo.poolPagedBytes;
			}
		}

		// Token: 0x17000BA6 RID: 2982
		// (get) Token: 0x06002FC8 RID: 12232 RVA: 0x000D7AA2 File Offset: 0x000D5CA2
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("ProcessPagedSystemMemorySize")]
		[ComVisible(false)]
		public long PagedSystemMemorySize64
		{
			get
			{
				this.EnsureState(Process.State.HaveNtProcessInfo);
				return this.processInfo.poolPagedBytes;
			}
		}

		// Token: 0x17000BA7 RID: 2983
		// (get) Token: 0x06002FC9 RID: 12233 RVA: 0x000D7AB7 File Offset: 0x000D5CB7
		[Obsolete("This property has been deprecated.  Please use System.Diagnostics.Process.PeakPagedMemorySize64 instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("ProcessPeakPagedMemorySize")]
		public int PeakPagedMemorySize
		{
			get
			{
				this.EnsureState(Process.State.HaveNtProcessInfo);
				return (int)this.processInfo.pageFileBytesPeak;
			}
		}

		// Token: 0x17000BA8 RID: 2984
		// (get) Token: 0x06002FCA RID: 12234 RVA: 0x000D7ACD File Offset: 0x000D5CCD
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("ProcessPeakPagedMemorySize")]
		[ComVisible(false)]
		public long PeakPagedMemorySize64
		{
			get
			{
				this.EnsureState(Process.State.HaveNtProcessInfo);
				return this.processInfo.pageFileBytesPeak;
			}
		}

		// Token: 0x17000BA9 RID: 2985
		// (get) Token: 0x06002FCB RID: 12235 RVA: 0x000D7AE2 File Offset: 0x000D5CE2
		[Obsolete("This property has been deprecated.  Please use System.Diagnostics.Process.PeakWorkingSet64 instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("ProcessPeakWorkingSet")]
		public int PeakWorkingSet
		{
			get
			{
				this.EnsureState(Process.State.HaveNtProcessInfo);
				return (int)this.processInfo.workingSetPeak;
			}
		}

		// Token: 0x17000BAA RID: 2986
		// (get) Token: 0x06002FCC RID: 12236 RVA: 0x000D7AF8 File Offset: 0x000D5CF8
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("ProcessPeakWorkingSet")]
		[ComVisible(false)]
		public long PeakWorkingSet64
		{
			get
			{
				this.EnsureState(Process.State.HaveNtProcessInfo);
				return this.processInfo.workingSetPeak;
			}
		}

		// Token: 0x17000BAB RID: 2987
		// (get) Token: 0x06002FCD RID: 12237 RVA: 0x000D7B0D File Offset: 0x000D5D0D
		[Obsolete("This property has been deprecated.  Please use System.Diagnostics.Process.PeakVirtualMemorySize64 instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("ProcessPeakVirtualMemorySize")]
		public int PeakVirtualMemorySize
		{
			get
			{
				this.EnsureState(Process.State.HaveNtProcessInfo);
				return (int)this.processInfo.virtualBytesPeak;
			}
		}

		// Token: 0x17000BAC RID: 2988
		// (get) Token: 0x06002FCE RID: 12238 RVA: 0x000D7B23 File Offset: 0x000D5D23
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("ProcessPeakVirtualMemorySize")]
		[ComVisible(false)]
		public long PeakVirtualMemorySize64
		{
			get
			{
				this.EnsureState(Process.State.HaveNtProcessInfo);
				return this.processInfo.virtualBytesPeak;
			}
		}

		// Token: 0x17000BAD RID: 2989
		// (get) Token: 0x06002FCF RID: 12239 RVA: 0x000D7B38 File Offset: 0x000D5D38
		private OperatingSystem OperatingSystem
		{
			get
			{
				if (this.operatingSystem == null)
				{
					this.operatingSystem = Environment.OSVersion;
				}
				return this.operatingSystem;
			}
		}

		// Token: 0x17000BAE RID: 2990
		// (get) Token: 0x06002FD0 RID: 12240 RVA: 0x000D7B54 File Offset: 0x000D5D54
		// (set) Token: 0x06002FD1 RID: 12241 RVA: 0x000D7BC4 File Offset: 0x000D5DC4
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("ProcessPriorityBoostEnabled")]
		public bool PriorityBoostEnabled
		{
			get
			{
				this.EnsureState(Process.State.IsNt);
				if (!this.havePriorityBoostEnabled)
				{
					SafeProcessHandle handle = null;
					try
					{
						handle = this.GetProcessHandle(1024);
						bool flag = false;
						if (!NativeMethods.GetProcessPriorityBoost(handle, out flag))
						{
							throw new Win32Exception();
						}
						this.priorityBoostEnabled = !flag;
						this.havePriorityBoostEnabled = true;
					}
					finally
					{
						this.ReleaseProcessHandle(handle);
					}
				}
				return this.priorityBoostEnabled;
			}
			set
			{
				this.EnsureState(Process.State.IsNt);
				SafeProcessHandle handle = null;
				try
				{
					handle = this.GetProcessHandle(512);
					if (!NativeMethods.SetProcessPriorityBoost(handle, !value))
					{
						throw new Win32Exception();
					}
					this.priorityBoostEnabled = value;
					this.havePriorityBoostEnabled = true;
				}
				finally
				{
					this.ReleaseProcessHandle(handle);
				}
			}
		}

		// Token: 0x17000BAF RID: 2991
		// (get) Token: 0x06002FD2 RID: 12242 RVA: 0x000D7C20 File Offset: 0x000D5E20
		// (set) Token: 0x06002FD3 RID: 12243 RVA: 0x000D7C84 File Offset: 0x000D5E84
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("ProcessPriorityClass")]
		public ProcessPriorityClass PriorityClass
		{
			get
			{
				if (!this.havePriorityClass)
				{
					SafeProcessHandle handle = null;
					try
					{
						handle = this.GetProcessHandle(1024);
						int num = NativeMethods.GetPriorityClass(handle);
						if (num == 0)
						{
							throw new Win32Exception();
						}
						this.priorityClass = (ProcessPriorityClass)num;
						this.havePriorityClass = true;
					}
					finally
					{
						this.ReleaseProcessHandle(handle);
					}
				}
				return this.priorityClass;
			}
			set
			{
				if (!Enum.IsDefined(typeof(ProcessPriorityClass), value))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(ProcessPriorityClass));
				}
				if ((value & (ProcessPriorityClass)49152) != (ProcessPriorityClass)0 && (this.OperatingSystem.Platform != PlatformID.Win32NT || this.OperatingSystem.Version.Major < 5))
				{
					throw new PlatformNotSupportedException(SR.GetString("PriorityClassNotSupported"), null);
				}
				SafeProcessHandle handle = null;
				try
				{
					handle = this.GetProcessHandle(512);
					if (!NativeMethods.SetPriorityClass(handle, (int)value))
					{
						throw new Win32Exception();
					}
					this.priorityClass = value;
					this.havePriorityClass = true;
				}
				finally
				{
					this.ReleaseProcessHandle(handle);
				}
			}
		}

		// Token: 0x17000BB0 RID: 2992
		// (get) Token: 0x06002FD4 RID: 12244 RVA: 0x000D7D40 File Offset: 0x000D5F40
		[Obsolete("This property has been deprecated.  Please use System.Diagnostics.Process.PrivateMemorySize64 instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("ProcessPrivateMemorySize")]
		public int PrivateMemorySize
		{
			get
			{
				this.EnsureState(Process.State.HaveNtProcessInfo);
				return (int)this.processInfo.privateBytes;
			}
		}

		// Token: 0x17000BB1 RID: 2993
		// (get) Token: 0x06002FD5 RID: 12245 RVA: 0x000D7D56 File Offset: 0x000D5F56
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("ProcessPrivateMemorySize")]
		[ComVisible(false)]
		public long PrivateMemorySize64
		{
			get
			{
				this.EnsureState(Process.State.HaveNtProcessInfo);
				return this.processInfo.privateBytes;
			}
		}

		// Token: 0x17000BB2 RID: 2994
		// (get) Token: 0x06002FD6 RID: 12246 RVA: 0x000D7D6B File Offset: 0x000D5F6B
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("ProcessPrivilegedProcessorTime")]
		public TimeSpan PrivilegedProcessorTime
		{
			get
			{
				this.EnsureState(Process.State.IsNt);
				return this.GetProcessTimes().PrivilegedProcessorTime;
			}
		}

		// Token: 0x17000BB3 RID: 2995
		// (get) Token: 0x06002FD7 RID: 12247 RVA: 0x000D7D80 File Offset: 0x000D5F80
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("ProcessProcessName")]
		public string ProcessName
		{
			get
			{
				this.EnsureState(Process.State.HaveProcessInfo);
				string processName = this.processInfo.processName;
				if (processName.Length == 15 && ProcessManager.IsNt && ProcessManager.IsOSOlderThanXP && !this.isRemoteMachine)
				{
					try
					{
						string moduleName = this.MainModule.ModuleName;
						if (moduleName != null)
						{
							this.processInfo.processName = Path.ChangeExtension(Path.GetFileName(moduleName), null);
						}
					}
					catch (Exception)
					{
					}
				}
				return this.processInfo.processName;
			}
		}

		// Token: 0x17000BB4 RID: 2996
		// (get) Token: 0x06002FD8 RID: 12248 RVA: 0x000D7E08 File Offset: 0x000D6008
		// (set) Token: 0x06002FD9 RID: 12249 RVA: 0x000D7E6C File Offset: 0x000D606C
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("ProcessProcessorAffinity")]
		public IntPtr ProcessorAffinity
		{
			get
			{
				if (!this.haveProcessorAffinity)
				{
					SafeProcessHandle handle = null;
					try
					{
						handle = this.GetProcessHandle(1024);
						IntPtr intPtr;
						IntPtr intPtr2;
						if (!NativeMethods.GetProcessAffinityMask(handle, out intPtr, out intPtr2))
						{
							throw new Win32Exception();
						}
						this.processorAffinity = intPtr;
					}
					finally
					{
						this.ReleaseProcessHandle(handle);
					}
					this.haveProcessorAffinity = true;
				}
				return this.processorAffinity;
			}
			set
			{
				SafeProcessHandle handle = null;
				try
				{
					handle = this.GetProcessHandle(512);
					if (!NativeMethods.SetProcessAffinityMask(handle, value))
					{
						throw new Win32Exception();
					}
					this.processorAffinity = value;
					this.haveProcessorAffinity = true;
				}
				finally
				{
					this.ReleaseProcessHandle(handle);
				}
			}
		}

		// Token: 0x17000BB5 RID: 2997
		// (get) Token: 0x06002FDA RID: 12250 RVA: 0x000D7EC0 File Offset: 0x000D60C0
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("ProcessResponding")]
		public bool Responding
		{
			get
			{
				if (!this.haveResponding)
				{
					IntPtr intPtr = this.MainWindowHandle;
					if (intPtr == (IntPtr)0)
					{
						this.responding = true;
					}
					else
					{
						IntPtr intPtr2;
						this.responding = (NativeMethods.SendMessageTimeout(new HandleRef(this, intPtr), 0, IntPtr.Zero, IntPtr.Zero, 2, 5000, out intPtr2) != (IntPtr)0);
					}
				}
				return this.responding;
			}
		}

		// Token: 0x17000BB6 RID: 2998
		// (get) Token: 0x06002FDB RID: 12251 RVA: 0x000D7F29 File Offset: 0x000D6129
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("ProcessSessionId")]
		public int SessionId
		{
			get
			{
				this.EnsureState(Process.State.HaveNtProcessInfo);
				return this.processInfo.sessionId;
			}
		}

		// Token: 0x17000BB7 RID: 2999
		// (get) Token: 0x06002FDC RID: 12252 RVA: 0x000D7F3E File Offset: 0x000D613E
		// (set) Token: 0x06002FDD RID: 12253 RVA: 0x000D7F5A File Offset: 0x000D615A
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[MonitoringDescription("ProcessStartInfo")]
		public ProcessStartInfo StartInfo
		{
			get
			{
				if (this.startInfo == null)
				{
					this.startInfo = new ProcessStartInfo(this);
				}
				return this.startInfo;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.startInfo = value;
			}
		}

		// Token: 0x17000BB8 RID: 3000
		// (get) Token: 0x06002FDE RID: 12254 RVA: 0x000D7F71 File Offset: 0x000D6171
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("ProcessStartTime")]
		public DateTime StartTime
		{
			get
			{
				this.EnsureState(Process.State.IsNt);
				return this.GetProcessTimes().StartTime;
			}
		}

		// Token: 0x17000BB9 RID: 3001
		// (get) Token: 0x06002FDF RID: 12255 RVA: 0x000D7F88 File Offset: 0x000D6188
		// (set) Token: 0x06002FE0 RID: 12256 RVA: 0x000D7FE2 File Offset: 0x000D61E2
		[Browsable(false)]
		[DefaultValue(null)]
		[MonitoringDescription("ProcessSynchronizingObject")]
		public ISynchronizeInvoke SynchronizingObject
		{
			get
			{
				if (this.synchronizingObject == null && base.DesignMode)
				{
					IDesignerHost designerHost = (IDesignerHost)this.GetService(typeof(IDesignerHost));
					if (designerHost != null)
					{
						object rootComponent = designerHost.RootComponent;
						if (rootComponent != null && rootComponent is ISynchronizeInvoke)
						{
							this.synchronizingObject = (ISynchronizeInvoke)rootComponent;
						}
					}
				}
				return this.synchronizingObject;
			}
			set
			{
				this.synchronizingObject = value;
			}
		}

		// Token: 0x17000BBA RID: 3002
		// (get) Token: 0x06002FE1 RID: 12257 RVA: 0x000D7FEC File Offset: 0x000D61EC
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("ProcessThreads")]
		public ProcessThreadCollection Threads
		{
			get
			{
				if (this.threads == null)
				{
					this.EnsureState(Process.State.HaveProcessInfo);
					int count = this.processInfo.threadInfoList.Count;
					ProcessThread[] array = new ProcessThread[count];
					for (int i = 0; i < count; i++)
					{
						array[i] = new ProcessThread(this.isRemoteMachine, (ThreadInfo)this.processInfo.threadInfoList[i]);
					}
					ProcessThreadCollection processThreadCollection = new ProcessThreadCollection(array);
					this.threads = processThreadCollection;
				}
				return this.threads;
			}
		}

		// Token: 0x17000BBB RID: 3003
		// (get) Token: 0x06002FE2 RID: 12258 RVA: 0x000D8064 File Offset: 0x000D6264
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("ProcessTotalProcessorTime")]
		public TimeSpan TotalProcessorTime
		{
			get
			{
				this.EnsureState(Process.State.IsNt);
				return this.GetProcessTimes().TotalProcessorTime;
			}
		}

		// Token: 0x17000BBC RID: 3004
		// (get) Token: 0x06002FE3 RID: 12259 RVA: 0x000D8078 File Offset: 0x000D6278
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("ProcessUserProcessorTime")]
		public TimeSpan UserProcessorTime
		{
			get
			{
				this.EnsureState(Process.State.IsNt);
				return this.GetProcessTimes().UserProcessorTime;
			}
		}

		// Token: 0x17000BBD RID: 3005
		// (get) Token: 0x06002FE4 RID: 12260 RVA: 0x000D808C File Offset: 0x000D628C
		[Obsolete("This property has been deprecated.  Please use System.Diagnostics.Process.VirtualMemorySize64 instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("ProcessVirtualMemorySize")]
		public int VirtualMemorySize
		{
			get
			{
				this.EnsureState(Process.State.HaveNtProcessInfo);
				return (int)this.processInfo.virtualBytes;
			}
		}

		// Token: 0x17000BBE RID: 3006
		// (get) Token: 0x06002FE5 RID: 12261 RVA: 0x000D80A2 File Offset: 0x000D62A2
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("ProcessVirtualMemorySize")]
		[ComVisible(false)]
		public long VirtualMemorySize64
		{
			get
			{
				this.EnsureState(Process.State.HaveNtProcessInfo);
				return this.processInfo.virtualBytes;
			}
		}

		// Token: 0x17000BBF RID: 3007
		// (get) Token: 0x06002FE6 RID: 12262 RVA: 0x000D80B7 File Offset: 0x000D62B7
		// (set) Token: 0x06002FE7 RID: 12263 RVA: 0x000D80BF File Offset: 0x000D62BF
		[Browsable(false)]
		[DefaultValue(false)]
		[MonitoringDescription("ProcessEnableRaisingEvents")]
		public bool EnableRaisingEvents
		{
			get
			{
				return this.watchForExit;
			}
			set
			{
				if (value != this.watchForExit)
				{
					if (this.Associated)
					{
						if (value)
						{
							this.OpenProcessHandle();
							this.EnsureWatchingForExit();
						}
						else
						{
							this.StopWatchingForExit();
						}
					}
					this.watchForExit = value;
				}
			}
		}

		// Token: 0x17000BC0 RID: 3008
		// (get) Token: 0x06002FE8 RID: 12264 RVA: 0x000D80F1 File Offset: 0x000D62F1
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("ProcessStandardInput")]
		public StreamWriter StandardInput
		{
			get
			{
				if (this.standardInput == null)
				{
					throw new InvalidOperationException(SR.GetString("CantGetStandardIn"));
				}
				return this.standardInput;
			}
		}

		// Token: 0x17000BC1 RID: 3009
		// (get) Token: 0x06002FE9 RID: 12265 RVA: 0x000D8114 File Offset: 0x000D6314
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("ProcessStandardOutput")]
		public StreamReader StandardOutput
		{
			get
			{
				if (this.standardOutput == null)
				{
					throw new InvalidOperationException(SR.GetString("CantGetStandardOut"));
				}
				if (this.outputStreamReadMode == Process.StreamReadMode.undefined)
				{
					this.outputStreamReadMode = Process.StreamReadMode.syncMode;
				}
				else if (this.outputStreamReadMode != Process.StreamReadMode.syncMode)
				{
					throw new InvalidOperationException(SR.GetString("CantMixSyncAsyncOperation"));
				}
				return this.standardOutput;
			}
		}

		// Token: 0x17000BC2 RID: 3010
		// (get) Token: 0x06002FEA RID: 12266 RVA: 0x000D816C File Offset: 0x000D636C
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("ProcessStandardError")]
		public StreamReader StandardError
		{
			get
			{
				if (this.standardError == null)
				{
					throw new InvalidOperationException(SR.GetString("CantGetStandardError"));
				}
				if (this.errorStreamReadMode == Process.StreamReadMode.undefined)
				{
					this.errorStreamReadMode = Process.StreamReadMode.syncMode;
				}
				else if (this.errorStreamReadMode != Process.StreamReadMode.syncMode)
				{
					throw new InvalidOperationException(SR.GetString("CantMixSyncAsyncOperation"));
				}
				return this.standardError;
			}
		}

		// Token: 0x17000BC3 RID: 3011
		// (get) Token: 0x06002FEB RID: 12267 RVA: 0x000D81C1 File Offset: 0x000D63C1
		[Obsolete("This property has been deprecated.  Please use System.Diagnostics.Process.WorkingSet64 instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("ProcessWorkingSet")]
		public int WorkingSet
		{
			get
			{
				this.EnsureState(Process.State.HaveNtProcessInfo);
				return (int)this.processInfo.workingSet;
			}
		}

		// Token: 0x17000BC4 RID: 3012
		// (get) Token: 0x06002FEC RID: 12268 RVA: 0x000D81D7 File Offset: 0x000D63D7
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[MonitoringDescription("ProcessWorkingSet")]
		[ComVisible(false)]
		public long WorkingSet64
		{
			get
			{
				this.EnsureState(Process.State.HaveNtProcessInfo);
				return this.processInfo.workingSet;
			}
		}

		// Token: 0x14000044 RID: 68
		// (add) Token: 0x06002FED RID: 12269 RVA: 0x000D81EC File Offset: 0x000D63EC
		// (remove) Token: 0x06002FEE RID: 12270 RVA: 0x000D8205 File Offset: 0x000D6405
		[Category("Behavior")]
		[MonitoringDescription("ProcessExited")]
		public event EventHandler Exited
		{
			add
			{
				this.onExited = (EventHandler)Delegate.Combine(this.onExited, value);
			}
			remove
			{
				this.onExited = (EventHandler)Delegate.Remove(this.onExited, value);
			}
		}

		// Token: 0x06002FEF RID: 12271 RVA: 0x000D8220 File Offset: 0x000D6420
		public bool CloseMainWindow()
		{
			IntPtr intPtr = this.MainWindowHandle;
			if (intPtr == (IntPtr)0)
			{
				return false;
			}
			int windowLong = NativeMethods.GetWindowLong(new HandleRef(this, intPtr), -16);
			if ((windowLong & 134217728) != 0)
			{
				return false;
			}
			NativeMethods.PostMessage(new HandleRef(this, intPtr), 16, IntPtr.Zero, IntPtr.Zero);
			return true;
		}

		// Token: 0x06002FF0 RID: 12272 RVA: 0x000D8278 File Offset: 0x000D6478
		private void ReleaseProcessHandle(SafeProcessHandle handle)
		{
			if (handle == null)
			{
				return;
			}
			if (this.haveProcessHandle && handle == this.m_processHandle)
			{
				return;
			}
			handle.Close();
		}

		// Token: 0x06002FF1 RID: 12273 RVA: 0x000D8296 File Offset: 0x000D6496
		private void CompletionCallback(object context, bool wasSignaled)
		{
			this.StopWatchingForExit();
			this.RaiseOnExited();
		}

		// Token: 0x06002FF2 RID: 12274 RVA: 0x000D82A4 File Offset: 0x000D64A4
		protected override void Dispose(bool disposing)
		{
			if (!this.disposed)
			{
				if (disposing)
				{
					this.Close();
				}
				this.disposed = true;
				base.Dispose(disposing);
			}
		}

		// Token: 0x06002FF3 RID: 12275 RVA: 0x000D82C8 File Offset: 0x000D64C8
		public void Close()
		{
			if (this.Associated)
			{
				if (this.haveProcessHandle)
				{
					this.StopWatchingForExit();
					this.m_processHandle.Close();
					this.m_processHandle = null;
					this.haveProcessHandle = false;
				}
				this.haveProcessId = false;
				this.isRemoteMachine = false;
				this.machineName = ".";
				this.raisedOnExited = false;
				this.standardOutput = null;
				this.standardInput = null;
				this.standardError = null;
				this.output = null;
				this.error = null;
				this.Refresh();
			}
		}

		// Token: 0x06002FF4 RID: 12276 RVA: 0x000D8350 File Offset: 0x000D6550
		private void EnsureState(Process.State state)
		{
			if ((state & Process.State.IsWin2k) != (Process.State)0 && (this.OperatingSystem.Platform != PlatformID.Win32NT || this.OperatingSystem.Version.Major < 5))
			{
				throw new PlatformNotSupportedException(SR.GetString("Win2kRequired"));
			}
			if ((state & Process.State.IsNt) != (Process.State)0 && this.OperatingSystem.Platform != PlatformID.Win32NT)
			{
				throw new PlatformNotSupportedException(SR.GetString("WinNTRequired"));
			}
			if ((state & Process.State.Associated) != (Process.State)0 && !this.Associated)
			{
				throw new InvalidOperationException(SR.GetString("NoAssociatedProcess"));
			}
			if ((state & Process.State.HaveId) != (Process.State)0 && !this.haveProcessId)
			{
				if (!this.haveProcessHandle)
				{
					this.EnsureState(Process.State.Associated);
					throw new InvalidOperationException(SR.GetString("ProcessIdRequired"));
				}
				this.SetProcessId(ProcessManager.GetProcessIdFromHandle(this.m_processHandle));
			}
			if ((state & Process.State.IsLocal) != (Process.State)0 && this.isRemoteMachine)
			{
				throw new NotSupportedException(SR.GetString("NotSupportedRemote"));
			}
			if ((state & Process.State.HaveProcessInfo) != (Process.State)0 && this.processInfo == null)
			{
				if ((state & Process.State.HaveId) == (Process.State)0)
				{
					this.EnsureState(Process.State.HaveId);
				}
				this.processInfo = ProcessManager.GetProcessInfo(this.processId, this.machineName);
				if (this.processInfo == null)
				{
					throw new InvalidOperationException(SR.GetString("NoProcessInfo"));
				}
			}
			if ((state & Process.State.Exited) != (Process.State)0)
			{
				if (!this.HasExited)
				{
					throw new InvalidOperationException(SR.GetString("WaitTillExit"));
				}
				if (!this.haveProcessHandle)
				{
					throw new InvalidOperationException(SR.GetString("NoProcessHandle"));
				}
			}
		}

		// Token: 0x06002FF5 RID: 12277 RVA: 0x000D84B0 File Offset: 0x000D66B0
		private void EnsureWatchingForExit()
		{
			if (!this.watchingForExit)
			{
				lock (this)
				{
					if (!this.watchingForExit)
					{
						this.watchingForExit = true;
						try
						{
							this.waitHandle = new ProcessWaitHandle(this.m_processHandle);
							this.registeredWaitHandle = ThreadPool.RegisterWaitForSingleObject(this.waitHandle, new WaitOrTimerCallback(this.CompletionCallback), null, -1, true);
						}
						catch
						{
							this.watchingForExit = false;
							throw;
						}
					}
				}
			}
		}

		// Token: 0x06002FF6 RID: 12278 RVA: 0x000D8548 File Offset: 0x000D6748
		private void EnsureWorkingSetLimits()
		{
			this.EnsureState(Process.State.IsNt);
			if (!this.haveWorkingSetLimits)
			{
				SafeProcessHandle handle = null;
				try
				{
					handle = this.GetProcessHandle(1024);
					IntPtr intPtr;
					IntPtr intPtr2;
					if (!NativeMethods.GetProcessWorkingSetSize(handle, out intPtr, out intPtr2))
					{
						throw new Win32Exception();
					}
					this.minWorkingSet = intPtr;
					this.maxWorkingSet = intPtr2;
					this.haveWorkingSetLimits = true;
				}
				finally
				{
					this.ReleaseProcessHandle(handle);
				}
			}
		}

		// Token: 0x06002FF7 RID: 12279 RVA: 0x000D85B4 File Offset: 0x000D67B4
		public static void EnterDebugMode()
		{
			if (ProcessManager.IsNt)
			{
				Process.SetPrivilege("SeDebugPrivilege", 2);
			}
		}

		// Token: 0x06002FF8 RID: 12280 RVA: 0x000D85C8 File Offset: 0x000D67C8
		private static void SetPrivilege(string privilegeName, int attrib)
		{
			IntPtr handle = (IntPtr)0;
			NativeMethods.LUID luid = default(NativeMethods.LUID);
			IntPtr currentProcess = NativeMethods.GetCurrentProcess();
			if (!NativeMethods.OpenProcessToken(new HandleRef(null, currentProcess), 32, out handle))
			{
				throw new Win32Exception();
			}
			try
			{
				if (!NativeMethods.LookupPrivilegeValue(null, privilegeName, out luid))
				{
					throw new Win32Exception();
				}
				NativeMethods.TokenPrivileges tokenPrivileges = new NativeMethods.TokenPrivileges();
				tokenPrivileges.Luid = luid;
				tokenPrivileges.Attributes = attrib;
				NativeMethods.AdjustTokenPrivileges(new HandleRef(null, handle), false, tokenPrivileges, 0, IntPtr.Zero, IntPtr.Zero);
				if (Marshal.GetLastWin32Error() != 0)
				{
					throw new Win32Exception();
				}
			}
			finally
			{
				SafeNativeMethods.CloseHandle(handle);
			}
		}

		// Token: 0x06002FF9 RID: 12281 RVA: 0x000D866C File Offset: 0x000D686C
		public static void LeaveDebugMode()
		{
			if (ProcessManager.IsNt)
			{
				Process.SetPrivilege("SeDebugPrivilege", 0);
			}
		}

		// Token: 0x06002FFA RID: 12282 RVA: 0x000D8680 File Offset: 0x000D6880
		public static Process GetProcessById(int processId, string machineName)
		{
			if (!ProcessManager.IsProcessRunning(processId, machineName))
			{
				throw new ArgumentException(SR.GetString("MissingProccess", new object[]
				{
					processId.ToString(CultureInfo.CurrentCulture)
				}));
			}
			return new Process(machineName, ProcessManager.IsRemoteMachine(machineName), processId, null);
		}

		// Token: 0x06002FFB RID: 12283 RVA: 0x000D86BE File Offset: 0x000D68BE
		public static Process GetProcessById(int processId)
		{
			return Process.GetProcessById(processId, ".");
		}

		// Token: 0x06002FFC RID: 12284 RVA: 0x000D86CB File Offset: 0x000D68CB
		public static Process[] GetProcessesByName(string processName)
		{
			return Process.GetProcessesByName(processName, ".");
		}

		// Token: 0x06002FFD RID: 12285 RVA: 0x000D86D8 File Offset: 0x000D68D8
		public static Process[] GetProcessesByName(string processName, string machineName)
		{
			if (processName == null)
			{
				processName = string.Empty;
			}
			Process[] processes = Process.GetProcesses(machineName);
			ArrayList arrayList = new ArrayList();
			for (int i = 0; i < processes.Length; i++)
			{
				if (string.Equals(processName, processes[i].ProcessName, StringComparison.OrdinalIgnoreCase))
				{
					arrayList.Add(processes[i]);
				}
				else
				{
					processes[i].Dispose();
				}
			}
			Process[] array = new Process[arrayList.Count];
			arrayList.CopyTo(array, 0);
			return array;
		}

		// Token: 0x06002FFE RID: 12286 RVA: 0x000D8744 File Offset: 0x000D6944
		public static Process[] GetProcesses()
		{
			return Process.GetProcesses(".");
		}

		// Token: 0x06002FFF RID: 12287 RVA: 0x000D8750 File Offset: 0x000D6950
		public static Process[] GetProcesses(string machineName)
		{
			bool flag = ProcessManager.IsRemoteMachine(machineName);
			ProcessInfo[] processInfos = ProcessManager.GetProcessInfos(machineName);
			Process[] array = new Process[processInfos.Length];
			for (int i = 0; i < processInfos.Length; i++)
			{
				ProcessInfo processInfo = processInfos[i];
				array[i] = new Process(machineName, flag, processInfo.processId, processInfo);
			}
			return array;
		}

		// Token: 0x06003000 RID: 12288 RVA: 0x000D879B File Offset: 0x000D699B
		public static Process GetCurrentProcess()
		{
			return new Process(".", false, NativeMethods.GetCurrentProcessId(), null);
		}

		// Token: 0x06003001 RID: 12289 RVA: 0x000D87B0 File Offset: 0x000D69B0
		protected void OnExited()
		{
			EventHandler eventHandler = this.onExited;
			if (eventHandler != null)
			{
				if (this.SynchronizingObject != null && this.SynchronizingObject.InvokeRequired)
				{
					this.SynchronizingObject.BeginInvoke(eventHandler, new object[]
					{
						this,
						EventArgs.Empty
					});
					return;
				}
				eventHandler(this, EventArgs.Empty);
			}
		}

		// Token: 0x06003002 RID: 12290 RVA: 0x000D8808 File Offset: 0x000D6A08
		private SafeProcessHandle GetProcessHandle(int access, bool throwIfExited)
		{
			if (this.haveProcessHandle)
			{
				if (throwIfExited)
				{
					ProcessWaitHandle processWaitHandle = null;
					try
					{
						processWaitHandle = new ProcessWaitHandle(this.m_processHandle);
						if (processWaitHandle.WaitOne(0, false))
						{
							if (this.haveProcessId)
							{
								throw new InvalidOperationException(SR.GetString("ProcessHasExited", new object[]
								{
									this.processId.ToString(CultureInfo.CurrentCulture)
								}));
							}
							throw new InvalidOperationException(SR.GetString("ProcessHasExitedNoId"));
						}
					}
					finally
					{
						if (processWaitHandle != null)
						{
							processWaitHandle.Close();
						}
					}
				}
				return this.m_processHandle;
			}
			this.EnsureState((Process.State)3);
			SafeProcessHandle safeProcessHandle = SafeProcessHandle.InvalidHandle;
			safeProcessHandle = ProcessManager.OpenProcess(this.processId, access, throwIfExited);
			if (throwIfExited && (access & 1024) != 0 && NativeMethods.GetExitCodeProcess(safeProcessHandle, out this.exitCode) && this.exitCode != 259)
			{
				throw new InvalidOperationException(SR.GetString("ProcessHasExited", new object[]
				{
					this.processId.ToString(CultureInfo.CurrentCulture)
				}));
			}
			return safeProcessHandle;
		}

		// Token: 0x06003003 RID: 12291 RVA: 0x000D8908 File Offset: 0x000D6B08
		private SafeProcessHandle GetProcessHandle(int access)
		{
			return this.GetProcessHandle(access, true);
		}

		// Token: 0x06003004 RID: 12292 RVA: 0x000D8912 File Offset: 0x000D6B12
		private SafeProcessHandle OpenProcessHandle()
		{
			return this.OpenProcessHandle(2035711);
		}

		// Token: 0x06003005 RID: 12293 RVA: 0x000D891F File Offset: 0x000D6B1F
		private SafeProcessHandle OpenProcessHandle(int access)
		{
			if (!this.haveProcessHandle)
			{
				if (this.disposed)
				{
					throw new ObjectDisposedException(base.GetType().Name);
				}
				this.SetProcessHandle(this.GetProcessHandle(access));
			}
			return this.m_processHandle;
		}

		// Token: 0x06003006 RID: 12294 RVA: 0x000D8958 File Offset: 0x000D6B58
		private void RaiseOnExited()
		{
			if (!this.raisedOnExited)
			{
				lock (this)
				{
					if (!this.raisedOnExited)
					{
						this.raisedOnExited = true;
						this.OnExited();
					}
				}
			}
		}

		// Token: 0x06003007 RID: 12295 RVA: 0x000D89AC File Offset: 0x000D6BAC
		public void Refresh()
		{
			this.processInfo = null;
			this.threads = null;
			this.modules = null;
			this.mainWindowTitle = null;
			this.exited = false;
			this.signaled = false;
			this.haveMainWindow = false;
			this.haveWorkingSetLimits = false;
			this.haveProcessorAffinity = false;
			this.havePriorityClass = false;
			this.haveExitTime = false;
			this.haveResponding = false;
			this.havePriorityBoostEnabled = false;
		}

		// Token: 0x06003008 RID: 12296 RVA: 0x000D8A14 File Offset: 0x000D6C14
		private void SetProcessHandle(SafeProcessHandle processHandle)
		{
			this.m_processHandle = processHandle;
			this.haveProcessHandle = true;
			if (this.watchForExit)
			{
				this.EnsureWatchingForExit();
			}
		}

		// Token: 0x06003009 RID: 12297 RVA: 0x000D8A32 File Offset: 0x000D6C32
		private void SetProcessId(int processId)
		{
			this.processId = processId;
			this.haveProcessId = true;
		}

		// Token: 0x0600300A RID: 12298 RVA: 0x000D8A44 File Offset: 0x000D6C44
		private void SetWorkingSetLimits(object newMin, object newMax)
		{
			this.EnsureState(Process.State.IsNt);
			SafeProcessHandle handle = null;
			try
			{
				handle = this.GetProcessHandle(1280);
				IntPtr intPtr;
				IntPtr intPtr2;
				if (!NativeMethods.GetProcessWorkingSetSize(handle, out intPtr, out intPtr2))
				{
					throw new Win32Exception();
				}
				if (newMin != null)
				{
					intPtr = (IntPtr)newMin;
				}
				if (newMax != null)
				{
					intPtr2 = (IntPtr)newMax;
				}
				if ((long)intPtr > (long)intPtr2)
				{
					if (newMin != null)
					{
						throw new ArgumentException(SR.GetString("BadMinWorkset"));
					}
					throw new ArgumentException(SR.GetString("BadMaxWorkset"));
				}
				else
				{
					if (!NativeMethods.SetProcessWorkingSetSize(handle, intPtr, intPtr2))
					{
						throw new Win32Exception();
					}
					if (!NativeMethods.GetProcessWorkingSetSize(handle, out intPtr, out intPtr2))
					{
						throw new Win32Exception();
					}
					this.minWorkingSet = intPtr;
					this.maxWorkingSet = intPtr2;
					this.haveWorkingSetLimits = true;
				}
			}
			finally
			{
				this.ReleaseProcessHandle(handle);
			}
		}

		// Token: 0x0600300B RID: 12299 RVA: 0x000D8B10 File Offset: 0x000D6D10
		public bool Start()
		{
			this.Close();
			ProcessStartInfo processStartInfo = this.StartInfo;
			if (processStartInfo.FileName.Length == 0)
			{
				throw new InvalidOperationException(SR.GetString("FileNameMissing"));
			}
			if (processStartInfo.UseShellExecute)
			{
				return this.StartWithShellExecuteEx(processStartInfo);
			}
			return this.StartWithCreateProcess(processStartInfo);
		}

		// Token: 0x0600300C RID: 12300 RVA: 0x000D8B60 File Offset: 0x000D6D60
		private static void CreatePipeWithSecurityAttributes(out SafeFileHandle hReadPipe, out SafeFileHandle hWritePipe, NativeMethods.SECURITY_ATTRIBUTES lpPipeAttributes, int nSize)
		{
			bool flag = NativeMethods.CreatePipe(out hReadPipe, out hWritePipe, lpPipeAttributes, nSize);
			if (!flag || hReadPipe.IsInvalid || hWritePipe.IsInvalid)
			{
				throw new Win32Exception();
			}
		}

		// Token: 0x0600300D RID: 12301 RVA: 0x000D8B94 File Offset: 0x000D6D94
		private void CreatePipe(out SafeFileHandle parentHandle, out SafeFileHandle childHandle, bool parentInputs)
		{
			NativeMethods.SECURITY_ATTRIBUTES security_ATTRIBUTES = new NativeMethods.SECURITY_ATTRIBUTES();
			security_ATTRIBUTES.bInheritHandle = true;
			SafeFileHandle safeFileHandle = null;
			try
			{
				if (parentInputs)
				{
					Process.CreatePipeWithSecurityAttributes(out childHandle, out safeFileHandle, security_ATTRIBUTES, 0);
				}
				else
				{
					Process.CreatePipeWithSecurityAttributes(out safeFileHandle, out childHandle, security_ATTRIBUTES, 0);
				}
				if (!NativeMethods.DuplicateHandle(new HandleRef(this, NativeMethods.GetCurrentProcess()), safeFileHandle, new HandleRef(this, NativeMethods.GetCurrentProcess()), out parentHandle, 0, false, 2))
				{
					throw new Win32Exception();
				}
			}
			finally
			{
				if (safeFileHandle != null && !safeFileHandle.IsInvalid)
				{
					safeFileHandle.Close();
				}
			}
		}

		// Token: 0x0600300E RID: 12302 RVA: 0x000D8C18 File Offset: 0x000D6E18
		private static StringBuilder BuildCommandLine(string executableFileName, string arguments)
		{
			StringBuilder stringBuilder = new StringBuilder();
			string text = executableFileName.Trim();
			bool flag = text.StartsWith("\"", StringComparison.Ordinal) && text.EndsWith("\"", StringComparison.Ordinal);
			if (!flag)
			{
				stringBuilder.Append("\"");
			}
			stringBuilder.Append(text);
			if (!flag)
			{
				stringBuilder.Append("\"");
			}
			if (!string.IsNullOrEmpty(arguments))
			{
				stringBuilder.Append(" ");
				stringBuilder.Append(arguments);
			}
			return stringBuilder;
		}

		// Token: 0x0600300F RID: 12303 RVA: 0x000D8C94 File Offset: 0x000D6E94
		private bool StartWithCreateProcess(ProcessStartInfo startInfo)
		{
			if (startInfo.StandardOutputEncoding != null && !startInfo.RedirectStandardOutput)
			{
				throw new InvalidOperationException(SR.GetString("StandardOutputEncodingNotAllowed"));
			}
			if (startInfo.StandardErrorEncoding != null && !startInfo.RedirectStandardError)
			{
				throw new InvalidOperationException(SR.GetString("StandardErrorEncodingNotAllowed"));
			}
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().Name);
			}
			StringBuilder stringBuilder = Process.BuildCommandLine(startInfo.FileName, startInfo.Arguments);
			NativeMethods.STARTUPINFO startupinfo = new NativeMethods.STARTUPINFO();
			SafeNativeMethods.PROCESS_INFORMATION process_INFORMATION = new SafeNativeMethods.PROCESS_INFORMATION();
			SafeProcessHandle safeProcessHandle = new SafeProcessHandle();
			SafeThreadHandle safeThreadHandle = new SafeThreadHandle();
			int num = 0;
			SafeFileHandle handle = null;
			SafeFileHandle handle2 = null;
			SafeFileHandle handle3 = null;
			GCHandle gchandle = default(GCHandle);
			object obj = Process.s_CreateProcessLock;
			lock (obj)
			{
				try
				{
					if (startInfo.RedirectStandardInput || startInfo.RedirectStandardOutput || startInfo.RedirectStandardError)
					{
						if (startInfo.RedirectStandardInput)
						{
							this.CreatePipe(out handle, out startupinfo.hStdInput, true);
						}
						else
						{
							startupinfo.hStdInput = new SafeFileHandle(NativeMethods.GetStdHandle(-10), false);
						}
						if (startInfo.RedirectStandardOutput)
						{
							this.CreatePipe(out handle2, out startupinfo.hStdOutput, false);
						}
						else
						{
							startupinfo.hStdOutput = new SafeFileHandle(NativeMethods.GetStdHandle(-11), false);
						}
						if (startInfo.RedirectStandardError)
						{
							this.CreatePipe(out handle3, out startupinfo.hStdError, false);
						}
						else
						{
							startupinfo.hStdError = new SafeFileHandle(NativeMethods.GetStdHandle(-12), false);
						}
						startupinfo.dwFlags = 256;
					}
					int num2 = 0;
					if (startInfo.CreateNoWindow)
					{
						num2 |= 134217728;
					}
					IntPtr intPtr = (IntPtr)0;
					if (startInfo.environmentVariables != null)
					{
						bool unicode = false;
						if (ProcessManager.IsNt)
						{
							num2 |= 1024;
							unicode = true;
						}
						byte[] value = EnvironmentBlock.ToByteArray(startInfo.environmentVariables, unicode);
						gchandle = GCHandle.Alloc(value, GCHandleType.Pinned);
						intPtr = gchandle.AddrOfPinnedObject();
					}
					string text = startInfo.WorkingDirectory;
					if (text == string.Empty)
					{
						text = Environment.CurrentDirectory;
					}
					bool flag2;
					if (startInfo.UserName.Length != 0)
					{
						if (startInfo.Password != null && startInfo.PasswordInClearText != null)
						{
							throw new ArgumentException(SR.GetString("CantSetDuplicatePassword"));
						}
						NativeMethods.LogonFlags logonFlags = (NativeMethods.LogonFlags)0;
						if (startInfo.LoadUserProfile)
						{
							logonFlags = NativeMethods.LogonFlags.LOGON_WITH_PROFILE;
						}
						IntPtr intPtr2 = IntPtr.Zero;
						try
						{
							if (startInfo.Password != null)
							{
								intPtr2 = Marshal.SecureStringToCoTaskMemUnicode(startInfo.Password);
							}
							else if (startInfo.PasswordInClearText != null)
							{
								intPtr2 = Marshal.StringToCoTaskMemUni(startInfo.PasswordInClearText);
							}
							else
							{
								intPtr2 = Marshal.StringToCoTaskMemUni(string.Empty);
							}
							RuntimeHelpers.PrepareConstrainedRegions();
							try
							{
							}
							finally
							{
								flag2 = NativeMethods.CreateProcessWithLogonW(startInfo.UserName, startInfo.Domain, intPtr2, logonFlags, null, stringBuilder, num2, intPtr, text, startupinfo, process_INFORMATION);
								if (!flag2)
								{
									num = Marshal.GetLastWin32Error();
								}
								if (process_INFORMATION.hProcess != (IntPtr)0 && process_INFORMATION.hProcess != NativeMethods.INVALID_HANDLE_VALUE)
								{
									safeProcessHandle.InitialSetHandle(process_INFORMATION.hProcess);
								}
								if (process_INFORMATION.hThread != (IntPtr)0 && process_INFORMATION.hThread != NativeMethods.INVALID_HANDLE_VALUE)
								{
									safeThreadHandle.InitialSetHandle(process_INFORMATION.hThread);
								}
							}
							if (flag2)
							{
								goto IL_416;
							}
							if (num == 193 || num == 216)
							{
								throw new Win32Exception(num, SR.GetString("InvalidApplication"));
							}
							throw new Win32Exception(num);
						}
						finally
						{
							if (intPtr2 != IntPtr.Zero)
							{
								Marshal.ZeroFreeCoTaskMemUnicode(intPtr2);
							}
						}
					}
					RuntimeHelpers.PrepareConstrainedRegions();
					try
					{
					}
					finally
					{
						flag2 = NativeMethods.CreateProcess(null, stringBuilder, null, null, true, num2, intPtr, text, startupinfo, process_INFORMATION);
						if (!flag2)
						{
							num = Marshal.GetLastWin32Error();
						}
						if (process_INFORMATION.hProcess != (IntPtr)0 && process_INFORMATION.hProcess != NativeMethods.INVALID_HANDLE_VALUE)
						{
							safeProcessHandle.InitialSetHandle(process_INFORMATION.hProcess);
						}
						if (process_INFORMATION.hThread != (IntPtr)0 && process_INFORMATION.hThread != NativeMethods.INVALID_HANDLE_VALUE)
						{
							safeThreadHandle.InitialSetHandle(process_INFORMATION.hThread);
						}
					}
					if (!flag2)
					{
						if (num == 193 || num == 216)
						{
							throw new Win32Exception(num, SR.GetString("InvalidApplication"));
						}
						throw new Win32Exception(num);
					}
				}
				finally
				{
					if (gchandle.IsAllocated)
					{
						gchandle.Free();
					}
					startupinfo.Dispose();
				}
			}
			IL_416:
			if (startInfo.RedirectStandardInput)
			{
				this.standardInput = new StreamWriter(new FileStream(handle, FileAccess.Write, 4096, false), Console.InputEncoding, 4096);
				this.standardInput.AutoFlush = true;
			}
			if (startInfo.RedirectStandardOutput)
			{
				Encoding encoding = (startInfo.StandardOutputEncoding != null) ? startInfo.StandardOutputEncoding : Console.OutputEncoding;
				this.standardOutput = new StreamReader(new FileStream(handle2, FileAccess.Read, 4096, false), encoding, true, 4096);
			}
			if (startInfo.RedirectStandardError)
			{
				Encoding encoding2 = (startInfo.StandardErrorEncoding != null) ? startInfo.StandardErrorEncoding : Console.OutputEncoding;
				this.standardError = new StreamReader(new FileStream(handle3, FileAccess.Read, 4096, false), encoding2, true, 4096);
			}
			bool result = false;
			if (!safeProcessHandle.IsInvalid)
			{
				this.SetProcessHandle(safeProcessHandle);
				this.SetProcessId(process_INFORMATION.dwProcessId);
				safeThreadHandle.Close();
				result = true;
			}
			return result;
		}

		// Token: 0x06003010 RID: 12304 RVA: 0x000D9214 File Offset: 0x000D7414
		private bool StartWithShellExecuteEx(ProcessStartInfo startInfo)
		{
			if (this.disposed)
			{
				throw new ObjectDisposedException(base.GetType().Name);
			}
			if (!string.IsNullOrEmpty(startInfo.UserName) || startInfo.Password != null)
			{
				throw new InvalidOperationException(SR.GetString("CantStartAsUser"));
			}
			if (startInfo.RedirectStandardInput || startInfo.RedirectStandardOutput || startInfo.RedirectStandardError)
			{
				throw new InvalidOperationException(SR.GetString("CantRedirectStreams"));
			}
			if (startInfo.StandardErrorEncoding != null)
			{
				throw new InvalidOperationException(SR.GetString("StandardErrorEncodingNotAllowed"));
			}
			if (startInfo.StandardOutputEncoding != null)
			{
				throw new InvalidOperationException(SR.GetString("StandardOutputEncodingNotAllowed"));
			}
			if (startInfo.environmentVariables != null)
			{
				throw new InvalidOperationException(SR.GetString("CantUseEnvVars"));
			}
			NativeMethods.ShellExecuteInfo shellExecuteInfo = new NativeMethods.ShellExecuteInfo();
			shellExecuteInfo.fMask = 64;
			if (startInfo.ErrorDialog)
			{
				shellExecuteInfo.hwnd = startInfo.ErrorDialogParentHandle;
			}
			else
			{
				shellExecuteInfo.fMask |= 1024;
			}
			switch (startInfo.WindowStyle)
			{
			case ProcessWindowStyle.Hidden:
				shellExecuteInfo.nShow = 0;
				break;
			case ProcessWindowStyle.Minimized:
				shellExecuteInfo.nShow = 2;
				break;
			case ProcessWindowStyle.Maximized:
				shellExecuteInfo.nShow = 3;
				break;
			default:
				shellExecuteInfo.nShow = 1;
				break;
			}
			try
			{
				if (startInfo.FileName.Length != 0)
				{
					shellExecuteInfo.lpFile = Marshal.StringToHGlobalAuto(startInfo.FileName);
				}
				if (startInfo.Verb.Length != 0)
				{
					shellExecuteInfo.lpVerb = Marshal.StringToHGlobalAuto(startInfo.Verb);
				}
				if (startInfo.Arguments.Length != 0)
				{
					shellExecuteInfo.lpParameters = Marshal.StringToHGlobalAuto(startInfo.Arguments);
				}
				if (startInfo.WorkingDirectory.Length != 0)
				{
					shellExecuteInfo.lpDirectory = Marshal.StringToHGlobalAuto(startInfo.WorkingDirectory);
				}
				shellExecuteInfo.fMask |= 256;
				ShellExecuteHelper shellExecuteHelper = new ShellExecuteHelper(shellExecuteInfo);
				if (!shellExecuteHelper.ShellExecuteOnSTAThread())
				{
					int num = shellExecuteHelper.ErrorCode;
					if (num == 0)
					{
						long num2 = (long)shellExecuteInfo.hInstApp;
						long num3 = num2 - 2L;
						if (num3 <= 6L)
						{
							switch ((uint)num3)
							{
							case 0U:
								num = 2;
								goto IL_274;
							case 1U:
								num = 3;
								goto IL_274;
							case 2U:
							case 4U:
							case 5U:
								goto IL_268;
							case 3U:
								num = 5;
								goto IL_274;
							case 6U:
								num = 8;
								goto IL_274;
							}
						}
						long num4 = num2 - 26L;
						if (num4 <= 6L)
						{
							switch ((uint)num4)
							{
							case 0U:
								num = 32;
								goto IL_274;
							case 2U:
							case 3U:
							case 4U:
								num = 1156;
								goto IL_274;
							case 5U:
								num = 1155;
								goto IL_274;
							case 6U:
								num = 1157;
								goto IL_274;
							}
						}
						IL_268:
						num = (int)shellExecuteInfo.hInstApp;
					}
					IL_274:
					if (num == 193 || num == 216)
					{
						throw new Win32Exception(num, SR.GetString("InvalidApplication"));
					}
					throw new Win32Exception(num);
				}
			}
			finally
			{
				if (shellExecuteInfo.lpFile != (IntPtr)0)
				{
					Marshal.FreeHGlobal(shellExecuteInfo.lpFile);
				}
				if (shellExecuteInfo.lpVerb != (IntPtr)0)
				{
					Marshal.FreeHGlobal(shellExecuteInfo.lpVerb);
				}
				if (shellExecuteInfo.lpParameters != (IntPtr)0)
				{
					Marshal.FreeHGlobal(shellExecuteInfo.lpParameters);
				}
				if (shellExecuteInfo.lpDirectory != (IntPtr)0)
				{
					Marshal.FreeHGlobal(shellExecuteInfo.lpDirectory);
				}
			}
			if (shellExecuteInfo.hProcess != (IntPtr)0)
			{
				SafeProcessHandle processHandle = new SafeProcessHandle(shellExecuteInfo.hProcess);
				this.SetProcessHandle(processHandle);
				return true;
			}
			return false;
		}

		// Token: 0x06003011 RID: 12305 RVA: 0x000D9580 File Offset: 0x000D7780
		public static Process Start(string fileName, string userName, SecureString password, string domain)
		{
			return Process.Start(new ProcessStartInfo(fileName)
			{
				UserName = userName,
				Password = password,
				Domain = domain,
				UseShellExecute = false
			});
		}

		// Token: 0x06003012 RID: 12306 RVA: 0x000D95B8 File Offset: 0x000D77B8
		public static Process Start(string fileName, string arguments, string userName, SecureString password, string domain)
		{
			return Process.Start(new ProcessStartInfo(fileName, arguments)
			{
				UserName = userName,
				Password = password,
				Domain = domain,
				UseShellExecute = false
			});
		}

		// Token: 0x06003013 RID: 12307 RVA: 0x000D95F0 File Offset: 0x000D77F0
		public static Process Start(string fileName)
		{
			return Process.Start(new ProcessStartInfo(fileName));
		}

		// Token: 0x06003014 RID: 12308 RVA: 0x000D95FD File Offset: 0x000D77FD
		public static Process Start(string fileName, string arguments)
		{
			return Process.Start(new ProcessStartInfo(fileName, arguments));
		}

		// Token: 0x06003015 RID: 12309 RVA: 0x000D960C File Offset: 0x000D780C
		public static Process Start(ProcessStartInfo startInfo)
		{
			Process process = new Process();
			if (startInfo == null)
			{
				throw new ArgumentNullException("startInfo");
			}
			process.StartInfo = startInfo;
			if (process.Start())
			{
				return process;
			}
			return null;
		}

		// Token: 0x06003016 RID: 12310 RVA: 0x000D9640 File Offset: 0x000D7840
		public void Kill()
		{
			SafeProcessHandle safeProcessHandle = null;
			try
			{
				safeProcessHandle = this.GetProcessHandle(1);
				if (!NativeMethods.TerminateProcess(safeProcessHandle, -1))
				{
					throw new Win32Exception();
				}
			}
			finally
			{
				this.ReleaseProcessHandle(safeProcessHandle);
			}
		}

		// Token: 0x06003017 RID: 12311 RVA: 0x000D9680 File Offset: 0x000D7880
		private void StopWatchingForExit()
		{
			if (this.watchingForExit)
			{
				lock (this)
				{
					if (this.watchingForExit)
					{
						this.watchingForExit = false;
						this.registeredWaitHandle.Unregister(null);
						this.waitHandle.Close();
						this.waitHandle = null;
						this.registeredWaitHandle = null;
					}
				}
			}
		}

		// Token: 0x06003018 RID: 12312 RVA: 0x000D96F4 File Offset: 0x000D78F4
		public override string ToString()
		{
			if (!this.Associated)
			{
				return base.ToString();
			}
			string text = string.Empty;
			try
			{
				text = this.ProcessName;
			}
			catch (PlatformNotSupportedException)
			{
			}
			if (text.Length != 0)
			{
				return string.Format(CultureInfo.CurrentCulture, "{0} ({1})", new object[]
				{
					base.ToString(),
					text
				});
			}
			return base.ToString();
		}

		// Token: 0x06003019 RID: 12313 RVA: 0x000D9764 File Offset: 0x000D7964
		public bool WaitForExit(int milliseconds)
		{
			SafeProcessHandle safeProcessHandle = null;
			ProcessWaitHandle processWaitHandle = null;
			bool flag;
			try
			{
				safeProcessHandle = this.GetProcessHandle(1048576, false);
				if (safeProcessHandle.IsInvalid)
				{
					flag = true;
				}
				else
				{
					processWaitHandle = new ProcessWaitHandle(safeProcessHandle);
					if (processWaitHandle.WaitOne(milliseconds, false))
					{
						flag = true;
						this.signaled = true;
					}
					else
					{
						flag = false;
						this.signaled = false;
					}
				}
			}
			finally
			{
				if (processWaitHandle != null)
				{
					processWaitHandle.Close();
				}
				if (this.output != null && milliseconds == -1)
				{
					this.output.WaitUtilEOF();
				}
				if (this.error != null && milliseconds == -1)
				{
					this.error.WaitUtilEOF();
				}
				this.ReleaseProcessHandle(safeProcessHandle);
			}
			if (flag && this.watchForExit)
			{
				this.RaiseOnExited();
			}
			return flag;
		}

		// Token: 0x0600301A RID: 12314 RVA: 0x000D9818 File Offset: 0x000D7A18
		public void WaitForExit()
		{
			this.WaitForExit(-1);
		}

		// Token: 0x0600301B RID: 12315 RVA: 0x000D9824 File Offset: 0x000D7A24
		public bool WaitForInputIdle(int milliseconds)
		{
			SafeProcessHandle handle = null;
			try
			{
				handle = this.GetProcessHandle(1049600);
				int num = NativeMethods.WaitForInputIdle(handle, milliseconds);
				if (num != -1)
				{
					if (num == 0)
					{
						return true;
					}
					if (num == 258)
					{
						return false;
					}
				}
				throw new InvalidOperationException(SR.GetString("InputIdleUnkownError"));
			}
			finally
			{
				this.ReleaseProcessHandle(handle);
			}
			bool result;
			return result;
		}

		// Token: 0x0600301C RID: 12316 RVA: 0x000D988C File Offset: 0x000D7A8C
		public bool WaitForInputIdle()
		{
			return this.WaitForInputIdle(int.MaxValue);
		}

		// Token: 0x0600301D RID: 12317 RVA: 0x000D989C File Offset: 0x000D7A9C
		[ComVisible(false)]
		public void BeginOutputReadLine()
		{
			if (this.outputStreamReadMode == Process.StreamReadMode.undefined)
			{
				this.outputStreamReadMode = Process.StreamReadMode.asyncMode;
			}
			else if (this.outputStreamReadMode != Process.StreamReadMode.asyncMode)
			{
				throw new InvalidOperationException(SR.GetString("CantMixSyncAsyncOperation"));
			}
			if (this.pendingOutputRead)
			{
				throw new InvalidOperationException(SR.GetString("PendingAsyncOperation"));
			}
			this.pendingOutputRead = true;
			if (this.output == null)
			{
				if (this.standardOutput == null)
				{
					throw new InvalidOperationException(SR.GetString("CantGetStandardOut"));
				}
				Stream baseStream = this.standardOutput.BaseStream;
				this.output = new AsyncStreamReader(this, baseStream, new UserCallBack(this.OutputReadNotifyUser), this.standardOutput.CurrentEncoding);
			}
			this.output.BeginReadLine();
		}

		// Token: 0x0600301E RID: 12318 RVA: 0x000D9950 File Offset: 0x000D7B50
		[ComVisible(false)]
		public void BeginErrorReadLine()
		{
			if (this.errorStreamReadMode == Process.StreamReadMode.undefined)
			{
				this.errorStreamReadMode = Process.StreamReadMode.asyncMode;
			}
			else if (this.errorStreamReadMode != Process.StreamReadMode.asyncMode)
			{
				throw new InvalidOperationException(SR.GetString("CantMixSyncAsyncOperation"));
			}
			if (this.pendingErrorRead)
			{
				throw new InvalidOperationException(SR.GetString("PendingAsyncOperation"));
			}
			this.pendingErrorRead = true;
			if (this.error == null)
			{
				if (this.standardError == null)
				{
					throw new InvalidOperationException(SR.GetString("CantGetStandardError"));
				}
				Stream baseStream = this.standardError.BaseStream;
				this.error = new AsyncStreamReader(this, baseStream, new UserCallBack(this.ErrorReadNotifyUser), this.standardError.CurrentEncoding);
			}
			this.error.BeginReadLine();
		}

		// Token: 0x0600301F RID: 12319 RVA: 0x000D9A01 File Offset: 0x000D7C01
		[ComVisible(false)]
		public void CancelOutputRead()
		{
			if (this.output != null)
			{
				this.output.CancelOperation();
				this.pendingOutputRead = false;
				return;
			}
			throw new InvalidOperationException(SR.GetString("NoAsyncOperation"));
		}

		// Token: 0x06003020 RID: 12320 RVA: 0x000D9A2F File Offset: 0x000D7C2F
		[ComVisible(false)]
		public void CancelErrorRead()
		{
			if (this.error != null)
			{
				this.error.CancelOperation();
				this.pendingErrorRead = false;
				return;
			}
			throw new InvalidOperationException(SR.GetString("NoAsyncOperation"));
		}

		// Token: 0x06003021 RID: 12321 RVA: 0x000D9A60 File Offset: 0x000D7C60
		internal void OutputReadNotifyUser(string data)
		{
			DataReceivedEventHandler outputDataReceived = this.OutputDataReceived;
			if (outputDataReceived != null)
			{
				DataReceivedEventArgs dataReceivedEventArgs = new DataReceivedEventArgs(data);
				if (this.SynchronizingObject != null && this.SynchronizingObject.InvokeRequired)
				{
					this.SynchronizingObject.Invoke(outputDataReceived, new object[]
					{
						this,
						dataReceivedEventArgs
					});
					return;
				}
				outputDataReceived(this, dataReceivedEventArgs);
			}
		}

		// Token: 0x06003022 RID: 12322 RVA: 0x000D9AB8 File Offset: 0x000D7CB8
		internal void ErrorReadNotifyUser(string data)
		{
			DataReceivedEventHandler errorDataReceived = this.ErrorDataReceived;
			if (errorDataReceived != null)
			{
				DataReceivedEventArgs dataReceivedEventArgs = new DataReceivedEventArgs(data);
				if (this.SynchronizingObject != null && this.SynchronizingObject.InvokeRequired)
				{
					this.SynchronizingObject.Invoke(errorDataReceived, new object[]
					{
						this,
						dataReceivedEventArgs
					});
					return;
				}
				errorDataReceived(this, dataReceivedEventArgs);
			}
		}

		// Token: 0x04002833 RID: 10291
		private bool haveProcessId;

		// Token: 0x04002834 RID: 10292
		private int processId;

		// Token: 0x04002835 RID: 10293
		private bool haveProcessHandle;

		// Token: 0x04002836 RID: 10294
		private SafeProcessHandle m_processHandle;

		// Token: 0x04002837 RID: 10295
		private bool isRemoteMachine;

		// Token: 0x04002838 RID: 10296
		private string machineName;

		// Token: 0x04002839 RID: 10297
		private ProcessInfo processInfo;

		// Token: 0x0400283A RID: 10298
		private int m_processAccess;

		// Token: 0x0400283B RID: 10299
		private ProcessThreadCollection threads;

		// Token: 0x0400283C RID: 10300
		private ProcessModuleCollection modules;

		// Token: 0x0400283D RID: 10301
		private bool haveMainWindow;

		// Token: 0x0400283E RID: 10302
		private IntPtr mainWindowHandle;

		// Token: 0x0400283F RID: 10303
		private string mainWindowTitle;

		// Token: 0x04002840 RID: 10304
		private bool haveWorkingSetLimits;

		// Token: 0x04002841 RID: 10305
		private IntPtr minWorkingSet;

		// Token: 0x04002842 RID: 10306
		private IntPtr maxWorkingSet;

		// Token: 0x04002843 RID: 10307
		private bool haveProcessorAffinity;

		// Token: 0x04002844 RID: 10308
		private IntPtr processorAffinity;

		// Token: 0x04002845 RID: 10309
		private bool havePriorityClass;

		// Token: 0x04002846 RID: 10310
		private ProcessPriorityClass priorityClass;

		// Token: 0x04002847 RID: 10311
		private ProcessStartInfo startInfo;

		// Token: 0x04002848 RID: 10312
		private bool watchForExit;

		// Token: 0x04002849 RID: 10313
		private bool watchingForExit;

		// Token: 0x0400284A RID: 10314
		private EventHandler onExited;

		// Token: 0x0400284B RID: 10315
		private bool exited;

		// Token: 0x0400284C RID: 10316
		private int exitCode;

		// Token: 0x0400284D RID: 10317
		private bool signaled;

		// Token: 0x0400284E RID: 10318
		private DateTime exitTime;

		// Token: 0x0400284F RID: 10319
		private bool haveExitTime;

		// Token: 0x04002850 RID: 10320
		private bool responding;

		// Token: 0x04002851 RID: 10321
		private bool haveResponding;

		// Token: 0x04002852 RID: 10322
		private bool priorityBoostEnabled;

		// Token: 0x04002853 RID: 10323
		private bool havePriorityBoostEnabled;

		// Token: 0x04002854 RID: 10324
		private bool raisedOnExited;

		// Token: 0x04002855 RID: 10325
		private RegisteredWaitHandle registeredWaitHandle;

		// Token: 0x04002856 RID: 10326
		private WaitHandle waitHandle;

		// Token: 0x04002857 RID: 10327
		private ISynchronizeInvoke synchronizingObject;

		// Token: 0x04002858 RID: 10328
		private StreamReader standardOutput;

		// Token: 0x04002859 RID: 10329
		private StreamWriter standardInput;

		// Token: 0x0400285A RID: 10330
		private StreamReader standardError;

		// Token: 0x0400285B RID: 10331
		private OperatingSystem operatingSystem;

		// Token: 0x0400285C RID: 10332
		private bool disposed;

		// Token: 0x0400285D RID: 10333
		private static object s_CreateProcessLock = new object();

		// Token: 0x0400285E RID: 10334
		private Process.StreamReadMode outputStreamReadMode;

		// Token: 0x0400285F RID: 10335
		private Process.StreamReadMode errorStreamReadMode;

		// Token: 0x04002862 RID: 10338
		internal AsyncStreamReader output;

		// Token: 0x04002863 RID: 10339
		internal AsyncStreamReader error;

		// Token: 0x04002864 RID: 10340
		internal bool pendingOutputRead;

		// Token: 0x04002865 RID: 10341
		internal bool pendingErrorRead;

		// Token: 0x04002866 RID: 10342
		private static SafeFileHandle InvalidPipeHandle = new SafeFileHandle(IntPtr.Zero, false);

		// Token: 0x04002867 RID: 10343
		internal static TraceSwitch processTracing = null;

		// Token: 0x02000880 RID: 2176
		private enum StreamReadMode
		{
			// Token: 0x04003749 RID: 14153
			undefined,
			// Token: 0x0400374A RID: 14154
			syncMode,
			// Token: 0x0400374B RID: 14155
			asyncMode
		}

		// Token: 0x02000881 RID: 2177
		private enum State
		{
			// Token: 0x0400374D RID: 14157
			HaveId = 1,
			// Token: 0x0400374E RID: 14158
			IsLocal,
			// Token: 0x0400374F RID: 14159
			IsNt = 4,
			// Token: 0x04003750 RID: 14160
			HaveProcessInfo = 8,
			// Token: 0x04003751 RID: 14161
			Exited = 16,
			// Token: 0x04003752 RID: 14162
			Associated = 32,
			// Token: 0x04003753 RID: 14163
			IsWin2k = 64,
			// Token: 0x04003754 RID: 14164
			HaveNtProcessInfo = 12
		}
	}
}

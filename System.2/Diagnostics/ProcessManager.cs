using System;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;

namespace System.Diagnostics
{
	// Token: 0x020004F8 RID: 1272
	internal static class ProcessManager
	{
		// Token: 0x06003039 RID: 12345 RVA: 0x000D9E28 File Offset: 0x000D8028
		static ProcessManager()
		{
			NativeMethods.LUID luid = default(NativeMethods.LUID);
			if (!NativeMethods.LookupPrivilegeValue(null, "SeDebugPrivilege", out luid))
			{
				return;
			}
			IntPtr zero = IntPtr.Zero;
			try
			{
				if (NativeMethods.OpenProcessToken(new HandleRef(null, NativeMethods.GetCurrentProcess()), 32, out zero))
				{
					NativeMethods.TokenPrivileges tokenPrivileges = new NativeMethods.TokenPrivileges();
					tokenPrivileges.PrivilegeCount = 1;
					tokenPrivileges.Luid = luid;
					tokenPrivileges.Attributes = 2;
					NativeMethods.AdjustTokenPrivileges(new HandleRef(null, zero), false, tokenPrivileges, 0, IntPtr.Zero, IntPtr.Zero);
				}
			}
			finally
			{
				if (zero != IntPtr.Zero)
				{
					SafeNativeMethods.CloseHandle(zero);
				}
			}
		}

		// Token: 0x17000BCB RID: 3019
		// (get) Token: 0x0600303A RID: 12346 RVA: 0x000D9EC8 File Offset: 0x000D80C8
		public static bool IsNt
		{
			get
			{
				return Environment.OSVersion.Platform == PlatformID.Win32NT;
			}
		}

		// Token: 0x17000BCC RID: 3020
		// (get) Token: 0x0600303B RID: 12347 RVA: 0x000D9ED7 File Offset: 0x000D80D7
		public static bool IsOSOlderThanXP
		{
			get
			{
				return Environment.OSVersion.Version.Major < 5 || (Environment.OSVersion.Version.Major == 5 && Environment.OSVersion.Version.Minor == 0);
			}
		}

		// Token: 0x0600303C RID: 12348 RVA: 0x000D9F14 File Offset: 0x000D8114
		public static ProcessInfo GetProcessInfo(int processId, string machineName)
		{
			bool flag = ProcessManager.IsRemoteMachine(machineName);
			if (!flag && ProcessManager.IsNt && Environment.OSVersion.Version.Major >= 5)
			{
				ProcessInfo[] processInfos = NtProcessInfoHelper.GetProcessInfos((int pid) => pid == processId);
				if (processInfos.Length == 1)
				{
					return processInfos[0];
				}
			}
			else
			{
				ProcessInfo[] processInfosCore = ProcessManager.GetProcessInfosCore(machineName, flag);
				foreach (ProcessInfo processInfo in processInfosCore)
				{
					if (processInfo.processId == processId)
					{
						return processInfo;
					}
				}
			}
			return null;
		}

		// Token: 0x0600303D RID: 12349 RVA: 0x000D9FA8 File Offset: 0x000D81A8
		public static ProcessInfo[] GetProcessInfos(string machineName)
		{
			bool isRemoteMachine = ProcessManager.IsRemoteMachine(machineName);
			return ProcessManager.GetProcessInfosCore(machineName, isRemoteMachine);
		}

		// Token: 0x0600303E RID: 12350 RVA: 0x000D9FC4 File Offset: 0x000D81C4
		private static ProcessInfo[] GetProcessInfosCore(string machineName, bool isRemoteMachine)
		{
			if (ProcessManager.IsNt)
			{
				if (!isRemoteMachine && Environment.OSVersion.Version.Major >= 5)
				{
					return NtProcessInfoHelper.GetProcessInfos(null);
				}
				return NtProcessManager.GetProcessInfos(machineName, isRemoteMachine);
			}
			else
			{
				if (isRemoteMachine)
				{
					throw new PlatformNotSupportedException(SR.GetString("WinNTRequiredForRemote"));
				}
				return WinProcessManager.GetProcessInfos();
			}
		}

		// Token: 0x0600303F RID: 12351 RVA: 0x000DA014 File Offset: 0x000D8214
		public static int[] GetProcessIds()
		{
			if (ProcessManager.IsNt)
			{
				return NtProcessManager.GetProcessIds();
			}
			return WinProcessManager.GetProcessIds();
		}

		// Token: 0x06003040 RID: 12352 RVA: 0x000DA028 File Offset: 0x000D8228
		public static int[] GetProcessIds(string machineName)
		{
			if (!ProcessManager.IsRemoteMachine(machineName))
			{
				return ProcessManager.GetProcessIds();
			}
			if (ProcessManager.IsNt)
			{
				return NtProcessManager.GetProcessIds(machineName, true);
			}
			throw new PlatformNotSupportedException(SR.GetString("WinNTRequiredForRemote"));
		}

		// Token: 0x06003041 RID: 12353 RVA: 0x000DA056 File Offset: 0x000D8256
		public static bool IsProcessRunning(int processId, string machineName)
		{
			return ProcessManager.IsProcessRunning(processId, ProcessManager.GetProcessIds(machineName));
		}

		// Token: 0x06003042 RID: 12354 RVA: 0x000DA064 File Offset: 0x000D8264
		public static bool IsProcessRunning(int processId)
		{
			return ProcessManager.IsProcessRunning(processId, ProcessManager.GetProcessIds());
		}

		// Token: 0x06003043 RID: 12355 RVA: 0x000DA074 File Offset: 0x000D8274
		private static bool IsProcessRunning(int processId, int[] processIds)
		{
			for (int i = 0; i < processIds.Length; i++)
			{
				if (processIds[i] == processId)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06003044 RID: 12356 RVA: 0x000DA098 File Offset: 0x000D8298
		public static int GetProcessIdFromHandle(SafeProcessHandle processHandle)
		{
			if (ProcessManager.IsNt)
			{
				return NtProcessManager.GetProcessIdFromHandle(processHandle);
			}
			throw new PlatformNotSupportedException(SR.GetString("WinNTRequired"));
		}

		// Token: 0x06003045 RID: 12357 RVA: 0x000DA0B8 File Offset: 0x000D82B8
		public static IntPtr GetMainWindowHandle(int processId)
		{
			MainWindowFinder mainWindowFinder = new MainWindowFinder();
			return mainWindowFinder.FindMainWindow(processId);
		}

		// Token: 0x06003046 RID: 12358 RVA: 0x000DA0D2 File Offset: 0x000D82D2
		public static ModuleInfo[] GetModuleInfos(int processId)
		{
			if (ProcessManager.IsNt)
			{
				return NtProcessManager.GetModuleInfos(processId);
			}
			return WinProcessManager.GetModuleInfos(processId);
		}

		// Token: 0x06003047 RID: 12359 RVA: 0x000DA0E8 File Offset: 0x000D82E8
		public static SafeProcessHandle OpenProcess(int processId, int access, bool throwIfExited)
		{
			SafeProcessHandle safeProcessHandle = NativeMethods.OpenProcess(access, false, processId);
			int lastWin32Error = Marshal.GetLastWin32Error();
			if (!safeProcessHandle.IsInvalid)
			{
				return safeProcessHandle;
			}
			if (processId == 0)
			{
				throw new Win32Exception(5);
			}
			if (ProcessManager.IsProcessRunning(processId))
			{
				throw new Win32Exception(lastWin32Error);
			}
			if (throwIfExited)
			{
				throw new InvalidOperationException(SR.GetString("ProcessHasExited", new object[]
				{
					processId.ToString(CultureInfo.CurrentCulture)
				}));
			}
			return SafeProcessHandle.InvalidHandle;
		}

		// Token: 0x06003048 RID: 12360 RVA: 0x000DA154 File Offset: 0x000D8354
		public static SafeThreadHandle OpenThread(int threadId, int access)
		{
			SafeThreadHandle result;
			try
			{
				SafeThreadHandle safeThreadHandle = NativeMethods.OpenThread(access, false, threadId);
				int lastWin32Error = Marshal.GetLastWin32Error();
				if (safeThreadHandle.IsInvalid)
				{
					if (lastWin32Error == 87)
					{
						throw new InvalidOperationException(SR.GetString("ThreadExited", new object[]
						{
							threadId.ToString(CultureInfo.CurrentCulture)
						}));
					}
					throw new Win32Exception(lastWin32Error);
				}
				else
				{
					result = safeThreadHandle;
				}
			}
			catch (EntryPointNotFoundException inner)
			{
				throw new PlatformNotSupportedException(SR.GetString("Win2000Required"), inner);
			}
			return result;
		}

		// Token: 0x06003049 RID: 12361 RVA: 0x000DA1D0 File Offset: 0x000D83D0
		public static bool IsRemoteMachine(string machineName)
		{
			if (machineName == null)
			{
				throw new ArgumentNullException("machineName");
			}
			if (machineName.Length == 0)
			{
				throw new ArgumentException(SR.GetString("InvalidParameter", new object[]
				{
					"machineName",
					machineName
				}));
			}
			string text;
			if (machineName.StartsWith("\\", StringComparison.Ordinal))
			{
				text = machineName.Substring(2);
			}
			else
			{
				text = machineName;
			}
			if (text.Equals("."))
			{
				return false;
			}
			StringBuilder stringBuilder = new StringBuilder(256);
			SafeNativeMethods.GetComputerName(stringBuilder, new int[]
			{
				stringBuilder.Capacity
			});
			string strA = stringBuilder.ToString();
			return string.Compare(strA, text, StringComparison.OrdinalIgnoreCase) != 0;
		}
	}
}

using System;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;
using Microsoft.Win32;
using Microsoft.Win32.SafeHandles;

namespace System.Diagnostics
{
	// Token: 0x02000780 RID: 1920
	internal static class ProcessManager
	{
		// Token: 0x06003B54 RID: 15188 RVA: 0x000FC4A0 File Offset: 0x000FB4A0
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
					SafeNativeMethods.CloseHandle(new HandleRef(null, zero));
				}
			}
		}

		// Token: 0x17000DEB RID: 3563
		// (get) Token: 0x06003B55 RID: 15189 RVA: 0x000FC548 File Offset: 0x000FB548
		public static bool IsNt
		{
			get
			{
				return Environment.OSVersion.Platform == PlatformID.Win32NT;
			}
		}

		// Token: 0x17000DEC RID: 3564
		// (get) Token: 0x06003B56 RID: 15190 RVA: 0x000FC557 File Offset: 0x000FB557
		public static bool IsOSOlderThanXP
		{
			get
			{
				return Environment.OSVersion.Version.Major < 5 || (Environment.OSVersion.Version.Major == 5 && Environment.OSVersion.Version.Minor == 0);
			}
		}

		// Token: 0x06003B57 RID: 15191 RVA: 0x000FC594 File Offset: 0x000FB594
		public static ProcessInfo[] GetProcessInfos(string machineName)
		{
			bool flag = ProcessManager.IsRemoteMachine(machineName);
			if (ProcessManager.IsNt)
			{
				if (!flag && Environment.OSVersion.Version.Major >= 5)
				{
					return NtProcessInfoHelper.GetProcessInfos();
				}
				return NtProcessManager.GetProcessInfos(machineName, flag);
			}
			else
			{
				if (flag)
				{
					throw new PlatformNotSupportedException(SR.GetString("WinNTRequiredForRemote"));
				}
				return WinProcessManager.GetProcessInfos();
			}
		}

		// Token: 0x06003B58 RID: 15192 RVA: 0x000FC5EA File Offset: 0x000FB5EA
		public static int[] GetProcessIds()
		{
			if (ProcessManager.IsNt)
			{
				return NtProcessManager.GetProcessIds();
			}
			return WinProcessManager.GetProcessIds();
		}

		// Token: 0x06003B59 RID: 15193 RVA: 0x000FC5FE File Offset: 0x000FB5FE
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

		// Token: 0x06003B5A RID: 15194 RVA: 0x000FC62C File Offset: 0x000FB62C
		public static bool IsProcessRunning(int processId, string machineName)
		{
			return ProcessManager.IsProcessRunning(processId, ProcessManager.GetProcessIds(machineName));
		}

		// Token: 0x06003B5B RID: 15195 RVA: 0x000FC63A File Offset: 0x000FB63A
		public static bool IsProcessRunning(int processId)
		{
			return ProcessManager.IsProcessRunning(processId, ProcessManager.GetProcessIds());
		}

		// Token: 0x06003B5C RID: 15196 RVA: 0x000FC648 File Offset: 0x000FB648
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

		// Token: 0x06003B5D RID: 15197 RVA: 0x000FC66C File Offset: 0x000FB66C
		public static int GetProcessIdFromHandle(SafeProcessHandle processHandle)
		{
			if (ProcessManager.IsNt)
			{
				return NtProcessManager.GetProcessIdFromHandle(processHandle);
			}
			throw new PlatformNotSupportedException(SR.GetString("WinNTRequired"));
		}

		// Token: 0x06003B5E RID: 15198 RVA: 0x000FC68C File Offset: 0x000FB68C
		public static IntPtr GetMainWindowHandle(ProcessInfo processInfo)
		{
			MainWindowFinder mainWindowFinder = new MainWindowFinder();
			return mainWindowFinder.FindMainWindow(processInfo.processId);
		}

		// Token: 0x06003B5F RID: 15199 RVA: 0x000FC6AB File Offset: 0x000FB6AB
		public static ModuleInfo[] GetModuleInfos(int processId)
		{
			if (ProcessManager.IsNt)
			{
				return NtProcessManager.GetModuleInfos(processId);
			}
			return WinProcessManager.GetModuleInfos(processId);
		}

		// Token: 0x06003B60 RID: 15200 RVA: 0x000FC6C4 File Offset: 0x000FB6C4
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

		// Token: 0x06003B61 RID: 15201 RVA: 0x000FC734 File Offset: 0x000FB734
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

		// Token: 0x06003B62 RID: 15202 RVA: 0x000FC7B8 File Offset: 0x000FB7B8
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

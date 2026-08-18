using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Principal;
using System.ServiceModel.Activation;
using System.Text;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200083B RID: 2107
	internal class AppContainerInfo
	{
		// Token: 0x06004EBC RID: 20156 RVA: 0x0011F06E File Offset: 0x0011D26E
		static AppContainerInfo()
		{
			AppContainerInfo.isAppContainerSupported = OSEnvironmentHelper.IsAtLeast(OSVersion.Win8);
			if (!AppContainerInfo.isAppContainerSupported)
			{
				AppContainerInfo.isRunningInAppContainerSet = true;
			}
		}

		// Token: 0x06004EBD RID: 20157 RVA: 0x0011F09E File Offset: 0x0011D29E
		private AppContainerInfo(int sessionId, string namedObjectPath)
		{
			this.SessionId = sessionId;
			this.NamedObjectPath = namedObjectPath;
		}

		// Token: 0x1700139D RID: 5021
		// (get) Token: 0x06004EBE RID: 20158 RVA: 0x0011F0B4 File Offset: 0x0011D2B4
		internal static bool IsAppContainerSupported
		{
			get
			{
				return AppContainerInfo.isAppContainerSupported;
			}
		}

		// Token: 0x1700139E RID: 5022
		// (get) Token: 0x06004EBF RID: 20159 RVA: 0x0011F0BC File Offset: 0x0011D2BC
		internal static bool IsRunningInAppContainer
		{
			get
			{
				if (!AppContainerInfo.isRunningInAppContainerSet)
				{
					object obj = AppContainerInfo.isRunningInAppContainerLock;
					lock (obj)
					{
						if (!AppContainerInfo.isRunningInAppContainerSet)
						{
							AppContainerInfo.isRunningInAppContainer = AppContainerInfo.RunningInAppContainer();
							AppContainerInfo.isRunningInAppContainerSet = true;
						}
					}
				}
				return AppContainerInfo.isRunningInAppContainer;
			}
		}

		// Token: 0x1700139F RID: 5023
		// (get) Token: 0x06004EC0 RID: 20160 RVA: 0x0011F120 File Offset: 0x0011D320
		// (set) Token: 0x06004EC1 RID: 20161 RVA: 0x0011F128 File Offset: 0x0011D328
		internal int SessionId { get; private set; }

		// Token: 0x170013A0 RID: 5024
		// (get) Token: 0x06004EC2 RID: 20162 RVA: 0x0011F131 File Offset: 0x0011D331
		// (set) Token: 0x06004EC3 RID: 20163 RVA: 0x0011F139 File Offset: 0x0011D339
		internal string NamedObjectPath { get; private set; }

		// Token: 0x06004EC4 RID: 20164 RVA: 0x0011F144 File Offset: 0x0011D344
		internal static AppContainerInfo CreateAppContainerInfo(string fullName, int sessionId)
		{
			int num = sessionId;
			if (num == -1)
			{
				object obj = AppContainerInfo.thisLock;
				lock (obj)
				{
					if (AppContainerInfo.currentSessionId == null)
					{
						AppContainerInfo.currentSessionId = new int?(AppContainerInfo.GetCurrentSessionId());
					}
				}
				num = AppContainerInfo.currentSessionId.Value;
			}
			string appContainerNamedObjectPath = AppContainerInfo.GetAppContainerNamedObjectPath(fullName);
			return new AppContainerInfo(num, appContainerNamedObjectPath);
		}

		// Token: 0x06004EC5 RID: 20165 RVA: 0x0011F1B8 File Offset: 0x0011D3B8
		[SecuritySafeCritical]
		internal static SecurityIdentifier GetCurrentAppContainerSid()
		{
			if (AppContainerInfo.currentAppContainerSid == null)
			{
				object obj = AppContainerInfo.thisLock;
				lock (obj)
				{
					if (AppContainerInfo.currentAppContainerSid == null)
					{
						SafeCloseHandle safeCloseHandle = null;
						try
						{
							safeCloseHandle = AppContainerInfo.GetCurrentProcessToken();
							AppContainerInfo.currentAppContainerSid = UnsafeNativeMethods.GetAppContainerSid(safeCloseHandle);
						}
						finally
						{
							if (safeCloseHandle != null)
							{
								safeCloseHandle.Dispose();
							}
						}
					}
				}
			}
			return AppContainerInfo.currentAppContainerSid;
		}

		// Token: 0x06004EC6 RID: 20166 RVA: 0x0011F244 File Offset: 0x0011D444
		[SecuritySafeCritical]
		private static bool RunningInAppContainer()
		{
			SafeCloseHandle safeCloseHandle = null;
			bool result;
			try
			{
				safeCloseHandle = AppContainerInfo.GetCurrentProcessToken();
				result = UnsafeNativeMethods.RunningInAppContainer(safeCloseHandle);
			}
			finally
			{
				if (safeCloseHandle != null)
				{
					safeCloseHandle.Dispose();
				}
			}
			return result;
		}

		// Token: 0x06004EC7 RID: 20167 RVA: 0x0011F280 File Offset: 0x0011D480
		[SecuritySafeCritical]
		private static string GetAppContainerNamedObjectPath(string name)
		{
			IntPtr zero = IntPtr.Zero;
			uint num = 260U;
			StringBuilder stringBuilder = new StringBuilder(260);
			int num2 = UnsafeNativeMethods.PackageFamilyNameFromFullName(name, ref num, stringBuilder);
			if (num2 != 0)
			{
				throw FxTrace.Exception.AsError(new Win32Exception(num2, SR.GetString("PackageFullNameInvalid", new object[]
				{
					name
				})));
			}
			string appContainerName = stringBuilder.ToString();
			string result;
			try
			{
				int num3 = UnsafeNativeMethods.DeriveAppContainerSidFromAppContainerName(appContainerName, out zero);
				if (num3 != 0)
				{
					num2 = Marshal.GetLastWin32Error();
					throw FxTrace.Exception.AsError(new Win32Exception(num2));
				}
				StringBuilder stringBuilder2 = new StringBuilder(260);
				uint num4 = 0U;
				if (!UnsafeNativeMethods.GetAppContainerNamedObjectPath(IntPtr.Zero, zero, 260U, stringBuilder2, ref num4))
				{
					num2 = Marshal.GetLastWin32Error();
					throw FxTrace.Exception.AsError(new Win32Exception(num2));
				}
				result = stringBuilder2.ToString();
			}
			finally
			{
				if (zero != IntPtr.Zero)
				{
					UnsafeNativeMethods.FreeSid(zero);
				}
			}
			return result;
		}

		// Token: 0x06004EC8 RID: 20168 RVA: 0x0011F378 File Offset: 0x0011D578
		[SecuritySafeCritical]
		private static int GetCurrentSessionId()
		{
			SafeCloseHandle safeCloseHandle = null;
			int sessionId;
			try
			{
				safeCloseHandle = AppContainerInfo.GetCurrentProcessToken();
				sessionId = UnsafeNativeMethods.GetSessionId(safeCloseHandle);
			}
			finally
			{
				if (safeCloseHandle != null)
				{
					safeCloseHandle.Dispose();
				}
			}
			return sessionId;
		}

		// Token: 0x06004EC9 RID: 20169 RVA: 0x0011F3B4 File Offset: 0x0011D5B4
		[SecurityCritical]
		private static SafeCloseHandle GetCurrentProcessToken()
		{
			SafeCloseHandle result = null;
			if (!UnsafeNativeMethods.OpenProcessToken(UnsafeNativeMethods.GetCurrentProcess(), TokenAccessLevels.Query, out result))
			{
				int lastWin32Error = Marshal.GetLastWin32Error();
				throw FxTrace.Exception.AsError(new Win32Exception(lastWin32Error));
			}
			return result;
		}

		// Token: 0x040030FB RID: 12539
		private static object thisLock = new object();

		// Token: 0x040030FC RID: 12540
		private static bool isAppContainerSupported;

		// Token: 0x040030FD RID: 12541
		private static bool isRunningInAppContainer;

		// Token: 0x040030FE RID: 12542
		private static volatile bool isRunningInAppContainerSet;

		// Token: 0x040030FF RID: 12543
		private static object isRunningInAppContainerLock = new object();

		// Token: 0x04003100 RID: 12544
		private static int? currentSessionId;

		// Token: 0x04003101 RID: 12545
		private static volatile SecurityIdentifier currentAppContainerSid;
	}
}

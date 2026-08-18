using System;
using System.ComponentModel;
using System.IdentityModel;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Security.Principal;
using System.ServiceModel.Diagnostics;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x020001E2 RID: 482
	internal static class SecurityUtils
	{
		// Token: 0x06000F8F RID: 3983 RVA: 0x0003767C File Offset: 0x0003587C
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
		public static SafeHandle GetTokenInformation(SafeCloseHandle token, TOKEN_INFORMATION_CLASS infoClass)
		{
			uint num;
			if (!SafeNativeMethods.GetTokenInformation(token, infoClass, SafeHGlobalHandle.InvalidHandle, 0U, out num))
			{
				int lastWin32Error = Marshal.GetLastWin32Error();
				if (lastWin32Error != 122)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new Win32Exception(lastWin32Error, SR.GetString("GetTokenInfoFailed", new object[]
					{
						lastWin32Error
					})));
				}
			}
			SafeHandle safeHandle = SafeHGlobalHandle.AllocHGlobal(num);
			try
			{
				if (!SafeNativeMethods.GetTokenInformation(token, infoClass, safeHandle, num, out num))
				{
					int lastWin32Error2 = Marshal.GetLastWin32Error();
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new Win32Exception(lastWin32Error2, SR.GetString("GetTokenInfoFailed", new object[]
					{
						lastWin32Error2
					})));
				}
			}
			catch
			{
				safeHandle.Dispose();
				throw;
			}
			return safeHandle;
		}

		// Token: 0x06000F90 RID: 3984 RVA: 0x00037730 File Offset: 0x00035930
		internal static bool IsAtleastImpersonationToken(SafeCloseHandle token)
		{
			bool result;
			using (SafeHandle tokenInformation = SecurityUtils.GetTokenInformation(token, TOKEN_INFORMATION_CLASS.TokenImpersonationLevel))
			{
				int num = Marshal.ReadInt32(tokenInformation.DangerousGetHandle());
				if (num < 2)
				{
					result = false;
				}
				else
				{
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06000F91 RID: 3985 RVA: 0x0003777C File Offset: 0x0003597C
		internal static bool IsPrimaryToken(SafeCloseHandle token)
		{
			bool result;
			using (SafeHandle tokenInformation = SecurityUtils.GetTokenInformation(token, TOKEN_INFORMATION_CLASS.TokenType))
			{
				int num = Marshal.ReadInt32(tokenInformation.DangerousGetHandle());
				result = (num == 1);
			}
			return result;
		}

		// Token: 0x06000F92 RID: 3986 RVA: 0x000377C0 File Offset: 0x000359C0
		internal static LUID GetModifiedIDLUID(SafeCloseHandle token)
		{
			LUID modifiedId;
			using (SafeHandle tokenInformation = SecurityUtils.GetTokenInformation(token, TOKEN_INFORMATION_CLASS.TokenStatistics))
			{
				TOKEN_STATISTICS token_STATISTICS = (TOKEN_STATISTICS)Marshal.PtrToStructure(tokenInformation.DangerousGetHandle(), typeof(TOKEN_STATISTICS));
				modifiedId = token_STATISTICS.ModifiedId;
			}
			return modifiedId;
		}

		// Token: 0x06000F93 RID: 3987 RVA: 0x00037818 File Offset: 0x00035A18
		public static WindowsIdentity GetAnonymousIdentity()
		{
			SafeCloseHandle safeCloseHandle = null;
			bool flag = false;
			object obj = SecurityUtils.lockObject;
			lock (obj)
			{
				if (SecurityUtils.anonymousIdentity == null)
				{
					try
					{
						try
						{
							if (!SafeNativeMethods.ImpersonateAnonymousUserOnCurrentThread(SafeNativeMethods.GetCurrentThread()))
							{
								int lastWin32Error = Marshal.GetLastWin32Error();
								throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new Win32Exception(lastWin32Error, SR.GetString("ImpersonateAnonymousTokenFailed", new object[]
								{
									lastWin32Error
								})));
							}
							flag = true;
							if (!SafeNativeMethods.OpenCurrentThreadToken(SafeNativeMethods.GetCurrentThread(), TokenAccessLevels.Query, true, out safeCloseHandle))
							{
								int lastWin32Error2 = Marshal.GetLastWin32Error();
								if (!SafeNativeMethods.RevertToSelf())
								{
									lastWin32Error2 = Marshal.GetLastWin32Error();
									DiagnosticUtility.FailFast("RevertToSelf() failed with " + lastWin32Error2.ToString());
								}
								flag = false;
								Utility.CloseInvalidOutSafeHandle(safeCloseHandle);
								throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new Win32Exception(lastWin32Error2, SR.GetString("OpenThreadTokenFailed", new object[]
								{
									lastWin32Error2
								})));
							}
							if (!SafeNativeMethods.RevertToSelf())
							{
								DiagnosticUtility.FailFast("RevertToSelf() failed with " + Marshal.GetLastWin32Error().ToString());
							}
							flag = false;
							using (safeCloseHandle)
							{
								SecurityUtils.anonymousIdentity = new WindowsIdentity(safeCloseHandle.DangerousGetHandle());
							}
						}
						finally
						{
							if (flag && !SafeNativeMethods.RevertToSelf())
							{
								DiagnosticUtility.FailFast("RevertToSelf() failed with " + Marshal.GetLastWin32Error().ToString());
							}
						}
					}
					catch
					{
						throw;
					}
				}
			}
			return SecurityUtils.anonymousIdentity;
		}

		// Token: 0x06000F94 RID: 3988 RVA: 0x000379F4 File Offset: 0x00035BF4
		public static WindowsIdentity GetProcessIdentity()
		{
			SafeCloseHandle safeCloseHandle = null;
			object obj = SecurityUtils.lockObject;
			lock (obj)
			{
				try
				{
					if (!SafeNativeMethods.GetCurrentProcessToken(SafeNativeMethods.GetCurrentProcess(), TokenAccessLevels.Query, out safeCloseHandle))
					{
						int lastWin32Error = Marshal.GetLastWin32Error();
						Utility.CloseInvalidOutSafeHandle(safeCloseHandle);
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new Win32Exception(lastWin32Error, SR.GetString("OpenProcessTokenFailed", new object[]
						{
							lastWin32Error
						})));
					}
					SecurityUtils.processIdentity = new WindowsIdentity(safeCloseHandle.DangerousGetHandle());
				}
				finally
				{
					if (safeCloseHandle != null)
					{
						safeCloseHandle.Dispose();
					}
				}
			}
			return SecurityUtils.processIdentity;
		}

		// Token: 0x040017C1 RID: 6081
		private static WindowsIdentity anonymousIdentity;

		// Token: 0x040017C2 RID: 6082
		private static WindowsIdentity processIdentity;

		// Token: 0x040017C3 RID: 6083
		private static object lockObject = new object();
	}
}

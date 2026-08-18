using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Security.Principal;
using System.ServiceModel.Diagnostics;

namespace System.IdentityModel
{
	// Token: 0x02000066 RID: 102
	internal class Privilege
	{
		// Token: 0x0600032E RID: 814 RVA: 0x0000C2E9 File Offset: 0x0000A4E9
		public Privilege(string privilege)
		{
			this.privilege = privilege;
			this.luid = Privilege.LuidFromPrivilege(privilege);
		}

		// Token: 0x0600032F RID: 815 RVA: 0x0000C304 File Offset: 0x0000A504
		public void Enable()
		{
			this.threadToken = this.GetThreadToken();
			this.EnableTokenPrivilege(this.threadToken);
		}

		// Token: 0x06000330 RID: 816 RVA: 0x0000C320 File Offset: 0x0000A520
		[ReliabilityContract(Consistency.WillNotCorruptState, Cer.Success)]
		public int Revert()
		{
			if (!this.isImpersonating)
			{
				if (this.needToRevert && !this.initialEnabled)
				{
					TOKEN_PRIVILEGE token_PRIVILEGE;
					token_PRIVILEGE.PrivilegeCount = 1U;
					token_PRIVILEGE.Privilege.Luid = this.luid;
					token_PRIVILEGE.Privilege.Attributes = 0U;
					uint num = 0U;
					TOKEN_PRIVILEGE token_PRIVILEGE2;
					if (!NativeMethods.AdjustTokenPrivileges(this.threadToken, false, ref token_PRIVILEGE, TOKEN_PRIVILEGE.Size, out token_PRIVILEGE2, out num))
					{
						return Marshal.GetLastWin32Error();
					}
				}
				this.needToRevert = false;
			}
			else
			{
				if (!NativeMethods.RevertToSelf())
				{
					return Marshal.GetLastWin32Error();
				}
				this.isImpersonating = false;
			}
			if (this.threadToken != null)
			{
				this.threadToken.Close();
				this.threadToken = null;
			}
			return 0;
		}

		// Token: 0x06000331 RID: 817 RVA: 0x0000C3C8 File Offset: 0x0000A5C8
		private SafeCloseHandle GetThreadToken()
		{
			SafeCloseHandle safeCloseHandle;
			if (!NativeMethods.OpenThreadToken(NativeMethods.GetCurrentThread(), TokenAccessLevels.Query | TokenAccessLevels.AdjustPrivileges, true, out safeCloseHandle))
			{
				int lastWin32Error = Marshal.GetLastWin32Error();
				Utility.CloseInvalidOutSafeHandle(safeCloseHandle);
				if (lastWin32Error != 1008)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new Win32Exception(lastWin32Error));
				}
				SafeCloseHandle safeCloseHandle2;
				if (!NativeMethods.OpenProcessToken(NativeMethods.GetCurrentProcess(), TokenAccessLevels.Duplicate, out safeCloseHandle2))
				{
					lastWin32Error = Marshal.GetLastWin32Error();
					Utility.CloseInvalidOutSafeHandle(safeCloseHandle2);
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new Win32Exception(lastWin32Error));
				}
				try
				{
					if (!NativeMethods.DuplicateTokenEx(safeCloseHandle2, TokenAccessLevels.Impersonate | TokenAccessLevels.Query | TokenAccessLevels.AdjustPrivileges, IntPtr.Zero, SECURITY_IMPERSONATION_LEVEL.Impersonation, TokenType.TokenImpersonation, out safeCloseHandle))
					{
						lastWin32Error = Marshal.GetLastWin32Error();
						Utility.CloseInvalidOutSafeHandle(safeCloseHandle);
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new Win32Exception(lastWin32Error));
					}
					this.SetThreadToken(safeCloseHandle);
				}
				finally
				{
					safeCloseHandle2.Close();
				}
			}
			return safeCloseHandle;
		}

		// Token: 0x06000332 RID: 818 RVA: 0x0000C48C File Offset: 0x0000A68C
		private void EnableTokenPrivilege(SafeCloseHandle threadToken)
		{
			TOKEN_PRIVILEGE token_PRIVILEGE;
			token_PRIVILEGE.PrivilegeCount = 1U;
			token_PRIVILEGE.Privilege.Luid = this.luid;
			token_PRIVILEGE.Privilege.Attributes = 2U;
			uint num = 0U;
			bool flag = false;
			int num2 = 0;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
			}
			finally
			{
				TOKEN_PRIVILEGE token_PRIVILEGE2;
				flag = NativeMethods.AdjustTokenPrivileges(threadToken, false, ref token_PRIVILEGE, TOKEN_PRIVILEGE.Size, out token_PRIVILEGE2, out num);
				num2 = Marshal.GetLastWin32Error();
				if (flag && num2 == 0)
				{
					this.initialEnabled = ((token_PRIVILEGE2.Privilege.Attributes & 2U) > 0U);
					this.needToRevert = true;
				}
			}
			if (num2 == 1300)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new PrivilegeNotHeldException(this.privilege));
			}
			if (!flag)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new Win32Exception(num2));
			}
		}

		// Token: 0x06000333 RID: 819 RVA: 0x0000C554 File Offset: 0x0000A754
		private void SetThreadToken(SafeCloseHandle threadToken)
		{
			int error = 0;
			RuntimeHelpers.PrepareConstrainedRegions();
			try
			{
			}
			finally
			{
				if (!NativeMethods.SetThreadToken(IntPtr.Zero, threadToken))
				{
					error = Marshal.GetLastWin32Error();
				}
				else
				{
					this.isImpersonating = true;
				}
			}
			if (!this.isImpersonating)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new Win32Exception(error));
			}
		}

		// Token: 0x06000334 RID: 820 RVA: 0x0000C5B0 File Offset: 0x0000A7B0
		private static LUID LuidFromPrivilege(string privilege)
		{
			Dictionary<string, LUID> obj = Privilege.luids;
			LUID luid;
			lock (obj)
			{
				if (Privilege.luids.TryGetValue(privilege, out luid))
				{
					return luid;
				}
			}
			if (!NativeMethods.LookupPrivilegeValueW(null, privilege, out luid))
			{
				int lastWin32Error = Marshal.GetLastWin32Error();
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new Win32Exception(lastWin32Error));
			}
			Dictionary<string, LUID> obj2 = Privilege.luids;
			lock (obj2)
			{
				if (!Privilege.luids.ContainsKey(privilege))
				{
					Privilege.luids[privilege] = luid;
				}
			}
			return luid;
		}

		// Token: 0x04000348 RID: 840
		private static Dictionary<string, LUID> luids = new Dictionary<string, LUID>();

		// Token: 0x04000349 RID: 841
		public const string SeAuditPrivilege = "SeAuditPrivilege";

		// Token: 0x0400034A RID: 842
		public const string SeTcbPrivilege = "SeTcbPrivilege";

		// Token: 0x0400034B RID: 843
		private const uint SE_PRIVILEGE_DISABLED = 0U;

		// Token: 0x0400034C RID: 844
		private const uint SE_PRIVILEGE_ENABLED_BY_DEFAULT = 1U;

		// Token: 0x0400034D RID: 845
		private const uint SE_PRIVILEGE_ENABLED = 2U;

		// Token: 0x0400034E RID: 846
		private const uint SE_PRIVILEGE_USED_FOR_ACCESS = 2147483648U;

		// Token: 0x0400034F RID: 847
		private const int ERROR_SUCCESS = 0;

		// Token: 0x04000350 RID: 848
		private const int ERROR_NO_TOKEN = 1008;

		// Token: 0x04000351 RID: 849
		private const int ERROR_NOT_ALL_ASSIGNED = 1300;

		// Token: 0x04000352 RID: 850
		private string privilege;

		// Token: 0x04000353 RID: 851
		private LUID luid;

		// Token: 0x04000354 RID: 852
		private bool needToRevert;

		// Token: 0x04000355 RID: 853
		private bool initialEnabled;

		// Token: 0x04000356 RID: 854
		private bool isImpersonating;

		// Token: 0x04000357 RID: 855
		private SafeCloseHandle threadToken;
	}
}

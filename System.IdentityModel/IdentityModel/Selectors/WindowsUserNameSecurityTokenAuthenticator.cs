using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IdentityModel.Claims;
using System.IdentityModel.Policy;
using System.IdentityModel.Tokens;
using System.Runtime.InteropServices;
using System.Security.Principal;

namespace System.IdentityModel.Selectors
{
	// Token: 0x020001B0 RID: 432
	public class WindowsUserNameSecurityTokenAuthenticator : UserNameSecurityTokenAuthenticator
	{
		// Token: 0x06000E15 RID: 3605 RVA: 0x0003FECE File Offset: 0x0003E0CE
		public WindowsUserNameSecurityTokenAuthenticator() : this(true)
		{
		}

		// Token: 0x06000E16 RID: 3606 RVA: 0x0003FED7 File Offset: 0x0003E0D7
		public WindowsUserNameSecurityTokenAuthenticator(bool includeWindowsGroups)
		{
			this.includeWindowsGroups = includeWindowsGroups;
		}

		// Token: 0x06000E17 RID: 3607 RVA: 0x0003FEE8 File Offset: 0x0003E0E8
		protected override ReadOnlyCollection<IAuthorizationPolicy> ValidateUserNamePasswordCore(string userName, string password)
		{
			string lpszDomain = null;
			string[] array = userName.Split(new char[]
			{
				'\\'
			});
			if (array.Length != 1)
			{
				if (array.Length != 2 || string.IsNullOrEmpty(array[0]))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument(SR.GetString("IncorrectUserNameFormat"));
				}
				userName = array[1];
				lpszDomain = array[0];
			}
			SafeCloseHandle safeCloseHandle = null;
			ReadOnlyCollection<IAuthorizationPolicy> result;
			try
			{
				if (!NativeMethods.LogonUser(userName, lpszDomain, password, 8U, 0U, out safeCloseHandle))
				{
					int lastWin32Error = Marshal.GetLastWin32Error();
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenValidationException(SR.GetString("FailLogonUser", new object[]
					{
						userName
					}), new Win32Exception(lastWin32Error)));
				}
				WindowsIdentity windowsIdentity = new WindowsIdentity(safeCloseHandle.DangerousGetHandle(), "Basic");
				WindowsClaimSet windowsClaimSet = new WindowsClaimSet(windowsIdentity, "Basic", this.includeWindowsGroups, false);
				result = SecurityUtils.CreateAuthorizationPolicies(windowsClaimSet, windowsClaimSet.ExpirationTime);
			}
			finally
			{
				if (safeCloseHandle != null)
				{
					safeCloseHandle.Close();
				}
			}
			return result;
		}

		// Token: 0x04000CEB RID: 3307
		private bool includeWindowsGroups;
	}
}

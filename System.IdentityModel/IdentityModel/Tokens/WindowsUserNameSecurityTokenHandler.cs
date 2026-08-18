using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime;
using System.Runtime.InteropServices;
using System.Security.Claims;
using System.Security.Principal;
using System.Xml;

namespace System.IdentityModel.Tokens
{
	// Token: 0x0200018A RID: 394
	public class WindowsUserNameSecurityTokenHandler : UserNameSecurityTokenHandler
	{
		// Token: 0x17000338 RID: 824
		// (get) Token: 0x06000CED RID: 3309 RVA: 0x00002434 File Offset: 0x00000634
		public override bool CanValidateToken
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000CEE RID: 3310 RVA: 0x0003BE34 File Offset: 0x0003A034
		public override ReadOnlyCollection<ClaimsIdentity> ValidateToken(SecurityToken token)
		{
			if (token == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("token");
			}
			UserNameSecurityToken userNameSecurityToken = token as UserNameSecurityToken;
			if (userNameSecurityToken == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("token", SR.GetString("ID0018", new object[]
				{
					typeof(UserNameSecurityToken)
				}));
			}
			if (base.Configuration == null)
			{
				throw DiagnosticUtility.ThrowHelperInvalidOperation(SR.GetString("ID4274"));
			}
			ReadOnlyCollection<ClaimsIdentity> result;
			try
			{
				string text = userNameSecurityToken.UserName;
				string password = userNameSecurityToken.Password;
				string lpszDomain = null;
				string[] array = userNameSecurityToken.UserName.Split(new char[]
				{
					'\\'
				});
				if (array.Length != 1)
				{
					if (array.Length != 2 || string.IsNullOrEmpty(array[0]))
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("token", SR.GetString("ID4062"));
					}
					text = array[1];
					lpszDomain = array[0];
				}
				SafeCloseHandle safeCloseHandle = null;
				try
				{
					if (!NativeMethods.LogonUser(text, lpszDomain, password, 8U, 0U, out safeCloseHandle))
					{
						int lastWin32Error = Marshal.GetLastWin32Error();
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenValidationException(SR.GetString("ID4063", new object[]
						{
							text
						}), new Win32Exception(lastWin32Error)));
					}
					WindowsIdentity windowsIdentity = new WindowsIdentity(safeCloseHandle.DangerousGetHandle(), "Password", WindowsAccountType.Normal, true);
					windowsIdentity.AddClaim(new Claim("http://schemas.microsoft.com/ws/2008/06/identity/claims/authenticationinstant", XmlConvert.ToString(DateTime.UtcNow, DateTimeFormats.Generated), "http://www.w3.org/2001/XMLSchema#dateTime"));
					windowsIdentity.AddClaim(new Claim("http://schemas.microsoft.com/ws/2008/06/identity/claims/authenticationmethod", "http://schemas.microsoft.com/ws/2008/06/identity/authenticationmethod/password"));
					if (base.Configuration.SaveBootstrapContext)
					{
						if (this.RetainPassword)
						{
							windowsIdentity.BootstrapContext = new BootstrapContext(userNameSecurityToken, this);
						}
						else
						{
							windowsIdentity.BootstrapContext = new BootstrapContext(new UserNameSecurityToken(userNameSecurityToken.UserName, null), this);
						}
					}
					base.TraceTokenValidationSuccess(token);
					result = new List<ClaimsIdentity>(1)
					{
						windowsIdentity
					}.AsReadOnly();
				}
				finally
				{
					if (safeCloseHandle != null)
					{
						safeCloseHandle.Close();
					}
				}
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				base.TraceTokenValidationFailure(token, ex.Message);
				throw ex;
			}
			return result;
		}
	}
}

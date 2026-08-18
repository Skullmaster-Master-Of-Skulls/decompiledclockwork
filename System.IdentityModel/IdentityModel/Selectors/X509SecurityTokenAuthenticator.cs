using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.IdentityModel.Claims;
using System.IdentityModel.Policy;
using System.IdentityModel.Tokens;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.AccessControl;
using System.Security.Cryptography.X509Certificates;
using System.Security.Permissions;
using System.Security.Principal;

namespace System.IdentityModel.Selectors
{
	// Token: 0x020001B4 RID: 436
	public class X509SecurityTokenAuthenticator : SecurityTokenAuthenticator
	{
		// Token: 0x06000E34 RID: 3636 RVA: 0x00040FB9 File Offset: 0x0003F1B9
		public X509SecurityTokenAuthenticator() : this(X509CertificateValidator.ChainTrust)
		{
		}

		// Token: 0x06000E35 RID: 3637 RVA: 0x00040FC6 File Offset: 0x0003F1C6
		public X509SecurityTokenAuthenticator(X509CertificateValidator validator) : this(validator, false)
		{
		}

		// Token: 0x06000E36 RID: 3638 RVA: 0x00040FD0 File Offset: 0x0003F1D0
		public X509SecurityTokenAuthenticator(X509CertificateValidator validator, bool mapToWindows) : this(validator, mapToWindows, true)
		{
		}

		// Token: 0x06000E37 RID: 3639 RVA: 0x00040FDB File Offset: 0x0003F1DB
		public X509SecurityTokenAuthenticator(X509CertificateValidator validator, bool mapToWindows, bool includeWindowsGroups) : this(validator, mapToWindows, includeWindowsGroups, true)
		{
		}

		// Token: 0x06000E38 RID: 3640 RVA: 0x00040FE7 File Offset: 0x0003F1E7
		internal X509SecurityTokenAuthenticator(X509CertificateValidator validator, bool mapToWindows, bool includeWindowsGroups, bool cloneHandle)
		{
			if (validator == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("validator");
			}
			this.validator = validator;
			this.mapToWindows = mapToWindows;
			this.includeWindowsGroups = includeWindowsGroups;
			this.cloneHandle = cloneHandle;
		}

		// Token: 0x17000373 RID: 883
		// (get) Token: 0x06000E39 RID: 3641 RVA: 0x0004101F File Offset: 0x0003F21F
		public bool MapCertificateToWindowsAccount
		{
			get
			{
				return this.mapToWindows;
			}
		}

		// Token: 0x06000E3A RID: 3642 RVA: 0x00041027 File Offset: 0x0003F227
		protected override bool CanValidateTokenCore(SecurityToken token)
		{
			return token is X509SecurityToken;
		}

		// Token: 0x06000E3B RID: 3643 RVA: 0x00041034 File Offset: 0x0003F234
		protected override ReadOnlyCollection<IAuthorizationPolicy> ValidateTokenCore(SecurityToken token)
		{
			X509SecurityToken x509SecurityToken = (X509SecurityToken)token;
			this.validator.Validate(x509SecurityToken.Certificate);
			X509CertificateClaimSet x509CertificateClaimSet = new X509CertificateClaimSet(x509SecurityToken.Certificate, this.cloneHandle);
			if (!this.mapToWindows)
			{
				return SecurityUtils.CreateAuthorizationPolicies(x509CertificateClaimSet, x509SecurityToken.ValidTo);
			}
			WindowsClaimSet item;
			if (token is X509WindowsSecurityToken)
			{
				item = new WindowsClaimSet(((X509WindowsSecurityToken)token).WindowsIdentity, "SSL/PCT", this.includeWindowsGroups, this.cloneHandle);
			}
			else
			{
				X509CertificateValidator.NTAuthChainTrust.Validate(x509SecurityToken.Certificate);
				WindowsIdentity windowsIdentity = null;
				if (Environment.OSVersion.Version.Major >= 6)
				{
					windowsIdentity = X509SecurityTokenAuthenticator.KerberosCertificateLogon(x509SecurityToken.Certificate);
				}
				else
				{
					string nameInfo = x509SecurityToken.Certificate.GetNameInfo(X509NameType.UpnName, false);
					if (string.IsNullOrEmpty(nameInfo))
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenValidationException(SR.GetString("InvalidNtMapping", new object[]
						{
							SecurityUtils.GetCertificateId(x509SecurityToken.Certificate)
						})));
					}
					using (WindowsIdentity windowsIdentity2 = new WindowsIdentity(nameInfo, "SSL/PCT"))
					{
						windowsIdentity = new WindowsIdentity(windowsIdentity2.Token, "SSL/PCT");
					}
				}
				item = new WindowsClaimSet(windowsIdentity, "SSL/PCT", this.includeWindowsGroups, false);
			}
			List<ClaimSet> list = new List<ClaimSet>(2);
			list.Add(item);
			list.Add(x509CertificateClaimSet);
			return new List<IAuthorizationPolicy>(1)
			{
				new UnconditionalPolicy(list.AsReadOnly(), x509SecurityToken.ValidTo)
			}.AsReadOnly();
		}

		// Token: 0x06000E3C RID: 3644 RVA: 0x000411B8 File Offset: 0x0003F3B8
		[SecuritySafeCritical]
		[SecurityPermission(SecurityAction.Demand, Unrestricted = true)]
		internal unsafe static WindowsIdentity KerberosCertificateLogon(X509Certificate2 certificate)
		{
			SafeHGlobalHandle safeHGlobalHandle = null;
			SafeHGlobalHandle safeHGlobalHandle2 = null;
			SafeHGlobalHandle safeHGlobalHandle3 = null;
			SafeLsaLogonProcessHandle safeLsaLogonProcessHandle = null;
			SafeLsaReturnBufferHandle safeLsaReturnBufferHandle = null;
			SafeCloseHandle safeCloseHandle = null;
			WindowsIdentity result;
			try
			{
				safeHGlobalHandle = SafeHGlobalHandle.AllocHGlobal(NativeMethods.LsaSourceName.Length + 1);
				Marshal.Copy(NativeMethods.LsaSourceName, 0, safeHGlobalHandle.DangerousGetHandle(), NativeMethods.LsaSourceName.Length);
				UNICODE_INTPTR_STRING unicode_INTPTR_STRING = new UNICODE_INTPTR_STRING(NativeMethods.LsaSourceName.Length, NativeMethods.LsaSourceName.Length + 1, safeHGlobalHandle.DangerousGetHandle());
				Privilege privilege = null;
				RuntimeHelpers.PrepareConstrainedRegions();
				int num;
				try
				{
					try
					{
						privilege = new Privilege("SeTcbPrivilege");
						privilege.Enable();
					}
					catch (PrivilegeNotHeldException exception)
					{
						DiagnosticUtility.TraceHandledException(exception, TraceEventType.Information);
					}
					IntPtr zero = IntPtr.Zero;
					num = NativeMethods.LsaRegisterLogonProcess(ref unicode_INTPTR_STRING, out safeLsaLogonProcessHandle, out zero);
					if (5 == NativeMethods.LsaNtStatusToWinError(num))
					{
						num = NativeMethods.LsaConnectUntrusted(out safeLsaLogonProcessHandle);
					}
					if (num < 0)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new Win32Exception(NativeMethods.LsaNtStatusToWinError(num)));
					}
				}
				finally
				{
					int num2 = -1;
					string message = null;
					try
					{
						num2 = privilege.Revert();
						if (num2 != 0)
						{
							message = SR.GetString("RevertingPrivilegeFailed", new object[]
							{
								new Win32Exception(num2)
							});
						}
					}
					finally
					{
						if (num2 != 0)
						{
							DiagnosticUtility.FailFast(message);
						}
					}
				}
				safeHGlobalHandle2 = SafeHGlobalHandle.AllocHGlobal(NativeMethods.LsaKerberosName.Length + 1);
				Marshal.Copy(NativeMethods.LsaKerberosName, 0, safeHGlobalHandle2.DangerousGetHandle(), NativeMethods.LsaKerberosName.Length);
				UNICODE_INTPTR_STRING unicode_INTPTR_STRING2 = new UNICODE_INTPTR_STRING(NativeMethods.LsaKerberosName.Length, NativeMethods.LsaKerberosName.Length + 1, safeHGlobalHandle2.DangerousGetHandle());
				uint authenticationPackage = 0U;
				num = NativeMethods.LsaLookupAuthenticationPackage(safeLsaLogonProcessHandle, ref unicode_INTPTR_STRING2, out authenticationPackage);
				if (num < 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new Win32Exception(NativeMethods.LsaNtStatusToWinError(num)));
				}
				TOKEN_SOURCE token_SOURCE = default(TOKEN_SOURCE);
				if (!NativeMethods.AllocateLocallyUniqueId(out token_SOURCE.SourceIdentifier))
				{
					int lastWin32Error = Marshal.GetLastWin32Error();
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new Win32Exception(lastWin32Error));
				}
				token_SOURCE.Name = new char[8];
				token_SOURCE.Name[0] = 'W';
				token_SOURCE.Name[1] = 'C';
				token_SOURCE.Name[2] = 'F';
				byte[] rawData = certificate.RawData;
				int num3 = KERB_CERTIFICATE_S4U_LOGON.Size + rawData.Length;
				safeHGlobalHandle3 = SafeHGlobalHandle.AllocHGlobal(num3);
				KERB_CERTIFICATE_S4U_LOGON* ptr = (KERB_CERTIFICATE_S4U_LOGON*)safeHGlobalHandle3.DangerousGetHandle().ToPointer();
				ptr->MessageType = KERB_LOGON_SUBMIT_TYPE.KerbCertificateS4ULogon;
				ptr->Flags = 2U;
				ptr->UserPrincipalName = new UNICODE_INTPTR_STRING(0, 0, IntPtr.Zero);
				ptr->DomainName = new UNICODE_INTPTR_STRING(0, 0, IntPtr.Zero);
				ptr->CertificateLength = (uint)rawData.Length;
				ptr->Certificate = new IntPtr(safeHGlobalHandle3.DangerousGetHandle().ToInt64() + (long)KERB_CERTIFICATE_S4U_LOGON.Size);
				Marshal.Copy(rawData, 0, ptr->Certificate, rawData.Length);
				QUOTA_LIMITS quota_LIMITS = default(QUOTA_LIMITS);
				LUID luid = default(LUID);
				int num4 = 0;
				uint num5;
				num = NativeMethods.LsaLogonUser(safeLsaLogonProcessHandle, ref unicode_INTPTR_STRING, SecurityLogonType.Network, authenticationPackage, safeHGlobalHandle3.DangerousGetHandle(), (uint)num3, IntPtr.Zero, ref token_SOURCE, out safeLsaReturnBufferHandle, out num5, out luid, out safeCloseHandle, out quota_LIMITS, out num4);
				if (num == -1073741714 && num4 < 0)
				{
					num = num4;
				}
				if (num < 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new Win32Exception(NativeMethods.LsaNtStatusToWinError(num)));
				}
				if (num4 < 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new Win32Exception(NativeMethods.LsaNtStatusToWinError(num4)));
				}
				result = new WindowsIdentity(safeCloseHandle.DangerousGetHandle(), "SSL/PCT");
			}
			finally
			{
				if (safeCloseHandle != null)
				{
					safeCloseHandle.Close();
				}
				if (safeHGlobalHandle3 != null)
				{
					safeHGlobalHandle3.Close();
				}
				if (safeLsaReturnBufferHandle != null)
				{
					safeLsaReturnBufferHandle.Close();
				}
				if (safeHGlobalHandle != null)
				{
					safeHGlobalHandle.Close();
				}
				if (safeHGlobalHandle2 != null)
				{
					safeHGlobalHandle2.Close();
				}
				if (safeLsaLogonProcessHandle != null)
				{
					safeLsaLogonProcessHandle.Close();
				}
			}
			return result;
		}

		// Token: 0x04000CF8 RID: 3320
		private X509CertificateValidator validator;

		// Token: 0x04000CF9 RID: 3321
		private bool mapToWindows;

		// Token: 0x04000CFA RID: 3322
		private bool includeWindowsGroups;

		// Token: 0x04000CFB RID: 3323
		private bool cloneHandle;
	}
}

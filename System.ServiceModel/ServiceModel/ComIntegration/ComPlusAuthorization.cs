using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IdentityModel;
using System.Runtime;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Security.Principal;
using System.ServiceModel.Diagnostics;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x020001E3 RID: 483
	internal sealed class ComPlusAuthorization
	{
		// Token: 0x06000F96 RID: 3990 RVA: 0x00037AB0 File Offset: 0x00035CB0
		public ComPlusAuthorization(string[] serviceRoleMembers, string[] contractRoleMembers, string[] operationRoleMembers)
		{
			this.serviceRoleMembers = serviceRoleMembers;
			this.contractRoleMembers = contractRoleMembers;
			this.operationRoleMembers = operationRoleMembers;
		}

		// Token: 0x06000F97 RID: 3991 RVA: 0x00037AD8 File Offset: 0x00035CD8
		private void BuildSecurityDescriptor()
		{
			RawAcl rawAcl = new RawAcl(GenericAcl.AclRevision, 1);
			int num = 0;
			if (this.operationRoleMembers != null)
			{
				foreach (string name in this.operationRoleMembers)
				{
					NTAccount ntaccount = new NTAccount(name);
					SecurityIdentifier sid = (SecurityIdentifier)ntaccount.Translate(typeof(SecurityIdentifier));
					CommonAce ace = new CommonAce(AceFlags.None, AceQualifier.AccessAllowed, 1, sid, false, null);
					rawAcl.InsertAce(num, ace);
					num++;
				}
			}
			if (this.contractRoleMembers != null)
			{
				foreach (string name2 in this.contractRoleMembers)
				{
					NTAccount ntaccount = new NTAccount(name2);
					SecurityIdentifier sid = (SecurityIdentifier)ntaccount.Translate(typeof(SecurityIdentifier));
					CommonAce ace = new CommonAce(AceFlags.None, AceQualifier.AccessAllowed, 1, sid, false, null);
					rawAcl.InsertAce(num, ace);
					num++;
				}
			}
			if (this.serviceRoleMembers != null)
			{
				foreach (string name3 in this.serviceRoleMembers)
				{
					NTAccount ntaccount = new NTAccount(name3);
					SecurityIdentifier sid = (SecurityIdentifier)ntaccount.Translate(typeof(SecurityIdentifier));
					CommonAce ace = new CommonAce(AceFlags.None, AceQualifier.AccessAllowed, 1, sid, false, null);
					rawAcl.InsertAce(num, ace);
					num++;
				}
			}
			DiscretionaryAcl discretionaryAcl = new DiscretionaryAcl(true, false, rawAcl);
			this.securityDescriptor = new CommonSecurityDescriptor(true, false, ControlFlags.DiscretionaryAclPresent, ComPlusAuthorization.sidAdministrators, ComPlusAuthorization.sidAdministrators, null, discretionaryAcl);
		}

		// Token: 0x06000F98 RID: 3992 RVA: 0x00037C44 File Offset: 0x00035E44
		private bool IsAccessCached(LUID luidModifiedID, out bool isAccessAllowed)
		{
			if (this.accessCheckCache == null)
			{
				throw Fx.AssertAndThrowFatal("AcessCheckCache must not be NULL");
			}
			bool result = false;
			lock (this)
			{
				result = this.accessCheckCache.TryGetValue(luidModifiedID, out isAccessAllowed);
			}
			return result;
		}

		// Token: 0x06000F99 RID: 3993 RVA: 0x00037CA0 File Offset: 0x00035EA0
		private void CacheAccessCheck(LUID luidModifiedID, bool isAccessAllowed)
		{
			if (this.accessCheckCache == null)
			{
				throw Fx.AssertAndThrowFatal("AcessCheckCache must not be NULL");
			}
			lock (this)
			{
				this.accessCheckCache[luidModifiedID] = isAccessAllowed;
			}
		}

		// Token: 0x06000F9A RID: 3994 RVA: 0x00037CF8 File Offset: 0x00035EF8
		private void CheckAccess(WindowsIdentity clientIdentity, out bool IsAccessAllowed)
		{
			if (this.securityDescriptor == null)
			{
				throw Fx.AssertAndThrowFatal("Security Descriptor must not be NULL");
			}
			IsAccessAllowed = false;
			byte[] binaryForm = new byte[this.securityDescriptor.BinaryLength];
			this.securityDescriptor.GetBinaryForm(binaryForm, 0);
			SafeCloseHandle safeCloseHandle = null;
			SafeCloseHandle safeCloseHandle2 = new SafeCloseHandle(clientIdentity.Token, false);
			try
			{
				if (SecurityUtils.IsPrimaryToken(safeCloseHandle2) && !SafeNativeMethods.DuplicateTokenEx(safeCloseHandle2, TokenAccessLevels.Query, IntPtr.Zero, SecurityImpersonationLevel.Identification, TokenType.TokenImpersonation, out safeCloseHandle))
				{
					int lastWin32Error = Marshal.GetLastWin32Error();
					Utility.CloseInvalidOutSafeHandle(safeCloseHandle);
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new Win32Exception(lastWin32Error, SR.GetString("DuplicateTokenExFailed", new object[]
					{
						lastWin32Error
					})));
				}
				GENERIC_MAPPING genericMapping = new GENERIC_MAPPING();
				PRIVILEGE_SET structure = new PRIVILEGE_SET();
				uint num = (uint)Marshal.SizeOf(structure);
				uint num2 = 0U;
				if (!SafeNativeMethods.AccessCheck(binaryForm, (safeCloseHandle != null) ? safeCloseHandle : safeCloseHandle2, 1, genericMapping, out structure, ref num, out num2, out IsAccessAllowed))
				{
					int lastWin32Error2 = Marshal.GetLastWin32Error();
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new Win32Exception(lastWin32Error2, SR.GetString("AccessCheckFailed", new object[]
					{
						lastWin32Error2
					})));
				}
			}
			finally
			{
				if (safeCloseHandle != null)
				{
					safeCloseHandle.Dispose();
				}
			}
		}

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x06000F9B RID: 3995 RVA: 0x00037E1C File Offset: 0x0003601C
		public string[] ServiceRoleMembers
		{
			get
			{
				return this.serviceRoleMembers;
			}
		}

		// Token: 0x170003B5 RID: 949
		// (get) Token: 0x06000F9C RID: 3996 RVA: 0x00037E24 File Offset: 0x00036024
		public string[] ContractRoleMembers
		{
			get
			{
				return this.contractRoleMembers;
			}
		}

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x06000F9D RID: 3997 RVA: 0x00037E2C File Offset: 0x0003602C
		public string[] OperationRoleMembers
		{
			get
			{
				return this.operationRoleMembers;
			}
		}

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x06000F9E RID: 3998 RVA: 0x00037E34 File Offset: 0x00036034
		public CommonSecurityDescriptor SecurityDescriptor
		{
			get
			{
				return this.securityDescriptor;
			}
		}

		// Token: 0x06000F9F RID: 3999 RVA: 0x00037E3C File Offset: 0x0003603C
		public bool IsAuthorizedForOperation(WindowsIdentity clientIdentity)
		{
			bool flag = false;
			if (clientIdentity == null)
			{
				throw Fx.AssertAndThrow("NULL Identity");
			}
			if (IntPtr.Zero == clientIdentity.Token)
			{
				throw Fx.AssertAndThrow("Token handle cannot be zero");
			}
			lock (this)
			{
				if (this.securityDescriptor == null)
				{
					this.BuildSecurityDescriptor();
				}
			}
			LUID modifiedIDLUID = SecurityUtils.GetModifiedIDLUID(new SafeCloseHandle(clientIdentity.Token, false));
			if (this.IsAccessCached(modifiedIDLUID, out flag))
			{
				return flag;
			}
			this.CheckAccess(clientIdentity, out flag);
			this.CacheAccessCheck(modifiedIDLUID, flag);
			return flag;
		}

		// Token: 0x040017C4 RID: 6084
		private string[] serviceRoleMembers;

		// Token: 0x040017C5 RID: 6085
		private string[] contractRoleMembers;

		// Token: 0x040017C6 RID: 6086
		private string[] operationRoleMembers;

		// Token: 0x040017C7 RID: 6087
		private CommonSecurityDescriptor securityDescriptor;

		// Token: 0x040017C8 RID: 6088
		private static SecurityIdentifier sidAdministrators = new SecurityIdentifier(WellKnownSidType.BuiltinAdministratorsSid, null);

		// Token: 0x040017C9 RID: 6089
		private Dictionary<LUID, bool> accessCheckCache = new Dictionary<LUID, bool>();
	}
}

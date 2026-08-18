using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Security.Principal;
using Microsoft.Win32.SafeHandles;

namespace System.Security.AccessControl
{
	// Token: 0x02000935 RID: 2357
	public sealed class RegistrySecurity : NativeObjectSecurity
	{
		// Token: 0x060054FF RID: 21759 RVA: 0x001344FA File Offset: 0x001334FA
		public RegistrySecurity() : base(true, ResourceType.RegistryKey)
		{
		}

		// Token: 0x06005500 RID: 21760 RVA: 0x00134504 File Offset: 0x00133504
		[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
		internal RegistrySecurity(SafeRegistryHandle hKey, string name, AccessControlSections includeSections) : base(true, ResourceType.RegistryKey, hKey, includeSections, new NativeObjectSecurity.ExceptionFromErrorCode(RegistrySecurity._HandleErrorCode), null)
		{
			new RegistryPermission(RegistryPermissionAccess.NoAccess, AccessControlActions.View, name).Demand();
		}

		// Token: 0x06005501 RID: 21761 RVA: 0x0013452C File Offset: 0x0013352C
		private static Exception _HandleErrorCode(int errorCode, string name, SafeHandle handle, object context)
		{
			Exception result = null;
			if (errorCode != 2)
			{
				if (errorCode != 6)
				{
					if (errorCode == 123)
					{
						result = new ArgumentException(Environment.GetResourceString("Arg_RegInvalidKeyName", new object[]
						{
							"name"
						}));
					}
				}
				else
				{
					result = new ArgumentException(Environment.GetResourceString("AccessControl_InvalidHandle"));
				}
			}
			else
			{
				result = new IOException(Environment.GetResourceString("Arg_RegKeyNotFound", new object[]
				{
					errorCode
				}));
			}
			return result;
		}

		// Token: 0x06005502 RID: 21762 RVA: 0x001345A2 File Offset: 0x001335A2
		public override AccessRule AccessRuleFactory(IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type)
		{
			return new RegistryAccessRule(identityReference, accessMask, isInherited, inheritanceFlags, propagationFlags, type);
		}

		// Token: 0x06005503 RID: 21763 RVA: 0x001345B2 File Offset: 0x001335B2
		public override AuditRule AuditRuleFactory(IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags)
		{
			return new RegistryAuditRule(identityReference, accessMask, isInherited, inheritanceFlags, propagationFlags, flags);
		}

		// Token: 0x06005504 RID: 21764 RVA: 0x001345C4 File Offset: 0x001335C4
		internal AccessControlSections GetAccessControlSectionsFromChanges()
		{
			AccessControlSections accessControlSections = AccessControlSections.None;
			if (base.AccessRulesModified)
			{
				accessControlSections = AccessControlSections.Access;
			}
			if (base.AuditRulesModified)
			{
				accessControlSections |= AccessControlSections.Audit;
			}
			if (base.OwnerModified)
			{
				accessControlSections |= AccessControlSections.Owner;
			}
			if (base.GroupModified)
			{
				accessControlSections |= AccessControlSections.Group;
			}
			return accessControlSections;
		}

		// Token: 0x06005505 RID: 21765 RVA: 0x00134604 File Offset: 0x00133604
		[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
		internal void Persist(SafeRegistryHandle hKey, string keyName)
		{
			new RegistryPermission(RegistryPermissionAccess.NoAccess, AccessControlActions.Change, keyName).Demand();
			base.WriteLock();
			try
			{
				AccessControlSections accessControlSectionsFromChanges = this.GetAccessControlSectionsFromChanges();
				if (accessControlSectionsFromChanges != AccessControlSections.None)
				{
					base.Persist(hKey, accessControlSectionsFromChanges);
					base.OwnerModified = (base.GroupModified = (base.AuditRulesModified = (base.AccessRulesModified = false)));
				}
			}
			finally
			{
				base.WriteUnlock();
			}
		}

		// Token: 0x06005506 RID: 21766 RVA: 0x00134674 File Offset: 0x00133674
		public void AddAccessRule(RegistryAccessRule rule)
		{
			base.AddAccessRule(rule);
		}

		// Token: 0x06005507 RID: 21767 RVA: 0x0013467D File Offset: 0x0013367D
		public void SetAccessRule(RegistryAccessRule rule)
		{
			base.SetAccessRule(rule);
		}

		// Token: 0x06005508 RID: 21768 RVA: 0x00134686 File Offset: 0x00133686
		public void ResetAccessRule(RegistryAccessRule rule)
		{
			base.ResetAccessRule(rule);
		}

		// Token: 0x06005509 RID: 21769 RVA: 0x0013468F File Offset: 0x0013368F
		public bool RemoveAccessRule(RegistryAccessRule rule)
		{
			return base.RemoveAccessRule(rule);
		}

		// Token: 0x0600550A RID: 21770 RVA: 0x00134698 File Offset: 0x00133698
		public void RemoveAccessRuleAll(RegistryAccessRule rule)
		{
			base.RemoveAccessRuleAll(rule);
		}

		// Token: 0x0600550B RID: 21771 RVA: 0x001346A1 File Offset: 0x001336A1
		public void RemoveAccessRuleSpecific(RegistryAccessRule rule)
		{
			base.RemoveAccessRuleSpecific(rule);
		}

		// Token: 0x0600550C RID: 21772 RVA: 0x001346AA File Offset: 0x001336AA
		public void AddAuditRule(RegistryAuditRule rule)
		{
			base.AddAuditRule(rule);
		}

		// Token: 0x0600550D RID: 21773 RVA: 0x001346B3 File Offset: 0x001336B3
		public void SetAuditRule(RegistryAuditRule rule)
		{
			base.SetAuditRule(rule);
		}

		// Token: 0x0600550E RID: 21774 RVA: 0x001346BC File Offset: 0x001336BC
		public bool RemoveAuditRule(RegistryAuditRule rule)
		{
			return base.RemoveAuditRule(rule);
		}

		// Token: 0x0600550F RID: 21775 RVA: 0x001346C5 File Offset: 0x001336C5
		public void RemoveAuditRuleAll(RegistryAuditRule rule)
		{
			base.RemoveAuditRuleAll(rule);
		}

		// Token: 0x06005510 RID: 21776 RVA: 0x001346CE File Offset: 0x001336CE
		public void RemoveAuditRuleSpecific(RegistryAuditRule rule)
		{
			base.RemoveAuditRuleSpecific(rule);
		}

		// Token: 0x17000EA9 RID: 3753
		// (get) Token: 0x06005511 RID: 21777 RVA: 0x001346D7 File Offset: 0x001336D7
		public override Type AccessRightType
		{
			get
			{
				return typeof(RegistryRights);
			}
		}

		// Token: 0x17000EAA RID: 3754
		// (get) Token: 0x06005512 RID: 21778 RVA: 0x001346E3 File Offset: 0x001336E3
		public override Type AccessRuleType
		{
			get
			{
				return typeof(RegistryAccessRule);
			}
		}

		// Token: 0x17000EAB RID: 3755
		// (get) Token: 0x06005513 RID: 21779 RVA: 0x001346EF File Offset: 0x001336EF
		public override Type AuditRuleType
		{
			get
			{
				return typeof(RegistryAuditRule);
			}
		}
	}
}

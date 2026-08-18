using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Security.Principal;
using Microsoft.Win32.SafeHandles;

namespace System.Security.AccessControl
{
	// Token: 0x02000926 RID: 2342
	public abstract class FileSystemSecurity : NativeObjectSecurity
	{
		// Token: 0x0600548D RID: 21645 RVA: 0x00132629 File Offset: 0x00131629
		internal FileSystemSecurity(bool isContainer) : base(isContainer, ResourceType.FileObject, new NativeObjectSecurity.ExceptionFromErrorCode(FileSystemSecurity._HandleErrorCode), isContainer)
		{
		}

		// Token: 0x0600548E RID: 21646 RVA: 0x00132645 File Offset: 0x00131645
		internal FileSystemSecurity(bool isContainer, string name, AccessControlSections includeSections, bool isDirectory) : base(isContainer, ResourceType.FileObject, name, includeSections, new NativeObjectSecurity.ExceptionFromErrorCode(FileSystemSecurity._HandleErrorCode), isDirectory)
		{
		}

		// Token: 0x0600548F RID: 21647 RVA: 0x00132664 File Offset: 0x00131664
		internal FileSystemSecurity(bool isContainer, SafeFileHandle handle, AccessControlSections includeSections, bool isDirectory) : base(isContainer, ResourceType.FileObject, handle, includeSections, new NativeObjectSecurity.ExceptionFromErrorCode(FileSystemSecurity._HandleErrorCode), isDirectory)
		{
		}

		// Token: 0x06005490 RID: 21648 RVA: 0x00132684 File Offset: 0x00131684
		private static Exception _HandleErrorCode(int errorCode, string name, SafeHandle handle, object context)
		{
			Exception result = null;
			if (errorCode != 2)
			{
				if (errorCode != 6)
				{
					if (errorCode == 123)
					{
						result = new ArgumentException(Environment.GetResourceString("Argument_InvalidName"), "name");
					}
				}
				else
				{
					result = new ArgumentException(Environment.GetResourceString("AccessControl_InvalidHandle"));
				}
			}
			else if (context != null && context is bool && (bool)context)
			{
				if (name != null && name.Length != 0)
				{
					result = new DirectoryNotFoundException(name);
				}
				else
				{
					result = new DirectoryNotFoundException();
				}
			}
			else if (name != null && name.Length != 0)
			{
				result = new FileNotFoundException(name);
			}
			else
			{
				result = new FileNotFoundException();
			}
			return result;
		}

		// Token: 0x06005491 RID: 21649 RVA: 0x00132715 File Offset: 0x00131715
		public sealed override AccessRule AccessRuleFactory(IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type)
		{
			return new FileSystemAccessRule(identityReference, accessMask, isInherited, inheritanceFlags, propagationFlags, type);
		}

		// Token: 0x06005492 RID: 21650 RVA: 0x00132725 File Offset: 0x00131725
		public sealed override AuditRule AuditRuleFactory(IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags)
		{
			return new FileSystemAuditRule(identityReference, accessMask, isInherited, inheritanceFlags, propagationFlags, flags);
		}

		// Token: 0x06005493 RID: 21651 RVA: 0x00132738 File Offset: 0x00131738
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

		// Token: 0x06005494 RID: 21652 RVA: 0x00132778 File Offset: 0x00131778
		[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
		internal void Persist(string fullPath)
		{
			new FileIOPermission(FileIOPermissionAccess.NoAccess, AccessControlActions.Change, fullPath).Demand();
			base.WriteLock();
			try
			{
				AccessControlSections accessControlSectionsFromChanges = this.GetAccessControlSectionsFromChanges();
				base.Persist(fullPath, accessControlSectionsFromChanges);
				base.OwnerModified = (base.GroupModified = (base.AuditRulesModified = (base.AccessRulesModified = false)));
			}
			finally
			{
				base.WriteUnlock();
			}
		}

		// Token: 0x06005495 RID: 21653 RVA: 0x001327E4 File Offset: 0x001317E4
		[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
		internal void Persist(SafeFileHandle handle, string fullPath)
		{
			if (fullPath != null)
			{
				new FileIOPermission(FileIOPermissionAccess.NoAccess, AccessControlActions.Change, fullPath).Demand();
			}
			else
			{
				new FileIOPermission(PermissionState.Unrestricted).Demand();
			}
			base.WriteLock();
			try
			{
				AccessControlSections accessControlSectionsFromChanges = this.GetAccessControlSectionsFromChanges();
				base.Persist(handle, accessControlSectionsFromChanges);
				base.OwnerModified = (base.GroupModified = (base.AuditRulesModified = (base.AccessRulesModified = false)));
			}
			finally
			{
				base.WriteUnlock();
			}
		}

		// Token: 0x06005496 RID: 21654 RVA: 0x00132860 File Offset: 0x00131860
		public void AddAccessRule(FileSystemAccessRule rule)
		{
			base.AddAccessRule(rule);
		}

		// Token: 0x06005497 RID: 21655 RVA: 0x00132869 File Offset: 0x00131869
		public void SetAccessRule(FileSystemAccessRule rule)
		{
			base.SetAccessRule(rule);
		}

		// Token: 0x06005498 RID: 21656 RVA: 0x00132872 File Offset: 0x00131872
		public void ResetAccessRule(FileSystemAccessRule rule)
		{
			base.ResetAccessRule(rule);
		}

		// Token: 0x06005499 RID: 21657 RVA: 0x0013287C File Offset: 0x0013187C
		public bool RemoveAccessRule(FileSystemAccessRule rule)
		{
			if (rule == null)
			{
				throw new ArgumentNullException("rule");
			}
			AuthorizationRuleCollection accessRules = base.GetAccessRules(true, true, rule.IdentityReference.GetType());
			for (int i = 0; i < accessRules.Count; i++)
			{
				FileSystemAccessRule fileSystemAccessRule = accessRules[i] as FileSystemAccessRule;
				if (fileSystemAccessRule != null && fileSystemAccessRule.FileSystemRights == rule.FileSystemRights && fileSystemAccessRule.IdentityReference == rule.IdentityReference && fileSystemAccessRule.AccessControlType == rule.AccessControlType)
				{
					return base.RemoveAccessRule(rule);
				}
			}
			FileSystemAccessRule rule2 = new FileSystemAccessRule(rule.IdentityReference, FileSystemAccessRule.AccessMaskFromRights(rule.FileSystemRights, AccessControlType.Deny), rule.IsInherited, rule.InheritanceFlags, rule.PropagationFlags, rule.AccessControlType);
			return base.RemoveAccessRule(rule2);
		}

		// Token: 0x0600549A RID: 21658 RVA: 0x0013293A File Offset: 0x0013193A
		public void RemoveAccessRuleAll(FileSystemAccessRule rule)
		{
			base.RemoveAccessRuleAll(rule);
		}

		// Token: 0x0600549B RID: 21659 RVA: 0x00132944 File Offset: 0x00131944
		public void RemoveAccessRuleSpecific(FileSystemAccessRule rule)
		{
			if (rule == null)
			{
				throw new ArgumentNullException("rule");
			}
			AuthorizationRuleCollection accessRules = base.GetAccessRules(true, true, rule.IdentityReference.GetType());
			for (int i = 0; i < accessRules.Count; i++)
			{
				FileSystemAccessRule fileSystemAccessRule = accessRules[i] as FileSystemAccessRule;
				if (fileSystemAccessRule != null && fileSystemAccessRule.FileSystemRights == rule.FileSystemRights && fileSystemAccessRule.IdentityReference == rule.IdentityReference && fileSystemAccessRule.AccessControlType == rule.AccessControlType)
				{
					base.RemoveAccessRuleSpecific(rule);
					return;
				}
			}
			FileSystemAccessRule rule2 = new FileSystemAccessRule(rule.IdentityReference, FileSystemAccessRule.AccessMaskFromRights(rule.FileSystemRights, AccessControlType.Deny), rule.IsInherited, rule.InheritanceFlags, rule.PropagationFlags, rule.AccessControlType);
			base.RemoveAccessRuleSpecific(rule2);
		}

		// Token: 0x0600549C RID: 21660 RVA: 0x00132A02 File Offset: 0x00131A02
		public void AddAuditRule(FileSystemAuditRule rule)
		{
			base.AddAuditRule(rule);
		}

		// Token: 0x0600549D RID: 21661 RVA: 0x00132A0B File Offset: 0x00131A0B
		public void SetAuditRule(FileSystemAuditRule rule)
		{
			base.SetAuditRule(rule);
		}

		// Token: 0x0600549E RID: 21662 RVA: 0x00132A14 File Offset: 0x00131A14
		public bool RemoveAuditRule(FileSystemAuditRule rule)
		{
			return base.RemoveAuditRule(rule);
		}

		// Token: 0x0600549F RID: 21663 RVA: 0x00132A1D File Offset: 0x00131A1D
		public void RemoveAuditRuleAll(FileSystemAuditRule rule)
		{
			base.RemoveAuditRuleAll(rule);
		}

		// Token: 0x060054A0 RID: 21664 RVA: 0x00132A26 File Offset: 0x00131A26
		public void RemoveAuditRuleSpecific(FileSystemAuditRule rule)
		{
			base.RemoveAuditRuleSpecific(rule);
		}

		// Token: 0x17000E9A RID: 3738
		// (get) Token: 0x060054A1 RID: 21665 RVA: 0x00132A2F File Offset: 0x00131A2F
		public override Type AccessRightType
		{
			get
			{
				return typeof(FileSystemRights);
			}
		}

		// Token: 0x17000E9B RID: 3739
		// (get) Token: 0x060054A2 RID: 21666 RVA: 0x00132A3B File Offset: 0x00131A3B
		public override Type AccessRuleType
		{
			get
			{
				return typeof(FileSystemAccessRule);
			}
		}

		// Token: 0x17000E9C RID: 3740
		// (get) Token: 0x060054A3 RID: 21667 RVA: 0x00132A47 File Offset: 0x00131A47
		public override Type AuditRuleType
		{
			get
			{
				return typeof(FileSystemAuditRule);
			}
		}

		// Token: 0x04002BE5 RID: 11237
		private const ResourceType s_ResourceType = ResourceType.FileObject;
	}
}

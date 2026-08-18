using System;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Threading;
using Microsoft.Win32.SafeHandles;

namespace System.Security.AccessControl
{
	// Token: 0x0200092C RID: 2348
	public sealed class MutexSecurity : NativeObjectSecurity
	{
		// Token: 0x060054B0 RID: 21680 RVA: 0x00132B44 File Offset: 0x00131B44
		public MutexSecurity() : base(true, ResourceType.KernelObject)
		{
		}

		// Token: 0x060054B1 RID: 21681 RVA: 0x00132B4E File Offset: 0x00131B4E
		public MutexSecurity(string name, AccessControlSections includeSections) : base(true, ResourceType.KernelObject, name, includeSections, new NativeObjectSecurity.ExceptionFromErrorCode(MutexSecurity._HandleErrorCode), null)
		{
		}

		// Token: 0x060054B2 RID: 21682 RVA: 0x00132B67 File Offset: 0x00131B67
		internal MutexSecurity(SafeWaitHandle handle, AccessControlSections includeSections) : base(true, ResourceType.KernelObject, handle, includeSections, new NativeObjectSecurity.ExceptionFromErrorCode(MutexSecurity._HandleErrorCode), null)
		{
		}

		// Token: 0x060054B3 RID: 21683 RVA: 0x00132B80 File Offset: 0x00131B80
		private static Exception _HandleErrorCode(int errorCode, string name, SafeHandle handle, object context)
		{
			Exception result = null;
			if (errorCode == 2 || errorCode == 6 || errorCode == 123)
			{
				if (name != null && name.Length != 0)
				{
					result = new WaitHandleCannotBeOpenedException(Environment.GetResourceString("Threading.WaitHandleCannotBeOpenedException_InvalidHandle", new object[]
					{
						name
					}));
				}
				else
				{
					result = new WaitHandleCannotBeOpenedException();
				}
			}
			return result;
		}

		// Token: 0x060054B4 RID: 21684 RVA: 0x00132BCE File Offset: 0x00131BCE
		public override AccessRule AccessRuleFactory(IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type)
		{
			return new MutexAccessRule(identityReference, accessMask, isInherited, inheritanceFlags, propagationFlags, type);
		}

		// Token: 0x060054B5 RID: 21685 RVA: 0x00132BDE File Offset: 0x00131BDE
		public override AuditRule AuditRuleFactory(IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags)
		{
			return new MutexAuditRule(identityReference, accessMask, isInherited, inheritanceFlags, propagationFlags, flags);
		}

		// Token: 0x060054B6 RID: 21686 RVA: 0x00132BF0 File Offset: 0x00131BF0
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

		// Token: 0x060054B7 RID: 21687 RVA: 0x00132C30 File Offset: 0x00131C30
		internal void Persist(SafeWaitHandle handle)
		{
			base.WriteLock();
			try
			{
				AccessControlSections accessControlSectionsFromChanges = this.GetAccessControlSectionsFromChanges();
				if (accessControlSectionsFromChanges != AccessControlSections.None)
				{
					base.Persist(handle, accessControlSectionsFromChanges);
					base.OwnerModified = (base.GroupModified = (base.AuditRulesModified = (base.AccessRulesModified = false)));
				}
			}
			finally
			{
				base.WriteUnlock();
			}
		}

		// Token: 0x060054B8 RID: 21688 RVA: 0x00132C94 File Offset: 0x00131C94
		public void AddAccessRule(MutexAccessRule rule)
		{
			base.AddAccessRule(rule);
		}

		// Token: 0x060054B9 RID: 21689 RVA: 0x00132C9D File Offset: 0x00131C9D
		public void SetAccessRule(MutexAccessRule rule)
		{
			base.SetAccessRule(rule);
		}

		// Token: 0x060054BA RID: 21690 RVA: 0x00132CA6 File Offset: 0x00131CA6
		public void ResetAccessRule(MutexAccessRule rule)
		{
			base.ResetAccessRule(rule);
		}

		// Token: 0x060054BB RID: 21691 RVA: 0x00132CAF File Offset: 0x00131CAF
		public bool RemoveAccessRule(MutexAccessRule rule)
		{
			return base.RemoveAccessRule(rule);
		}

		// Token: 0x060054BC RID: 21692 RVA: 0x00132CB8 File Offset: 0x00131CB8
		public void RemoveAccessRuleAll(MutexAccessRule rule)
		{
			base.RemoveAccessRuleAll(rule);
		}

		// Token: 0x060054BD RID: 21693 RVA: 0x00132CC1 File Offset: 0x00131CC1
		public void RemoveAccessRuleSpecific(MutexAccessRule rule)
		{
			base.RemoveAccessRuleSpecific(rule);
		}

		// Token: 0x060054BE RID: 21694 RVA: 0x00132CCA File Offset: 0x00131CCA
		public void AddAuditRule(MutexAuditRule rule)
		{
			base.AddAuditRule(rule);
		}

		// Token: 0x060054BF RID: 21695 RVA: 0x00132CD3 File Offset: 0x00131CD3
		public void SetAuditRule(MutexAuditRule rule)
		{
			base.SetAuditRule(rule);
		}

		// Token: 0x060054C0 RID: 21696 RVA: 0x00132CDC File Offset: 0x00131CDC
		public bool RemoveAuditRule(MutexAuditRule rule)
		{
			return base.RemoveAuditRule(rule);
		}

		// Token: 0x060054C1 RID: 21697 RVA: 0x00132CE5 File Offset: 0x00131CE5
		public void RemoveAuditRuleAll(MutexAuditRule rule)
		{
			base.RemoveAuditRuleAll(rule);
		}

		// Token: 0x060054C2 RID: 21698 RVA: 0x00132CEE File Offset: 0x00131CEE
		public void RemoveAuditRuleSpecific(MutexAuditRule rule)
		{
			base.RemoveAuditRuleSpecific(rule);
		}

		// Token: 0x17000E9F RID: 3743
		// (get) Token: 0x060054C3 RID: 21699 RVA: 0x00132CF7 File Offset: 0x00131CF7
		public override Type AccessRightType
		{
			get
			{
				return typeof(MutexRights);
			}
		}

		// Token: 0x17000EA0 RID: 3744
		// (get) Token: 0x060054C4 RID: 21700 RVA: 0x00132D03 File Offset: 0x00131D03
		public override Type AccessRuleType
		{
			get
			{
				return typeof(MutexAccessRule);
			}
		}

		// Token: 0x17000EA1 RID: 3745
		// (get) Token: 0x060054C5 RID: 21701 RVA: 0x00132D0F File Offset: 0x00131D0F
		public override Type AuditRuleType
		{
			get
			{
				return typeof(MutexAuditRule);
			}
		}
	}
}

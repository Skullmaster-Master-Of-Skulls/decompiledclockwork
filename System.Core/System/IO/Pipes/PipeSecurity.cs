using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.AccessControl;
using System.Security.Permissions;
using System.Security.Principal;
using Microsoft.Win32.SafeHandles;

namespace System.IO.Pipes
{
	// Token: 0x020000BB RID: 187
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public class PipeSecurity : NativeObjectSecurity
	{
		// Token: 0x0600054E RID: 1358 RVA: 0x00010B5C File Offset: 0x0000ED5C
		public PipeSecurity() : base(false, ResourceType.KernelObject)
		{
		}

		// Token: 0x0600054F RID: 1359 RVA: 0x00010B66 File Offset: 0x0000ED66
		[SecuritySafeCritical]
		internal PipeSecurity(SafePipeHandle safeHandle, AccessControlSections includeSections) : base(false, ResourceType.KernelObject, safeHandle, includeSections)
		{
		}

		// Token: 0x06000550 RID: 1360 RVA: 0x00010B72 File Offset: 0x0000ED72
		public void AddAccessRule(PipeAccessRule rule)
		{
			if (rule == null)
			{
				throw new ArgumentNullException("rule");
			}
			base.AddAccessRule(rule);
		}

		// Token: 0x06000551 RID: 1361 RVA: 0x00010B89 File Offset: 0x0000ED89
		public void SetAccessRule(PipeAccessRule rule)
		{
			if (rule == null)
			{
				throw new ArgumentNullException("rule");
			}
			base.SetAccessRule(rule);
		}

		// Token: 0x06000552 RID: 1362 RVA: 0x00010BA0 File Offset: 0x0000EDA0
		public void ResetAccessRule(PipeAccessRule rule)
		{
			if (rule == null)
			{
				throw new ArgumentNullException("rule");
			}
			base.ResetAccessRule(rule);
		}

		// Token: 0x06000553 RID: 1363 RVA: 0x00010BB8 File Offset: 0x0000EDB8
		public bool RemoveAccessRule(PipeAccessRule rule)
		{
			if (rule == null)
			{
				throw new ArgumentNullException("rule");
			}
			AuthorizationRuleCollection accessRules = base.GetAccessRules(true, true, rule.IdentityReference.GetType());
			for (int i = 0; i < accessRules.Count; i++)
			{
				PipeAccessRule pipeAccessRule = accessRules[i] as PipeAccessRule;
				if (pipeAccessRule != null && pipeAccessRule.PipeAccessRights == rule.PipeAccessRights && pipeAccessRule.IdentityReference == rule.IdentityReference && pipeAccessRule.AccessControlType == rule.AccessControlType)
				{
					return base.RemoveAccessRule(rule);
				}
			}
			if (rule.PipeAccessRights != PipeAccessRights.FullControl)
			{
				return base.RemoveAccessRule(new PipeAccessRule(rule.IdentityReference, PipeAccessRule.AccessMaskFromRights(rule.PipeAccessRights, AccessControlType.Deny), false, rule.AccessControlType));
			}
			return base.RemoveAccessRule(rule);
		}

		// Token: 0x06000554 RID: 1364 RVA: 0x00010C78 File Offset: 0x0000EE78
		public void RemoveAccessRuleSpecific(PipeAccessRule rule)
		{
			if (rule == null)
			{
				throw new ArgumentNullException("rule");
			}
			AuthorizationRuleCollection accessRules = base.GetAccessRules(true, true, rule.IdentityReference.GetType());
			for (int i = 0; i < accessRules.Count; i++)
			{
				PipeAccessRule pipeAccessRule = accessRules[i] as PipeAccessRule;
				if (pipeAccessRule != null && pipeAccessRule.PipeAccessRights == rule.PipeAccessRights && pipeAccessRule.IdentityReference == rule.IdentityReference && pipeAccessRule.AccessControlType == rule.AccessControlType)
				{
					base.RemoveAccessRuleSpecific(rule);
					return;
				}
			}
			if (rule.PipeAccessRights != PipeAccessRights.FullControl)
			{
				base.RemoveAccessRuleSpecific(new PipeAccessRule(rule.IdentityReference, PipeAccessRule.AccessMaskFromRights(rule.PipeAccessRights, AccessControlType.Deny), false, rule.AccessControlType));
				return;
			}
			base.RemoveAccessRuleSpecific(rule);
		}

		// Token: 0x06000555 RID: 1365 RVA: 0x00010D38 File Offset: 0x0000EF38
		public void AddAuditRule(PipeAuditRule rule)
		{
			base.AddAuditRule(rule);
		}

		// Token: 0x06000556 RID: 1366 RVA: 0x00010D41 File Offset: 0x0000EF41
		public void SetAuditRule(PipeAuditRule rule)
		{
			base.SetAuditRule(rule);
		}

		// Token: 0x06000557 RID: 1367 RVA: 0x00010D4A File Offset: 0x0000EF4A
		public bool RemoveAuditRule(PipeAuditRule rule)
		{
			return base.RemoveAuditRule(rule);
		}

		// Token: 0x06000558 RID: 1368 RVA: 0x00010D53 File Offset: 0x0000EF53
		public void RemoveAuditRuleAll(PipeAuditRule rule)
		{
			base.RemoveAuditRuleAll(rule);
		}

		// Token: 0x06000559 RID: 1369 RVA: 0x00010D5C File Offset: 0x0000EF5C
		public void RemoveAuditRuleSpecific(PipeAuditRule rule)
		{
			base.RemoveAuditRuleSpecific(rule);
		}

		// Token: 0x0600055A RID: 1370 RVA: 0x00010D65 File Offset: 0x0000EF65
		public override AccessRule AccessRuleFactory(IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type)
		{
			if (inheritanceFlags != InheritanceFlags.None)
			{
				throw new ArgumentException(SR.GetString("Argument_NonContainerInvalidAnyFlag"), "inheritanceFlags");
			}
			if (propagationFlags != PropagationFlags.None)
			{
				throw new ArgumentException(SR.GetString("Argument_NonContainerInvalidAnyFlag"), "propagationFlags");
			}
			return new PipeAccessRule(identityReference, accessMask, isInherited, type);
		}

		// Token: 0x0600055B RID: 1371 RVA: 0x00010DA3 File Offset: 0x0000EFA3
		public sealed override AuditRule AuditRuleFactory(IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags)
		{
			if (inheritanceFlags != InheritanceFlags.None)
			{
				throw new ArgumentException(SR.GetString("Argument_NonContainerInvalidAnyFlag"), "inheritanceFlags");
			}
			if (propagationFlags != PropagationFlags.None)
			{
				throw new ArgumentException(SR.GetString("Argument_NonContainerInvalidAnyFlag"), "propagationFlags");
			}
			return new PipeAuditRule(identityReference, accessMask, isInherited, flags);
		}

		// Token: 0x0600055C RID: 1372 RVA: 0x00010DE4 File Offset: 0x0000EFE4
		private AccessControlSections GetAccessControlSectionsFromChanges()
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

		// Token: 0x0600055D RID: 1373 RVA: 0x00010E24 File Offset: 0x0000F024
		[SecurityCritical]
		[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
		protected internal void Persist(SafeHandle handle)
		{
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

		// Token: 0x0600055E RID: 1374 RVA: 0x00010E84 File Offset: 0x0000F084
		[SecurityCritical]
		[SecurityPermission(SecurityAction.Assert, UnmanagedCode = true)]
		protected internal void Persist(string name)
		{
			base.WriteLock();
			try
			{
				AccessControlSections accessControlSectionsFromChanges = this.GetAccessControlSectionsFromChanges();
				base.Persist(name, accessControlSectionsFromChanges);
				base.OwnerModified = (base.GroupModified = (base.AuditRulesModified = (base.AccessRulesModified = false)));
			}
			finally
			{
				base.WriteUnlock();
			}
		}

		// Token: 0x17000124 RID: 292
		// (get) Token: 0x0600055F RID: 1375 RVA: 0x00010EE4 File Offset: 0x0000F0E4
		public override Type AccessRightType
		{
			get
			{
				return typeof(PipeAccessRights);
			}
		}

		// Token: 0x17000125 RID: 293
		// (get) Token: 0x06000560 RID: 1376 RVA: 0x00010EF0 File Offset: 0x0000F0F0
		public override Type AccessRuleType
		{
			get
			{
				return typeof(PipeAccessRule);
			}
		}

		// Token: 0x17000126 RID: 294
		// (get) Token: 0x06000561 RID: 1377 RVA: 0x00010EFC File Offset: 0x0000F0FC
		public override Type AuditRuleType
		{
			get
			{
				return typeof(PipeAuditRule);
			}
		}
	}
}

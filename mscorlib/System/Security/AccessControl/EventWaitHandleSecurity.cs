using System;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Threading;
using Microsoft.Win32.SafeHandles;

namespace System.Security.AccessControl
{
	// Token: 0x02000922 RID: 2338
	public sealed class EventWaitHandleSecurity : NativeObjectSecurity
	{
		// Token: 0x06005468 RID: 21608 RVA: 0x001322A1 File Offset: 0x001312A1
		public EventWaitHandleSecurity() : base(true, ResourceType.KernelObject)
		{
		}

		// Token: 0x06005469 RID: 21609 RVA: 0x001322AB File Offset: 0x001312AB
		internal EventWaitHandleSecurity(string name, AccessControlSections includeSections) : base(true, ResourceType.KernelObject, name, includeSections, new NativeObjectSecurity.ExceptionFromErrorCode(EventWaitHandleSecurity._HandleErrorCode), null)
		{
		}

		// Token: 0x0600546A RID: 21610 RVA: 0x001322C4 File Offset: 0x001312C4
		internal EventWaitHandleSecurity(SafeWaitHandle handle, AccessControlSections includeSections) : base(true, ResourceType.KernelObject, handle, includeSections, new NativeObjectSecurity.ExceptionFromErrorCode(EventWaitHandleSecurity._HandleErrorCode), null)
		{
		}

		// Token: 0x0600546B RID: 21611 RVA: 0x001322E0 File Offset: 0x001312E0
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

		// Token: 0x0600546C RID: 21612 RVA: 0x0013232E File Offset: 0x0013132E
		public override AccessRule AccessRuleFactory(IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type)
		{
			return new EventWaitHandleAccessRule(identityReference, accessMask, isInherited, inheritanceFlags, propagationFlags, type);
		}

		// Token: 0x0600546D RID: 21613 RVA: 0x0013233E File Offset: 0x0013133E
		public override AuditRule AuditRuleFactory(IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags)
		{
			return new EventWaitHandleAuditRule(identityReference, accessMask, isInherited, inheritanceFlags, propagationFlags, flags);
		}

		// Token: 0x0600546E RID: 21614 RVA: 0x00132350 File Offset: 0x00131350
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

		// Token: 0x0600546F RID: 21615 RVA: 0x00132390 File Offset: 0x00131390
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

		// Token: 0x06005470 RID: 21616 RVA: 0x001323F4 File Offset: 0x001313F4
		public void AddAccessRule(EventWaitHandleAccessRule rule)
		{
			base.AddAccessRule(rule);
		}

		// Token: 0x06005471 RID: 21617 RVA: 0x001323FD File Offset: 0x001313FD
		public void SetAccessRule(EventWaitHandleAccessRule rule)
		{
			base.SetAccessRule(rule);
		}

		// Token: 0x06005472 RID: 21618 RVA: 0x00132406 File Offset: 0x00131406
		public void ResetAccessRule(EventWaitHandleAccessRule rule)
		{
			base.ResetAccessRule(rule);
		}

		// Token: 0x06005473 RID: 21619 RVA: 0x0013240F File Offset: 0x0013140F
		public bool RemoveAccessRule(EventWaitHandleAccessRule rule)
		{
			return base.RemoveAccessRule(rule);
		}

		// Token: 0x06005474 RID: 21620 RVA: 0x00132418 File Offset: 0x00131418
		public void RemoveAccessRuleAll(EventWaitHandleAccessRule rule)
		{
			base.RemoveAccessRuleAll(rule);
		}

		// Token: 0x06005475 RID: 21621 RVA: 0x00132421 File Offset: 0x00131421
		public void RemoveAccessRuleSpecific(EventWaitHandleAccessRule rule)
		{
			base.RemoveAccessRuleSpecific(rule);
		}

		// Token: 0x06005476 RID: 21622 RVA: 0x0013242A File Offset: 0x0013142A
		public void AddAuditRule(EventWaitHandleAuditRule rule)
		{
			base.AddAuditRule(rule);
		}

		// Token: 0x06005477 RID: 21623 RVA: 0x00132433 File Offset: 0x00131433
		public void SetAuditRule(EventWaitHandleAuditRule rule)
		{
			base.SetAuditRule(rule);
		}

		// Token: 0x06005478 RID: 21624 RVA: 0x0013243C File Offset: 0x0013143C
		public bool RemoveAuditRule(EventWaitHandleAuditRule rule)
		{
			return base.RemoveAuditRule(rule);
		}

		// Token: 0x06005479 RID: 21625 RVA: 0x00132445 File Offset: 0x00131445
		public void RemoveAuditRuleAll(EventWaitHandleAuditRule rule)
		{
			base.RemoveAuditRuleAll(rule);
		}

		// Token: 0x0600547A RID: 21626 RVA: 0x0013244E File Offset: 0x0013144E
		public void RemoveAuditRuleSpecific(EventWaitHandleAuditRule rule)
		{
			base.RemoveAuditRuleSpecific(rule);
		}

		// Token: 0x17000E95 RID: 3733
		// (get) Token: 0x0600547B RID: 21627 RVA: 0x00132457 File Offset: 0x00131457
		public override Type AccessRightType
		{
			get
			{
				return typeof(EventWaitHandleRights);
			}
		}

		// Token: 0x17000E96 RID: 3734
		// (get) Token: 0x0600547C RID: 21628 RVA: 0x00132463 File Offset: 0x00131463
		public override Type AccessRuleType
		{
			get
			{
				return typeof(EventWaitHandleAccessRule);
			}
		}

		// Token: 0x17000E97 RID: 3735
		// (get) Token: 0x0600547D RID: 21629 RVA: 0x0013246F File Offset: 0x0013146F
		public override Type AuditRuleType
		{
			get
			{
				return typeof(EventWaitHandleAuditRule);
			}
		}
	}
}

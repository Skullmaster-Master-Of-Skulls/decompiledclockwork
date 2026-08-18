using System;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.Threading;
using Microsoft.Win32.SafeHandles;

namespace System.Security.AccessControl
{
	// Token: 0x0200048F RID: 1167
	[ComVisible(false)]
	public sealed class SemaphoreSecurity : NativeObjectSecurity
	{
		// Token: 0x06002B3F RID: 11071 RVA: 0x000C4D79 File Offset: 0x000C2F79
		public SemaphoreSecurity() : base(true, ResourceType.KernelObject)
		{
		}

		// Token: 0x06002B40 RID: 11072 RVA: 0x000C4D83 File Offset: 0x000C2F83
		public SemaphoreSecurity(string name, AccessControlSections includeSections) : base(true, ResourceType.KernelObject, name, includeSections, new NativeObjectSecurity.ExceptionFromErrorCode(SemaphoreSecurity._HandleErrorCode), null)
		{
		}

		// Token: 0x06002B41 RID: 11073 RVA: 0x000C4D9C File Offset: 0x000C2F9C
		internal SemaphoreSecurity(SafeWaitHandle handle, AccessControlSections includeSections) : base(true, ResourceType.KernelObject, handle, includeSections, new NativeObjectSecurity.ExceptionFromErrorCode(SemaphoreSecurity._HandleErrorCode), null)
		{
		}

		// Token: 0x06002B42 RID: 11074 RVA: 0x000C4DB8 File Offset: 0x000C2FB8
		private static Exception _HandleErrorCode(int errorCode, string name, SafeHandle handle, object context)
		{
			Exception result = null;
			if (errorCode == 2 || errorCode == 6 || errorCode == 123)
			{
				if (name != null && name.Length != 0)
				{
					result = new WaitHandleCannotBeOpenedException(SR.GetString("WaitHandleCannotBeOpenedException_InvalidHandle", new object[]
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

		// Token: 0x06002B43 RID: 11075 RVA: 0x000C4E02 File Offset: 0x000C3002
		public override AccessRule AccessRuleFactory(IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type)
		{
			return new SemaphoreAccessRule(identityReference, accessMask, isInherited, inheritanceFlags, propagationFlags, type);
		}

		// Token: 0x06002B44 RID: 11076 RVA: 0x000C4E12 File Offset: 0x000C3012
		public override AuditRule AuditRuleFactory(IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags)
		{
			return new SemaphoreAuditRule(identityReference, accessMask, isInherited, inheritanceFlags, propagationFlags, flags);
		}

		// Token: 0x06002B45 RID: 11077 RVA: 0x000C4E24 File Offset: 0x000C3024
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

		// Token: 0x06002B46 RID: 11078 RVA: 0x000C4E64 File Offset: 0x000C3064
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

		// Token: 0x06002B47 RID: 11079 RVA: 0x000C4EC8 File Offset: 0x000C30C8
		public void AddAccessRule(SemaphoreAccessRule rule)
		{
			base.AddAccessRule(rule);
		}

		// Token: 0x06002B48 RID: 11080 RVA: 0x000C4ED1 File Offset: 0x000C30D1
		public void SetAccessRule(SemaphoreAccessRule rule)
		{
			base.SetAccessRule(rule);
		}

		// Token: 0x06002B49 RID: 11081 RVA: 0x000C4EDA File Offset: 0x000C30DA
		public void ResetAccessRule(SemaphoreAccessRule rule)
		{
			base.ResetAccessRule(rule);
		}

		// Token: 0x06002B4A RID: 11082 RVA: 0x000C4EE3 File Offset: 0x000C30E3
		public bool RemoveAccessRule(SemaphoreAccessRule rule)
		{
			return base.RemoveAccessRule(rule);
		}

		// Token: 0x06002B4B RID: 11083 RVA: 0x000C4EEC File Offset: 0x000C30EC
		public void RemoveAccessRuleAll(SemaphoreAccessRule rule)
		{
			base.RemoveAccessRuleAll(rule);
		}

		// Token: 0x06002B4C RID: 11084 RVA: 0x000C4EF5 File Offset: 0x000C30F5
		public void RemoveAccessRuleSpecific(SemaphoreAccessRule rule)
		{
			base.RemoveAccessRuleSpecific(rule);
		}

		// Token: 0x06002B4D RID: 11085 RVA: 0x000C4EFE File Offset: 0x000C30FE
		public void AddAuditRule(SemaphoreAuditRule rule)
		{
			base.AddAuditRule(rule);
		}

		// Token: 0x06002B4E RID: 11086 RVA: 0x000C4F07 File Offset: 0x000C3107
		public void SetAuditRule(SemaphoreAuditRule rule)
		{
			base.SetAuditRule(rule);
		}

		// Token: 0x06002B4F RID: 11087 RVA: 0x000C4F10 File Offset: 0x000C3110
		public bool RemoveAuditRule(SemaphoreAuditRule rule)
		{
			return base.RemoveAuditRule(rule);
		}

		// Token: 0x06002B50 RID: 11088 RVA: 0x000C4F19 File Offset: 0x000C3119
		public void RemoveAuditRuleAll(SemaphoreAuditRule rule)
		{
			base.RemoveAuditRuleAll(rule);
		}

		// Token: 0x06002B51 RID: 11089 RVA: 0x000C4F22 File Offset: 0x000C3122
		public void RemoveAuditRuleSpecific(SemaphoreAuditRule rule)
		{
			base.RemoveAuditRuleSpecific(rule);
		}

		// Token: 0x17000A77 RID: 2679
		// (get) Token: 0x06002B52 RID: 11090 RVA: 0x000C4F2B File Offset: 0x000C312B
		public override Type AccessRightType
		{
			get
			{
				return typeof(SemaphoreRights);
			}
		}

		// Token: 0x17000A78 RID: 2680
		// (get) Token: 0x06002B53 RID: 11091 RVA: 0x000C4F37 File Offset: 0x000C3137
		public override Type AccessRuleType
		{
			get
			{
				return typeof(SemaphoreAccessRule);
			}
		}

		// Token: 0x17000A79 RID: 2681
		// (get) Token: 0x06002B54 RID: 11092 RVA: 0x000C4F43 File Offset: 0x000C3143
		public override Type AuditRuleType
		{
			get
			{
				return typeof(SemaphoreAuditRule);
			}
		}
	}
}

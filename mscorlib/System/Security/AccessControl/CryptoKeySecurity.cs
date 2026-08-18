using System;
using System.Runtime.CompilerServices;
using System.Security.Principal;

namespace System.Security.AccessControl
{
	// Token: 0x02000917 RID: 2327
	public sealed class CryptoKeySecurity : NativeObjectSecurity
	{
		// Token: 0x0600544E RID: 21582 RVA: 0x00132104 File Offset: 0x00131104
		public CryptoKeySecurity() : base(false, ResourceType.FileObject)
		{
		}

		// Token: 0x0600544F RID: 21583 RVA: 0x0013210E File Offset: 0x0013110E
		public CryptoKeySecurity(CommonSecurityDescriptor securityDescriptor) : base(ResourceType.FileObject, securityDescriptor)
		{
		}

		// Token: 0x06005450 RID: 21584 RVA: 0x00132118 File Offset: 0x00131118
		public sealed override AccessRule AccessRuleFactory(IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AccessControlType type)
		{
			return new CryptoKeyAccessRule(identityReference, CryptoKeyAccessRule.RightsFromAccessMask(accessMask), type);
		}

		// Token: 0x06005451 RID: 21585 RVA: 0x00132128 File Offset: 0x00131128
		public sealed override AuditRule AuditRuleFactory(IdentityReference identityReference, int accessMask, bool isInherited, InheritanceFlags inheritanceFlags, PropagationFlags propagationFlags, AuditFlags flags)
		{
			return new CryptoKeyAuditRule(identityReference, CryptoKeyAuditRule.RightsFromAccessMask(accessMask), flags);
		}

		// Token: 0x06005452 RID: 21586 RVA: 0x00132138 File Offset: 0x00131138
		public void AddAccessRule(CryptoKeyAccessRule rule)
		{
			base.AddAccessRule(rule);
		}

		// Token: 0x06005453 RID: 21587 RVA: 0x00132141 File Offset: 0x00131141
		public void SetAccessRule(CryptoKeyAccessRule rule)
		{
			base.SetAccessRule(rule);
		}

		// Token: 0x06005454 RID: 21588 RVA: 0x0013214A File Offset: 0x0013114A
		public void ResetAccessRule(CryptoKeyAccessRule rule)
		{
			base.ResetAccessRule(rule);
		}

		// Token: 0x06005455 RID: 21589 RVA: 0x00132153 File Offset: 0x00131153
		public bool RemoveAccessRule(CryptoKeyAccessRule rule)
		{
			return base.RemoveAccessRule(rule);
		}

		// Token: 0x06005456 RID: 21590 RVA: 0x0013215C File Offset: 0x0013115C
		public void RemoveAccessRuleAll(CryptoKeyAccessRule rule)
		{
			base.RemoveAccessRuleAll(rule);
		}

		// Token: 0x06005457 RID: 21591 RVA: 0x00132165 File Offset: 0x00131165
		public void RemoveAccessRuleSpecific(CryptoKeyAccessRule rule)
		{
			base.RemoveAccessRuleSpecific(rule);
		}

		// Token: 0x06005458 RID: 21592 RVA: 0x0013216E File Offset: 0x0013116E
		public void AddAuditRule(CryptoKeyAuditRule rule)
		{
			base.AddAuditRule(rule);
		}

		// Token: 0x06005459 RID: 21593 RVA: 0x00132177 File Offset: 0x00131177
		public void SetAuditRule(CryptoKeyAuditRule rule)
		{
			base.SetAuditRule(rule);
		}

		// Token: 0x0600545A RID: 21594 RVA: 0x00132180 File Offset: 0x00131180
		public bool RemoveAuditRule(CryptoKeyAuditRule rule)
		{
			return base.RemoveAuditRule(rule);
		}

		// Token: 0x0600545B RID: 21595 RVA: 0x00132189 File Offset: 0x00131189
		public void RemoveAuditRuleAll(CryptoKeyAuditRule rule)
		{
			base.RemoveAuditRuleAll(rule);
		}

		// Token: 0x0600545C RID: 21596 RVA: 0x00132192 File Offset: 0x00131192
		public void RemoveAuditRuleSpecific(CryptoKeyAuditRule rule)
		{
			base.RemoveAuditRuleSpecific(rule);
		}

		// Token: 0x17000E8F RID: 3727
		// (get) Token: 0x0600545D RID: 21597 RVA: 0x0013219B File Offset: 0x0013119B
		public override Type AccessRightType
		{
			get
			{
				return typeof(CryptoKeyRights);
			}
		}

		// Token: 0x17000E90 RID: 3728
		// (get) Token: 0x0600545E RID: 21598 RVA: 0x001321A7 File Offset: 0x001311A7
		public override Type AccessRuleType
		{
			get
			{
				return typeof(CryptoKeyAccessRule);
			}
		}

		// Token: 0x17000E91 RID: 3729
		// (get) Token: 0x0600545F RID: 21599 RVA: 0x001321B3 File Offset: 0x001311B3
		public override Type AuditRuleType
		{
			get
			{
				return typeof(CryptoKeyAuditRule);
			}
		}

		// Token: 0x17000E92 RID: 3730
		// (get) Token: 0x06005460 RID: 21600 RVA: 0x001321C0 File Offset: 0x001311C0
		internal AccessControlSections ChangedAccessControlSections
		{
			get
			{
				AccessControlSections accessControlSections = AccessControlSections.None;
				bool flag = false;
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
					RuntimeHelpers.PrepareConstrainedRegions();
					try
					{
					}
					finally
					{
						base.ReadLock();
						flag = true;
					}
					if (base.AccessRulesModified)
					{
						accessControlSections |= AccessControlSections.Access;
					}
					if (base.AuditRulesModified)
					{
						accessControlSections |= AccessControlSections.Audit;
					}
					if (base.GroupModified)
					{
						accessControlSections |= AccessControlSections.Group;
					}
					if (base.OwnerModified)
					{
						accessControlSections |= AccessControlSections.Owner;
					}
				}
				finally
				{
					if (flag)
					{
						base.ReadUnlock();
					}
				}
				return accessControlSections;
			}
		}

		// Token: 0x04002B9A RID: 11162
		private const ResourceType s_ResourceType = ResourceType.FileObject;
	}
}

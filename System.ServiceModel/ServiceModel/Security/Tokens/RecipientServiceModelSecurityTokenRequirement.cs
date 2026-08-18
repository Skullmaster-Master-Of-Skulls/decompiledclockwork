using System;

namespace System.ServiceModel.Security.Tokens
{
	// Token: 0x02000386 RID: 902
	public sealed class RecipientServiceModelSecurityTokenRequirement : ServiceModelSecurityTokenRequirement
	{
		// Token: 0x06002161 RID: 8545 RVA: 0x0007B92D File Offset: 0x00079B2D
		public RecipientServiceModelSecurityTokenRequirement()
		{
			base.Properties.Add(ServiceModelSecurityTokenRequirement.IsInitiatorProperty, false);
		}

		// Token: 0x17000805 RID: 2053
		// (get) Token: 0x06002162 RID: 8546 RVA: 0x0007B94B File Offset: 0x00079B4B
		// (set) Token: 0x06002163 RID: 8547 RVA: 0x0007B959 File Offset: 0x00079B59
		public Uri ListenUri
		{
			get
			{
				return base.GetPropertyOrDefault<Uri>(ServiceModelSecurityTokenRequirement.ListenUriProperty, null);
			}
			set
			{
				base.Properties[ServiceModelSecurityTokenRequirement.ListenUriProperty] = value;
			}
		}

		// Token: 0x17000806 RID: 2054
		// (get) Token: 0x06002164 RID: 8548 RVA: 0x0007B96C File Offset: 0x00079B6C
		// (set) Token: 0x06002165 RID: 8549 RVA: 0x0007B97A File Offset: 0x00079B7A
		public AuditLogLocation AuditLogLocation
		{
			get
			{
				return base.GetPropertyOrDefault<AuditLogLocation>(ServiceModelSecurityTokenRequirement.AuditLogLocationProperty, AuditLogLocation.Default);
			}
			set
			{
				base.Properties[ServiceModelSecurityTokenRequirement.AuditLogLocationProperty] = value;
			}
		}

		// Token: 0x17000807 RID: 2055
		// (get) Token: 0x06002166 RID: 8550 RVA: 0x0007B992 File Offset: 0x00079B92
		// (set) Token: 0x06002167 RID: 8551 RVA: 0x0007B9A0 File Offset: 0x00079BA0
		public bool SuppressAuditFailure
		{
			get
			{
				return base.GetPropertyOrDefault<bool>(ServiceModelSecurityTokenRequirement.SuppressAuditFailureProperty, true);
			}
			set
			{
				base.Properties[ServiceModelSecurityTokenRequirement.SuppressAuditFailureProperty] = value;
			}
		}

		// Token: 0x17000808 RID: 2056
		// (get) Token: 0x06002168 RID: 8552 RVA: 0x0007B9B8 File Offset: 0x00079BB8
		// (set) Token: 0x06002169 RID: 8553 RVA: 0x0007B9C6 File Offset: 0x00079BC6
		public AuditLevel MessageAuthenticationAuditLevel
		{
			get
			{
				return base.GetPropertyOrDefault<AuditLevel>(ServiceModelSecurityTokenRequirement.MessageAuthenticationAuditLevelProperty, AuditLevel.None);
			}
			set
			{
				base.Properties[ServiceModelSecurityTokenRequirement.MessageAuthenticationAuditLevelProperty] = value;
			}
		}

		// Token: 0x0600216A RID: 8554 RVA: 0x0007B9DE File Offset: 0x00079BDE
		public override string ToString()
		{
			return base.InternalToString();
		}
	}
}

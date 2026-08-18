using System;
using System.Collections.ObjectModel;
using System.IdentityModel.Tokens;
using System.Net;
using System.Security.Principal;

namespace System.ServiceModel.Security.Tokens
{
	// Token: 0x0200038D RID: 909
	public class SspiSecurityToken : SecurityToken
	{
		// Token: 0x060021BB RID: 8635 RVA: 0x0007BE9C File Offset: 0x0007A09C
		public SspiSecurityToken(TokenImpersonationLevel impersonationLevel, bool allowNtlm, NetworkCredential networkCredential)
		{
			this.impersonationLevel = impersonationLevel;
			this.allowNtlm = allowNtlm;
			this.networkCredential = SecurityUtils.GetNetworkCredentialsCopy(networkCredential);
			this.effectiveTime = DateTime.UtcNow;
			this.expirationTime = this.effectiveTime.AddHours(10.0);
		}

		// Token: 0x060021BC RID: 8636 RVA: 0x0007BEF0 File Offset: 0x0007A0F0
		public SspiSecurityToken(NetworkCredential networkCredential, bool extractGroupsForWindowsAccounts, bool allowUnauthenticatedCallers)
		{
			this.networkCredential = SecurityUtils.GetNetworkCredentialsCopy(networkCredential);
			this.extractGroupsForWindowsAccounts = extractGroupsForWindowsAccounts;
			this.allowUnauthenticatedCallers = allowUnauthenticatedCallers;
			this.effectiveTime = DateTime.UtcNow;
			this.expirationTime = this.effectiveTime.AddHours(10.0);
		}

		// Token: 0x1700083D RID: 2109
		// (get) Token: 0x060021BD RID: 8637 RVA: 0x0007BF44 File Offset: 0x0007A144
		public override string Id
		{
			get
			{
				if (this.id == null)
				{
					this.id = SecurityUniqueId.Create().Value;
				}
				return this.id;
			}
		}

		// Token: 0x1700083E RID: 2110
		// (get) Token: 0x060021BE RID: 8638 RVA: 0x0007BF72 File Offset: 0x0007A172
		public override DateTime ValidFrom
		{
			get
			{
				return this.effectiveTime;
			}
		}

		// Token: 0x1700083F RID: 2111
		// (get) Token: 0x060021BF RID: 8639 RVA: 0x0007BF7A File Offset: 0x0007A17A
		public override DateTime ValidTo
		{
			get
			{
				return this.expirationTime;
			}
		}

		// Token: 0x17000840 RID: 2112
		// (get) Token: 0x060021C0 RID: 8640 RVA: 0x0007BF82 File Offset: 0x0007A182
		public bool AllowUnauthenticatedCallers
		{
			get
			{
				return this.allowUnauthenticatedCallers;
			}
		}

		// Token: 0x17000841 RID: 2113
		// (get) Token: 0x060021C1 RID: 8641 RVA: 0x0007BF8A File Offset: 0x0007A18A
		public TokenImpersonationLevel ImpersonationLevel
		{
			get
			{
				return this.impersonationLevel;
			}
		}

		// Token: 0x17000842 RID: 2114
		// (get) Token: 0x060021C2 RID: 8642 RVA: 0x0007BF92 File Offset: 0x0007A192
		public bool AllowNtlm
		{
			get
			{
				return this.allowNtlm;
			}
		}

		// Token: 0x17000843 RID: 2115
		// (get) Token: 0x060021C3 RID: 8643 RVA: 0x0007BF9A File Offset: 0x0007A19A
		public NetworkCredential NetworkCredential
		{
			get
			{
				return this.networkCredential;
			}
		}

		// Token: 0x17000844 RID: 2116
		// (get) Token: 0x060021C4 RID: 8644 RVA: 0x0007BFA2 File Offset: 0x0007A1A2
		public bool ExtractGroupsForWindowsAccounts
		{
			get
			{
				return this.extractGroupsForWindowsAccounts;
			}
		}

		// Token: 0x17000845 RID: 2117
		// (get) Token: 0x060021C5 RID: 8645 RVA: 0x0007BFAA File Offset: 0x0007A1AA
		public override ReadOnlyCollection<SecurityKey> SecurityKeys
		{
			get
			{
				return EmptyReadOnlyCollection<SecurityKey>.Instance;
			}
		}

		// Token: 0x04001F7B RID: 8059
		private string id;

		// Token: 0x04001F7C RID: 8060
		private TokenImpersonationLevel impersonationLevel;

		// Token: 0x04001F7D RID: 8061
		private bool allowNtlm;

		// Token: 0x04001F7E RID: 8062
		private NetworkCredential networkCredential;

		// Token: 0x04001F7F RID: 8063
		private bool extractGroupsForWindowsAccounts;

		// Token: 0x04001F80 RID: 8064
		private bool allowUnauthenticatedCallers;

		// Token: 0x04001F81 RID: 8065
		private DateTime effectiveTime;

		// Token: 0x04001F82 RID: 8066
		private DateTime expirationTime;
	}
}

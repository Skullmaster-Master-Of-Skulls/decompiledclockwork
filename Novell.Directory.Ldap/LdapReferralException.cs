using System;
using Novell.Directory.Ldap.Utilclass;

namespace Novell.Directory.Ldap
{
	// Token: 0x0200003F RID: 63
	public class LdapReferralException : LdapException
	{
		// Token: 0x1700008C RID: 140
		// (get) Token: 0x0600026E RID: 622 RVA: 0x0000D058 File Offset: 0x0000C058
		// (set) Token: 0x0600026F RID: 623 RVA: 0x0000D070 File Offset: 0x0000C070
		public virtual string FailedReferral
		{
			get
			{
				return this.failedReferral;
			}
			set
			{
				this.failedReferral = value;
			}
		}

		// Token: 0x06000270 RID: 624 RVA: 0x0000D088 File Offset: 0x0000C088
		public LdapReferralException()
		{
		}

		// Token: 0x06000271 RID: 625 RVA: 0x0000D0AC File Offset: 0x0000C0AC
		public LdapReferralException(string message) : base(message, 10, null)
		{
		}

		// Token: 0x06000272 RID: 626 RVA: 0x0000D0D4 File Offset: 0x0000C0D4
		public LdapReferralException(string message, object[] arguments) : base(message, arguments, 10, null)
		{
		}

		// Token: 0x06000273 RID: 627 RVA: 0x0000D0FC File Offset: 0x0000C0FC
		public LdapReferralException(string message, Exception rootException) : base(message, 10, null, rootException)
		{
		}

		// Token: 0x06000274 RID: 628 RVA: 0x0000D124 File Offset: 0x0000C124
		public LdapReferralException(string message, object[] arguments, Exception rootException) : base(message, arguments, 10, null, rootException)
		{
		}

		// Token: 0x06000275 RID: 629 RVA: 0x0000D150 File Offset: 0x0000C150
		public LdapReferralException(string message, int resultCode, string serverMessage) : base(message, resultCode, serverMessage)
		{
		}

		// Token: 0x06000276 RID: 630 RVA: 0x0000D178 File Offset: 0x0000C178
		public LdapReferralException(string message, object[] arguments, int resultCode, string serverMessage) : base(message, arguments, resultCode, serverMessage)
		{
		}

		// Token: 0x06000277 RID: 631 RVA: 0x0000D1A0 File Offset: 0x0000C1A0
		public LdapReferralException(string message, int resultCode, string serverMessage, Exception rootException) : base(message, resultCode, serverMessage, rootException)
		{
		}

		// Token: 0x06000278 RID: 632 RVA: 0x0000D1C8 File Offset: 0x0000C1C8
		public LdapReferralException(string message, object[] arguments, int resultCode, string serverMessage, Exception rootException) : base(message, arguments, resultCode, serverMessage, rootException)
		{
		}

		// Token: 0x06000279 RID: 633 RVA: 0x0000D1F4 File Offset: 0x0000C1F4
		public virtual string[] getReferrals()
		{
			return this.referrals;
		}

		// Token: 0x0600027A RID: 634 RVA: 0x0000D20C File Offset: 0x0000C20C
		internal virtual void setReferrals(string[] urls)
		{
			this.referrals = urls;
		}

		// Token: 0x0600027B RID: 635 RVA: 0x0000D224 File Offset: 0x0000C224
		public override string ToString()
		{
			string text = this.getExceptionString("LdapReferralException");
			if (this.failedReferral != null)
			{
				string text2 = ResourcesHandler.getMessage("FAILED_REFERRAL", new object[]
				{
					"LdapReferralException",
					this.failedReferral
				});
				if (text2.ToUpper().Equals("SERVER_MSG".ToUpper()))
				{
					text2 = "LdapReferralException: Failed Referral: " + this.failedReferral;
				}
				text = text + '\n' + text2;
			}
			if (this.referrals != null)
			{
				for (int i = 0; i < this.referrals.Length; i++)
				{
					string text2 = ResourcesHandler.getMessage("REFERRAL_ITEM", new object[]
					{
						"LdapReferralException",
						this.referrals[i]
					});
					if (text2.ToUpper().Equals("SERVER_MSG".ToUpper()))
					{
						text2 = "LdapReferralException: Referral: " + this.referrals[i];
					}
					text = text + '\n' + text2;
				}
			}
			return text;
		}

		// Token: 0x04000125 RID: 293
		private string failedReferral = null;

		// Token: 0x04000126 RID: 294
		private string[] referrals = null;
	}
}

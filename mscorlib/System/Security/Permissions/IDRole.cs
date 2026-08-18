using System;
using System.Security.Principal;

namespace System.Security.Permissions
{
	// Token: 0x0200064E RID: 1614
	[Serializable]
	internal class IDRole
	{
		// Token: 0x170009D9 RID: 2521
		// (get) Token: 0x06003A29 RID: 14889 RVA: 0x000C30E0 File Offset: 0x000C20E0
		internal SecurityIdentifier Sid
		{
			get
			{
				if (string.IsNullOrEmpty(this.m_role))
				{
					return null;
				}
				if (this.m_sid == null)
				{
					NTAccount identity = new NTAccount(this.m_role);
					IdentityReferenceCollection identityReferenceCollection = NTAccount.Translate(new IdentityReferenceCollection(1)
					{
						identity
					}, typeof(SecurityIdentifier), false);
					this.m_sid = (identityReferenceCollection[0] as SecurityIdentifier);
				}
				return this.m_sid;
			}
		}

		// Token: 0x06003A2A RID: 14890 RVA: 0x000C3150 File Offset: 0x000C2150
		internal SecurityElement ToXml()
		{
			SecurityElement securityElement = new SecurityElement("Identity");
			if (this.m_authenticated)
			{
				securityElement.AddAttribute("Authenticated", "true");
			}
			if (this.m_id != null)
			{
				securityElement.AddAttribute("ID", SecurityElement.Escape(this.m_id));
			}
			if (this.m_role != null)
			{
				securityElement.AddAttribute("Role", SecurityElement.Escape(this.m_role));
			}
			return securityElement;
		}

		// Token: 0x06003A2B RID: 14891 RVA: 0x000C31C0 File Offset: 0x000C21C0
		internal void FromXml(SecurityElement e)
		{
			string text = e.Attribute("Authenticated");
			if (text != null)
			{
				this.m_authenticated = (string.Compare(text, "true", StringComparison.OrdinalIgnoreCase) == 0);
			}
			else
			{
				this.m_authenticated = false;
			}
			string text2 = e.Attribute("ID");
			if (text2 != null)
			{
				this.m_id = text2;
			}
			else
			{
				this.m_id = null;
			}
			string text3 = e.Attribute("Role");
			if (text3 != null)
			{
				this.m_role = text3;
				return;
			}
			this.m_role = null;
		}

		// Token: 0x06003A2C RID: 14892 RVA: 0x000C3237 File Offset: 0x000C2237
		public override int GetHashCode()
		{
			return (this.m_authenticated ? 0 : 101) + ((this.m_id == null) ? 0 : this.m_id.GetHashCode()) + ((this.m_role == null) ? 0 : this.m_role.GetHashCode());
		}

		// Token: 0x04001E2F RID: 7727
		internal bool m_authenticated;

		// Token: 0x04001E30 RID: 7728
		internal string m_id;

		// Token: 0x04001E31 RID: 7729
		internal string m_role;

		// Token: 0x04001E32 RID: 7730
		[NonSerialized]
		private SecurityIdentifier m_sid;
	}
}

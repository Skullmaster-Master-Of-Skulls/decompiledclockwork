using System;
using System.Security.Cryptography.X509Certificates;

namespace System.Security.Cryptography
{
	// Token: 0x0200045E RID: 1118
	public sealed class Oid
	{
		// Token: 0x0600298C RID: 10636 RVA: 0x000BCAC4 File Offset: 0x000BACC4
		public Oid()
		{
		}

		// Token: 0x0600298D RID: 10637 RVA: 0x000BCACC File Offset: 0x000BACCC
		public Oid(string oid) : this(oid, OidGroup.All, true)
		{
		}

		// Token: 0x0600298E RID: 10638 RVA: 0x000BCAD8 File Offset: 0x000BACD8
		internal Oid(string oid, OidGroup group, bool lookupFriendlyName)
		{
			if (lookupFriendlyName)
			{
				string text = X509Utils.FindOidInfoWithFallback(2U, oid, group);
				if (text == null)
				{
					text = oid;
				}
				this.Value = text;
			}
			else
			{
				this.Value = oid;
			}
			this.m_group = group;
		}

		// Token: 0x0600298F RID: 10639 RVA: 0x000BCB13 File Offset: 0x000BAD13
		public Oid(string value, string friendlyName)
		{
			this.m_value = value;
			this.m_friendlyName = friendlyName;
		}

		// Token: 0x06002990 RID: 10640 RVA: 0x000BCB29 File Offset: 0x000BAD29
		public Oid(Oid oid)
		{
			if (oid == null)
			{
				throw new ArgumentNullException("oid");
			}
			this.m_value = oid.m_value;
			this.m_friendlyName = oid.m_friendlyName;
			this.m_group = oid.m_group;
		}

		// Token: 0x06002991 RID: 10641 RVA: 0x000BCB63 File Offset: 0x000BAD63
		private Oid(string value, string friendlyName, OidGroup group)
		{
			this.m_value = value;
			this.m_friendlyName = friendlyName;
			this.m_group = group;
		}

		// Token: 0x06002992 RID: 10642 RVA: 0x000BCB80 File Offset: 0x000BAD80
		public static Oid FromFriendlyName(string friendlyName, OidGroup group)
		{
			if (friendlyName == null)
			{
				throw new ArgumentNullException("friendlyName");
			}
			string text = X509Utils.FindOidInfo(2U, friendlyName, group);
			if (text == null)
			{
				throw new CryptographicException(SR.GetString("Cryptography_Oid_InvalidValue"));
			}
			return new Oid(text, friendlyName, group);
		}

		// Token: 0x06002993 RID: 10643 RVA: 0x000BCBC0 File Offset: 0x000BADC0
		public static Oid FromOidValue(string oidValue, OidGroup group)
		{
			if (oidValue == null)
			{
				throw new ArgumentNullException("oidValue");
			}
			string text = X509Utils.FindOidInfo(1U, oidValue, group);
			if (text == null)
			{
				throw new CryptographicException(SR.GetString("Cryptography_Oid_InvalidValue"));
			}
			return new Oid(oidValue, text, group);
		}

		// Token: 0x17000A15 RID: 2581
		// (get) Token: 0x06002994 RID: 10644 RVA: 0x000BCBFF File Offset: 0x000BADFF
		// (set) Token: 0x06002995 RID: 10645 RVA: 0x000BCC07 File Offset: 0x000BAE07
		public string Value
		{
			get
			{
				return this.m_value;
			}
			set
			{
				this.m_value = value;
			}
		}

		// Token: 0x17000A16 RID: 2582
		// (get) Token: 0x06002996 RID: 10646 RVA: 0x000BCC10 File Offset: 0x000BAE10
		// (set) Token: 0x06002997 RID: 10647 RVA: 0x000BCC40 File Offset: 0x000BAE40
		public string FriendlyName
		{
			get
			{
				if (this.m_friendlyName == null && this.m_value != null)
				{
					this.m_friendlyName = X509Utils.FindOidInfoWithFallback(1U, this.m_value, this.m_group);
				}
				return this.m_friendlyName;
			}
			set
			{
				this.m_friendlyName = value;
				if (this.m_friendlyName != null)
				{
					string text = X509Utils.FindOidInfoWithFallback(2U, this.m_friendlyName, this.m_group);
					if (text != null)
					{
						this.m_value = text;
					}
				}
			}
		}

		// Token: 0x0400259E RID: 9630
		private string m_value;

		// Token: 0x0400259F RID: 9631
		private string m_friendlyName;

		// Token: 0x040025A0 RID: 9632
		private OidGroup m_group;
	}
}

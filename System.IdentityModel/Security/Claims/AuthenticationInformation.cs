using System;
using System.Collections.ObjectModel;
using System.IdentityModel.Tokens;

namespace System.Security.Claims
{
	// Token: 0x02000018 RID: 24
	public class AuthenticationInformation
	{
		// Token: 0x060000BB RID: 187 RVA: 0x00003FFE File Offset: 0x000021FE
		public AuthenticationInformation()
		{
			this._authContexts = new Collection<AuthenticationContext>();
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x060000BC RID: 188 RVA: 0x00004011 File Offset: 0x00002211
		// (set) Token: 0x060000BD RID: 189 RVA: 0x00004019 File Offset: 0x00002219
		public string Address
		{
			get
			{
				return this._address;
			}
			set
			{
				this._address = value;
			}
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000BE RID: 190 RVA: 0x00004022 File Offset: 0x00002222
		public Collection<AuthenticationContext> AuthorizationContexts
		{
			get
			{
				return this._authContexts;
			}
		}

		// Token: 0x17000034 RID: 52
		// (get) Token: 0x060000BF RID: 191 RVA: 0x0000402A File Offset: 0x0000222A
		// (set) Token: 0x060000C0 RID: 192 RVA: 0x00004032 File Offset: 0x00002232
		public string DnsName
		{
			get
			{
				return this._dnsName;
			}
			set
			{
				this._dnsName = value;
			}
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000C1 RID: 193 RVA: 0x0000403B File Offset: 0x0000223B
		// (set) Token: 0x060000C2 RID: 194 RVA: 0x00004043 File Offset: 0x00002243
		public DateTime? NotOnOrAfter
		{
			get
			{
				return this._notOnOrAfter;
			}
			set
			{
				this._notOnOrAfter = value;
			}
		}

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000C3 RID: 195 RVA: 0x0000404C File Offset: 0x0000224C
		// (set) Token: 0x060000C4 RID: 196 RVA: 0x00004054 File Offset: 0x00002254
		public string Session
		{
			get
			{
				return this._session;
			}
			set
			{
				this._session = value;
			}
		}

		// Token: 0x040000AD RID: 173
		private string _address;

		// Token: 0x040000AE RID: 174
		private Collection<AuthenticationContext> _authContexts;

		// Token: 0x040000AF RID: 175
		private string _dnsName;

		// Token: 0x040000B0 RID: 176
		private DateTime? _notOnOrAfter;

		// Token: 0x040000B1 RID: 177
		private string _session;
	}
}

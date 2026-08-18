using System;
using System.IdentityModel.Tokens;

namespace System.IdentityModel.Protocols.WSTrust
{
	// Token: 0x02000203 RID: 515
	public class UseKey
	{
		// Token: 0x06001112 RID: 4370 RVA: 0x00004469 File Offset: 0x00002669
		public UseKey()
		{
		}

		// Token: 0x06001113 RID: 4371 RVA: 0x00047964 File Offset: 0x00045B64
		public UseKey(SecurityKeyIdentifier ski) : this(ski, null)
		{
		}

		// Token: 0x06001114 RID: 4372 RVA: 0x0004796E File Offset: 0x00045B6E
		public UseKey(SecurityToken token) : this(null, token)
		{
		}

		// Token: 0x06001115 RID: 4373 RVA: 0x00047978 File Offset: 0x00045B78
		public UseKey(SecurityKeyIdentifier ski, SecurityToken token)
		{
			this._ski = ski;
			this._token = token;
		}

		// Token: 0x170004CC RID: 1228
		// (get) Token: 0x06001116 RID: 4374 RVA: 0x0004798E File Offset: 0x00045B8E
		public SecurityToken Token
		{
			get
			{
				return this._token;
			}
		}

		// Token: 0x170004CD RID: 1229
		// (get) Token: 0x06001117 RID: 4375 RVA: 0x00047996 File Offset: 0x00045B96
		public SecurityKeyIdentifier SecurityKeyIdentifier
		{
			get
			{
				return this._ski;
			}
		}

		// Token: 0x04000EA2 RID: 3746
		private SecurityToken _token;

		// Token: 0x04000EA3 RID: 3747
		private SecurityKeyIdentifier _ski;
	}
}

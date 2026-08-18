using System;
using System.IdentityModel.Tokens;

namespace System.IdentityModel.Protocols.WSTrust
{
	// Token: 0x020001F4 RID: 500
	internal class FederatedClientCredentialsParameters
	{
		// Token: 0x170004A0 RID: 1184
		// (get) Token: 0x060010A5 RID: 4261 RVA: 0x000472F9 File Offset: 0x000454F9
		// (set) Token: 0x060010A6 RID: 4262 RVA: 0x00047301 File Offset: 0x00045501
		public SecurityToken ActAs
		{
			get
			{
				return this._actAs;
			}
			set
			{
				this._actAs = value;
			}
		}

		// Token: 0x170004A1 RID: 1185
		// (get) Token: 0x060010A7 RID: 4263 RVA: 0x0004730A File Offset: 0x0004550A
		// (set) Token: 0x060010A8 RID: 4264 RVA: 0x00047312 File Offset: 0x00045512
		public SecurityToken OnBehalfOf
		{
			get
			{
				return this._onBehalfOf;
			}
			set
			{
				this._onBehalfOf = value;
			}
		}

		// Token: 0x170004A2 RID: 1186
		// (get) Token: 0x060010A9 RID: 4265 RVA: 0x0004731B File Offset: 0x0004551B
		// (set) Token: 0x060010AA RID: 4266 RVA: 0x00047323 File Offset: 0x00045523
		public SecurityToken IssuedSecurityToken
		{
			get
			{
				return this._issuedSecurityToken;
			}
			set
			{
				this._issuedSecurityToken = value;
			}
		}

		// Token: 0x04000E6C RID: 3692
		private SecurityToken _actAs;

		// Token: 0x04000E6D RID: 3693
		private SecurityToken _onBehalfOf;

		// Token: 0x04000E6E RID: 3694
		private SecurityToken _issuedSecurityToken;
	}
}

using System;

namespace System.IdentityModel.Protocols.WSTrust
{
	// Token: 0x020001FB RID: 507
	public class RequestClaim
	{
		// Token: 0x060010C4 RID: 4292 RVA: 0x00047491 File Offset: 0x00045691
		public RequestClaim(string claimType) : this(claimType, false)
		{
		}

		// Token: 0x060010C5 RID: 4293 RVA: 0x0004749B File Offset: 0x0004569B
		public RequestClaim(string claimType, bool isOptional) : this(claimType, isOptional, null)
		{
		}

		// Token: 0x060010C6 RID: 4294 RVA: 0x000474A8 File Offset: 0x000456A8
		public RequestClaim(string claimType, bool isOptional, string value)
		{
			if (string.IsNullOrEmpty(claimType))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("ID0006"), "claimType"));
			}
			this._claimType = claimType;
			this._isOptional = isOptional;
			this._value = value;
		}

		// Token: 0x170004AA RID: 1194
		// (get) Token: 0x060010C7 RID: 4295 RVA: 0x000474F7 File Offset: 0x000456F7
		public string ClaimType
		{
			get
			{
				return this._claimType;
			}
		}

		// Token: 0x170004AB RID: 1195
		// (get) Token: 0x060010C8 RID: 4296 RVA: 0x000474FF File Offset: 0x000456FF
		// (set) Token: 0x060010C9 RID: 4297 RVA: 0x00047507 File Offset: 0x00045707
		public bool IsOptional
		{
			get
			{
				return this._isOptional;
			}
			set
			{
				this._isOptional = value;
			}
		}

		// Token: 0x170004AC RID: 1196
		// (get) Token: 0x060010CA RID: 4298 RVA: 0x00047510 File Offset: 0x00045710
		// (set) Token: 0x060010CB RID: 4299 RVA: 0x00047518 File Offset: 0x00045718
		public string Value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = value;
			}
		}

		// Token: 0x04000E7A RID: 3706
		private string _claimType;

		// Token: 0x04000E7B RID: 3707
		private bool _isOptional;

		// Token: 0x04000E7C RID: 3708
		private string _value;
	}
}

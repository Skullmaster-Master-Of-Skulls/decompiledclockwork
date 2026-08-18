using System;

namespace System.IdentityModel.Protocols.WSTrust
{
	// Token: 0x020001FA RID: 506
	public class Renewing
	{
		// Token: 0x060010BE RID: 4286 RVA: 0x00047443 File Offset: 0x00045643
		public Renewing()
		{
		}

		// Token: 0x060010BF RID: 4287 RVA: 0x00047452 File Offset: 0x00045652
		public Renewing(bool allowRenewal, bool okForRenewalAfterExpiration)
		{
			this._allowRenewal = allowRenewal;
			this._okForRenewalAfterExpiration = okForRenewalAfterExpiration;
		}

		// Token: 0x170004A8 RID: 1192
		// (get) Token: 0x060010C0 RID: 4288 RVA: 0x0004746F File Offset: 0x0004566F
		// (set) Token: 0x060010C1 RID: 4289 RVA: 0x00047477 File Offset: 0x00045677
		public bool AllowRenewal
		{
			get
			{
				return this._allowRenewal;
			}
			set
			{
				this._allowRenewal = value;
			}
		}

		// Token: 0x170004A9 RID: 1193
		// (get) Token: 0x060010C2 RID: 4290 RVA: 0x00047480 File Offset: 0x00045680
		// (set) Token: 0x060010C3 RID: 4291 RVA: 0x00047488 File Offset: 0x00045688
		public bool OkForRenewalAfterExpiration
		{
			get
			{
				return this._okForRenewalAfterExpiration;
			}
			set
			{
				this._okForRenewalAfterExpiration = value;
			}
		}

		// Token: 0x04000E78 RID: 3704
		private bool _allowRenewal = true;

		// Token: 0x04000E79 RID: 3705
		private bool _okForRenewalAfterExpiration;
	}
}

using System;
using System.IdentityModel.Tokens;
using System.ServiceModel.Diagnostics;

namespace System.ServiceModel.Security
{
	// Token: 0x020002C1 RID: 705
	internal class SecurityProtocolCorrelationState
	{
		// Token: 0x06001685 RID: 5765 RVA: 0x00055C04 File Offset: 0x00053E04
		public SecurityProtocolCorrelationState(SecurityToken token)
		{
			this.token = token;
			this.activity = (DiagnosticUtility.ShouldUseActivity ? ServiceModelActivity.Current : null);
		}

		// Token: 0x1700050E RID: 1294
		// (get) Token: 0x06001686 RID: 5766 RVA: 0x00055C28 File Offset: 0x00053E28
		public SecurityToken Token
		{
			get
			{
				return this.token;
			}
		}

		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x06001687 RID: 5767 RVA: 0x00055C30 File Offset: 0x00053E30
		// (set) Token: 0x06001688 RID: 5768 RVA: 0x00055C38 File Offset: 0x00053E38
		internal SignatureConfirmations SignatureConfirmations
		{
			get
			{
				return this.signatureConfirmations;
			}
			set
			{
				this.signatureConfirmations = value;
			}
		}

		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x06001689 RID: 5769 RVA: 0x00055C41 File Offset: 0x00053E41
		internal ServiceModelActivity Activity
		{
			get
			{
				return this.activity;
			}
		}

		// Token: 0x04001BC2 RID: 7106
		private SecurityToken token;

		// Token: 0x04001BC3 RID: 7107
		private SignatureConfirmations signatureConfirmations;

		// Token: 0x04001BC4 RID: 7108
		private ServiceModelActivity activity;
	}
}

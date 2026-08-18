using System;
using System.ComponentModel;
using System.ServiceModel.Channels;

namespace System.ServiceModel.MsmqIntegration
{
	// Token: 0x020003B7 RID: 951
	public sealed class MsmqIntegrationSecurity
	{
		// Token: 0x060023A6 RID: 9126 RVA: 0x0008227B File Offset: 0x0008047B
		public MsmqIntegrationSecurity()
		{
			this.mode = MsmqIntegrationSecurityMode.Transport;
			this.transportSecurity = new MsmqTransportSecurity();
		}

		// Token: 0x170008E0 RID: 2272
		// (get) Token: 0x060023A7 RID: 9127 RVA: 0x00082295 File Offset: 0x00080495
		// (set) Token: 0x060023A8 RID: 9128 RVA: 0x0008229D File Offset: 0x0008049D
		[DefaultValue(MsmqIntegrationSecurityMode.Transport)]
		public MsmqIntegrationSecurityMode Mode
		{
			get
			{
				return this.mode;
			}
			set
			{
				if (!MsmqIntegrationSecurityModeHelper.IsDefined(value))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value"));
				}
				this.mode = value;
			}
		}

		// Token: 0x170008E1 RID: 2273
		// (get) Token: 0x060023A9 RID: 9129 RVA: 0x000822C3 File Offset: 0x000804C3
		// (set) Token: 0x060023AA RID: 9130 RVA: 0x000822CB File Offset: 0x000804CB
		public MsmqTransportSecurity Transport
		{
			get
			{
				return this.transportSecurity;
			}
			set
			{
				this.transportSecurity = value;
			}
		}

		// Token: 0x060023AB RID: 9131 RVA: 0x000822D4 File Offset: 0x000804D4
		internal void ConfigureTransportSecurity(MsmqBindingElementBase msmq)
		{
			if (this.mode == MsmqIntegrationSecurityMode.Transport)
			{
				msmq.MsmqTransportSecurity = this.Transport;
				return;
			}
			msmq.MsmqTransportSecurity.Disable();
		}

		// Token: 0x0400201E RID: 8222
		internal const MsmqIntegrationSecurityMode DefaultMode = MsmqIntegrationSecurityMode.Transport;

		// Token: 0x0400201F RID: 8223
		private MsmqIntegrationSecurityMode mode;

		// Token: 0x04002020 RID: 8224
		private MsmqTransportSecurity transportSecurity;
	}
}

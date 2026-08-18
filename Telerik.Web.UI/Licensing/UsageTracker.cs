using System;

namespace Telerik.Licensing
{
	// Token: 0x0200042E RID: 1070
	internal class UsageTracker : IUsageTracker
	{
		// Token: 0x06002673 RID: 9843 RVA: 0x0007DED5 File Offset: 0x0007C0D5
		public UsageTracker(ILicenseProvider provider, ITransportService transportService)
		{
			this.LicenseProvider = provider;
			this.TransportService = transportService;
		}

		// Token: 0x17000C5A RID: 3162
		// (get) Token: 0x06002674 RID: 9844 RVA: 0x0007DEEB File Offset: 0x0007C0EB
		// (set) Token: 0x06002675 RID: 9845 RVA: 0x0007DEF3 File Offset: 0x0007C0F3
		private ITransportService TransportService { get; set; }

		// Token: 0x17000C5B RID: 3163
		// (get) Token: 0x06002676 RID: 9846 RVA: 0x0007DEFC File Offset: 0x0007C0FC
		// (set) Token: 0x06002677 RID: 9847 RVA: 0x0007DF04 File Offset: 0x0007C104
		private ILicenseProvider LicenseProvider { get; set; }

		// Token: 0x06002678 RID: 9848 RVA: 0x0007DF0D File Offset: 0x0007C10D
		public virtual void Track(RequestPayload payload)
		{
			this.TransportService.CallHome(payload);
		}

		// Token: 0x06002679 RID: 9849 RVA: 0x0007DF1B File Offset: 0x0007C11B
		public void StartTracking()
		{
			if (!this._isTracking)
			{
				this.LicenseProvider.ProductUsed += this.ProductUsed;
				this.LicenseProvider.ComponentUsed += this.ComponentUsed;
				this._isTracking = true;
			}
		}

		// Token: 0x0600267A RID: 9850 RVA: 0x0007DF5A File Offset: 0x0007C15A
		public bool IsTracking()
		{
			return this._isTracking;
		}

		// Token: 0x0600267B RID: 9851 RVA: 0x0007DF62 File Offset: 0x0007C162
		public void StopTracking()
		{
			this._isTracking = false;
			this.LicenseProvider.ProductUsed -= this.ProductUsed;
			this.LicenseProvider.ComponentUsed -= this.ComponentUsed;
		}

		// Token: 0x0600267C RID: 9852 RVA: 0x0007DF99 File Offset: 0x0007C199
		private void ProductUsed(object sender, ProductUsedEventArgs e)
		{
			this.Track(new ProductUsedPayload(e.Type, UniqueMachineId.GetIdWithDefaultHash(), e.SessionId));
		}

		// Token: 0x0600267D RID: 9853 RVA: 0x0007DFB7 File Offset: 0x0007C1B7
		private void ComponentUsed(object sender, ComponentUsedEventArgs e)
		{
			this.Track(new ComponentUsedPayload(e.Type, UniqueMachineId.GetIdWithDefaultHash(), e.SessionId));
		}

		// Token: 0x040009D2 RID: 2514
		private bool _isTracking;
	}
}

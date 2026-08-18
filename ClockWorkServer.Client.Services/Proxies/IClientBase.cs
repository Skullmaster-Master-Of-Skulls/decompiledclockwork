using System;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x020000A4 RID: 164
	public interface IClientBase : IDisposable
	{
		// Token: 0x14000004 RID: 4
		// (add) Token: 0x06000695 RID: 1685
		// (remove) Token: 0x06000696 RID: 1686
		event ProxyCreatedHandler ProxyCreated;

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000697 RID: 1687
		ClientCredentials ClientCredentials { get; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000698 RID: 1688
		ServiceEndpoint Endpoint { get; }

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000699 RID: 1689
		ServiceEndpoint CurrentEndpoint { get; }

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600069A RID: 1690
		IClientChannel InnerChannel { get; }

		// Token: 0x1700000E RID: 14
		// (get) Token: 0x0600069B RID: 1691
		CommunicationState State { get; }

		// Token: 0x0600069C RID: 1692
		void Abort();

		// Token: 0x0600069D RID: 1693
		void Close();

		// Token: 0x0600069E RID: 1694
		void Open();

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x0600069F RID: 1695
		// (set) Token: 0x060006A0 RID: 1696
		object Tag { get; set; }
	}
}

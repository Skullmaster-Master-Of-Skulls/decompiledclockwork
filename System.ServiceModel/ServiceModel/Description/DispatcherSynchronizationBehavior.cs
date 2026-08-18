using System;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;

namespace System.ServiceModel.Description
{
	// Token: 0x020003F9 RID: 1017
	public class DispatcherSynchronizationBehavior : IEndpointBehavior
	{
		// Token: 0x06002687 RID: 9863 RVA: 0x0008AB8B File Offset: 0x00088D8B
		public DispatcherSynchronizationBehavior() : this(false, 1)
		{
		}

		// Token: 0x06002688 RID: 9864 RVA: 0x0008AB95 File Offset: 0x00088D95
		public DispatcherSynchronizationBehavior(bool asynchronousSendEnabled, int maxPendingReceives)
		{
			this.AsynchronousSendEnabled = asynchronousSendEnabled;
			this.MaxPendingReceives = maxPendingReceives;
		}

		// Token: 0x170009CA RID: 2506
		// (get) Token: 0x06002689 RID: 9865 RVA: 0x0008ABAB File Offset: 0x00088DAB
		// (set) Token: 0x0600268A RID: 9866 RVA: 0x0008ABB3 File Offset: 0x00088DB3
		public bool AsynchronousSendEnabled { get; set; }

		// Token: 0x170009CB RID: 2507
		// (get) Token: 0x0600268B RID: 9867 RVA: 0x0008ABBC File Offset: 0x00088DBC
		// (set) Token: 0x0600268C RID: 9868 RVA: 0x0008ABC4 File Offset: 0x00088DC4
		public int MaxPendingReceives { get; set; }

		// Token: 0x0600268D RID: 9869 RVA: 0x0008ABCD File Offset: 0x00088DCD
		void IEndpointBehavior.Validate(ServiceEndpoint serviceEndpoint)
		{
		}

		// Token: 0x0600268E RID: 9870 RVA: 0x0008ABCF File Offset: 0x00088DCF
		void IEndpointBehavior.AddBindingParameters(ServiceEndpoint serviceEndpoint, BindingParameterCollection parameters)
		{
		}

		// Token: 0x0600268F RID: 9871 RVA: 0x0008ABD1 File Offset: 0x00088DD1
		void IEndpointBehavior.ApplyDispatchBehavior(ServiceEndpoint serviceEndpoint, EndpointDispatcher endpointDispatcher)
		{
			if (endpointDispatcher == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("endpointDispatcher");
			}
			endpointDispatcher.ChannelDispatcher.SendAsynchronously = this.AsynchronousSendEnabled;
			endpointDispatcher.ChannelDispatcher.MaxPendingReceives = this.MaxPendingReceives;
		}

		// Token: 0x06002690 RID: 9872 RVA: 0x0008AC08 File Offset: 0x00088E08
		void IEndpointBehavior.ApplyClientBehavior(ServiceEndpoint serviceEndpoint, ClientRuntime behavior)
		{
		}
	}
}

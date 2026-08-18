using System;
using System.ServiceModel.Description;

namespace System.ServiceModel
{
	// Token: 0x020000F1 RID: 241
	internal sealed class ServiceEndpointTrait<TChannel> : EndpointTrait<TChannel> where TChannel : class
	{
		// Token: 0x0600050A RID: 1290 RVA: 0x00017F65 File Offset: 0x00016165
		public ServiceEndpointTrait(ServiceEndpoint endpoint, InstanceContext callbackInstance)
		{
			this.endpoint = endpoint;
			this.callbackInstance = callbackInstance;
		}

		// Token: 0x0600050B RID: 1291 RVA: 0x00017F7C File Offset: 0x0001617C
		public override bool Equals(object obj)
		{
			ServiceEndpointTrait<TChannel> serviceEndpointTrait = obj as ServiceEndpointTrait<TChannel>;
			return serviceEndpointTrait != null && this.callbackInstance == serviceEndpointTrait.callbackInstance && this.endpoint == serviceEndpointTrait.endpoint;
		}

		// Token: 0x0600050C RID: 1292 RVA: 0x00017FB8 File Offset: 0x000161B8
		public override int GetHashCode()
		{
			int num = 0;
			if (this.callbackInstance != null)
			{
				num ^= this.callbackInstance.GetHashCode();
			}
			return num ^ this.endpoint.GetHashCode();
		}

		// Token: 0x0600050D RID: 1293 RVA: 0x00017FEC File Offset: 0x000161EC
		public override ChannelFactory<TChannel> CreateChannelFactory()
		{
			if (this.callbackInstance != null)
			{
				return this.CreateDuplexFactory();
			}
			return this.CreateSimplexFactory();
		}

		// Token: 0x0600050E RID: 1294 RVA: 0x00018003 File Offset: 0x00016203
		private DuplexChannelFactory<TChannel> CreateDuplexFactory()
		{
			return new DuplexChannelFactory<TChannel>(this.callbackInstance, this.endpoint);
		}

		// Token: 0x0600050F RID: 1295 RVA: 0x00018016 File Offset: 0x00016216
		private ChannelFactory<TChannel> CreateSimplexFactory()
		{
			return new ChannelFactory<TChannel>(this.endpoint);
		}

		// Token: 0x04000A2C RID: 2604
		private InstanceContext callbackInstance;

		// Token: 0x04000A2D RID: 2605
		private ServiceEndpoint endpoint;
	}
}

using System;

namespace System.ServiceModel
{
	// Token: 0x020000EF RID: 239
	internal sealed class ConfigurationEndpointTrait<TChannel> : EndpointTrait<TChannel> where TChannel : class
	{
		// Token: 0x060004FE RID: 1278 RVA: 0x00017D31 File Offset: 0x00015F31
		public ConfigurationEndpointTrait(string endpointConfigurationName, EndpointAddress remoteAddress, InstanceContext callbackInstance)
		{
			this.endpointConfigurationName = endpointConfigurationName;
			this.remoteAddress = remoteAddress;
			this.callbackInstance = callbackInstance;
		}

		// Token: 0x060004FF RID: 1279 RVA: 0x00017D50 File Offset: 0x00015F50
		public override bool Equals(object obj)
		{
			ConfigurationEndpointTrait<TChannel> configurationEndpointTrait = obj as ConfigurationEndpointTrait<TChannel>;
			return configurationEndpointTrait != null && this.callbackInstance == configurationEndpointTrait.callbackInstance && string.CompareOrdinal(this.endpointConfigurationName, configurationEndpointTrait.endpointConfigurationName) == 0 && !(this.remoteAddress != configurationEndpointTrait.remoteAddress);
		}

		// Token: 0x06000500 RID: 1280 RVA: 0x00017DA4 File Offset: 0x00015FA4
		public override int GetHashCode()
		{
			int num = 0;
			if (this.callbackInstance != null)
			{
				num ^= this.callbackInstance.GetHashCode();
			}
			num ^= this.endpointConfigurationName.GetHashCode();
			if (this.remoteAddress != null)
			{
				num ^= this.remoteAddress.GetHashCode();
			}
			return num;
		}

		// Token: 0x06000501 RID: 1281 RVA: 0x00017DF4 File Offset: 0x00015FF4
		public override ChannelFactory<TChannel> CreateChannelFactory()
		{
			if (this.callbackInstance != null)
			{
				return this.CreateDuplexFactory();
			}
			return this.CreateSimplexFactory();
		}

		// Token: 0x06000502 RID: 1282 RVA: 0x00017E0B File Offset: 0x0001600B
		private DuplexChannelFactory<TChannel> CreateDuplexFactory()
		{
			if (this.remoteAddress != null)
			{
				return new DuplexChannelFactory<TChannel>(this.callbackInstance, this.endpointConfigurationName, this.remoteAddress);
			}
			return new DuplexChannelFactory<TChannel>(this.callbackInstance, this.endpointConfigurationName);
		}

		// Token: 0x06000503 RID: 1283 RVA: 0x00017E44 File Offset: 0x00016044
		private ChannelFactory<TChannel> CreateSimplexFactory()
		{
			if (this.remoteAddress != null)
			{
				return new ChannelFactory<TChannel>(this.endpointConfigurationName, this.remoteAddress);
			}
			return new ChannelFactory<TChannel>(this.endpointConfigurationName);
		}

		// Token: 0x04000A26 RID: 2598
		private string endpointConfigurationName;

		// Token: 0x04000A27 RID: 2599
		private EndpointAddress remoteAddress;

		// Token: 0x04000A28 RID: 2600
		private InstanceContext callbackInstance;
	}
}

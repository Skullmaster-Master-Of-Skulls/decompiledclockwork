using System;
using System.ServiceModel.Channels;

namespace System.ServiceModel
{
	// Token: 0x020000F0 RID: 240
	internal sealed class ProgrammaticEndpointTrait<TChannel> : EndpointTrait<TChannel> where TChannel : class
	{
		// Token: 0x06000504 RID: 1284 RVA: 0x00017E71 File Offset: 0x00016071
		public ProgrammaticEndpointTrait(Binding binding, EndpointAddress remoteAddress, InstanceContext callbackInstance)
		{
			this.binding = binding;
			this.remoteAddress = remoteAddress;
			this.callbackInstance = callbackInstance;
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x00017E90 File Offset: 0x00016090
		public override bool Equals(object obj)
		{
			ProgrammaticEndpointTrait<TChannel> programmaticEndpointTrait = obj as ProgrammaticEndpointTrait<TChannel>;
			return programmaticEndpointTrait != null && this.callbackInstance == programmaticEndpointTrait.callbackInstance && !(this.remoteAddress != programmaticEndpointTrait.remoteAddress) && this.binding == programmaticEndpointTrait.binding;
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x00017EE0 File Offset: 0x000160E0
		public override int GetHashCode()
		{
			int num = 0;
			if (this.callbackInstance != null)
			{
				num ^= this.callbackInstance.GetHashCode();
			}
			num ^= this.remoteAddress.GetHashCode();
			return num ^ this.binding.GetHashCode();
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x00017F22 File Offset: 0x00016122
		public override ChannelFactory<TChannel> CreateChannelFactory()
		{
			if (this.callbackInstance != null)
			{
				return this.CreateDuplexFactory();
			}
			return this.CreateSimplexFactory();
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x00017F39 File Offset: 0x00016139
		private DuplexChannelFactory<TChannel> CreateDuplexFactory()
		{
			return new DuplexChannelFactory<TChannel>(this.callbackInstance, this.binding, this.remoteAddress);
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x00017F52 File Offset: 0x00016152
		private ChannelFactory<TChannel> CreateSimplexFactory()
		{
			return new ChannelFactory<TChannel>(this.binding, this.remoteAddress);
		}

		// Token: 0x04000A29 RID: 2601
		private EndpointAddress remoteAddress;

		// Token: 0x04000A2A RID: 2602
		private Binding binding;

		// Token: 0x04000A2B RID: 2603
		private InstanceContext callbackInstance;
	}
}

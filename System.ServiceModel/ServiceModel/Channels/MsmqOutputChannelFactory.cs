using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x020008DD RID: 2269
	internal sealed class MsmqOutputChannelFactory : MsmqChannelFactory<IOutputChannel>
	{
		// Token: 0x06005662 RID: 22114 RVA: 0x0013C457 File Offset: 0x0013A657
		internal MsmqOutputChannelFactory(MsmqTransportBindingElement bindingElement, BindingContext context) : base(bindingElement, context)
		{
		}

		// Token: 0x06005663 RID: 22115 RVA: 0x0013C461 File Offset: 0x0013A661
		protected override IOutputChannel OnCreateChannel(EndpointAddress to, Uri via)
		{
			base.ValidateScheme(via);
			return new MsmqOutputChannel(this, to, via, base.ManualAddressing);
		}
	}
}

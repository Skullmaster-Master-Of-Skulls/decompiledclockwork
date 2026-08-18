using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x020008DE RID: 2270
	internal sealed class MsmqOutputSessionChannelFactory : MsmqChannelFactory<IOutputSessionChannel>
	{
		// Token: 0x06005664 RID: 22116 RVA: 0x0013C478 File Offset: 0x0013A678
		internal MsmqOutputSessionChannelFactory(MsmqTransportBindingElement bindingElement, BindingContext context) : base(bindingElement, context)
		{
		}

		// Token: 0x06005665 RID: 22117 RVA: 0x0013C482 File Offset: 0x0013A682
		protected override IOutputSessionChannel OnCreateChannel(EndpointAddress to, Uri via)
		{
			base.ValidateScheme(via);
			return new MsmqOutputSessionChannel(this, to, via, base.ManualAddressing);
		}
	}
}

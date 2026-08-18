using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x020008F9 RID: 2297
	internal sealed class MsmqTransportReceiveParameters : MsmqReceiveParameters
	{
		// Token: 0x060057A5 RID: 22437 RVA: 0x00141DF5 File Offset: 0x0013FFF5
		internal MsmqTransportReceiveParameters(MsmqTransportBindingElement bindingElement, MsmqUri.IAddressTranslator addressTranslator) : base(bindingElement, addressTranslator)
		{
			this.maxPoolSize = bindingElement.MaxPoolSize;
			this.useActiveDirectory = bindingElement.UseActiveDirectory;
			this.queueTransferProtocol = bindingElement.QueueTransferProtocol;
		}

		// Token: 0x1700155A RID: 5466
		// (get) Token: 0x060057A6 RID: 22438 RVA: 0x00141E23 File Offset: 0x00140023
		internal int MaxPoolSize
		{
			get
			{
				return this.maxPoolSize;
			}
		}

		// Token: 0x1700155B RID: 5467
		// (get) Token: 0x060057A7 RID: 22439 RVA: 0x00141E2B File Offset: 0x0014002B
		internal bool UseActiveDirectory
		{
			get
			{
				return this.useActiveDirectory;
			}
		}

		// Token: 0x1700155C RID: 5468
		// (get) Token: 0x060057A8 RID: 22440 RVA: 0x00141E33 File Offset: 0x00140033
		internal QueueTransferProtocol QueueTransferProtocol
		{
			get
			{
				return this.queueTransferProtocol;
			}
		}

		// Token: 0x040035EA RID: 13802
		private int maxPoolSize;

		// Token: 0x040035EB RID: 13803
		private bool useActiveDirectory;

		// Token: 0x040035EC RID: 13804
		private QueueTransferProtocol queueTransferProtocol;
	}
}

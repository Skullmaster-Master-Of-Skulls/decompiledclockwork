using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x020008DC RID: 2268
	internal abstract class MsmqChannelFactory<TChannel> : MsmqChannelFactoryBase<TChannel>
	{
		// Token: 0x0600565E RID: 22110 RVA: 0x0013C411 File Offset: 0x0013A611
		protected MsmqChannelFactory(MsmqTransportBindingElement bindingElement, BindingContext context) : base(bindingElement, context)
		{
			this.maxPoolSize = bindingElement.MaxPoolSize;
			this.queueTransferProtocol = bindingElement.QueueTransferProtocol;
			this.useActiveDirectory = bindingElement.UseActiveDirectory;
		}

		// Token: 0x1700151A RID: 5402
		// (get) Token: 0x0600565F RID: 22111 RVA: 0x0013C43F File Offset: 0x0013A63F
		public int MaxPoolSize
		{
			get
			{
				return this.maxPoolSize;
			}
		}

		// Token: 0x1700151B RID: 5403
		// (get) Token: 0x06005660 RID: 22112 RVA: 0x0013C447 File Offset: 0x0013A647
		public QueueTransferProtocol QueueTransferProtocol
		{
			get
			{
				return this.queueTransferProtocol;
			}
		}

		// Token: 0x1700151C RID: 5404
		// (get) Token: 0x06005661 RID: 22113 RVA: 0x0013C44F File Offset: 0x0013A64F
		public bool UseActiveDirectory
		{
			get
			{
				return this.useActiveDirectory;
			}
		}

		// Token: 0x04003565 RID: 13669
		private int maxPoolSize;

		// Token: 0x04003566 RID: 13670
		private QueueTransferProtocol queueTransferProtocol;

		// Token: 0x04003567 RID: 13671
		private bool useActiveDirectory;
	}
}

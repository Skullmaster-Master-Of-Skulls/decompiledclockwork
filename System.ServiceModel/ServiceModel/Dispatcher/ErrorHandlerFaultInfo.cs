using System;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x0200055C RID: 1372
	internal struct ErrorHandlerFaultInfo
	{
		// Token: 0x0600357E RID: 13694 RVA: 0x000D05B0 File Offset: 0x000CE7B0
		public ErrorHandlerFaultInfo(string defaultFaultAction)
		{
			this.defaultFaultAction = defaultFaultAction;
			this.fault = null;
			this.isConsideredUnhandled = false;
		}

		// Token: 0x17000CC8 RID: 3272
		// (get) Token: 0x0600357F RID: 13695 RVA: 0x000D05C7 File Offset: 0x000CE7C7
		// (set) Token: 0x06003580 RID: 13696 RVA: 0x000D05CF File Offset: 0x000CE7CF
		public Message Fault
		{
			get
			{
				return this.fault;
			}
			set
			{
				this.fault = value;
			}
		}

		// Token: 0x17000CC9 RID: 3273
		// (get) Token: 0x06003581 RID: 13697 RVA: 0x000D05D8 File Offset: 0x000CE7D8
		// (set) Token: 0x06003582 RID: 13698 RVA: 0x000D05E0 File Offset: 0x000CE7E0
		public string DefaultFaultAction
		{
			get
			{
				return this.defaultFaultAction;
			}
			set
			{
				this.defaultFaultAction = value;
			}
		}

		// Token: 0x17000CCA RID: 3274
		// (get) Token: 0x06003583 RID: 13699 RVA: 0x000D05E9 File Offset: 0x000CE7E9
		// (set) Token: 0x06003584 RID: 13700 RVA: 0x000D05F1 File Offset: 0x000CE7F1
		public bool IsConsideredUnhandled
		{
			get
			{
				return this.isConsideredUnhandled;
			}
			set
			{
				this.isConsideredUnhandled = value;
			}
		}

		// Token: 0x04002880 RID: 10368
		private Message fault;

		// Token: 0x04002881 RID: 10369
		private bool isConsideredUnhandled;

		// Token: 0x04002882 RID: 10370
		private string defaultFaultAction;
	}
}

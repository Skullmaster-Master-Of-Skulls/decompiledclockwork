using System;
using System.ServiceModel.Channels;

namespace System.ServiceModel
{
	// Token: 0x0200011C RID: 284
	[__DynamicallyInvokable]
	public sealed class UnknownMessageReceivedEventArgs : EventArgs
	{
		// Token: 0x06000741 RID: 1857 RVA: 0x0001E651 File Offset: 0x0001C851
		internal UnknownMessageReceivedEventArgs(Message message)
		{
			this.message = message;
		}

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x06000742 RID: 1858 RVA: 0x0001E660 File Offset: 0x0001C860
		[__DynamicallyInvokable]
		public Message Message
		{
			[__DynamicallyInvokable]
			get
			{
				return this.message;
			}
		}

		// Token: 0x04000ABD RID: 2749
		private Message message;
	}
}

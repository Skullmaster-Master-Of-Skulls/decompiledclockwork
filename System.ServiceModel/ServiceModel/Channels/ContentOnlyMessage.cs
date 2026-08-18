using System;
using System.ServiceModel.Diagnostics;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200085B RID: 2139
	internal abstract class ContentOnlyMessage : Message
	{
		// Token: 0x0600502A RID: 20522 RVA: 0x00126142 File Offset: 0x00124342
		protected ContentOnlyMessage()
		{
			this.headers = new MessageHeaders(MessageVersion.None);
		}

		// Token: 0x170013DC RID: 5084
		// (get) Token: 0x0600502B RID: 20523 RVA: 0x0012615A File Offset: 0x0012435A
		public override MessageHeaders Headers
		{
			get
			{
				if (base.IsDisposed)
				{
					throw TraceUtility.ThrowHelperError(base.CreateMessageDisposedException(), this);
				}
				return this.headers;
			}
		}

		// Token: 0x170013DD RID: 5085
		// (get) Token: 0x0600502C RID: 20524 RVA: 0x00126177 File Offset: 0x00124377
		public override MessageProperties Properties
		{
			get
			{
				if (base.IsDisposed)
				{
					throw TraceUtility.ThrowHelperError(base.CreateMessageDisposedException(), this);
				}
				if (this.properties == null)
				{
					this.properties = new MessageProperties();
				}
				return this.properties;
			}
		}

		// Token: 0x170013DE RID: 5086
		// (get) Token: 0x0600502D RID: 20525 RVA: 0x001261A7 File Offset: 0x001243A7
		public override MessageVersion Version
		{
			get
			{
				return this.headers.MessageVersion;
			}
		}

		// Token: 0x0600502E RID: 20526 RVA: 0x001261B4 File Offset: 0x001243B4
		protected override void OnBodyToString(XmlDictionaryWriter writer)
		{
			this.OnWriteBodyContents(writer);
		}

		// Token: 0x040031A3 RID: 12707
		private MessageHeaders headers;

		// Token: 0x040031A4 RID: 12708
		private MessageProperties properties;
	}
}

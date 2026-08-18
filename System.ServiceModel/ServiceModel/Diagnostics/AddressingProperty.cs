using System;
using System.ServiceModel.Channels;
using System.Xml;

namespace System.ServiceModel.Diagnostics
{
	// Token: 0x02000A72 RID: 2674
	internal class AddressingProperty
	{
		// Token: 0x06006964 RID: 26980 RVA: 0x00189603 File Offset: 0x00187803
		public AddressingProperty(MessageHeaders headers)
		{
			this.action = headers.Action;
			this.to = headers.To;
			this.replyTo = headers.ReplyTo;
			this.messageId = headers.MessageId;
		}

		// Token: 0x17001928 RID: 6440
		// (get) Token: 0x06006965 RID: 26981 RVA: 0x0018963B File Offset: 0x0018783B
		public string Action
		{
			get
			{
				return this.action;
			}
		}

		// Token: 0x17001929 RID: 6441
		// (get) Token: 0x06006966 RID: 26982 RVA: 0x00189643 File Offset: 0x00187843
		public UniqueId MessageId
		{
			get
			{
				return this.messageId;
			}
		}

		// Token: 0x1700192A RID: 6442
		// (get) Token: 0x06006967 RID: 26983 RVA: 0x0018964B File Offset: 0x0018784B
		public static string Name
		{
			get
			{
				return "Addressing";
			}
		}

		// Token: 0x1700192B RID: 6443
		// (get) Token: 0x06006968 RID: 26984 RVA: 0x00189652 File Offset: 0x00187852
		public EndpointAddress ReplyTo
		{
			get
			{
				return this.replyTo;
			}
		}

		// Token: 0x1700192C RID: 6444
		// (get) Token: 0x06006969 RID: 26985 RVA: 0x0018965A File Offset: 0x0018785A
		public Uri To
		{
			get
			{
				return this.to;
			}
		}

		// Token: 0x04003C46 RID: 15430
		private string action;

		// Token: 0x04003C47 RID: 15431
		private Uri to;

		// Token: 0x04003C48 RID: 15432
		private EndpointAddress replyTo;

		// Token: 0x04003C49 RID: 15433
		private UniqueId messageId;
	}
}

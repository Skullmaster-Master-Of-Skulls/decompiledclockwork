using System;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000462 RID: 1122
	internal class AndMessageFilter : MessageFilter
	{
		// Token: 0x06002B6E RID: 11118 RVA: 0x000AA1FD File Offset: 0x000A83FD
		public AndMessageFilter(MessageFilter filter1, MessageFilter filter2)
		{
			if (filter1 == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("filter1");
			}
			if (filter2 == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("filter2");
			}
			this.filter1 = filter1;
			this.filter2 = filter2;
		}

		// Token: 0x17000A8E RID: 2702
		// (get) Token: 0x06002B6F RID: 11119 RVA: 0x000AA239 File Offset: 0x000A8439
		public MessageFilter Filter1
		{
			get
			{
				return this.filter1;
			}
		}

		// Token: 0x17000A8F RID: 2703
		// (get) Token: 0x06002B70 RID: 11120 RVA: 0x000AA241 File Offset: 0x000A8441
		public MessageFilter Filter2
		{
			get
			{
				return this.filter2;
			}
		}

		// Token: 0x06002B71 RID: 11121 RVA: 0x000AA249 File Offset: 0x000A8449
		protected internal override IMessageFilterTable<FilterData> CreateFilterTable<FilterData>()
		{
			return new AndMessageFilterTable<FilterData>();
		}

		// Token: 0x06002B72 RID: 11122 RVA: 0x000AA250 File Offset: 0x000A8450
		public override bool Match(Message message)
		{
			if (message == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("message");
			}
			return this.filter1.Match(message) && this.filter2.Match(message);
		}

		// Token: 0x06002B73 RID: 11123 RVA: 0x000AA281 File Offset: 0x000A8481
		internal bool Match(Message message, out bool addressMatched)
		{
			if (message == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("message");
			}
			if (this.filter1.Match(message))
			{
				addressMatched = true;
				return this.filter2.Match(message);
			}
			addressMatched = false;
			return false;
		}

		// Token: 0x06002B74 RID: 11124 RVA: 0x000AA2B8 File Offset: 0x000A84B8
		public override bool Match(MessageBuffer messageBuffer)
		{
			if (messageBuffer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("messageBuffer");
			}
			return this.filter1.Match(messageBuffer) && this.filter2.Match(messageBuffer);
		}

		// Token: 0x04002416 RID: 9238
		private MessageFilter filter1;

		// Token: 0x04002417 RID: 9239
		private MessageFilter filter2;
	}
}

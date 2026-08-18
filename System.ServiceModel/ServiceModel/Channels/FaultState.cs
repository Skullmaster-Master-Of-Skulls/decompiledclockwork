using System;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000939 RID: 2361
	internal struct FaultState
	{
		// Token: 0x06005ACA RID: 23242 RVA: 0x0014D890 File Offset: 0x0014BA90
		public FaultState(RequestContext requestContext, Message faultMessage)
		{
			this.requestContext = requestContext;
			this.faultMessage = faultMessage;
		}

		// Token: 0x170015ED RID: 5613
		// (get) Token: 0x06005ACB RID: 23243 RVA: 0x0014D8A0 File Offset: 0x0014BAA0
		public Message FaultMessage
		{
			get
			{
				return this.faultMessage;
			}
		}

		// Token: 0x170015EE RID: 5614
		// (get) Token: 0x06005ACC RID: 23244 RVA: 0x0014D8A8 File Offset: 0x0014BAA8
		public RequestContext RequestContext
		{
			get
			{
				return this.requestContext;
			}
		}

		// Token: 0x040036BA RID: 14010
		private Message faultMessage;

		// Token: 0x040036BB RID: 14011
		private RequestContext requestContext;
	}
}

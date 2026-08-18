using System;
using System.Runtime.Serialization;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x0200046C RID: 1132
	[DataContract]
	public class MatchAllMessageFilter : MessageFilter
	{
		// Token: 0x06002BF3 RID: 11251 RVA: 0x000AC442 File Offset: 0x000AA642
		public override bool Match(MessageBuffer messageBuffer)
		{
			if (messageBuffer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("messageBuffer");
			}
			return true;
		}

		// Token: 0x06002BF4 RID: 11252 RVA: 0x000AC458 File Offset: 0x000AA658
		public override bool Match(Message message)
		{
			if (message == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("message");
			}
			return true;
		}
	}
}

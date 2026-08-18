using System;
using System.Runtime.Serialization;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x0200046D RID: 1133
	[DataContract]
	public class MatchNoneMessageFilter : MessageFilter
	{
		// Token: 0x06002BF6 RID: 11254 RVA: 0x000AC476 File Offset: 0x000AA676
		public override bool Match(MessageBuffer messageBuffer)
		{
			if (messageBuffer == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("messageBuffer");
			}
			return false;
		}

		// Token: 0x06002BF7 RID: 11255 RVA: 0x000AC48C File Offset: 0x000AA68C
		public override bool Match(Message message)
		{
			if (message == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("message");
			}
			return false;
		}
	}
}

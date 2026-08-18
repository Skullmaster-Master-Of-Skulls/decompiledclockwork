using System;
using System.Runtime.Serialization;

namespace System.ServiceModel.Security
{
	// Token: 0x02000304 RID: 772
	[Serializable]
	public class SecurityNegotiationException : CommunicationException
	{
		// Token: 0x06001A59 RID: 6745 RVA: 0x00062B62 File Offset: 0x00060D62
		public SecurityNegotiationException()
		{
		}

		// Token: 0x06001A5A RID: 6746 RVA: 0x00062B6A File Offset: 0x00060D6A
		public SecurityNegotiationException(string message) : base(message)
		{
		}

		// Token: 0x06001A5B RID: 6747 RVA: 0x00062B73 File Offset: 0x00060D73
		public SecurityNegotiationException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06001A5C RID: 6748 RVA: 0x00062B7D File Offset: 0x00060D7D
		protected SecurityNegotiationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}

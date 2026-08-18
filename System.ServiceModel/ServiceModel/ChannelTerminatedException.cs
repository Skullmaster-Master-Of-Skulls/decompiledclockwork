using System;
using System.Runtime.Serialization;

namespace System.ServiceModel
{
	// Token: 0x02000028 RID: 40
	[Serializable]
	public class ChannelTerminatedException : CommunicationException
	{
		// Token: 0x06000172 RID: 370 RVA: 0x000089D6 File Offset: 0x00006BD6
		public ChannelTerminatedException()
		{
		}

		// Token: 0x06000173 RID: 371 RVA: 0x000089DE File Offset: 0x00006BDE
		public ChannelTerminatedException(string message) : base(message)
		{
		}

		// Token: 0x06000174 RID: 372 RVA: 0x000089E7 File Offset: 0x00006BE7
		public ChannelTerminatedException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000175 RID: 373 RVA: 0x000089F1 File Offset: 0x00006BF1
		protected ChannelTerminatedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}

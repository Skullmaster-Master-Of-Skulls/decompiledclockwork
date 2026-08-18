using System;
using System.Runtime.Serialization;
using System.Security;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000988 RID: 2440
	[Serializable]
	public class RetryException : CommunicationException
	{
		// Token: 0x06005E76 RID: 24182 RVA: 0x0015D7CC File Offset: 0x0015B9CC
		public RetryException() : this(null, null)
		{
		}

		// Token: 0x06005E77 RID: 24183 RVA: 0x0015D7D6 File Offset: 0x0015B9D6
		public RetryException(string message) : this(message, null)
		{
		}

		// Token: 0x06005E78 RID: 24184 RVA: 0x0015D7E0 File Offset: 0x0015B9E0
		public RetryException(string message, Exception innerException) : base(message ?? SR.GetString("RetryGenericMessage"), innerException)
		{
		}

		// Token: 0x06005E79 RID: 24185 RVA: 0x0015D7F8 File Offset: 0x0015B9F8
		[SecurityCritical]
		protected RetryException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}

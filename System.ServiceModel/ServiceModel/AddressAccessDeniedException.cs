using System;
using System.Runtime.Serialization;

namespace System.ServiceModel
{
	// Token: 0x02000027 RID: 39
	[Serializable]
	public class AddressAccessDeniedException : CommunicationException
	{
		// Token: 0x0600016E RID: 366 RVA: 0x000089B1 File Offset: 0x00006BB1
		public AddressAccessDeniedException()
		{
		}

		// Token: 0x0600016F RID: 367 RVA: 0x000089B9 File Offset: 0x00006BB9
		public AddressAccessDeniedException(string message) : base(message)
		{
		}

		// Token: 0x06000170 RID: 368 RVA: 0x000089C2 File Offset: 0x00006BC2
		public AddressAccessDeniedException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000171 RID: 369 RVA: 0x000089CC File Offset: 0x00006BCC
		protected AddressAccessDeniedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}

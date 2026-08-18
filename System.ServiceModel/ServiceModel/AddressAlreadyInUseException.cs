using System;
using System.Runtime.Serialization;

namespace System.ServiceModel
{
	// Token: 0x02000026 RID: 38
	[Serializable]
	public class AddressAlreadyInUseException : CommunicationException
	{
		// Token: 0x0600016A RID: 362 RVA: 0x0000898C File Offset: 0x00006B8C
		public AddressAlreadyInUseException()
		{
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00008994 File Offset: 0x00006B94
		public AddressAlreadyInUseException(string message) : base(message)
		{
		}

		// Token: 0x0600016C RID: 364 RVA: 0x0000899D File Offset: 0x00006B9D
		public AddressAlreadyInUseException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600016D RID: 365 RVA: 0x000089A7 File Offset: 0x00006BA7
		protected AddressAlreadyInUseException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}

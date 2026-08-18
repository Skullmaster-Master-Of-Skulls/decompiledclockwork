using System;
using System.Runtime.Serialization;

namespace System.ServiceModel
{
	// Token: 0x020000B1 RID: 177
	[Serializable]
	public class PoisonMessageException : CommunicationException
	{
		// Token: 0x06000303 RID: 771 RVA: 0x00011E3B File Offset: 0x0001003B
		public PoisonMessageException()
		{
		}

		// Token: 0x06000304 RID: 772 RVA: 0x00011E43 File Offset: 0x00010043
		public PoisonMessageException(string message) : base(message)
		{
		}

		// Token: 0x06000305 RID: 773 RVA: 0x00011E4C File Offset: 0x0001004C
		public PoisonMessageException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000306 RID: 774 RVA: 0x00011E56 File Offset: 0x00010056
		protected PoisonMessageException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}

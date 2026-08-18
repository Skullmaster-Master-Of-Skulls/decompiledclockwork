using System;
using System.Runtime.Serialization;
using a;

namespace MailBee
{
	// Token: 0x02000069 RID: 105
	[Serializable]
	public class MailBeeProtocolExtensionNotSupportedException : MailBeeEmailProtocolException
	{
		// Token: 0x060003A1 RID: 929 RVA: 0x00008FCD File Offset: 0x00007FCD
		internal MailBeeProtocolExtensionNotSupportedException(int A_0, ai A_1) : base(A_0, A_1)
		{
		}

		// Token: 0x060003A2 RID: 930 RVA: 0x00008FD7 File Offset: 0x00007FD7
		protected MailBeeProtocolExtensionNotSupportedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}

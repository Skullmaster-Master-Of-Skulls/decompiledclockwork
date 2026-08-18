using System;
using System.Runtime.Serialization;
using a;

namespace MailBee.EwsMail
{
	// Token: 0x02000523 RID: 1315
	[Serializable]
	public class MailBeeEwsException : MailBeeEmailProtocolException
	{
		// Token: 0x06002B42 RID: 11074 RVA: 0x000CC559 File Offset: 0x000CB559
		internal MailBeeEwsException(int A_0, Exception A_1, ai A_2) : base(A_0, A_1, A_2)
		{
		}

		// Token: 0x06002B43 RID: 11075 RVA: 0x000CC564 File Offset: 0x000CB564
		protected MailBeeEwsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}

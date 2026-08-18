using System;
using System.Runtime.Serialization;
using a;

namespace MailBee
{
	// Token: 0x02000055 RID: 85
	public abstract class MailBeeProtocolException : MailBeeRemoteHostException
	{
		// Token: 0x060001D4 RID: 468 RVA: 0x000082A6 File Offset: 0x000072A6
		internal MailBeeProtocolException(string A_0, int A_1, ai A_2) : base(A_0, A_1, A_2)
		{
		}

		// Token: 0x060001D5 RID: 469 RVA: 0x000082B1 File Offset: 0x000072B1
		internal MailBeeProtocolException(int A_0, ai A_1) : base(A_0, A_1)
		{
		}

		// Token: 0x060001D6 RID: 470 RVA: 0x000082BB File Offset: 0x000072BB
		internal MailBeeProtocolException(int A_0, Exception A_1, ai A_2) : base(A_0, A_1, A_2)
		{
		}

		// Token: 0x060001D7 RID: 471 RVA: 0x000082C6 File Offset: 0x000072C6
		protected MailBeeProtocolException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}

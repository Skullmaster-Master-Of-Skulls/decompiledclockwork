using System;
using System.Runtime.Serialization;
using a;

namespace MailBee
{
	// Token: 0x02000054 RID: 84
	public abstract class MailBeeInvalidResponseException : MailBeeConnectionException
	{
		// Token: 0x060001CF RID: 463 RVA: 0x0000826F File Offset: 0x0000726F
		internal MailBeeInvalidResponseException(string A_0, int A_1, ai A_2) : base(A_0, A_1, A_2)
		{
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x0000827A File Offset: 0x0000727A
		internal MailBeeInvalidResponseException(int A_0, ai A_1) : base(A_0, A_1)
		{
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x00008284 File Offset: 0x00007284
		internal MailBeeInvalidResponseException(string A_0, int A_1, Exception A_2, ai A_3) : base(A_0, A_1, A_2, A_3)
		{
		}

		// Token: 0x060001D2 RID: 466 RVA: 0x00008291 File Offset: 0x00007291
		internal MailBeeInvalidResponseException(int A_0, Exception A_1, ai A_2) : base(A_0, A_1, A_2)
		{
		}

		// Token: 0x060001D3 RID: 467 RVA: 0x0000829C File Offset: 0x0000729C
		protected MailBeeInvalidResponseException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}

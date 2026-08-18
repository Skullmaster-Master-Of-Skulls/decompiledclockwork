using System;
using System.Runtime.Serialization;
using a;

namespace MailBee
{
	// Token: 0x02000048 RID: 72
	public abstract class MailBeeConnectionException : MailBeeRemoteHostException, IMailBeeSocketMustCloseException
	{
		// Token: 0x060001B4 RID: 436 RVA: 0x0000814C File Offset: 0x0000714C
		internal MailBeeConnectionException(string A_0, int A_1, ai A_2) : base(A_0, A_1, A_2)
		{
		}

		// Token: 0x060001B5 RID: 437 RVA: 0x00008157 File Offset: 0x00007157
		internal MailBeeConnectionException(int A_0, ai A_1) : base(A_0, A_1)
		{
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x00008161 File Offset: 0x00007161
		internal MailBeeConnectionException(string A_0, int A_1, Exception A_2, ai A_3) : base(A_0, A_1, A_2, A_3)
		{
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x0000816E File Offset: 0x0000716E
		internal MailBeeConnectionException(int A_0, Exception A_1, ai A_2) : base(A_0, A_1, A_2)
		{
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x00008179 File Offset: 0x00007179
		protected MailBeeConnectionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}

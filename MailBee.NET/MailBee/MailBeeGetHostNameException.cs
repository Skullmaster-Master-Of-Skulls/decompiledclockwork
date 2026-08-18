using System;
using System.Runtime.Serialization;

namespace MailBee
{
	// Token: 0x02000044 RID: 68
	public abstract class MailBeeGetHostNameException : MailBeeNetworkException
	{
		// Token: 0x060001A0 RID: 416 RVA: 0x00008031 File Offset: 0x00007031
		internal MailBeeGetHostNameException(int A_0) : base(A_0)
		{
		}

		// Token: 0x060001A1 RID: 417 RVA: 0x0000803A File Offset: 0x0000703A
		internal MailBeeGetHostNameException(string A_0, int A_1, Exception A_2) : base(A_0, A_1, A_2)
		{
		}

		// Token: 0x060001A2 RID: 418 RVA: 0x00008045 File Offset: 0x00007045
		internal MailBeeGetHostNameException(int A_0, Exception A_1) : base(A_0, A_1)
		{
		}

		// Token: 0x060001A3 RID: 419 RVA: 0x0000804F File Offset: 0x0000704F
		protected MailBeeGetHostNameException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}

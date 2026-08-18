using System;
using System.Runtime.Serialization;

namespace MailBee
{
	// Token: 0x02000042 RID: 66
	public abstract class MailBeeNetworkException : MailBeeException
	{
		// Token: 0x06000197 RID: 407 RVA: 0x00007FCE File Offset: 0x00006FCE
		internal MailBeeNetworkException(string A_0, int A_1) : base(A_0, A_1)
		{
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00007FD8 File Offset: 0x00006FD8
		internal MailBeeNetworkException(int A_0) : base(A_0)
		{
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00007FE1 File Offset: 0x00006FE1
		internal MailBeeNetworkException(string A_0, int A_1, Exception A_2) : base(A_0, A_1, A_2)
		{
		}

		// Token: 0x0600019A RID: 410 RVA: 0x00007FEC File Offset: 0x00006FEC
		internal MailBeeNetworkException(int A_0, Exception A_1) : base(A_0, A_1)
		{
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00007FF6 File Offset: 0x00006FF6
		protected MailBeeNetworkException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}

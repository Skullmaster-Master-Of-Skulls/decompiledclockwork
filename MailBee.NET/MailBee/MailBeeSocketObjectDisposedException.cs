using System;
using System.Runtime.Serialization;
using a;

namespace MailBee
{
	// Token: 0x0200004B RID: 75
	[Serializable]
	public class MailBeeSocketObjectDisposedException : MailBeeConnectionException
	{
		// Token: 0x060001BD RID: 445 RVA: 0x000081AB File Offset: 0x000071AB
		internal MailBeeSocketObjectDisposedException(int A_0, Exception A_1, ai A_2) : base(A_0, A_1, A_2)
		{
		}

		// Token: 0x060001BE RID: 446 RVA: 0x000081B6 File Offset: 0x000071B6
		protected MailBeeSocketObjectDisposedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}

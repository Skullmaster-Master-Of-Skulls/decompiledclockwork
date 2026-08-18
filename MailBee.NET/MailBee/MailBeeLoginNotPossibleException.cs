using System;
using System.Runtime.Serialization;
using a;

namespace MailBee
{
	// Token: 0x0200006A RID: 106
	public abstract class MailBeeLoginNotPossibleException : MailBeeEmailProtocolException, IMailBeeLoginException
	{
		// Token: 0x060003A3 RID: 931 RVA: 0x00008FE1 File Offset: 0x00007FE1
		internal MailBeeLoginNotPossibleException(int A_0, ai A_1) : base(A_0, A_1)
		{
		}

		// Token: 0x060003A4 RID: 932 RVA: 0x00008FEB File Offset: 0x00007FEB
		protected MailBeeLoginNotPossibleException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}

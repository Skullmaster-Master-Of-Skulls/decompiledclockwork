using System;
using System.Runtime.Serialization;
using a;

namespace MailBee
{
	// Token: 0x02000068 RID: 104
	public abstract class MailBeeEmailProtocolException : MailBeeProtocolException
	{
		// Token: 0x0600039D RID: 925 RVA: 0x00008FA3 File Offset: 0x00007FA3
		internal MailBeeEmailProtocolException(string A_0, int A_1, ai A_2) : base(A_0, A_1, A_2)
		{
		}

		// Token: 0x0600039E RID: 926 RVA: 0x00008FAE File Offset: 0x00007FAE
		internal MailBeeEmailProtocolException(int A_0, ai A_1) : base(A_0, A_1)
		{
		}

		// Token: 0x0600039F RID: 927 RVA: 0x00008FB8 File Offset: 0x00007FB8
		internal MailBeeEmailProtocolException(int A_0, Exception A_1, ai A_2) : base(A_0, A_1, A_2)
		{
		}

		// Token: 0x060003A0 RID: 928 RVA: 0x00008FC3 File Offset: 0x00007FC3
		protected MailBeeEmailProtocolException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}

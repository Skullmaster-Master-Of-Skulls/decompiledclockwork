using System;
using System.Runtime.Serialization;

namespace MailBee.Outlook
{
	// Token: 0x020005B1 RID: 1457
	public abstract class MailBeePstException : MailBeeLocalException
	{
		// Token: 0x060030FC RID: 12540 RVA: 0x000E6307 File Offset: 0x000E5307
		internal MailBeePstException(string A_0, int A_1) : base(A_0, A_1)
		{
		}

		// Token: 0x060030FD RID: 12541 RVA: 0x000E6311 File Offset: 0x000E5311
		internal MailBeePstException(int A_0) : base(A_0)
		{
		}

		// Token: 0x060030FE RID: 12542 RVA: 0x000E631A File Offset: 0x000E531A
		internal MailBeePstException(int A_0, Exception A_1) : base(A_0, A_1)
		{
		}

		// Token: 0x060030FF RID: 12543 RVA: 0x000E6324 File Offset: 0x000E5324
		protected MailBeePstException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}

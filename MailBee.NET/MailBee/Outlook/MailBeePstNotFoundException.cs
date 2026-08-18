using System;
using System.Runtime.Serialization;

namespace MailBee.Outlook
{
	// Token: 0x020005B2 RID: 1458
	public class MailBeePstNotFoundException : MailBeePstException
	{
		// Token: 0x06003100 RID: 12544 RVA: 0x000E632E File Offset: 0x000E532E
		internal MailBeePstNotFoundException(string A_0, int A_1) : base(A_0, A_1)
		{
		}

		// Token: 0x06003101 RID: 12545 RVA: 0x000E6338 File Offset: 0x000E5338
		internal MailBeePstNotFoundException(int A_0) : base(A_0)
		{
		}

		// Token: 0x06003102 RID: 12546 RVA: 0x000E6341 File Offset: 0x000E5341
		internal MailBeePstNotFoundException(int A_0, Exception A_1) : base(A_0, A_1)
		{
		}

		// Token: 0x06003103 RID: 12547 RVA: 0x000E634B File Offset: 0x000E534B
		protected MailBeePstNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}

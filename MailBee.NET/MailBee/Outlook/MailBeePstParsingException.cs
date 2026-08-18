using System;
using System.Runtime.Serialization;

namespace MailBee.Outlook
{
	// Token: 0x020005B3 RID: 1459
	public class MailBeePstParsingException : MailBeePstException
	{
		// Token: 0x06003104 RID: 12548 RVA: 0x000E6355 File Offset: 0x000E5355
		internal MailBeePstParsingException(string A_0, int A_1) : base(A_0, A_1)
		{
		}

		// Token: 0x06003105 RID: 12549 RVA: 0x000E635F File Offset: 0x000E535F
		internal MailBeePstParsingException(int A_0) : base(A_0)
		{
		}

		// Token: 0x06003106 RID: 12550 RVA: 0x000E6368 File Offset: 0x000E5368
		internal MailBeePstParsingException(int A_0, Exception A_1) : base(A_0, A_1)
		{
		}

		// Token: 0x06003107 RID: 12551 RVA: 0x000E6372 File Offset: 0x000E5372
		protected MailBeePstParsingException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}

using System;
using System.Runtime.Serialization;
using a;

namespace MailBee
{
	// Token: 0x02000052 RID: 82
	[Serializable]
	public class MailBeeSocketHostNotFoundException : MailBeeSocketException
	{
		// Token: 0x060001CB RID: 459 RVA: 0x00008243 File Offset: 0x00007243
		internal MailBeeSocketHostNotFoundException(Exception A_0, ai A_1) : base(56, A_0, A_1)
		{
		}

		// Token: 0x060001CC RID: 460 RVA: 0x0000824F File Offset: 0x0000724F
		protected MailBeeSocketHostNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}

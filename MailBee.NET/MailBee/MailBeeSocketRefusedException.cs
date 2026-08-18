using System;
using System.Runtime.Serialization;
using a;

namespace MailBee
{
	// Token: 0x02000050 RID: 80
	[Serializable]
	public class MailBeeSocketRefusedException : MailBeeSocketException
	{
		// Token: 0x060001C7 RID: 455 RVA: 0x00008217 File Offset: 0x00007217
		internal MailBeeSocketRefusedException(Exception A_0, ai A_1) : base(54, A_0, A_1)
		{
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x00008223 File Offset: 0x00007223
		protected MailBeeSocketRefusedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}

using System;
using System.Runtime.Serialization;
using a;

namespace MailBee
{
	// Token: 0x02000051 RID: 81
	[Serializable]
	public class MailBeeSocketHostDownException : MailBeeSocketException
	{
		// Token: 0x060001C9 RID: 457 RVA: 0x0000822D File Offset: 0x0000722D
		internal MailBeeSocketHostDownException(Exception A_0, ai A_1) : base(57, A_0, A_1)
		{
		}

		// Token: 0x060001CA RID: 458 RVA: 0x00008239 File Offset: 0x00007239
		protected MailBeeSocketHostDownException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}

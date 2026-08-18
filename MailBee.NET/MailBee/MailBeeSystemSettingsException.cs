using System;
using System.Runtime.Serialization;

namespace MailBee
{
	// Token: 0x02000029 RID: 41
	[Serializable]
	public class MailBeeSystemSettingsException : MailBeeLocalException
	{
		// Token: 0x0600011F RID: 287 RVA: 0x000079A0 File Offset: 0x000069A0
		internal MailBeeSystemSettingsException(int A_0) : base(A_0)
		{
		}

		// Token: 0x06000120 RID: 288 RVA: 0x000079A9 File Offset: 0x000069A9
		protected MailBeeSystemSettingsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}

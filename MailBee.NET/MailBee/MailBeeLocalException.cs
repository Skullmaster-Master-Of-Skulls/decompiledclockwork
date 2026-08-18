using System;
using System.Runtime.Serialization;

namespace MailBee
{
	// Token: 0x02000021 RID: 33
	[Serializable]
	public class MailBeeLocalException : MailBeeException
	{
		// Token: 0x0600010A RID: 266 RVA: 0x000077C6 File Offset: 0x000067C6
		internal MailBeeLocalException(string A_0, int A_1) : base(A_0, A_1)
		{
		}

		// Token: 0x0600010B RID: 267 RVA: 0x000077D0 File Offset: 0x000067D0
		internal MailBeeLocalException(int A_0) : base(A_0)
		{
		}

		// Token: 0x0600010C RID: 268 RVA: 0x000077D9 File Offset: 0x000067D9
		internal MailBeeLocalException(int A_0, Exception A_1) : base(A_0, A_1)
		{
		}

		// Token: 0x0600010D RID: 269 RVA: 0x000077E3 File Offset: 0x000067E3
		protected MailBeeLocalException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}

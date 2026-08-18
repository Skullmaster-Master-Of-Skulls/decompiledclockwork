using System;
using System.Runtime.Serialization;
using a;
using MailBee.Mime;

namespace MailBee.SmtpMail
{
	// Token: 0x02000169 RID: 361
	[Serializable]
	public class MailBeeSmtpMessageSizeOutOfRangeException : MailBeeSmtpMessageNotAllowedException
	{
		// Token: 0x06000C36 RID: 3126 RVA: 0x00031A1B File Offset: 0x00030A1B
		internal MailBeeSmtpMessageSizeOutOfRangeException(int A_0, ai A_1, MailMessage A_2, string A_3, EmailAddressCollection A_4, int A_5) : base(A_0, A_1, A_2, A_3, A_4)
		{
			this.m_maxMessageSize = A_5;
		}

		// Token: 0x06000C37 RID: 3127 RVA: 0x00031A32 File Offset: 0x00030A32
		protected MailBeeSmtpMessageSizeOutOfRangeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x06000C38 RID: 3128 RVA: 0x00031A3C File Offset: 0x00030A3C
		public int MaxAllowedMessageSize
		{
			get
			{
				return this.m_maxMessageSize;
			}
		}

		// Token: 0x0400089C RID: 2204
		private int m_maxMessageSize;
	}
}

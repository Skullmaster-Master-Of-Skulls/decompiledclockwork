using System;
using System.Runtime.Serialization;
using a;

namespace MailBee.DnsMX
{
	// Token: 0x02000573 RID: 1395
	[Serializable]
	public class MailBeeDnsQueryMismatchException : MailBeeDnsProtocolException
	{
		// Token: 0x06002E40 RID: 11840 RVA: 0x000DE71C File Offset: 0x000DD71C
		internal MailBeeDnsQueryMismatchException(int A_0, ai A_1, string A_2, short A_3, short A_4) : base(A_0, A_1, A_2)
		{
			this.m_actualID = A_4;
			this.m_expectedID = A_3;
		}

		// Token: 0x06002E41 RID: 11841 RVA: 0x000DE737 File Offset: 0x000DD737
		protected MailBeeDnsQueryMismatchException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x170005B4 RID: 1460
		// (get) Token: 0x06002E42 RID: 11842 RVA: 0x000DE741 File Offset: 0x000DD741
		public short ActualID
		{
			get
			{
				return this.m_actualID;
			}
		}

		// Token: 0x170005B5 RID: 1461
		// (get) Token: 0x06002E43 RID: 11843 RVA: 0x000DE749 File Offset: 0x000DD749
		public short ExpectedID
		{
			get
			{
				return this.m_expectedID;
			}
		}

		// Token: 0x04001FCE RID: 8142
		private short m_actualID;

		// Token: 0x04001FCF RID: 8143
		private short m_expectedID;
	}
}

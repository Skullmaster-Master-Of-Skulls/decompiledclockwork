using System;
using System.Runtime.Serialization;
using a;
using a.f;

namespace MailBee.ImapMail
{
	// Token: 0x0200018B RID: 395
	[Serializable]
	public class MailBeeImapNegativeResponseException : MailBeeEmailProtocolNegativeResponseException
	{
		// Token: 0x06000E52 RID: 3666 RVA: 0x00035931 File Offset: 0x00034931
		internal MailBeeImapNegativeResponseException(int A_0, ai A_1, at A_2) : base(A_0, A_1, A_2)
		{
		}

		// Token: 0x06000E53 RID: 3667 RVA: 0x0003593C File Offset: 0x0003493C
		protected MailBeeImapNegativeResponseException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x06000E54 RID: 3668 RVA: 0x00035946 File Offset: 0x00034946
		public string CompletionResult
		{
			get
			{
				return ((global::a.f.a)this.a).l();
			}
		}

		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x06000E55 RID: 3669 RVA: 0x00035958 File Offset: 0x00034958
		public string OptionalResponse
		{
			get
			{
				return ((global::a.f.a)this.a).e();
			}
		}

		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x06000E56 RID: 3670 RVA: 0x0003596A File Offset: 0x0003496A
		public string HumanReadable
		{
			get
			{
				return ((global::a.f.a)this.a).r();
			}
		}
	}
}

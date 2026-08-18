using System;
using System.Runtime.Serialization;
using a;

namespace MailBee.ImapMail
{
	// Token: 0x02000190 RID: 400
	[Serializable]
	public class MailBeeImapMessageIndexNotFoundException : MailBeeEmailProtocolException
	{
		// Token: 0x06000E62 RID: 3682 RVA: 0x000359FF File Offset: 0x000349FF
		internal MailBeeImapMessageIndexNotFoundException(int A_0, ai A_1, long A_2, bool A_3) : base(A_0, A_1)
		{
			this.m_messageIndex = A_2;
			this.m_indexIsUid = A_3;
		}

		// Token: 0x06000E63 RID: 3683 RVA: 0x00035A18 File Offset: 0x00034A18
		protected MailBeeImapMessageIndexNotFoundException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000E64 RID: 3684 RVA: 0x00035A24 File Offset: 0x00034A24
		private static string a(int A_0, long A_1, bool A_2)
		{
			if (A_2)
			{
				return a5.a(A_0) + string.Format(Resources.Instance.ErrorDescSuffix_ImapNonExistentUid0, A_1);
			}
			return a5.a(A_0) + string.Format(Resources.Instance.ErrorDescSuffix_ImapNonExistentMessageNumber0, A_1);
		}

		// Token: 0x17000477 RID: 1143
		// (get) Token: 0x06000E65 RID: 3685 RVA: 0x00035A75 File Offset: 0x00034A75
		public long MessageIndex
		{
			get
			{
				return this.m_messageIndex;
			}
		}

		// Token: 0x17000478 RID: 1144
		// (get) Token: 0x06000E66 RID: 3686 RVA: 0x00035A7D File Offset: 0x00034A7D
		public bool IndexIsUid
		{
			get
			{
				return this.m_indexIsUid;
			}
		}

		// Token: 0x04000941 RID: 2369
		private long m_messageIndex;

		// Token: 0x04000942 RID: 2370
		private bool m_indexIsUid;
	}
}

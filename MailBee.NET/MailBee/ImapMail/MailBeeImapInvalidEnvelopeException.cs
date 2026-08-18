using System;
using System.Runtime.Serialization;
using a;

namespace MailBee.ImapMail
{
	// Token: 0x02000191 RID: 401
	[Serializable]
	public class MailBeeImapInvalidEnvelopeException : MailBeeEmailProtocolException
	{
		// Token: 0x06000E67 RID: 3687 RVA: 0x00035A85 File Offset: 0x00034A85
		internal MailBeeImapInvalidEnvelopeException(int A_0, ai A_1, Envelope A_2) : base(MailBeeImapInvalidEnvelopeException.a(A_0, A_2), A_0, A_1)
		{
			this.m_invalidEnvelope = A_2;
		}

		// Token: 0x06000E68 RID: 3688 RVA: 0x00035A9D File Offset: 0x00034A9D
		protected MailBeeImapInvalidEnvelopeException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000E69 RID: 3689 RVA: 0x00035AA7 File Offset: 0x00034AA7
		private static string a(int A_0, Envelope A_1)
		{
			return a5.a(A_0) + string.Format(Resources.Instance.ErrorDescSuffix_ImapInvalidEnvelopeMessageNumber0, A_1.MessageNumber);
		}

		// Token: 0x17000479 RID: 1145
		// (get) Token: 0x06000E6A RID: 3690 RVA: 0x00035ACE File Offset: 0x00034ACE
		public Envelope InvalidEnvelope
		{
			get
			{
				return this.m_invalidEnvelope;
			}
		}

		// Token: 0x04000943 RID: 2371
		private Envelope m_invalidEnvelope;
	}
}

using System;
using System.Runtime.Serialization;
using a;

namespace MailBee.SmtpMail
{
	// Token: 0x02000161 RID: 353
	[Serializable]
	public class MailBeeDnsRecordsDisabledException : MailBeeNetworkException
	{
		// Token: 0x06000C28 RID: 3112 RVA: 0x00031964 File Offset: 0x00030964
		internal MailBeeDnsRecordsDisabledException(int A_0, string A_1) : base(MailBeeDnsRecordsDisabledException.a(A_0, A_1), A_0)
		{
			this.m_domain = A_1;
		}

		// Token: 0x06000C29 RID: 3113 RVA: 0x0003197B File Offset: 0x0003097B
		protected MailBeeDnsRecordsDisabledException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000C2A RID: 3114 RVA: 0x00031985 File Offset: 0x00030985
		private static string a(int A_0, string A_1)
		{
			if (A_0 == 410)
			{
				return string.Format(a5.a(410), A_1);
			}
			if (A_0 != 411)
			{
				return null;
			}
			return string.Format(a5.a(411), A_1);
		}

		// Token: 0x170003CC RID: 972
		// (get) Token: 0x06000C2B RID: 3115 RVA: 0x000319BC File Offset: 0x000309BC
		public string Domain
		{
			get
			{
				return this.m_domain;
			}
		}

		// Token: 0x0400088C RID: 2188
		private string m_domain;
	}
}

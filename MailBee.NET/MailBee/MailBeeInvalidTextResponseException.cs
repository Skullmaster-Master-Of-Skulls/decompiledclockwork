using System;
using System.Runtime.Serialization;
using System.Text;
using a;

namespace MailBee
{
	// Token: 0x02000066 RID: 102
	[Serializable]
	public class MailBeeInvalidTextResponseException : MailBeeInvalidResponseException
	{
		// Token: 0x06000395 RID: 917 RVA: 0x00008EF6 File Offset: 0x00007EF6
		internal MailBeeInvalidTextResponseException(int A_0, ai A_1, string A_2, Encoding A_3) : base(MailBeeInvalidTextResponseException.a(A_0, A_2), A_0, A_1)
		{
			this.m_responseString = A_2;
			this.m_responseEncoding = A_3;
		}

		// Token: 0x06000396 RID: 918 RVA: 0x00008F16 File Offset: 0x00007F16
		internal MailBeeInvalidTextResponseException(int A_0, Exception A_1, ai A_2, string A_3, Encoding A_4) : base(MailBeeInvalidTextResponseException.a(A_0, A_3), A_0, A_1, A_2)
		{
			this.m_responseString = A_3;
			this.m_responseEncoding = A_4;
		}

		// Token: 0x06000397 RID: 919 RVA: 0x00008F39 File Offset: 0x00007F39
		protected MailBeeInvalidTextResponseException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000398 RID: 920 RVA: 0x00008F43 File Offset: 0x00007F43
		private static string a(int A_0, string A_1)
		{
			if (A_1 == null)
			{
				A_1 = "[null]";
			}
			A_1 = A_1.Replace("\r\n", "\\r\\n");
			return a5.a(A_0) + string.Format(Resources.Instance.ErrorDescSuffix_ResponseString0, A_1);
		}

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x06000399 RID: 921 RVA: 0x00008F7C File Offset: 0x00007F7C
		public Encoding ResponseEncoding
		{
			get
			{
				return this.m_responseEncoding;
			}
		}

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x0600039A RID: 922 RVA: 0x00008F84 File Offset: 0x00007F84
		public string ResponseString
		{
			get
			{
				return this.m_responseString;
			}
		}

		// Token: 0x0400016A RID: 362
		private Encoding m_responseEncoding;

		// Token: 0x0400016B RID: 363
		private string m_responseString;
	}
}

using System;
using System.Runtime.Serialization;
using System.Text;
using a;

namespace MailBee
{
	// Token: 0x0200006E RID: 110
	public abstract class MailBeeEmailProtocolNegativeResponseException : MailBeeEmailProtocolException, IMailBeeNegativeTextResponseException
	{
		// Token: 0x060003B1 RID: 945 RVA: 0x000090AE File Offset: 0x000080AE
		internal MailBeeEmailProtocolNegativeResponseException(int A_0, ai A_1, at A_2) : base(MailBeeEmailProtocolNegativeResponseException.a(A_0, A_2), A_0, A_1)
		{
			this.a = A_2;
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x000090C6 File Offset: 0x000080C6
		protected MailBeeEmailProtocolNegativeResponseException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x000090D0 File Offset: 0x000080D0
		private static string a(int A_0, at A_1)
		{
			string text = A_1.o();
			if (text == null)
			{
				text = "[null]";
			}
			return a5.a(A_0) + string.Format(Resources.Instance.ErrorDescSuffix_ServerResponded0, text.TrimEnd(new char[]
			{
				'\r',
				'\n',
				'.'
			}));
		}

		// Token: 0x17000225 RID: 549
		// (get) Token: 0x060003B4 RID: 948 RVA: 0x0000911E File Offset: 0x0000811E
		public string ResponseString
		{
			get
			{
				return this.a.o();
			}
		}

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x060003B5 RID: 949 RVA: 0x0000912B File Offset: 0x0000812B
		public Encoding ResponseEncoding
		{
			get
			{
				return this.a.p();
			}
		}

		// Token: 0x04000171 RID: 369
		internal at a;
	}
}

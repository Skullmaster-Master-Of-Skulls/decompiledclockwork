using System;
using System.Runtime.Serialization;
using a;

namespace MailBee
{
	// Token: 0x0200006B RID: 107
	[Serializable]
	public class MailBeeLoginNoSupportedMethodsException : MailBeeLoginNotPossibleException
	{
		// Token: 0x060003A5 RID: 933 RVA: 0x00008FF5 File Offset: 0x00007FF5
		internal MailBeeLoginNoSupportedMethodsException(int A_0, ai A_1, AuthenticationMethods A_2, AuthenticationMethods A_3) : base(A_0, A_1)
		{
			this.m_requestedMethods = A_2;
			this.m_supportedMethods = A_3;
		}

		// Token: 0x060003A6 RID: 934 RVA: 0x0000900E File Offset: 0x0000800E
		protected MailBeeLoginNoSupportedMethodsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x060003A7 RID: 935 RVA: 0x00009018 File Offset: 0x00008018
		public AuthenticationMethods RequestedMethods
		{
			get
			{
				return this.m_requestedMethods;
			}
		}

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x060003A8 RID: 936 RVA: 0x00009020 File Offset: 0x00008020
		public AuthenticationMethods SupportedMethods
		{
			get
			{
				return this.m_supportedMethods;
			}
		}

		// Token: 0x0400016C RID: 364
		private AuthenticationMethods m_requestedMethods;

		// Token: 0x0400016D RID: 365
		private AuthenticationMethods m_supportedMethods;
	}
}

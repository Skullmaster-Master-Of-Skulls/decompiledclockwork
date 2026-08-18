using System;
using System.Runtime.Serialization;
using a;

namespace MailBee.DnsMX
{
	// Token: 0x02000575 RID: 1397
	[Serializable]
	public class MailBeeDnsProtocolNegativeResponseException : MailBeeDnsProtocolException, IMailBeeNegativeDnsResponseException
	{
		// Token: 0x06002E46 RID: 11846 RVA: 0x000DE766 File Offset: 0x000DD766
		internal MailBeeDnsProtocolNegativeResponseException(int A_0, ai A_1, string A_2, short A_3, byte[] A_4, DnsReplyCode A_5) : base(MailBeeDnsProtocolNegativeResponseException.a(A_0, A_4, A_5, A_2), A_0, A_1, A_2)
		{
			this.m_id = A_3;
			this.m_responseData = A_4;
			this.m_responseCode = A_5;
		}

		// Token: 0x06002E47 RID: 11847 RVA: 0x000DE794 File Offset: 0x000DD794
		protected MailBeeDnsProtocolNegativeResponseException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06002E48 RID: 11848 RVA: 0x000DE7A0 File Offset: 0x000DD7A0
		private static string a(int A_0, byte[] A_1, DnsReplyCode A_2, string A_3)
		{
			string arg;
			if (A_1 == null)
			{
				arg = "[null]";
			}
			else
			{
				arg = w.b(A_1);
			}
			return a5.a(A_0) + string.Format(Resources.Instance.ErrorDescSuffix_DnsResponseCode0HostName1Base64EncodedData2, A_2, A_3, arg);
		}

		// Token: 0x170005B6 RID: 1462
		// (get) Token: 0x06002E49 RID: 11849 RVA: 0x000DE7E1 File Offset: 0x000DD7E1
		public short ID
		{
			get
			{
				return this.m_id;
			}
		}

		// Token: 0x170005B7 RID: 1463
		// (get) Token: 0x06002E4A RID: 11850 RVA: 0x000DE7E9 File Offset: 0x000DD7E9
		public byte[] ResponseData
		{
			get
			{
				return this.m_responseData;
			}
		}

		// Token: 0x170005B8 RID: 1464
		// (get) Token: 0x06002E4B RID: 11851 RVA: 0x000DE7F1 File Offset: 0x000DD7F1
		public DnsReplyCode ResponseCode
		{
			get
			{
				return this.m_responseCode;
			}
		}

		// Token: 0x04001FD0 RID: 8144
		private short m_id;

		// Token: 0x04001FD1 RID: 8145
		private byte[] m_responseData;

		// Token: 0x04001FD2 RID: 8146
		private DnsReplyCode m_responseCode;
	}
}

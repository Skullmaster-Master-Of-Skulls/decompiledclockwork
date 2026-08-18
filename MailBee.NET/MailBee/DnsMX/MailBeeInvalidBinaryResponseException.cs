using System;
using System.Runtime.Serialization;
using a;

namespace MailBee.DnsMX
{
	// Token: 0x02000571 RID: 1393
	[Serializable]
	public class MailBeeInvalidBinaryResponseException : MailBeeInvalidResponseException
	{
		// Token: 0x06002E37 RID: 11831 RVA: 0x000DE69D File Offset: 0x000DD69D
		internal MailBeeInvalidBinaryResponseException(string A_0, int A_1, ai A_2, byte[] A_3) : base(A_0, A_1, A_2)
		{
			this.m_responseData = A_3;
		}

		// Token: 0x06002E38 RID: 11832 RVA: 0x000DE6B0 File Offset: 0x000DD6B0
		internal MailBeeInvalidBinaryResponseException(int A_0, ai A_1, byte[] A_2) : base(A_0, A_1)
		{
			this.m_responseData = A_2;
		}

		// Token: 0x06002E39 RID: 11833 RVA: 0x000DE6C1 File Offset: 0x000DD6C1
		internal MailBeeInvalidBinaryResponseException(int A_0, Exception A_1, ai A_2, byte[] A_3) : base(A_0, A_1, A_2)
		{
			this.m_responseData = A_3;
		}

		// Token: 0x06002E3A RID: 11834 RVA: 0x000DE6D4 File Offset: 0x000DD6D4
		protected MailBeeInvalidBinaryResponseException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x170005B2 RID: 1458
		// (get) Token: 0x06002E3B RID: 11835 RVA: 0x000DE6DE File Offset: 0x000DD6DE
		public byte[] ResponseData
		{
			get
			{
				return this.m_responseData;
			}
		}

		// Token: 0x04001FCC RID: 8140
		private byte[] m_responseData;
	}
}

using System;
using System.Runtime.Serialization;

namespace System.ServiceModel.Security
{
	// Token: 0x020002DB RID: 731
	[Serializable]
	public class ExpiredSecurityTokenException : MessageSecurityException
	{
		// Token: 0x060017E2 RID: 6114 RVA: 0x0005AED7 File Offset: 0x000590D7
		public ExpiredSecurityTokenException()
		{
		}

		// Token: 0x060017E3 RID: 6115 RVA: 0x0005AEDF File Offset: 0x000590DF
		public ExpiredSecurityTokenException(string message) : base(message)
		{
		}

		// Token: 0x060017E4 RID: 6116 RVA: 0x0005AEE8 File Offset: 0x000590E8
		public ExpiredSecurityTokenException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060017E5 RID: 6117 RVA: 0x0005AEF2 File Offset: 0x000590F2
		protected ExpiredSecurityTokenException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}

using System;
using System.Runtime.Serialization;

namespace System.IdentityModel.Tokens
{
	// Token: 0x0200017E RID: 382
	[Serializable]
	public class SecurityTokenValidationException : SecurityTokenException
	{
		// Token: 0x06000C3F RID: 3135 RVA: 0x00038395 File Offset: 0x00036595
		public SecurityTokenValidationException()
		{
		}

		// Token: 0x06000C40 RID: 3136 RVA: 0x000101E4 File Offset: 0x0000E3E4
		public SecurityTokenValidationException(string message) : base(message)
		{
		}

		// Token: 0x06000C41 RID: 3137 RVA: 0x000101ED File Offset: 0x0000E3ED
		public SecurityTokenValidationException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000C42 RID: 3138 RVA: 0x000101F7 File Offset: 0x0000E3F7
		protected SecurityTokenValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}

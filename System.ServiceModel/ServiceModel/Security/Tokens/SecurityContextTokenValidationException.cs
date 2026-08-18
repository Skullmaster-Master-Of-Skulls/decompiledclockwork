using System;
using System.IdentityModel.Tokens;
using System.Runtime.Serialization;

namespace System.ServiceModel.Security.Tokens
{
	// Token: 0x02000388 RID: 904
	[Serializable]
	internal class SecurityContextTokenValidationException : SecurityTokenValidationException
	{
		// Token: 0x06002174 RID: 8564 RVA: 0x0007BA3E File Offset: 0x00079C3E
		public SecurityContextTokenValidationException()
		{
		}

		// Token: 0x06002175 RID: 8565 RVA: 0x0007BA46 File Offset: 0x00079C46
		public SecurityContextTokenValidationException(string message) : base(message)
		{
		}

		// Token: 0x06002176 RID: 8566 RVA: 0x0007BA4F File Offset: 0x00079C4F
		public SecurityContextTokenValidationException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06002177 RID: 8567 RVA: 0x0007BA59 File Offset: 0x00079C59
		protected SecurityContextTokenValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}

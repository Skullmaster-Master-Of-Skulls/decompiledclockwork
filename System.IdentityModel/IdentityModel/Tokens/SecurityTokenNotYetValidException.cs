using System;
using System.Runtime.Serialization;

namespace System.IdentityModel.Tokens
{
	// Token: 0x02000175 RID: 373
	[Serializable]
	public class SecurityTokenNotYetValidException : SecurityTokenValidationException
	{
		// Token: 0x06000BB8 RID: 3000 RVA: 0x00037101 File Offset: 0x00035301
		public SecurityTokenNotYetValidException() : base(SR.GetString("ID4182"))
		{
		}

		// Token: 0x06000BB9 RID: 3001 RVA: 0x0001F63A File Offset: 0x0001D83A
		public SecurityTokenNotYetValidException(string message) : base(message)
		{
		}

		// Token: 0x06000BBA RID: 3002 RVA: 0x0001F643 File Offset: 0x0001D843
		public SecurityTokenNotYetValidException(string message, Exception inner) : base(message, inner)
		{
		}

		// Token: 0x06000BBB RID: 3003 RVA: 0x0001F64D File Offset: 0x0001D84D
		protected SecurityTokenNotYetValidException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}

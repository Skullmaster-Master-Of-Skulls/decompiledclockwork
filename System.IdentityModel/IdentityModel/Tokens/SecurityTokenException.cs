using System;
using System.Runtime.Serialization;

namespace System.IdentityModel.Tokens
{
	// Token: 0x02000173 RID: 371
	[Serializable]
	public class SecurityTokenException : SystemException
	{
		// Token: 0x06000BB0 RID: 2992 RVA: 0x0000BA18 File Offset: 0x00009C18
		public SecurityTokenException()
		{
		}

		// Token: 0x06000BB1 RID: 2993 RVA: 0x0000BA20 File Offset: 0x00009C20
		public SecurityTokenException(string message) : base(message)
		{
		}

		// Token: 0x06000BB2 RID: 2994 RVA: 0x0000BA29 File Offset: 0x00009C29
		public SecurityTokenException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000BB3 RID: 2995 RVA: 0x0000BA33 File Offset: 0x00009C33
		protected SecurityTokenException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}

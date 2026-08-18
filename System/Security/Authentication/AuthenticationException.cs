using System;
using System.Runtime.Serialization;

namespace System.Security.Authentication
{
	// Token: 0x02000589 RID: 1417
	[Serializable]
	public class AuthenticationException : SystemException
	{
		// Token: 0x06002B99 RID: 11161 RVA: 0x000BCC70 File Offset: 0x000BBC70
		public AuthenticationException()
		{
		}

		// Token: 0x06002B9A RID: 11162 RVA: 0x000BCC78 File Offset: 0x000BBC78
		protected AuthenticationException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
		{
		}

		// Token: 0x06002B9B RID: 11163 RVA: 0x000BCC82 File Offset: 0x000BBC82
		public AuthenticationException(string message) : base(message)
		{
		}

		// Token: 0x06002B9C RID: 11164 RVA: 0x000BCC8B File Offset: 0x000BBC8B
		public AuthenticationException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}

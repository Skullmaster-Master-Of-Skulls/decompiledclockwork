using System;
using System.Runtime.Serialization;

namespace System.Security.Authentication
{
	// Token: 0x0200058A RID: 1418
	[Serializable]
	public class InvalidCredentialException : AuthenticationException
	{
		// Token: 0x06002B9D RID: 11165 RVA: 0x000BCC95 File Offset: 0x000BBC95
		public InvalidCredentialException()
		{
		}

		// Token: 0x06002B9E RID: 11166 RVA: 0x000BCC9D File Offset: 0x000BBC9D
		protected InvalidCredentialException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
		{
		}

		// Token: 0x06002B9F RID: 11167 RVA: 0x000BCCA7 File Offset: 0x000BBCA7
		public InvalidCredentialException(string message) : base(message)
		{
		}

		// Token: 0x06002BA0 RID: 11168 RVA: 0x000BCCB0 File Offset: 0x000BBCB0
		public InvalidCredentialException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}

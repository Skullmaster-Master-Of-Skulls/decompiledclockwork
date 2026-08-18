using System;
using System.Runtime.Serialization;

namespace System.Security.Authentication
{
	// Token: 0x0200043B RID: 1083
	[Serializable]
	public class InvalidCredentialException : AuthenticationException
	{
		// Token: 0x06002887 RID: 10375 RVA: 0x000BA2C7 File Offset: 0x000B84C7
		public InvalidCredentialException()
		{
		}

		// Token: 0x06002888 RID: 10376 RVA: 0x000BA2CF File Offset: 0x000B84CF
		protected InvalidCredentialException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
		{
		}

		// Token: 0x06002889 RID: 10377 RVA: 0x000BA2D9 File Offset: 0x000B84D9
		public InvalidCredentialException(string message) : base(message)
		{
		}

		// Token: 0x0600288A RID: 10378 RVA: 0x000BA2E2 File Offset: 0x000B84E2
		public InvalidCredentialException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}

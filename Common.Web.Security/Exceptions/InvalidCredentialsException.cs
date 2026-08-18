using System;

namespace TechnoPro.Common.Web.Security.Exceptions
{
	// Token: 0x0200000D RID: 13
	[Serializable]
	public class InvalidCredentialsException : Exception
	{
		// Token: 0x06000078 RID: 120 RVA: 0x0000373C File Offset: 0x0000193C
		public InvalidCredentialsException()
		{
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00003744 File Offset: 0x00001944
		public InvalidCredentialsException(string message) : base(message)
		{
		}

		// Token: 0x0600007A RID: 122 RVA: 0x0000374D File Offset: 0x0000194D
		public InvalidCredentialsException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}

using System;
using System.Runtime.Serialization;

namespace System.IdentityModel
{
	// Token: 0x02000025 RID: 37
	[Serializable]
	public class BadRequestException : RequestException
	{
		// Token: 0x06000111 RID: 273 RVA: 0x0000579B File Offset: 0x0000399B
		public BadRequestException() : base(SR.GetString("ID2009"))
		{
		}

		// Token: 0x06000112 RID: 274 RVA: 0x000057AD File Offset: 0x000039AD
		public BadRequestException(string message) : base(message)
		{
		}

		// Token: 0x06000113 RID: 275 RVA: 0x000057B6 File Offset: 0x000039B6
		public BadRequestException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000114 RID: 276 RVA: 0x000057C0 File Offset: 0x000039C0
		protected BadRequestException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}

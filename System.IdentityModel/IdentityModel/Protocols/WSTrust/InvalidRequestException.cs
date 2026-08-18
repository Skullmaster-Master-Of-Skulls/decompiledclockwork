using System;
using System.Runtime.Serialization;

namespace System.IdentityModel.Protocols.WSTrust
{
	// Token: 0x020001F5 RID: 501
	[Serializable]
	public class InvalidRequestException : RequestException
	{
		// Token: 0x060010AC RID: 4268 RVA: 0x0004732C File Offset: 0x0004552C
		public InvalidRequestException() : base(SR.GetString("ID2005"))
		{
		}

		// Token: 0x060010AD RID: 4269 RVA: 0x000057AD File Offset: 0x000039AD
		public InvalidRequestException(string message) : base(message)
		{
		}

		// Token: 0x060010AE RID: 4270 RVA: 0x000057B6 File Offset: 0x000039B6
		public InvalidRequestException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060010AF RID: 4271 RVA: 0x000057C0 File Offset: 0x000039C0
		protected InvalidRequestException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}

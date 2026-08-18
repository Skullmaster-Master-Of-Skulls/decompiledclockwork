using System;
using System.Runtime.Serialization;

namespace System.IdentityModel
{
	// Token: 0x02000074 RID: 116
	[Serializable]
	public class SecurityMessageSerializationException : SystemException
	{
		// Token: 0x060003C8 RID: 968 RVA: 0x0000BA18 File Offset: 0x00009C18
		public SecurityMessageSerializationException()
		{
		}

		// Token: 0x060003C9 RID: 969 RVA: 0x0000BA20 File Offset: 0x00009C20
		public SecurityMessageSerializationException(string message) : base(message)
		{
		}

		// Token: 0x060003CA RID: 970 RVA: 0x0000BA29 File Offset: 0x00009C29
		public SecurityMessageSerializationException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060003CB RID: 971 RVA: 0x0000BA33 File Offset: 0x00009C33
		protected SecurityMessageSerializationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}

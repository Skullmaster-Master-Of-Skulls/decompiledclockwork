using System;
using System.Runtime.Serialization;

namespace System.IdentityModel
{
	// Token: 0x0200004C RID: 76
	[Serializable]
	public class LimitExceededException : SystemException
	{
		// Token: 0x060002D8 RID: 728 RVA: 0x0000BA18 File Offset: 0x00009C18
		public LimitExceededException()
		{
		}

		// Token: 0x060002D9 RID: 729 RVA: 0x0000BA20 File Offset: 0x00009C20
		public LimitExceededException(string message) : base(message)
		{
		}

		// Token: 0x060002DA RID: 730 RVA: 0x0000BA29 File Offset: 0x00009C29
		public LimitExceededException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060002DB RID: 731 RVA: 0x0000BA33 File Offset: 0x00009C33
		protected LimitExceededException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}

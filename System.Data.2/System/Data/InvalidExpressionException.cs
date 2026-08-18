using System;
using System.Runtime.Serialization;

namespace System.Data
{
	// Token: 0x020000F0 RID: 240
	[Serializable]
	public class InvalidExpressionException : DataException
	{
		// Token: 0x06000F9D RID: 3997 RVA: 0x0007E31C File Offset: 0x0007D71C
		protected InvalidExpressionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000F9E RID: 3998 RVA: 0x0007E334 File Offset: 0x0007D734
		public InvalidExpressionException()
		{
		}

		// Token: 0x06000F9F RID: 3999 RVA: 0x0007E348 File Offset: 0x0007D748
		public InvalidExpressionException(string s) : base(s)
		{
		}

		// Token: 0x06000FA0 RID: 4000 RVA: 0x0007E35C File Offset: 0x0007D75C
		public InvalidExpressionException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}

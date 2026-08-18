using System;
using System.Runtime.Serialization;

namespace System.Data
{
	// Token: 0x020000F2 RID: 242
	[Serializable]
	public class SyntaxErrorException : InvalidExpressionException
	{
		// Token: 0x06000FA5 RID: 4005 RVA: 0x0007E3CC File Offset: 0x0007D7CC
		protected SyntaxErrorException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000FA6 RID: 4006 RVA: 0x0007E3E4 File Offset: 0x0007D7E4
		public SyntaxErrorException()
		{
		}

		// Token: 0x06000FA7 RID: 4007 RVA: 0x0007E3F8 File Offset: 0x0007D7F8
		public SyntaxErrorException(string s) : base(s)
		{
		}

		// Token: 0x06000FA8 RID: 4008 RVA: 0x0007E40C File Offset: 0x0007D80C
		public SyntaxErrorException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}

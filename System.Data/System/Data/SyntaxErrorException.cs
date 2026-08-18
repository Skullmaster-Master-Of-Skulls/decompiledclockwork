using System;
using System.Runtime.Serialization;

namespace System.Data
{
	// Token: 0x020001B0 RID: 432
	[Serializable]
	public class SyntaxErrorException : InvalidExpressionException
	{
		// Token: 0x060018BC RID: 6332 RVA: 0x002561C8 File Offset: 0x002555C8
		protected SyntaxErrorException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060018BD RID: 6333 RVA: 0x002561E8 File Offset: 0x002555E8
		public SyntaxErrorException()
		{
		}

		// Token: 0x060018BE RID: 6334 RVA: 0x00256208 File Offset: 0x00255608
		public SyntaxErrorException(string s) : base(s)
		{
		}

		// Token: 0x060018BF RID: 6335 RVA: 0x00256228 File Offset: 0x00255628
		public SyntaxErrorException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}

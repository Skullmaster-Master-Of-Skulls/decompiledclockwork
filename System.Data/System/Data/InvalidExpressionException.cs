using System;
using System.Runtime.Serialization;

namespace System.Data
{
	// Token: 0x020001AE RID: 430
	[Serializable]
	public class InvalidExpressionException : DataException
	{
		// Token: 0x060018B4 RID: 6324 RVA: 0x002560C8 File Offset: 0x002554C8
		protected InvalidExpressionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060018B5 RID: 6325 RVA: 0x002560E8 File Offset: 0x002554E8
		public InvalidExpressionException()
		{
		}

		// Token: 0x060018B6 RID: 6326 RVA: 0x00256108 File Offset: 0x00255508
		public InvalidExpressionException(string s) : base(s)
		{
		}

		// Token: 0x060018B7 RID: 6327 RVA: 0x00256128 File Offset: 0x00255528
		public InvalidExpressionException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}

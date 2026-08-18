using System;
using System.Runtime.Serialization;

namespace System.Data
{
	// Token: 0x020001AF RID: 431
	[Serializable]
	public class EvaluateException : InvalidExpressionException
	{
		// Token: 0x060018B8 RID: 6328 RVA: 0x00256148 File Offset: 0x00255548
		protected EvaluateException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060018B9 RID: 6329 RVA: 0x00256168 File Offset: 0x00255568
		public EvaluateException()
		{
		}

		// Token: 0x060018BA RID: 6330 RVA: 0x00256188 File Offset: 0x00255588
		public EvaluateException(string s) : base(s)
		{
		}

		// Token: 0x060018BB RID: 6331 RVA: 0x002561A8 File Offset: 0x002555A8
		public EvaluateException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}

using System;
using System.Runtime.Serialization;

namespace System.Data
{
	// Token: 0x020000F1 RID: 241
	[Serializable]
	public class EvaluateException : InvalidExpressionException
	{
		// Token: 0x06000FA1 RID: 4001 RVA: 0x0007E374 File Offset: 0x0007D774
		protected EvaluateException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000FA2 RID: 4002 RVA: 0x0007E38C File Offset: 0x0007D78C
		public EvaluateException()
		{
		}

		// Token: 0x06000FA3 RID: 4003 RVA: 0x0007E3A0 File Offset: 0x0007D7A0
		public EvaluateException(string s) : base(s)
		{
		}

		// Token: 0x06000FA4 RID: 4004 RVA: 0x0007E3B4 File Offset: 0x0007D7B4
		public EvaluateException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}

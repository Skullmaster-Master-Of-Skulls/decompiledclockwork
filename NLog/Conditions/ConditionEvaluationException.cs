using System;
using System.Runtime.Serialization;

namespace NLog.Conditions
{
	// Token: 0x0200002B RID: 43
	[Serializable]
	public class ConditionEvaluationException : Exception
	{
		// Token: 0x060000BB RID: 187 RVA: 0x0000350C File Offset: 0x0000170C
		public ConditionEvaluationException()
		{
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00003514 File Offset: 0x00001714
		public ConditionEvaluationException(string message) : base(message)
		{
		}

		// Token: 0x060000BD RID: 189 RVA: 0x0000351D File Offset: 0x0000171D
		public ConditionEvaluationException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060000BE RID: 190 RVA: 0x00003527 File Offset: 0x00001727
		protected ConditionEvaluationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}

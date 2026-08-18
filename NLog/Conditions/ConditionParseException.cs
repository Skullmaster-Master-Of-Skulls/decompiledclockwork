using System;
using System.Runtime.Serialization;

namespace NLog.Conditions
{
	// Token: 0x02000038 RID: 56
	[Serializable]
	public class ConditionParseException : Exception
	{
		// Token: 0x060000F1 RID: 241 RVA: 0x00003A68 File Offset: 0x00001C68
		public ConditionParseException()
		{
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x00003A70 File Offset: 0x00001C70
		public ConditionParseException(string message) : base(message)
		{
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00003A79 File Offset: 0x00001C79
		public ConditionParseException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00003A83 File Offset: 0x00001C83
		protected ConditionParseException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}

using System;
using System.Runtime.Serialization;

namespace System.Runtime
{
	// Token: 0x02000018 RID: 24
	[Serializable]
	internal class FatalException : SystemException
	{
		// Token: 0x0600009E RID: 158 RVA: 0x00003D80 File Offset: 0x00001F80
		public FatalException()
		{
		}

		// Token: 0x0600009F RID: 159 RVA: 0x00003D88 File Offset: 0x00001F88
		public FatalException(string message) : base(message)
		{
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x00003D91 File Offset: 0x00001F91
		public FatalException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x00003D9B File Offset: 0x00001F9B
		protected FatalException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}

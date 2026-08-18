using System;
using System.Runtime.Serialization;

namespace System.Data
{
	// Token: 0x02000092 RID: 146
	[Serializable]
	public class StrongTypingException : DataException
	{
		// Token: 0x060007C4 RID: 1988 RVA: 0x00056110 File Offset: 0x00055510
		protected StrongTypingException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060007C5 RID: 1989 RVA: 0x00056128 File Offset: 0x00055528
		public StrongTypingException()
		{
			base.HResult = -2146232021;
		}

		// Token: 0x060007C6 RID: 1990 RVA: 0x00056148 File Offset: 0x00055548
		public StrongTypingException(string message) : base(message)
		{
			base.HResult = -2146232021;
		}

		// Token: 0x060007C7 RID: 1991 RVA: 0x00056168 File Offset: 0x00055568
		public StrongTypingException(string s, Exception innerException) : base(s, innerException)
		{
			base.HResult = -2146232021;
		}
	}
}

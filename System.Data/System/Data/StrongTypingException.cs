using System;
using System.Runtime.Serialization;

namespace System.Data
{
	// Token: 0x02000104 RID: 260
	[Serializable]
	public class StrongTypingException : DataException
	{
		// Token: 0x06000F3E RID: 3902 RVA: 0x0022CA78 File Offset: 0x0022BE78
		protected StrongTypingException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000F3F RID: 3903 RVA: 0x0022CA98 File Offset: 0x0022BE98
		public StrongTypingException()
		{
			base.HResult = -2146232021;
		}

		// Token: 0x06000F40 RID: 3904 RVA: 0x0022CAB8 File Offset: 0x0022BEB8
		public StrongTypingException(string message) : base(message)
		{
			base.HResult = -2146232021;
		}

		// Token: 0x06000F41 RID: 3905 RVA: 0x0022CAD8 File Offset: 0x0022BED8
		public StrongTypingException(string s, Exception innerException) : base(s, innerException)
		{
			base.HResult = -2146232021;
		}
	}
}

using System;
using System.Runtime.Serialization;

namespace System.Runtime.InteropServices
{
	// Token: 0x02000532 RID: 1330
	[ComVisible(true)]
	[Serializable]
	public class SafeArrayRankMismatchException : SystemException
	{
		// Token: 0x0600331A RID: 13082 RVA: 0x000AD340 File Offset: 0x000AC340
		public SafeArrayRankMismatchException() : base(Environment.GetResourceString("Arg_SafeArrayRankMismatchException"))
		{
			base.SetErrorCode(-2146233032);
		}

		// Token: 0x0600331B RID: 13083 RVA: 0x000AD35D File Offset: 0x000AC35D
		public SafeArrayRankMismatchException(string message) : base(message)
		{
			base.SetErrorCode(-2146233032);
		}

		// Token: 0x0600331C RID: 13084 RVA: 0x000AD371 File Offset: 0x000AC371
		public SafeArrayRankMismatchException(string message, Exception inner) : base(message, inner)
		{
			base.SetErrorCode(-2146233032);
		}

		// Token: 0x0600331D RID: 13085 RVA: 0x000AD386 File Offset: 0x000AC386
		protected SafeArrayRankMismatchException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}

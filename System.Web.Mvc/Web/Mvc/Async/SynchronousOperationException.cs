using System;
using System.Runtime.Serialization;

namespace System.Web.Mvc.Async
{
	// Token: 0x020000EF RID: 239
	[Serializable]
	public sealed class SynchronousOperationException : HttpException
	{
		// Token: 0x0600062F RID: 1583 RVA: 0x00011C74 File Offset: 0x0000FE74
		public SynchronousOperationException()
		{
		}

		// Token: 0x06000630 RID: 1584 RVA: 0x00011C7C File Offset: 0x0000FE7C
		private SynchronousOperationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x06000631 RID: 1585 RVA: 0x00011C86 File Offset: 0x0000FE86
		public SynchronousOperationException(string message) : base(message)
		{
		}

		// Token: 0x06000632 RID: 1586 RVA: 0x00011C8F File Offset: 0x0000FE8F
		public SynchronousOperationException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}

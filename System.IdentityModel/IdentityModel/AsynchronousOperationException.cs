using System;
using System.Runtime.Serialization;

namespace System.IdentityModel
{
	// Token: 0x02000023 RID: 35
	[Serializable]
	public class AsynchronousOperationException : Exception
	{
		// Token: 0x060000FF RID: 255 RVA: 0x0000543B File Offset: 0x0000363B
		public AsynchronousOperationException() : base(SR.GetString("ID4004"))
		{
		}

		// Token: 0x06000100 RID: 256 RVA: 0x0000544D File Offset: 0x0000364D
		public AsynchronousOperationException(string message) : base(message)
		{
		}

		// Token: 0x06000101 RID: 257 RVA: 0x00005456 File Offset: 0x00003656
		public AsynchronousOperationException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000102 RID: 258 RVA: 0x00005460 File Offset: 0x00003660
		public AsynchronousOperationException(Exception innerException) : base(SR.GetString("ID4004"), innerException)
		{
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00005473 File Offset: 0x00003673
		protected AsynchronousOperationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}

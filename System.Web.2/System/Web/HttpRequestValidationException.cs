using System;
using System.Runtime.Serialization;

namespace System.Web
{
	// Token: 0x0200009C RID: 156
	[Serializable]
	public sealed class HttpRequestValidationException : HttpException
	{
		// Token: 0x06000A01 RID: 2561 RVA: 0x00016C58 File Offset: 0x00014E58
		public HttpRequestValidationException()
		{
		}

		// Token: 0x06000A02 RID: 2562 RVA: 0x00017007 File Offset: 0x00015207
		public HttpRequestValidationException(string message) : base(message)
		{
			base.SetFormatter(new UnhandledErrorFormatter(this, SR.GetString("Dangerous_input_detected_descr"), null));
		}

		// Token: 0x06000A03 RID: 2563 RVA: 0x00016CA3 File Offset: 0x00014EA3
		public HttpRequestValidationException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06000A04 RID: 2564 RVA: 0x00016C99 File Offset: 0x00014E99
		private HttpRequestValidationException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}

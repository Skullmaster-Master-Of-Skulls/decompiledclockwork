using System;
using System.Runtime.Serialization;

namespace System.Web
{
	// Token: 0x02000099 RID: 153
	[Serializable]
	public sealed class HttpUnhandledException : HttpException
	{
		// Token: 0x060009E0 RID: 2528 RVA: 0x00016C58 File Offset: 0x00014E58
		public HttpUnhandledException()
		{
		}

		// Token: 0x060009E1 RID: 2529 RVA: 0x00016C60 File Offset: 0x00014E60
		public HttpUnhandledException(string message) : base(message)
		{
		}

		// Token: 0x060009E2 RID: 2530 RVA: 0x00016C69 File Offset: 0x00014E69
		public HttpUnhandledException(string message, Exception innerException) : base(message, innerException)
		{
			base.SetFormatter(new UnhandledErrorFormatter(innerException, message, null));
		}

		// Token: 0x060009E3 RID: 2531 RVA: 0x00016C81 File Offset: 0x00014E81
		internal HttpUnhandledException(string message, string postMessage, Exception innerException) : base(message, innerException)
		{
			base.SetFormatter(new UnhandledErrorFormatter(innerException, message, postMessage));
		}

		// Token: 0x060009E4 RID: 2532 RVA: 0x00016C99 File Offset: 0x00014E99
		private HttpUnhandledException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}

using System;
using System.Net.Http.Headers;
using System.Runtime.Serialization;
using System.Web.Http;

namespace System.Net.Http
{
	// Token: 0x0200001D RID: 29
	public class InvalidByteRangeException : Exception
	{
		// Token: 0x060000F4 RID: 244 RVA: 0x00004D92 File Offset: 0x00002F92
		public InvalidByteRangeException(ContentRangeHeaderValue contentRange)
		{
			this.Initialize(contentRange);
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00004DA1 File Offset: 0x00002FA1
		public InvalidByteRangeException(ContentRangeHeaderValue contentRange, string message) : base(message)
		{
			this.Initialize(contentRange);
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00004DB1 File Offset: 0x00002FB1
		public InvalidByteRangeException(ContentRangeHeaderValue contentRange, string message, Exception innerException) : base(message, innerException)
		{
			this.Initialize(contentRange);
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00004DC2 File Offset: 0x00002FC2
		public InvalidByteRangeException(ContentRangeHeaderValue contentRange, SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.Initialize(contentRange);
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000F8 RID: 248 RVA: 0x00004DD3 File Offset: 0x00002FD3
		// (set) Token: 0x060000F9 RID: 249 RVA: 0x00004DDB File Offset: 0x00002FDB
		public ContentRangeHeaderValue ContentRange { get; private set; }

		// Token: 0x060000FA RID: 250 RVA: 0x00004DE4 File Offset: 0x00002FE4
		private void Initialize(ContentRangeHeaderValue contentRange)
		{
			if (contentRange == null)
			{
				throw Error.ArgumentNull("contentRange");
			}
			this.ContentRange = contentRange;
		}
	}
}

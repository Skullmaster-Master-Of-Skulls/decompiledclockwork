using System;
using System.Net.Http.Headers;
using System.Web.Http;

namespace System.Net.Http.Formatting
{
	// Token: 0x02000030 RID: 48
	public class ContentNegotiationResult
	{
		// Token: 0x06000166 RID: 358 RVA: 0x000068A2 File Offset: 0x00004AA2
		public ContentNegotiationResult(MediaTypeFormatter formatter, MediaTypeHeaderValue mediaType)
		{
			if (formatter == null)
			{
				throw Error.ArgumentNull("formatter");
			}
			this._formatter = formatter;
			this.MediaType = mediaType;
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000167 RID: 359 RVA: 0x000068C6 File Offset: 0x00004AC6
		// (set) Token: 0x06000168 RID: 360 RVA: 0x000068CE File Offset: 0x00004ACE
		public MediaTypeFormatter Formatter
		{
			get
			{
				return this._formatter;
			}
			set
			{
				if (value == null)
				{
					throw Error.ArgumentNull("value");
				}
				this._formatter = value;
			}
		}

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000169 RID: 361 RVA: 0x000068E5 File Offset: 0x00004AE5
		// (set) Token: 0x0600016A RID: 362 RVA: 0x000068ED File Offset: 0x00004AED
		public MediaTypeHeaderValue MediaType { get; set; }

		// Token: 0x04000069 RID: 105
		private MediaTypeFormatter _formatter;
	}
}

using System;
using System.Net.Http.Headers;
using System.Web.Http;

namespace System.Net.Http
{
	// Token: 0x02000018 RID: 24
	public class UnsupportedMediaTypeException : Exception
	{
		// Token: 0x060000B3 RID: 179 RVA: 0x00004661 File Offset: 0x00002861
		public UnsupportedMediaTypeException(string message, MediaTypeHeaderValue mediaType) : base(message)
		{
			if (mediaType == null)
			{
				throw Error.ArgumentNull("mediaType");
			}
			this.MediaType = mediaType;
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000B4 RID: 180 RVA: 0x0000467F File Offset: 0x0000287F
		// (set) Token: 0x060000B5 RID: 181 RVA: 0x00004687 File Offset: 0x00002887
		public MediaTypeHeaderValue MediaType { get; private set; }
	}
}

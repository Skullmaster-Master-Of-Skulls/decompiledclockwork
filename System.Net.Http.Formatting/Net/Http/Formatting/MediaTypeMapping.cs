using System;
using System.Net.Http.Headers;
using System.Web.Http;

namespace System.Net.Http.Formatting
{
	// Token: 0x02000048 RID: 72
	public abstract class MediaTypeMapping
	{
		// Token: 0x060002AA RID: 682 RVA: 0x0000A365 File Offset: 0x00008565
		protected MediaTypeMapping(MediaTypeHeaderValue mediaType)
		{
			if (mediaType == null)
			{
				throw Error.ArgumentNull("mediaType");
			}
			this.MediaType = mediaType;
		}

		// Token: 0x060002AB RID: 683 RVA: 0x0000A382 File Offset: 0x00008582
		protected MediaTypeMapping(string mediaType)
		{
			if (string.IsNullOrWhiteSpace(mediaType))
			{
				throw Error.ArgumentNull("mediaType");
			}
			this.MediaType = new MediaTypeHeaderValue(mediaType);
		}

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060002AC RID: 684 RVA: 0x0000A3A9 File Offset: 0x000085A9
		// (set) Token: 0x060002AD RID: 685 RVA: 0x0000A3B1 File Offset: 0x000085B1
		public MediaTypeHeaderValue MediaType { get; private set; }

		// Token: 0x060002AE RID: 686
		public abstract double TryMatchMediaType(HttpRequestMessage request);
	}
}

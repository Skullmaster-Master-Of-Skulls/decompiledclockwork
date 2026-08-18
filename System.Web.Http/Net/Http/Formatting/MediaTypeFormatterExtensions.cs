using System;
using System.ComponentModel;
using System.Net.Http.Headers;

namespace System.Net.Http.Formatting
{
	// Token: 0x0200010B RID: 267
	[EditorBrowsable(EditorBrowsableState.Never)]
	public static class MediaTypeFormatterExtensions
	{
		// Token: 0x0600067B RID: 1659 RVA: 0x00015D04 File Offset: 0x00013F04
		public static void AddUriPathExtensionMapping(this MediaTypeFormatter formatter, string uriPathExtension, MediaTypeHeaderValue mediaType)
		{
			if (formatter == null)
			{
				throw new ArgumentNullException("formatter");
			}
			UriPathExtensionMapping item = new UriPathExtensionMapping(uriPathExtension, mediaType);
			formatter.MediaTypeMappings.Add(item);
		}

		// Token: 0x0600067C RID: 1660 RVA: 0x00015D34 File Offset: 0x00013F34
		public static void AddUriPathExtensionMapping(this MediaTypeFormatter formatter, string uriPathExtension, string mediaType)
		{
			if (formatter == null)
			{
				throw new ArgumentNullException("formatter");
			}
			UriPathExtensionMapping item = new UriPathExtensionMapping(uriPathExtension, mediaType);
			formatter.MediaTypeMappings.Add(item);
		}
	}
}

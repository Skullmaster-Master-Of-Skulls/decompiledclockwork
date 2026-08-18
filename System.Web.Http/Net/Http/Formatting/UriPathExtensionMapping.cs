using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Web.Http.Routing;

namespace System.Net.Http.Formatting
{
	// Token: 0x02000113 RID: 275
	public class UriPathExtensionMapping : MediaTypeMapping
	{
		// Token: 0x0600068D RID: 1677 RVA: 0x00015E60 File Offset: 0x00014060
		public UriPathExtensionMapping(string uriPathExtension, string mediaType) : base(mediaType)
		{
			this.Initialize(uriPathExtension);
		}

		// Token: 0x0600068E RID: 1678 RVA: 0x00015E70 File Offset: 0x00014070
		public UriPathExtensionMapping(string uriPathExtension, MediaTypeHeaderValue mediaType) : base(mediaType)
		{
			this.Initialize(uriPathExtension);
		}

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x0600068F RID: 1679 RVA: 0x00015E80 File Offset: 0x00014080
		// (set) Token: 0x06000690 RID: 1680 RVA: 0x00015E88 File Offset: 0x00014088
		public string UriPathExtension { get; private set; }

		// Token: 0x06000691 RID: 1681 RVA: 0x00015E94 File Offset: 0x00014094
		public override double TryMatchMediaType(HttpRequestMessage request)
		{
			if (request == null)
			{
				throw new ArgumentNullException("request");
			}
			string uriPathExtensionOrNull = UriPathExtensionMapping.GetUriPathExtensionOrNull(request);
			if (!string.Equals(uriPathExtensionOrNull, this.UriPathExtension, StringComparison.OrdinalIgnoreCase))
			{
				return 0.0;
			}
			return 1.0;
		}

		// Token: 0x06000692 RID: 1682 RVA: 0x00015ED8 File Offset: 0x000140D8
		private static string GetUriPathExtensionOrNull(HttpRequestMessage request)
		{
			IHttpRouteData routeData = request.GetRouteData();
			string result;
			if (routeData != null && routeData.Values.TryGetValue(UriPathExtensionMapping.UriPathExtensionKey, out result))
			{
				return result;
			}
			return null;
		}

		// Token: 0x06000693 RID: 1683 RVA: 0x00015F08 File Offset: 0x00014108
		private void Initialize(string uriPathExtension)
		{
			if (string.IsNullOrWhiteSpace(uriPathExtension))
			{
				throw new ArgumentNullException("uriPathExtension");
			}
			this.UriPathExtension = uriPathExtension.Trim().TrimStart(new char[]
			{
				'.'
			});
		}

		// Token: 0x040001D4 RID: 468
		public static readonly string UriPathExtensionKey = "ext";
	}
}

using System;
using System.Globalization;
using System.IO;
using System.Web.WebPages.Resources;
using Microsoft.Internal.Web.Utils;

namespace System.Web.WebPages.ApplicationParts
{
	// Token: 0x0200000F RID: 15
	internal class ResourceHandler : IHttpHandler
	{
		// Token: 0x0600005E RID: 94 RVA: 0x00002E9C File Offset: 0x0000109C
		public ResourceHandler(ApplicationPart applicationPart, string path)
		{
			if (applicationPart == null)
			{
				throw new ArgumentNullException("applicationPart");
			}
			if (string.IsNullOrEmpty(path))
			{
				throw new ArgumentException(CommonResources.Argument_Cannot_Be_Null_Or_Empty, "path");
			}
			this._applicationPart = applicationPart;
			this._path = path;
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600005F RID: 95 RVA: 0x00002ED8 File Offset: 0x000010D8
		public bool IsReusable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06000060 RID: 96 RVA: 0x00002EDB File Offset: 0x000010DB
		public void ProcessRequest(HttpContext context)
		{
			this.ProcessRequest(new HttpResponseWrapper(context.Response));
		}

		// Token: 0x06000061 RID: 97 RVA: 0x00002EF0 File Offset: 0x000010F0
		internal void ProcessRequest(HttpResponseBase response)
		{
			string text = this._path;
			if (!text.StartsWith("~/", StringComparison.Ordinal))
			{
				text = "~/" + text;
			}
			using (Stream resourceStream = this._applicationPart.GetResourceStream(text))
			{
				if (resourceStream == null)
				{
					throw new HttpException(404, string.Format(CultureInfo.CurrentCulture, WebPageResources.ApplicationPart_ResourceNotFound, new object[]
					{
						this._path
					}));
				}
				response.ContentType = MimeMapping.GetMimeMapping(text);
				resourceStream.CopyTo(response.OutputStream);
			}
		}

		// Token: 0x0400001A RID: 26
		private readonly string _path;

		// Token: 0x0400001B RID: 27
		private readonly ApplicationPart _applicationPart;
	}
}

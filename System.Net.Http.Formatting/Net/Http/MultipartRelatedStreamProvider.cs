using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http.Headers;
using System.Web.Http;

namespace System.Net.Http
{
	// Token: 0x0200001E RID: 30
	public class MultipartRelatedStreamProvider : MultipartStreamProvider
	{
		// Token: 0x1700002B RID: 43
		// (get) Token: 0x060000FB RID: 251 RVA: 0x00004DFB File Offset: 0x00002FFB
		public HttpContent RootContent
		{
			get
			{
				if (this._rootContent == null)
				{
					this._rootContent = MultipartRelatedStreamProvider.FindRootContent(this._parent, base.Contents);
				}
				return this._rootContent;
			}
		}

		// Token: 0x060000FC RID: 252 RVA: 0x00004E22 File Offset: 0x00003022
		public override Stream GetStream(HttpContent parent, HttpContentHeaders headers)
		{
			if (parent == null)
			{
				throw Error.ArgumentNull("parent");
			}
			if (headers == null)
			{
				throw Error.ArgumentNull("headers");
			}
			if (this._parent == null)
			{
				this._parent = parent;
			}
			return new MemoryStream();
		}

		// Token: 0x060000FD RID: 253 RVA: 0x00004E98 File Offset: 0x00003098
		private static HttpContent FindRootContent(HttpContent parent, IEnumerable<HttpContent> children)
		{
			NameValueHeaderValue nameValueHeaderValue = MultipartRelatedStreamProvider.FindMultipartRelatedParameter(parent, "Start");
			if (nameValueHeaderValue == null)
			{
				return children.FirstOrDefault<HttpContent>();
			}
			string startValue = FormattingUtilities.UnquoteToken(nameValueHeaderValue.Value);
			return children.FirstOrDefault(delegate(HttpContent content)
			{
				IEnumerable<string> source;
				return content.Headers.TryGetValues("Content-ID", out source) && string.Equals(FormattingUtilities.UnquoteToken(source.ElementAt(0)), startValue, StringComparison.OrdinalIgnoreCase);
			});
		}

		// Token: 0x060000FE RID: 254 RVA: 0x00004F00 File Offset: 0x00003100
		private static NameValueHeaderValue FindMultipartRelatedParameter(HttpContent content, string parameterName)
		{
			if (content == null)
			{
				return null;
			}
			MediaTypeHeaderValue contentType = content.Headers.ContentType;
			if (contentType == null || !content.IsMimeMultipartContent("related"))
			{
				return null;
			}
			return contentType.Parameters.FirstOrDefault((NameValueHeaderValue nvp) => string.Equals(nvp.Name, parameterName, StringComparison.OrdinalIgnoreCase));
		}

		// Token: 0x04000041 RID: 65
		private const string RelatedSubType = "related";

		// Token: 0x04000042 RID: 66
		private const string ContentID = "Content-ID";

		// Token: 0x04000043 RID: 67
		private const string StartParameter = "Start";

		// Token: 0x04000044 RID: 68
		private HttpContent _rootContent;

		// Token: 0x04000045 RID: 69
		private HttpContent _parent;
	}
}

using System;
using System.Net.Http.Headers;

namespace System.Net.Http
{
	// Token: 0x02000008 RID: 8
	[__DynamicallyInvokable]
	public class MultipartFormDataContent : MultipartContent
	{
		// Token: 0x06000050 RID: 80 RVA: 0x00002D73 File Offset: 0x00000F73
		[__DynamicallyInvokable]
		public MultipartFormDataContent() : base("form-data")
		{
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002D80 File Offset: 0x00000F80
		[__DynamicallyInvokable]
		public MultipartFormDataContent(string boundary) : base("form-data", boundary)
		{
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002D8E File Offset: 0x00000F8E
		[__DynamicallyInvokable]
		public override void Add(HttpContent content)
		{
			if (content == null)
			{
				throw new ArgumentNullException("content");
			}
			if (content.Headers.ContentDisposition == null)
			{
				content.Headers.ContentDisposition = new ContentDispositionHeaderValue("form-data");
			}
			base.Add(content);
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002DC7 File Offset: 0x00000FC7
		[__DynamicallyInvokable]
		public void Add(HttpContent content, string name)
		{
			if (content == null)
			{
				throw new ArgumentNullException("content");
			}
			if (string.IsNullOrWhiteSpace(name))
			{
				throw new ArgumentException(SR.net_http_argument_empty_string, "name");
			}
			this.AddInternal(content, name, null);
		}

		// Token: 0x06000054 RID: 84 RVA: 0x00002DF8 File Offset: 0x00000FF8
		[__DynamicallyInvokable]
		public void Add(HttpContent content, string name, string fileName)
		{
			if (content == null)
			{
				throw new ArgumentNullException("content");
			}
			if (string.IsNullOrWhiteSpace(name))
			{
				throw new ArgumentException(SR.net_http_argument_empty_string, "name");
			}
			if (string.IsNullOrWhiteSpace(fileName))
			{
				throw new ArgumentException(SR.net_http_argument_empty_string, "fileName");
			}
			this.AddInternal(content, name, fileName);
		}

		// Token: 0x06000055 RID: 85 RVA: 0x00002E4C File Offset: 0x0000104C
		private void AddInternal(HttpContent content, string name, string fileName)
		{
			if (content.Headers.ContentDisposition == null)
			{
				ContentDispositionHeaderValue contentDispositionHeaderValue = new ContentDispositionHeaderValue("form-data");
				contentDispositionHeaderValue.Name = name;
				contentDispositionHeaderValue.FileName = fileName;
				contentDispositionHeaderValue.FileNameStar = fileName;
				content.Headers.ContentDisposition = contentDispositionHeaderValue;
			}
			base.Add(content);
		}

		// Token: 0x04000058 RID: 88
		private const string formData = "form-data";
	}
}

using System;
using System.Net.Http.Headers;
using System.Text;

namespace System.Net.Http
{
	// Token: 0x0200001B RID: 27
	[__DynamicallyInvokable]
	public class StringContent : ByteArrayContent
	{
		// Token: 0x06000169 RID: 361 RVA: 0x00006848 File Offset: 0x00004A48
		[__DynamicallyInvokable]
		public StringContent(string content) : this(content, null, null)
		{
		}

		// Token: 0x0600016A RID: 362 RVA: 0x00006853 File Offset: 0x00004A53
		[__DynamicallyInvokable]
		public StringContent(string content, Encoding encoding) : this(content, encoding, null)
		{
		}

		// Token: 0x0600016B RID: 363 RVA: 0x00006860 File Offset: 0x00004A60
		[__DynamicallyInvokable]
		public StringContent(string content, Encoding encoding, string mediaType) : base(StringContent.GetContentByteArray(content, encoding))
		{
			MediaTypeHeaderValue mediaTypeHeaderValue = new MediaTypeHeaderValue((mediaType == null) ? "text/plain" : mediaType);
			mediaTypeHeaderValue.CharSet = ((encoding == null) ? HttpContent.DefaultStringEncoding.WebName : encoding.WebName);
			base.Headers.ContentType = mediaTypeHeaderValue;
		}

		// Token: 0x0600016C RID: 364 RVA: 0x000068B2 File Offset: 0x00004AB2
		private static byte[] GetContentByteArray(string content, Encoding encoding)
		{
			if (content == null)
			{
				throw new ArgumentNullException("content");
			}
			if (encoding == null)
			{
				encoding = HttpContent.DefaultStringEncoding;
			}
			return encoding.GetBytes(content);
		}

		// Token: 0x040000CD RID: 205
		private const string defaultMediaType = "text/plain";
	}
}

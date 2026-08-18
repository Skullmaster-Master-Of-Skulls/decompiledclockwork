using System;
using System.IO;

namespace Telerik.Web.UI.AsyncUpload
{
	// Token: 0x02000136 RID: 310
	internal class ContentAppender : ITempFileAppender
	{
		// Token: 0x06000CCD RID: 3277 RVA: 0x0002DCD3 File Offset: 0x0002BED3
		public ContentAppender()
		{
		}

		// Token: 0x06000CCE RID: 3278 RVA: 0x0002DCDB File Offset: 0x0002BEDB
		public ContentAppender(Stream content)
		{
			this._content = content;
		}

		// Token: 0x17000469 RID: 1129
		// (get) Token: 0x06000CCF RID: 3279 RVA: 0x0002DCEA File Offset: 0x0002BEEA
		public long AppendedContentLength
		{
			get
			{
				return ContentAppender.ContentLength;
			}
		}

		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x06000CD0 RID: 3280 RVA: 0x0002DCF1 File Offset: 0x0002BEF1
		// (set) Token: 0x06000CD1 RID: 3281 RVA: 0x0002DCF8 File Offset: 0x0002BEF8
		public static long ContentLength { get; set; }

		// Token: 0x06000CD2 RID: 3282 RVA: 0x0002DD00 File Offset: 0x0002BF00
		public void AppendTo(string fullPath)
		{
			ContentAppender.AppendToFile(fullPath, this._content, 1);
		}

		// Token: 0x06000CD3 RID: 3283 RVA: 0x0002DD10 File Offset: 0x0002BF10
		public static void AppendToFile(string fullPath, Stream content, int retries = 1)
		{
			try
			{
				using (FileStream fileStream = new FileStream(fullPath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
				{
					try
					{
						ContentAppender.ContentLength = fileStream.Length + content.Length;
						StreamExtensions.CopyTo(content, fileStream);
					}
					finally
					{
						if (content != null)
						{
							((IDisposable)content).Dispose();
						}
					}
				}
			}
			catch (IOException ex)
			{
				if (retries >= 10)
				{
					throw ex;
				}
				ContentAppender.AppendToFile(fullPath, content, retries + 1);
			}
		}

		// Token: 0x0400031A RID: 794
		private const int MaxRetries = 10;

		// Token: 0x0400031B RID: 795
		private readonly Stream _content;
	}
}

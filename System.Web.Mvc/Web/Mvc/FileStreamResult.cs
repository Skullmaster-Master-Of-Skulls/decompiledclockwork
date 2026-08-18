using System;
using System.IO;

namespace System.Web.Mvc
{
	// Token: 0x020001A2 RID: 418
	public class FileStreamResult : FileResult
	{
		// Token: 0x06000BB3 RID: 2995 RVA: 0x0001EA81 File Offset: 0x0001CC81
		public FileStreamResult(Stream fileStream, string contentType) : base(contentType)
		{
			if (fileStream == null)
			{
				throw new ArgumentNullException("fileStream");
			}
			this.FileStream = fileStream;
		}

		// Token: 0x170002B4 RID: 692
		// (get) Token: 0x06000BB4 RID: 2996 RVA: 0x0001EA9F File Offset: 0x0001CC9F
		// (set) Token: 0x06000BB5 RID: 2997 RVA: 0x0001EAA7 File Offset: 0x0001CCA7
		public Stream FileStream { get; private set; }

		// Token: 0x06000BB6 RID: 2998 RVA: 0x0001EAB0 File Offset: 0x0001CCB0
		protected override void WriteFile(HttpResponseBase response)
		{
			Stream outputStream = response.OutputStream;
			using (this.FileStream)
			{
				byte[] buffer = new byte[4096];
				for (;;)
				{
					int num = this.FileStream.Read(buffer, 0, 4096);
					if (num == 0)
					{
						break;
					}
					outputStream.Write(buffer, 0, num);
				}
			}
		}

		// Token: 0x04000318 RID: 792
		private const int BufferSize = 4096;
	}
}

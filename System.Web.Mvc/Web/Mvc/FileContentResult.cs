using System;

namespace System.Web.Mvc
{
	// Token: 0x020001A0 RID: 416
	public class FileContentResult : FileResult
	{
		// Token: 0x06000BAB RID: 2987 RVA: 0x0001E9EF File Offset: 0x0001CBEF
		public FileContentResult(byte[] fileContents, string contentType) : base(contentType)
		{
			if (fileContents == null)
			{
				throw new ArgumentNullException("fileContents");
			}
			this.FileContents = fileContents;
		}

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06000BAC RID: 2988 RVA: 0x0001EA0D File Offset: 0x0001CC0D
		// (set) Token: 0x06000BAD RID: 2989 RVA: 0x0001EA15 File Offset: 0x0001CC15
		public byte[] FileContents { get; private set; }

		// Token: 0x06000BAE RID: 2990 RVA: 0x0001EA1E File Offset: 0x0001CC1E
		protected override void WriteFile(HttpResponseBase response)
		{
			response.OutputStream.Write(this.FileContents, 0, this.FileContents.Length);
		}
	}
}

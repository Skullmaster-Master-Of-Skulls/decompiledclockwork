using System;
using System.Web.Mvc.Properties;

namespace System.Web.Mvc
{
	// Token: 0x020001A1 RID: 417
	public class FilePathResult : FileResult
	{
		// Token: 0x06000BAF RID: 2991 RVA: 0x0001EA3A File Offset: 0x0001CC3A
		public FilePathResult(string fileName, string contentType) : base(contentType)
		{
			if (string.IsNullOrEmpty(fileName))
			{
				throw new ArgumentException(MvcResources.Common_NullOrEmpty, "fileName");
			}
			this.FileName = fileName;
		}

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x06000BB0 RID: 2992 RVA: 0x0001EA62 File Offset: 0x0001CC62
		// (set) Token: 0x06000BB1 RID: 2993 RVA: 0x0001EA6A File Offset: 0x0001CC6A
		public string FileName { get; private set; }

		// Token: 0x06000BB2 RID: 2994 RVA: 0x0001EA73 File Offset: 0x0001CC73
		protected override void WriteFile(HttpResponseBase response)
		{
			response.TransmitFile(this.FileName);
		}
	}
}

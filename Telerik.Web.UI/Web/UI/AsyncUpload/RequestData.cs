using System;
using System.Collections.Specialized;
using System.Web;

namespace Telerik.Web.UI.AsyncUpload
{
	// Token: 0x0200018C RID: 396
	internal class RequestData : IRequestData
	{
		// Token: 0x17000499 RID: 1177
		// (get) Token: 0x06000D96 RID: 3478 RVA: 0x00033E84 File Offset: 0x00032084
		// (set) Token: 0x06000D97 RID: 3479 RVA: 0x00033E8C File Offset: 0x0003208C
		public NameValueCollection FormValues { get; set; }

		// Token: 0x1700049A RID: 1178
		// (get) Token: 0x06000D98 RID: 3480 RVA: 0x00033E95 File Offset: 0x00032095
		// (set) Token: 0x06000D99 RID: 3481 RVA: 0x00033E9D File Offset: 0x0003209D
		public UploadedFile UploadedFile { get; set; }

		// Token: 0x06000D9A RID: 3482 RVA: 0x00033EA6 File Offset: 0x000320A6
		public RequestData()
		{
		}

		// Token: 0x06000D9B RID: 3483 RVA: 0x00033EB0 File Offset: 0x000320B0
		public RequestData(HttpContext context)
		{
			this.FormValues = context.Request.Form;
			try
			{
				this.UploadedFile = UploadedFile.FromHttpPostedFile(context.Request.Files[0]);
			}
			catch (ArgumentOutOfRangeException)
			{
				throw new AsyncUploadHandlerExeption("RadAsyncUpload handler is registered successfully, however, it may not be accessed directly.");
			}
		}
	}
}

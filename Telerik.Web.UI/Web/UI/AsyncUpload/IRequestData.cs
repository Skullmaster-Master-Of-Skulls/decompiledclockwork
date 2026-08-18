using System;
using System.Collections.Specialized;

namespace Telerik.Web.UI.AsyncUpload
{
	// Token: 0x0200018B RID: 395
	public interface IRequestData
	{
		// Token: 0x17000497 RID: 1175
		// (get) Token: 0x06000D92 RID: 3474
		// (set) Token: 0x06000D93 RID: 3475
		NameValueCollection FormValues { get; set; }

		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x06000D94 RID: 3476
		// (set) Token: 0x06000D95 RID: 3477
		UploadedFile UploadedFile { get; set; }
	}
}

using System;

namespace Telerik.Web.UI
{
	// Token: 0x02000077 RID: 119
	public interface IAsyncUploadResult
	{
		// Token: 0x170001CD RID: 461
		// (get) Token: 0x060004E3 RID: 1251
		// (set) Token: 0x060004E4 RID: 1252
		string FileName { get; set; }

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x060004E5 RID: 1253
		// (set) Token: 0x060004E6 RID: 1254
		string ContentType { get; set; }

		// Token: 0x170001CF RID: 463
		// (get) Token: 0x060004E7 RID: 1255
		// (set) Token: 0x060004E8 RID: 1256
		long ContentLength { get; set; }
	}
}

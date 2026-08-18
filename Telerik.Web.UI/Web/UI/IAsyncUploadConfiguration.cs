using System;

namespace Telerik.Web.UI
{
	// Token: 0x02000072 RID: 114
	public interface IAsyncUploadConfiguration
	{
		// Token: 0x170001B6 RID: 438
		// (get) Token: 0x0600049A RID: 1178
		// (set) Token: 0x0600049B RID: 1179
		string TargetFolder { get; set; }

		// Token: 0x170001B7 RID: 439
		// (get) Token: 0x0600049C RID: 1180
		// (set) Token: 0x0600049D RID: 1181
		string TempTargetFolder { get; set; }

		// Token: 0x170001B8 RID: 440
		// (get) Token: 0x0600049E RID: 1182
		// (set) Token: 0x0600049F RID: 1183
		int MaxFileSize { get; set; }

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x060004A0 RID: 1184
		// (set) Token: 0x060004A1 RID: 1185
		TimeSpan TimeToLive { get; set; }
	}
}

using System;

namespace Telerik.Web.UI
{
	// Token: 0x02000073 RID: 115
	[Serializable]
	public class AsyncUploadConfiguration : IAsyncUploadConfiguration
	{
		// Token: 0x170001BA RID: 442
		// (get) Token: 0x060004A2 RID: 1186 RVA: 0x0000BD08 File Offset: 0x00009F08
		// (set) Token: 0x060004A3 RID: 1187 RVA: 0x0000BD10 File Offset: 0x00009F10
		public string TargetFolder { get; set; }

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x060004A4 RID: 1188 RVA: 0x0000BD19 File Offset: 0x00009F19
		// (set) Token: 0x060004A5 RID: 1189 RVA: 0x0000BD21 File Offset: 0x00009F21
		public string TempTargetFolder { get; set; }

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x060004A6 RID: 1190 RVA: 0x0000BD2A File Offset: 0x00009F2A
		// (set) Token: 0x060004A7 RID: 1191 RVA: 0x0000BD32 File Offset: 0x00009F32
		public int MaxFileSize { get; set; }

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x060004A8 RID: 1192 RVA: 0x0000BD3B File Offset: 0x00009F3B
		// (set) Token: 0x060004A9 RID: 1193 RVA: 0x0000BD43 File Offset: 0x00009F43
		public TimeSpan TimeToLive { get; set; }

		// Token: 0x170001BE RID: 446
		// (get) Token: 0x060004AA RID: 1194 RVA: 0x0000BD4C File Offset: 0x00009F4C
		// (set) Token: 0x060004AB RID: 1195 RVA: 0x0000BD54 File Offset: 0x00009F54
		public bool UseApplicationPoolImpersonation { get; set; }

		// Token: 0x170001BF RID: 447
		// (get) Token: 0x060004AC RID: 1196 RVA: 0x0000BD5D File Offset: 0x00009F5D
		// (set) Token: 0x060004AD RID: 1197 RVA: 0x0000BD65 File Offset: 0x00009F65
		public string[] AllowedFileExtensions { get; set; }

		// Token: 0x060004AE RID: 1198 RVA: 0x0000BD6E File Offset: 0x00009F6E
		public AsyncUploadConfiguration()
		{
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x0000BD76 File Offset: 0x00009F76
		public AsyncUploadConfiguration(string targetFolder, string tempTargetFolder, int maxFileSize, TimeSpan timeToLive)
		{
			this.TargetFolder = targetFolder;
			this.TempTargetFolder = tempTargetFolder;
			this.MaxFileSize = maxFileSize;
			this.TimeToLive = timeToLive;
		}
	}
}

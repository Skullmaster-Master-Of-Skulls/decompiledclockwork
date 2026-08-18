using System;
using TechnoPro.Common.Public.Entities.FTP;

namespace TechnoPro.Common.Public.Entities.Updates
{
	// Token: 0x0200014B RID: 331
	public class UpdateRequiredResp : BusinessBase<string>
	{
		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x060007E4 RID: 2020 RVA: 0x0001149C File Offset: 0x0000F69C
		// (set) Token: 0x060007E5 RID: 2021 RVA: 0x000114B9 File Offset: 0x0000F6B9
		public override string Id
		{
			get
			{
				return this.ServerFileInfo.Filename;
			}
			set
			{
				this.ServerFileInfo.Filename = value;
			}
		}

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x060007E6 RID: 2022 RVA: 0x000114C9 File Offset: 0x0000F6C9
		// (set) Token: 0x060007E7 RID: 2023 RVA: 0x000114D1 File Offset: 0x0000F6D1
		public bool IsUpdateRequired { get; set; }

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x060007E8 RID: 2024 RVA: 0x000114DA File Offset: 0x0000F6DA
		// (set) Token: 0x060007E9 RID: 2025 RVA: 0x000114E2 File Offset: 0x0000F6E2
		public string CurrentVersionOnServer { get; set; }

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x060007EA RID: 2026 RVA: 0x000114EB File Offset: 0x0000F6EB
		// (set) Token: 0x060007EB RID: 2027 RVA: 0x000114F3 File Offset: 0x0000F6F3
		public FtpFileInfo ServerFileInfo { get; set; }

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x060007EC RID: 2028 RVA: 0x000114FC File Offset: 0x0000F6FC
		// (set) Token: 0x060007ED RID: 2029 RVA: 0x00011504 File Offset: 0x0000F704
		public bool IsSecondaryUpdate { get; set; }
	}
}

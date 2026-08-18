using System;

namespace TechnoPro.Common.Public.Entities.Reports
{
	// Token: 0x0200021F RID: 543
	public class FormattedReport : BusinessBase<int>
	{
		// Token: 0x170006C2 RID: 1730
		// (get) Token: 0x06001081 RID: 4225 RVA: 0x00017868 File Offset: 0x00015A68
		// (set) Token: 0x06001082 RID: 4226 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int ReportFileId
		{
			get
			{
				return this.Id;
			}
			set
			{
				this.Id = value;
			}
		}

		// Token: 0x170006C3 RID: 1731
		// (get) Token: 0x06001083 RID: 4227 RVA: 0x00017880 File Offset: 0x00015A80
		// (set) Token: 0x06001084 RID: 4228 RVA: 0x00017888 File Offset: 0x00015A88
		public string Title { get; set; }

		// Token: 0x170006C4 RID: 1732
		// (get) Token: 0x06001085 RID: 4229 RVA: 0x00017891 File Offset: 0x00015A91
		// (set) Token: 0x06001086 RID: 4230 RVA: 0x00017899 File Offset: 0x00015A99
		public string Description { get; set; }

		// Token: 0x170006C5 RID: 1733
		// (get) Token: 0x06001087 RID: 4231 RVA: 0x000178A2 File Offset: 0x00015AA2
		// (set) Token: 0x06001088 RID: 4232 RVA: 0x000178AA File Offset: 0x00015AAA
		public byte[] FormattedReportTemplate { get; set; }

		// Token: 0x170006C6 RID: 1734
		// (get) Token: 0x06001089 RID: 4233 RVA: 0x000178B3 File Offset: 0x00015AB3
		// (set) Token: 0x0600108A RID: 4234 RVA: 0x000178BB File Offset: 0x00015ABB
		public string FileChecksum { get; set; }

		// Token: 0x170006C7 RID: 1735
		// (get) Token: 0x0600108B RID: 4235 RVA: 0x000178C4 File Offset: 0x00015AC4
		// (set) Token: 0x0600108C RID: 4236 RVA: 0x000178CC File Offset: 0x00015ACC
		public int OrderNum { get; set; }
	}
}

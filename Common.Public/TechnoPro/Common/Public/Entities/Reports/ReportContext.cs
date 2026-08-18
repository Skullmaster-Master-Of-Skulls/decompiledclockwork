using System;
using System.Collections.Generic;

namespace TechnoPro.Common.Public.Entities.Reports
{
	// Token: 0x02000216 RID: 534
	public class ReportContext
	{
		// Token: 0x06001042 RID: 4162 RVA: 0x00017640 File Offset: 0x00015840
		public ReportContext()
		{
			this.ReportSource = eReportSource.All;
		}

		// Token: 0x170006A6 RID: 1702
		// (get) Token: 0x06001043 RID: 4163 RVA: 0x00017652 File Offset: 0x00015852
		// (set) Token: 0x06001044 RID: 4164 RVA: 0x0001765A File Offset: 0x0001585A
		public IList<int> ReportIds { get; set; }

		// Token: 0x170006A7 RID: 1703
		// (get) Token: 0x06001045 RID: 4165 RVA: 0x00017663 File Offset: 0x00015863
		// (set) Token: 0x06001046 RID: 4166 RVA: 0x0001766B File Offset: 0x0001586B
		public IList<int> ReportGroupIds { get; set; }

		// Token: 0x170006A8 RID: 1704
		// (get) Token: 0x06001047 RID: 4167 RVA: 0x00017674 File Offset: 0x00015874
		// (set) Token: 0x06001048 RID: 4168 RVA: 0x0001767C File Offset: 0x0001587C
		public eReportSource ReportSource { get; set; }

		// Token: 0x170006A9 RID: 1705
		// (get) Token: 0x06001049 RID: 4169 RVA: 0x00017685 File Offset: 0x00015885
		// (set) Token: 0x0600104A RID: 4170 RVA: 0x0001768D File Offset: 0x0001588D
		public bool ReturnReportDisplayInformationOnly { get; set; }

		// Token: 0x170006AA RID: 1706
		// (get) Token: 0x0600104B RID: 4171 RVA: 0x00017696 File Offset: 0x00015896
		// (set) Token: 0x0600104C RID: 4172 RVA: 0x0001769E File Offset: 0x0001589E
		public string ReportXmlStore { get; set; }
	}
}

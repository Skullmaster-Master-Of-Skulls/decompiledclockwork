using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.Reports;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Reports
{
	// Token: 0x0200033D RID: 829
	[DataContract(Namespace = "http://tpro.ca")]
	public class ReportContextDTO
	{
		// Token: 0x060012B3 RID: 4787 RVA: 0x00008B70 File Offset: 0x00006D70
		public ReportContextDTO()
		{
			this.ReportSource = eReportSource.All;
			this.ReturnReportDisplayInformationOnly = false;
		}

		// Token: 0x17000584 RID: 1412
		// (get) Token: 0x060012B4 RID: 4788 RVA: 0x00008B8A File Offset: 0x00006D8A
		// (set) Token: 0x060012B5 RID: 4789 RVA: 0x00008B92 File Offset: 0x00006D92
		[DataMember]
		public IList<int> ReportIds { get; set; }

		// Token: 0x17000585 RID: 1413
		// (get) Token: 0x060012B6 RID: 4790 RVA: 0x00008B9B File Offset: 0x00006D9B
		// (set) Token: 0x060012B7 RID: 4791 RVA: 0x00008BA3 File Offset: 0x00006DA3
		[DataMember]
		public IList<int> ReportGroupIds { get; set; }

		// Token: 0x17000586 RID: 1414
		// (get) Token: 0x060012B8 RID: 4792 RVA: 0x00008BAC File Offset: 0x00006DAC
		// (set) Token: 0x060012B9 RID: 4793 RVA: 0x00008BB4 File Offset: 0x00006DB4
		[DataMember]
		public eReportSource ReportSource { get; set; }

		// Token: 0x17000587 RID: 1415
		// (get) Token: 0x060012BA RID: 4794 RVA: 0x00008BBD File Offset: 0x00006DBD
		// (set) Token: 0x060012BB RID: 4795 RVA: 0x00008BC5 File Offset: 0x00006DC5
		[DataMember]
		public bool ReturnReportDisplayInformationOnly { get; set; }

		// Token: 0x17000588 RID: 1416
		// (get) Token: 0x060012BC RID: 4796 RVA: 0x00008BCE File Offset: 0x00006DCE
		// (set) Token: 0x060012BD RID: 4797 RVA: 0x00008BD6 File Offset: 0x00006DD6
		[DataMember]
		public string ReportXmlStore { get; set; }
	}
}

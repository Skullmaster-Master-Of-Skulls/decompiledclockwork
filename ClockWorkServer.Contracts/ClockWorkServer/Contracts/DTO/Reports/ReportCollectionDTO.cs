using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Reports
{
	// Token: 0x0200033B RID: 827
	[DataContract(Namespace = "http://tpro.ca")]
	public class ReportCollectionDTO
	{
		// Token: 0x1700057C RID: 1404
		// (get) Token: 0x060012A2 RID: 4770 RVA: 0x00008AC7 File Offset: 0x00006CC7
		// (set) Token: 0x060012A3 RID: 4771 RVA: 0x00008ACF File Offset: 0x00006CCF
		[DataMember]
		public IList<ReportDTO> Reports { get; set; }

		// Token: 0x1700057D RID: 1405
		// (get) Token: 0x060012A4 RID: 4772 RVA: 0x00008AD8 File Offset: 0x00006CD8
		// (set) Token: 0x060012A5 RID: 4773 RVA: 0x00008AE0 File Offset: 0x00006CE0
		[DataMember]
		public IList<ReportGroupDTO> ReportGroups { get; set; }

		// Token: 0x1700057E RID: 1406
		// (get) Token: 0x060012A6 RID: 4774 RVA: 0x00008AEC File Offset: 0x00006CEC
		public static ReportCollectionDTO Empty
		{
			get
			{
				return new ReportCollectionDTO
				{
					Reports = new List<ReportDTO>(),
					ReportGroups = new List<ReportGroupDTO>()
				};
			}
		}
	}
}

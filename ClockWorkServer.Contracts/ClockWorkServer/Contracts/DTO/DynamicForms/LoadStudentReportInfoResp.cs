using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms.DynamicDataForReports.StudentReportInfo;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x02000643 RID: 1603
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadStudentReportInfoResp
	{
		// Token: 0x17000AF5 RID: 2805
		// (get) Token: 0x060020AA RID: 8362 RVA: 0x0000EDA6 File Offset: 0x0000CFA6
		// (set) Token: 0x060020AB RID: 8363 RVA: 0x0000EDAE File Offset: 0x0000CFAE
		[DataMember]
		public List<StudentInfoItemBaseDTO>[] Items { get; set; }
	}
}

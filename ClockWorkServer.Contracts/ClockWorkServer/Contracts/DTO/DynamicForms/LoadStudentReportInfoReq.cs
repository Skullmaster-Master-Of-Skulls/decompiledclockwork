using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.DynamicForms;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x02000642 RID: 1602
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadStudentReportInfoReq : BaseMessageReq
	{
		// Token: 0x17000AF2 RID: 2802
		// (get) Token: 0x060020A3 RID: 8355 RVA: 0x0000ED73 File Offset: 0x0000CF73
		// (set) Token: 0x060020A4 RID: 8356 RVA: 0x0000ED7B File Offset: 0x0000CF7B
		[DataMember]
		public int[] StudentPersonIds { get; set; }

		// Token: 0x17000AF3 RID: 2803
		// (get) Token: 0x060020A5 RID: 8357 RVA: 0x0000ED84 File Offset: 0x0000CF84
		// (set) Token: 0x060020A6 RID: 8358 RVA: 0x0000ED8C File Offset: 0x0000CF8C
		[DataMember]
		public eDynamicStudentReportInfoType[] TypesToLoad { get; set; }

		// Token: 0x17000AF4 RID: 2804
		// (get) Token: 0x060020A7 RID: 8359 RVA: 0x0000ED95 File Offset: 0x0000CF95
		// (set) Token: 0x060020A8 RID: 8360 RVA: 0x0000ED9D File Offset: 0x0000CF9D
		[DataMember]
		public IDictionary<eDynamicStudentReportInfoType, int> ControlIds { get; set; }
	}
}

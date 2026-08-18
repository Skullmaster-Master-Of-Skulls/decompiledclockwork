using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.Reports;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Reports
{
	// Token: 0x0200033F RID: 831
	[DataContract(Namespace = "http://tpro.ca")]
	public class ReportFunctionDTO
	{
		// Token: 0x170005A0 RID: 1440
		// (get) Token: 0x060012ED RID: 4845 RVA: 0x00008D66 File Offset: 0x00006F66
		// (set) Token: 0x060012EE RID: 4846 RVA: 0x00008D6E File Offset: 0x00006F6E
		[DataMember]
		public int ReportFunctionId { get; set; }

		// Token: 0x170005A1 RID: 1441
		// (get) Token: 0x060012EF RID: 4847 RVA: 0x00008D77 File Offset: 0x00006F77
		// (set) Token: 0x060012F0 RID: 4848 RVA: 0x00008D7F File Offset: 0x00006F7F
		[DataMember]
		public string Title { get; set; }

		// Token: 0x170005A2 RID: 1442
		// (get) Token: 0x060012F1 RID: 4849 RVA: 0x00008D88 File Offset: 0x00006F88
		// (set) Token: 0x060012F2 RID: 4850 RVA: 0x00008D90 File Offset: 0x00006F90
		[DataMember]
		public string Description { get; set; }

		// Token: 0x170005A3 RID: 1443
		// (get) Token: 0x060012F3 RID: 4851 RVA: 0x00008D99 File Offset: 0x00006F99
		// (set) Token: 0x060012F4 RID: 4852 RVA: 0x00008DA1 File Offset: 0x00006FA1
		[DataMember]
		public string ExampleUsage { get; set; }

		// Token: 0x170005A4 RID: 1444
		// (get) Token: 0x060012F5 RID: 4853 RVA: 0x00008DAA File Offset: 0x00006FAA
		// (set) Token: 0x060012F6 RID: 4854 RVA: 0x00008DB2 File Offset: 0x00006FB2
		[DataMember]
		public IList<ReportParameterDTO> FunctionParameters { get; set; }

		// Token: 0x170005A5 RID: 1445
		// (get) Token: 0x060012F7 RID: 4855 RVA: 0x00008DBB File Offset: 0x00006FBB
		// (set) Token: 0x060012F8 RID: 4856 RVA: 0x00008DC3 File Offset: 0x00006FC3
		[DataMember]
		public eFunctionType FunctionCode { get; set; }

		// Token: 0x170005A6 RID: 1446
		// (get) Token: 0x060012F9 RID: 4857 RVA: 0x00008DCC File Offset: 0x00006FCC
		// (set) Token: 0x060012FA RID: 4858 RVA: 0x00008DD4 File Offset: 0x00006FD4
		[DataMember]
		public int OrderNum { get; set; }

		// Token: 0x170005A7 RID: 1447
		// (get) Token: 0x060012FB RID: 4859 RVA: 0x00008DDD File Offset: 0x00006FDD
		// (set) Token: 0x060012FC RID: 4860 RVA: 0x00008DE5 File Offset: 0x00006FE5
		[DataMember]
		public bool ExecuteThisFunctionOnClientIfPossible { get; set; }
	}
}

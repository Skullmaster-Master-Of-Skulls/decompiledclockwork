using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Cases
{
	// Token: 0x0200089B RID: 2203
	[DataContract(Namespace = "http://tpro.ca")]
	public class CaseForDisplayDTO : CaseBaseDTO
	{
		// Token: 0x17000FB9 RID: 4025
		// (get) Token: 0x06002CAD RID: 11437 RVA: 0x00015287 File Offset: 0x00013487
		// (set) Token: 0x06002CAE RID: 11438 RVA: 0x0001528F File Offset: 0x0001348F
		[DataMember]
		public DateTime DateEntered { get; set; }

		// Token: 0x17000FBA RID: 4026
		// (get) Token: 0x06002CAF RID: 11439 RVA: 0x00015298 File Offset: 0x00013498
		// (set) Token: 0x06002CB0 RID: 11440 RVA: 0x000152A0 File Offset: 0x000134A0
		[DataMember]
		public PersonBaseDTO WhoEntered { get; set; }

		// Token: 0x17000FBB RID: 4027
		// (get) Token: 0x06002CB1 RID: 11441 RVA: 0x000152A9 File Offset: 0x000134A9
		// (set) Token: 0x06002CB2 RID: 11442 RVA: 0x000152B1 File Offset: 0x000134B1
		[DataMember]
		public string Status { get; set; }

		// Token: 0x17000FBC RID: 4028
		// (get) Token: 0x06002CB3 RID: 11443 RVA: 0x000152BA File Offset: 0x000134BA
		// (set) Token: 0x06002CB4 RID: 11444 RVA: 0x000152C2 File Offset: 0x000134C2
		[DataMember]
		public IList<DynamicDataDTO> DynamicFormDataSummary { get; set; }
	}
}

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Cases
{
	// Token: 0x0200089D RID: 2205
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadCasesForDisplayForStudentResp
	{
		// Token: 0x17000FC0 RID: 4032
		// (get) Token: 0x06002CBD RID: 11453 RVA: 0x000152FE File Offset: 0x000134FE
		// (set) Token: 0x06002CBE RID: 11454 RVA: 0x00015306 File Offset: 0x00013506
		[DataMember]
		public IList<CaseForDisplayDTO> CasesForDisplay { get; set; }
	}
}

using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x02000685 RID: 1669
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetEmailFieldResp
	{
		// Token: 0x17000B76 RID: 2934
		// (get) Token: 0x060021F1 RID: 8689 RVA: 0x0000F7BC File Offset: 0x0000D9BC
		// (set) Token: 0x060021F2 RID: 8690 RVA: 0x0000F7C4 File Offset: 0x0000D9C4
		[DataMember]
		public DynamicFieldDTO EmailField { get; set; }
	}
}

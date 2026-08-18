using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x02000681 RID: 1665
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateFieldResp
	{
		// Token: 0x17000B72 RID: 2930
		// (get) Token: 0x060021E5 RID: 8677 RVA: 0x0000F778 File Offset: 0x0000D978
		// (set) Token: 0x060021E6 RID: 8678 RVA: 0x0000F780 File Offset: 0x0000D980
		[DataMember]
		public int ControlId { get; set; }
	}
}

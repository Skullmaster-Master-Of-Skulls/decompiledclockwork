using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x02000645 RID: 1605
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadEmailResp
	{
		// Token: 0x17000AF7 RID: 2807
		// (get) Token: 0x060020B0 RID: 8368 RVA: 0x0000EDC8 File Offset: 0x0000CFC8
		// (set) Token: 0x060020B1 RID: 8369 RVA: 0x0000EDD0 File Offset: 0x0000CFD0
		[DataMember]
		public DynamicDataDTO EmailData { get; set; }
	}
}

using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x020006AF RID: 1711
	[DataContract(Namespace = "http://tpro.ca")]
	public class DeleteFormResp
	{
		// Token: 0x17000BB8 RID: 3000
		// (get) Token: 0x060022A0 RID: 8864 RVA: 0x0000FCE8 File Offset: 0x0000DEE8
		// (set) Token: 0x060022A1 RID: 8865 RVA: 0x0000FCF0 File Offset: 0x0000DEF0
		[DataMember]
		public bool Worked { get; set; }
	}
}

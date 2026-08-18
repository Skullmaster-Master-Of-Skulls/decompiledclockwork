using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x0200067C RID: 1660
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadFieldsByControlIdsReq : BaseMessageReq
	{
		// Token: 0x17000B6D RID: 2925
		// (get) Token: 0x060021D6 RID: 8662 RVA: 0x0000F723 File Offset: 0x0000D923
		// (set) Token: 0x060021D7 RID: 8663 RVA: 0x0000F72B File Offset: 0x0000D92B
		[DataMember]
		public List<int> ControlIds { get; set; }
	}
}

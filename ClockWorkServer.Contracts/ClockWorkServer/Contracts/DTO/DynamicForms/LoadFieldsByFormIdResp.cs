using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x02000679 RID: 1657
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadFieldsByFormIdResp
	{
		// Token: 0x17000B69 RID: 2921
		// (get) Token: 0x060021CB RID: 8651 RVA: 0x0000F6DF File Offset: 0x0000D8DF
		// (set) Token: 0x060021CC RID: 8652 RVA: 0x0000F6E7 File Offset: 0x0000D8E7
		[DataMember]
		public List<DynamicFieldDTO> Fields { get; set; }
	}
}

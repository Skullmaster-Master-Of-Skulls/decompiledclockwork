using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x0200067D RID: 1661
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadFieldsByControlIdsResp
	{
		// Token: 0x17000B6E RID: 2926
		// (get) Token: 0x060021D9 RID: 8665 RVA: 0x0000F734 File Offset: 0x0000D934
		// (set) Token: 0x060021DA RID: 8666 RVA: 0x0000F73C File Offset: 0x0000D93C
		[DataMember]
		public List<DynamicFieldDTO> Fields { get; set; }
	}
}

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x020006A9 RID: 1705
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadFormsWithExtendedInfoByScreenNumsResp
	{
		// Token: 0x17000BB3 RID: 2995
		// (get) Token: 0x06002290 RID: 8848 RVA: 0x0000FC93 File Offset: 0x0000DE93
		// (set) Token: 0x06002291 RID: 8849 RVA: 0x0000FC9B File Offset: 0x0000DE9B
		[DataMember]
		public IList<DynamicFormWithExtendedInfoDTO> Forms { get; set; }
	}
}

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x0200068E RID: 1678
	[DataContract(Namespace = "http://tpro.ca")]
	public class CreateFieldsReq : BaseMessageReq
	{
		// Token: 0x17000B7F RID: 2943
		// (get) Token: 0x0600220C RID: 8716 RVA: 0x0000F855 File Offset: 0x0000DA55
		// (set) Token: 0x0600220D RID: 8717 RVA: 0x0000F85D File Offset: 0x0000DA5D
		[DataMember]
		public int ScreenNum { get; set; }

		// Token: 0x17000B80 RID: 2944
		// (get) Token: 0x0600220E RID: 8718 RVA: 0x0000F866 File Offset: 0x0000DA66
		// (set) Token: 0x0600220F RID: 8719 RVA: 0x0000F86E File Offset: 0x0000DA6E
		[DataMember]
		public IList<DynamicFieldDTO> Fields { get; set; }
	}
}

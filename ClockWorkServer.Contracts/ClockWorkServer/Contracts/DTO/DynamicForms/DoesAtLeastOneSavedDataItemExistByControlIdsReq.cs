using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x0200065E RID: 1630
	[DataContract(Namespace = "http://tpro.ca")]
	public class DoesAtLeastOneSavedDataItemExistByControlIdsReq : BaseMessageReq
	{
		// Token: 0x17000B23 RID: 2851
		// (get) Token: 0x06002121 RID: 8481 RVA: 0x0000F0B4 File Offset: 0x0000D2B4
		// (set) Token: 0x06002122 RID: 8482 RVA: 0x0000F0BC File Offset: 0x0000D2BC
		[DataMember]
		public DynamicDataContextDTO Context { get; set; }

		// Token: 0x17000B24 RID: 2852
		// (get) Token: 0x06002123 RID: 8483 RVA: 0x0000F0C5 File Offset: 0x0000D2C5
		// (set) Token: 0x06002124 RID: 8484 RVA: 0x0000F0CD File Offset: 0x0000D2CD
		[DataMember]
		public IList<int> ControlIds { get; set; }

		// Token: 0x17000B25 RID: 2853
		// (get) Token: 0x06002125 RID: 8485 RVA: 0x0000F0D6 File Offset: 0x0000D2D6
		// (set) Token: 0x06002126 RID: 8486 RVA: 0x0000F0DE File Offset: 0x0000D2DE
		[DataMember]
		public eDynamicFormTypeDTO FormType { get; set; }
	}
}

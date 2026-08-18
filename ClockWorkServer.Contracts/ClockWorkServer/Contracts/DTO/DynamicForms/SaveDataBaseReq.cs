using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x0200065A RID: 1626
	[DataContract(Namespace = "http://tpro.ca")]
	public class SaveDataBaseReq : BaseMessageReq
	{
		// Token: 0x17000B1C RID: 2844
		// (get) Token: 0x0600210F RID: 8463 RVA: 0x0000F03D File Offset: 0x0000D23D
		// (set) Token: 0x06002110 RID: 8464 RVA: 0x0000F045 File Offset: 0x0000D245
		[DataMember]
		public DynamicDataContextDTO Context { get; set; }

		// Token: 0x17000B1D RID: 2845
		// (get) Token: 0x06002111 RID: 8465 RVA: 0x0000F04E File Offset: 0x0000D24E
		// (set) Token: 0x06002112 RID: 8466 RVA: 0x0000F056 File Offset: 0x0000D256
		[DataMember]
		public List<DynamicDataBaseDTO> Data { get; set; }

		// Token: 0x17000B1E RID: 2846
		// (get) Token: 0x06002113 RID: 8467 RVA: 0x0000F05F File Offset: 0x0000D25F
		// (set) Token: 0x06002114 RID: 8468 RVA: 0x0000F067 File Offset: 0x0000D267
		[DataMember]
		public eDynamicFormTypeDTO FormType { get; set; }
	}
}

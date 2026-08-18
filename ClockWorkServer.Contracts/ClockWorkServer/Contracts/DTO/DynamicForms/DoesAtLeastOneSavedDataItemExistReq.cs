using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x0200065C RID: 1628
	[DataContract(Namespace = "http://tpro.ca")]
	public class DoesAtLeastOneSavedDataItemExistReq : BaseMessageReq
	{
		// Token: 0x17000B1F RID: 2847
		// (get) Token: 0x06002117 RID: 8471 RVA: 0x0000F070 File Offset: 0x0000D270
		// (set) Token: 0x06002118 RID: 8472 RVA: 0x0000F078 File Offset: 0x0000D278
		[DataMember]
		public DynamicDataContextDTO Context { get; set; }

		// Token: 0x17000B20 RID: 2848
		// (get) Token: 0x06002119 RID: 8473 RVA: 0x0000F081 File Offset: 0x0000D281
		// (set) Token: 0x0600211A RID: 8474 RVA: 0x0000F089 File Offset: 0x0000D289
		[DataMember]
		public int ScreenNum { get; set; }

		// Token: 0x17000B21 RID: 2849
		// (get) Token: 0x0600211B RID: 8475 RVA: 0x0000F092 File Offset: 0x0000D292
		// (set) Token: 0x0600211C RID: 8476 RVA: 0x0000F09A File Offset: 0x0000D29A
		[DataMember]
		public eDynamicFormTypeDTO FormType { get; set; }
	}
}

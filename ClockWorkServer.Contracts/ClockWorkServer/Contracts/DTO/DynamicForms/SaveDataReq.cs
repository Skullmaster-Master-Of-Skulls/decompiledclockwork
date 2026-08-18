using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x02000654 RID: 1620
	[DataContract(Namespace = "http://tpro.ca")]
	public class SaveDataReq : BaseMessageReq
	{
		// Token: 0x17000B13 RID: 2835
		// (get) Token: 0x060020F7 RID: 8439 RVA: 0x0000EFA4 File Offset: 0x0000D1A4
		// (set) Token: 0x060020F8 RID: 8440 RVA: 0x0000EFAC File Offset: 0x0000D1AC
		[DataMember]
		public DynamicDataContextDTO Context { get; set; }

		// Token: 0x17000B14 RID: 2836
		// (get) Token: 0x060020F9 RID: 8441 RVA: 0x0000EFB5 File Offset: 0x0000D1B5
		// (set) Token: 0x060020FA RID: 8442 RVA: 0x0000EFBD File Offset: 0x0000D1BD
		[DataMember]
		public List<DynamicDataDTO> Data { get; set; }

		// Token: 0x17000B15 RID: 2837
		// (get) Token: 0x060020FB RID: 8443 RVA: 0x0000EFC6 File Offset: 0x0000D1C6
		// (set) Token: 0x060020FC RID: 8444 RVA: 0x0000EFCE File Offset: 0x0000D1CE
		[DataMember]
		public eDynamicFormTypeDTO FormType { get; set; }
	}
}

using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x0200064A RID: 1610
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadDataByFormReq : BaseMessageReq
	{
		// Token: 0x17000B04 RID: 2820
		// (get) Token: 0x060020CF RID: 8399 RVA: 0x0000EEA5 File Offset: 0x0000D0A5
		// (set) Token: 0x060020D0 RID: 8400 RVA: 0x0000EEAD File Offset: 0x0000D0AD
		[DataMember]
		public DynamicDataContextDTO Context { get; set; }

		// Token: 0x17000B05 RID: 2821
		// (get) Token: 0x060020D1 RID: 8401 RVA: 0x0000EEB6 File Offset: 0x0000D0B6
		// (set) Token: 0x060020D2 RID: 8402 RVA: 0x0000EEBE File Offset: 0x0000D0BE
		[DataMember]
		public int ScreenNum { get; set; }

		// Token: 0x17000B06 RID: 2822
		// (get) Token: 0x060020D3 RID: 8403 RVA: 0x0000EEC7 File Offset: 0x0000D0C7
		// (set) Token: 0x060020D4 RID: 8404 RVA: 0x0000EECF File Offset: 0x0000D0CF
		[DataMember]
		public eDynamicFormTypeDTO FormType { get; set; }
	}
}

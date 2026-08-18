using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.Files;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x02000648 RID: 1608
	[DataContract(Namespace = "http://tpro.ca")]
	public class StoreFileInDocumentsReq : BaseMessageReq
	{
		// Token: 0x17000AFC RID: 2812
		// (get) Token: 0x060020BD RID: 8381 RVA: 0x0000EE1D File Offset: 0x0000D01D
		// (set) Token: 0x060020BE RID: 8382 RVA: 0x0000EE25 File Offset: 0x0000D025
		[DataMember]
		public int StudentPersonId { get; set; }

		// Token: 0x17000AFD RID: 2813
		// (get) Token: 0x060020BF RID: 8383 RVA: 0x0000EE2E File Offset: 0x0000D02E
		// (set) Token: 0x060020C0 RID: 8384 RVA: 0x0000EE36 File Offset: 0x0000D036
		[DataMember]
		public string Title { get; set; }

		// Token: 0x17000AFE RID: 2814
		// (get) Token: 0x060020C1 RID: 8385 RVA: 0x0000EE3F File Offset: 0x0000D03F
		// (set) Token: 0x060020C2 RID: 8386 RVA: 0x0000EE47 File Offset: 0x0000D047
		[DataMember]
		public string Notes { get; set; }

		// Token: 0x17000AFF RID: 2815
		// (get) Token: 0x060020C3 RID: 8387 RVA: 0x0000EE50 File Offset: 0x0000D050
		// (set) Token: 0x060020C4 RID: 8388 RVA: 0x0000EE58 File Offset: 0x0000D058
		[DataMember]
		public BinaryFileDTO File { get; set; }

		// Token: 0x17000B00 RID: 2816
		// (get) Token: 0x060020C5 RID: 8389 RVA: 0x0000EE61 File Offset: 0x0000D061
		// (set) Token: 0x060020C6 RID: 8390 RVA: 0x0000EE69 File Offset: 0x0000D069
		[DataMember]
		public DynamicDataContextDTO DataContext { get; set; }

		// Token: 0x17000B01 RID: 2817
		// (get) Token: 0x060020C7 RID: 8391 RVA: 0x0000EE72 File Offset: 0x0000D072
		// (set) Token: 0x060020C8 RID: 8392 RVA: 0x0000EE7A File Offset: 0x0000D07A
		[DataMember]
		public eDynamicFormTypeDTO FormType { get; set; }

		// Token: 0x17000B02 RID: 2818
		// (get) Token: 0x060020C9 RID: 8393 RVA: 0x0000EE83 File Offset: 0x0000D083
		// (set) Token: 0x060020CA RID: 8394 RVA: 0x0000EE8B File Offset: 0x0000D08B
		[DataMember]
		public int ControlId { get; set; }
	}
}

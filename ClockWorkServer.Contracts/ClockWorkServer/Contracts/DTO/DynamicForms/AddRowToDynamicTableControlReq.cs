using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x02000660 RID: 1632
	[DataContract(Namespace = "http://tpro.ca")]
	public class AddRowToDynamicTableControlReq : BaseMessageReq
	{
		// Token: 0x17000B27 RID: 2855
		// (get) Token: 0x0600212B RID: 8491 RVA: 0x0000F0F8 File Offset: 0x0000D2F8
		// (set) Token: 0x0600212C RID: 8492 RVA: 0x0000F100 File Offset: 0x0000D300
		[DataMember]
		public DynamicDataContextDTO Context { get; set; }

		// Token: 0x17000B28 RID: 2856
		// (get) Token: 0x0600212D RID: 8493 RVA: 0x0000F109 File Offset: 0x0000D309
		// (set) Token: 0x0600212E RID: 8494 RVA: 0x0000F111 File Offset: 0x0000D311
		[DataMember]
		public int ControlId { get; set; }

		// Token: 0x17000B29 RID: 2857
		// (get) Token: 0x0600212F RID: 8495 RVA: 0x0000F11A File Offset: 0x0000D31A
		// (set) Token: 0x06002130 RID: 8496 RVA: 0x0000F122 File Offset: 0x0000D322
		[DataMember]
		public eDynamicFormTypeDTO FormType { get; set; }

		// Token: 0x17000B2A RID: 2858
		// (get) Token: 0x06002131 RID: 8497 RVA: 0x0000F12B File Offset: 0x0000D32B
		// (set) Token: 0x06002132 RID: 8498 RVA: 0x0000F133 File Offset: 0x0000D333
		[DataMember]
		public IList<string> ColumnValues { get; set; }
	}
}

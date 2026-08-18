using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x02000646 RID: 1606
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadDataByFieldsReq : BaseMessageReq
	{
		// Token: 0x17000AF8 RID: 2808
		// (get) Token: 0x060020B3 RID: 8371 RVA: 0x0000EDD9 File Offset: 0x0000CFD9
		// (set) Token: 0x060020B4 RID: 8372 RVA: 0x0000EDE1 File Offset: 0x0000CFE1
		[DataMember]
		public DynamicDataContextDTO Context { get; set; }

		// Token: 0x17000AF9 RID: 2809
		// (get) Token: 0x060020B5 RID: 8373 RVA: 0x0000EDEA File Offset: 0x0000CFEA
		// (set) Token: 0x060020B6 RID: 8374 RVA: 0x0000EDF2 File Offset: 0x0000CFF2
		[DataMember]
		public List<int> ControlIds { get; set; }

		// Token: 0x17000AFA RID: 2810
		// (get) Token: 0x060020B7 RID: 8375 RVA: 0x0000EDFB File Offset: 0x0000CFFB
		// (set) Token: 0x060020B8 RID: 8376 RVA: 0x0000EE03 File Offset: 0x0000D003
		[DataMember]
		public eDynamicFormTypeDTO DataType { get; set; }
	}
}

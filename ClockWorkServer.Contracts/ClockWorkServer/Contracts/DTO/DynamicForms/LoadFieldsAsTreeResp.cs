using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.Common.DataStructure.Tree;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x02000675 RID: 1653
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadFieldsAsTreeResp
	{
		// Token: 0x17000B63 RID: 2915
		// (get) Token: 0x060021BB RID: 8635 RVA: 0x0000F679 File Offset: 0x0000D879
		// (set) Token: 0x060021BC RID: 8636 RVA: 0x0000F681 File Offset: 0x0000D881
		[DataMember]
		public List<DynamicFieldDTO> Fields { get; set; }

		// Token: 0x17000B64 RID: 2916
		// (get) Token: 0x060021BD RID: 8637 RVA: 0x0000F68A File Offset: 0x0000D88A
		// (set) Token: 0x060021BE RID: 8638 RVA: 0x0000F692 File Offset: 0x0000D892
		[DataMember]
		public Forest<DynamicFieldDTO> Tree { get; set; }
	}
}

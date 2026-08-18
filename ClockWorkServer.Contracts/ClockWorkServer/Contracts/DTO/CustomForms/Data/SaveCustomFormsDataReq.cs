using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.CustomForms.Data
{
	// Token: 0x02000766 RID: 1894
	[DataContract(Namespace = "http://tpro.ca")]
	public class SaveCustomFormsDataReq : BaseMessageReq
	{
		// Token: 0x17000D88 RID: 3464
		// (get) Token: 0x060026F1 RID: 9969 RVA: 0x000120FA File Offset: 0x000102FA
		// (set) Token: 0x060026F2 RID: 9970 RVA: 0x00012102 File Offset: 0x00010302
		[DataMember]
		public CustomDataSetDTO DataSet { get; set; }

		// Token: 0x17000D89 RID: 3465
		// (get) Token: 0x060026F3 RID: 9971 RVA: 0x0001210B File Offset: 0x0001030B
		// (set) Token: 0x060026F4 RID: 9972 RVA: 0x00012113 File Offset: 0x00010313
		[DataMember]
		public IList<Guid> DataInstanceIds { get; set; }
	}
}

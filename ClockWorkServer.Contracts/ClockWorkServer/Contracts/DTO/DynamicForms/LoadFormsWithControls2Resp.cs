using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x02000683 RID: 1667
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadFormsWithControls2Resp
	{
		// Token: 0x17000B75 RID: 2933
		// (get) Token: 0x060021ED RID: 8685 RVA: 0x0000F7AB File Offset: 0x0000D9AB
		// (set) Token: 0x060021EE RID: 8686 RVA: 0x0000F7B3 File Offset: 0x0000D9B3
		[DataMember]
		public IList<DynamicFormOrGroupOrFieldDTO> FormsWithControls { get; set; }
	}
}

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x02000666 RID: 1638
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadPerStudentDataForMultipleStudentsAsDataTableReq : BaseMessageReq
	{
		// Token: 0x17000B34 RID: 2868
		// (get) Token: 0x0600214B RID: 8523 RVA: 0x0000F1D5 File Offset: 0x0000D3D5
		// (set) Token: 0x0600214C RID: 8524 RVA: 0x0000F1DD File Offset: 0x0000D3DD
		[DataMember]
		public IList<int> PersonIds { get; set; }

		// Token: 0x17000B35 RID: 2869
		// (get) Token: 0x0600214D RID: 8525 RVA: 0x0000F1E6 File Offset: 0x0000D3E6
		// (set) Token: 0x0600214E RID: 8526 RVA: 0x0000F1EE File Offset: 0x0000D3EE
		[DataMember]
		public IList<int> ControlIds { get; set; }
	}
}

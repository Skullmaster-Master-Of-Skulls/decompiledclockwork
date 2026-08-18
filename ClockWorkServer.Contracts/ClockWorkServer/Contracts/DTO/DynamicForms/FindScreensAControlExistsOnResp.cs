using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x020006B1 RID: 1713
	[DataContract(Namespace = "http://tpro.ca")]
	public class FindScreensAControlExistsOnResp
	{
		// Token: 0x17000BBA RID: 3002
		// (get) Token: 0x060022A6 RID: 8870 RVA: 0x0000FD0A File Offset: 0x0000DF0A
		// (set) Token: 0x060022A7 RID: 8871 RVA: 0x0000FD12 File Offset: 0x0000DF12
		[DataMember]
		public IList<int> FormNums { get; set; }
	}
}

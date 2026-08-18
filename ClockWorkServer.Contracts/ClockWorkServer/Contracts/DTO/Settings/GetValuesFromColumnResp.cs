using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Settings
{
	// Token: 0x0200026A RID: 618
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetValuesFromColumnResp
	{
		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x06000E3E RID: 3646 RVA: 0x00006B1D File Offset: 0x00004D1D
		// (set) Token: 0x06000E3F RID: 3647 RVA: 0x00006B25 File Offset: 0x00004D25
		[DataMember]
		public IList<KeyValuePair<int, string>> Values { get; set; }
	}
}

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts
{
	// Token: 0x020000BB RID: 187
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetSettingsByGroupResp
	{
		// Token: 0x1700003A RID: 58
		// (get) Token: 0x0600056B RID: 1387 RVA: 0x0000253B File Offset: 0x0000073B
		// (set) Token: 0x0600056C RID: 1388 RVA: 0x00002543 File Offset: 0x00000743
		[DataMember]
		public IList<AppSettingDTO> Settings { get; set; }
	}
}

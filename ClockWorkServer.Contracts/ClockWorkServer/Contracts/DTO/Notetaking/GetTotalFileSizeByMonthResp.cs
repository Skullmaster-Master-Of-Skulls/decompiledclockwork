using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Notetaking
{
	// Token: 0x0200042A RID: 1066
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetTotalFileSizeByMonthResp
	{
		// Token: 0x1700073E RID: 1854
		// (get) Token: 0x0600171A RID: 5914 RVA: 0x0000AB80 File Offset: 0x00008D80
		// (set) Token: 0x0600171B RID: 5915 RVA: 0x0000AB88 File Offset: 0x00008D88
		[DataMember]
		public IDictionary<DateTime, long> TotalFileSizesByMonths { get; set; }
	}
}

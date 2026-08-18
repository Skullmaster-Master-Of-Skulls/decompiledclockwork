using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Notetaking
{
	// Token: 0x0200044B RID: 1099
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadUniqueAvailableCourseStartDatesByNotetakerResp
	{
		// Token: 0x17000766 RID: 1894
		// (get) Token: 0x0600178B RID: 6027 RVA: 0x0000AE28 File Offset: 0x00009028
		// (set) Token: 0x0600178C RID: 6028 RVA: 0x0000AE30 File Offset: 0x00009030
		[DataMember]
		public IList<DateTime> UniqueDates { get; set; }
	}
}

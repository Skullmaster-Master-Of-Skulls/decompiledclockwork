using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x020009F0 RID: 2544
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadClassTestDefinitionsResp
	{
		// Token: 0x1700131A RID: 4890
		// (get) Token: 0x060034FB RID: 13563 RVA: 0x00019C8D File Offset: 0x00017E8D
		// (set) Token: 0x060034FC RID: 13564 RVA: 0x00019C95 File Offset: 0x00017E95
		[DataMember]
		public List<ClassTestDTO> ClassTests { get; set; }
	}
}

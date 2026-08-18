using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities.MailMergeData;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x02000A0D RID: 2573
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadTestBookingMailMergeInfoByDateResp
	{
		// Token: 0x17001339 RID: 4921
		// (get) Token: 0x06003556 RID: 13654 RVA: 0x00019E9C File Offset: 0x0001809C
		// (set) Token: 0x06003557 RID: 13655 RVA: 0x00019EA4 File Offset: 0x000180A4
		[DataMember]
		public IList<MailMergeTestBookingDTO> Items { get; set; }
	}
}

using System;
using System.Runtime.Serialization;
using TechnoPro.Common.DataStructure;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x020009ED RID: 2541
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetSittingEffectiveTimeRangeResp
	{
		// Token: 0x17001317 RID: 4887
		// (get) Token: 0x060034F2 RID: 13554 RVA: 0x00019C5A File Offset: 0x00017E5A
		// (set) Token: 0x060034F3 RID: 13555 RVA: 0x00019C62 File Offset: 0x00017E62
		[DataMember]
		public Range<DateTime> DateRange { get; set; }
	}
}

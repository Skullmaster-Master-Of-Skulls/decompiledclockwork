using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar.AppParameters
{
	// Token: 0x02000B07 RID: 2823
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAvailabilityForChannelReq : BaseMessageReq
	{
		// Token: 0x170015E4 RID: 5604
		// (get) Token: 0x06003BA5 RID: 15269 RVA: 0x0001D023 File Offset: 0x0001B223
		// (set) Token: 0x06003BA6 RID: 15270 RVA: 0x0001D02B File Offset: 0x0001B22B
		[DataMember]
		public int StudentPersonId { get; set; }

		// Token: 0x170015E5 RID: 5605
		// (get) Token: 0x06003BA7 RID: 15271 RVA: 0x0001D034 File Offset: 0x0001B234
		// (set) Token: 0x06003BA8 RID: 15272 RVA: 0x0001D03C File Offset: 0x0001B23C
		[DataMember]
		public string ChannelId { get; set; }

		// Token: 0x170015E6 RID: 5606
		// (get) Token: 0x06003BA9 RID: 15273 RVA: 0x0001D045 File Offset: 0x0001B245
		// (set) Token: 0x06003BAA RID: 15274 RVA: 0x0001D04D File Offset: 0x0001B24D
		[DataMember]
		public string OptionalCalendarName { get; set; }

		// Token: 0x170015E7 RID: 5607
		// (get) Token: 0x06003BAB RID: 15275 RVA: 0x0001D056 File Offset: 0x0001B256
		// (set) Token: 0x06003BAC RID: 15276 RVA: 0x0001D05E File Offset: 0x0001B25E
		[DataMember]
		public DateTime StartDate { get; set; }

		// Token: 0x170015E8 RID: 5608
		// (get) Token: 0x06003BAD RID: 15277 RVA: 0x0001D067 File Offset: 0x0001B267
		// (set) Token: 0x06003BAE RID: 15278 RVA: 0x0001D06F File Offset: 0x0001B26F
		[DataMember]
		public int NumDays { get; set; }
	}
}

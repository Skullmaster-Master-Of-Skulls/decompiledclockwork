using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms
{
	// Token: 0x02000629 RID: 1577
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetAppointmentNotesSummaryHtmlReq : BaseMessageReq
	{
		// Token: 0x17000AB9 RID: 2745
		// (get) Token: 0x06002015 RID: 8213 RVA: 0x0000E907 File Offset: 0x0000CB07
		// (set) Token: 0x06002016 RID: 8214 RVA: 0x0000E90F File Offset: 0x0000CB0F
		[DataMember]
		public int StudentPersonId { get; set; }

		// Token: 0x17000ABA RID: 2746
		// (get) Token: 0x06002017 RID: 8215 RVA: 0x0000E918 File Offset: 0x0000CB18
		// (set) Token: 0x06002018 RID: 8216 RVA: 0x0000E920 File Offset: 0x0000CB20
		[DataMember]
		public IList<int> AppointmentIds { get; set; }

		// Token: 0x17000ABB RID: 2747
		// (get) Token: 0x06002019 RID: 8217 RVA: 0x0000E929 File Offset: 0x0000CB29
		// (set) Token: 0x0600201A RID: 8218 RVA: 0x0000E931 File Offset: 0x0000CB31
		[DataMember]
		public IList<int> ScreenNums { get; set; }
	}
}

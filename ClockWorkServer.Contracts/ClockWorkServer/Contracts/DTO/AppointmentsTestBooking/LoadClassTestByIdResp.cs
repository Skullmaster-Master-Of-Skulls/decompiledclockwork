using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsTestBooking
{
	// Token: 0x02000A1B RID: 2587
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadClassTestByIdResp
	{
		// Token: 0x1700134C RID: 4940
		// (get) Token: 0x0600358A RID: 13706 RVA: 0x00019FDF File Offset: 0x000181DF
		// (set) Token: 0x0600358B RID: 13707 RVA: 0x00019FE7 File Offset: 0x000181E7
		[DataMember]
		public ClassTestDTO ClassTest { get; set; }
	}
}

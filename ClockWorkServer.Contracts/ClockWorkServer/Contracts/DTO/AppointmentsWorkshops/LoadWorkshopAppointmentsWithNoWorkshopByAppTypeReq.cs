using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsWorkshops
{
	// Token: 0x020008F7 RID: 2295
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadWorkshopAppointmentsWithNoWorkshopByAppTypeReq : BaseMessageReq
	{
		// Token: 0x17001092 RID: 4242
		// (get) Token: 0x06002EC9 RID: 11977 RVA: 0x0001642B File Offset: 0x0001462B
		// (set) Token: 0x06002ECA RID: 11978 RVA: 0x00016433 File Offset: 0x00014633
		[DataMember]
		public DateTime StartDate { get; set; }

		// Token: 0x17001093 RID: 4243
		// (get) Token: 0x06002ECB RID: 11979 RVA: 0x0001643C File Offset: 0x0001463C
		// (set) Token: 0x06002ECC RID: 11980 RVA: 0x00016444 File Offset: 0x00014644
		[DataMember]
		public DateTime EndDate { get; set; }

		// Token: 0x17001094 RID: 4244
		// (get) Token: 0x06002ECD RID: 11981 RVA: 0x0001644D File Offset: 0x0001464D
		// (set) Token: 0x06002ECE RID: 11982 RVA: 0x00016455 File Offset: 0x00014655
		[DataMember]
		public int AppTypeId { get; set; }
	}
}

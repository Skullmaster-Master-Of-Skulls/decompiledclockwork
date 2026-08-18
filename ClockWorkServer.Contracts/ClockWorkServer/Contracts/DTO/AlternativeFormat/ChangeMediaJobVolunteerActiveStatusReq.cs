using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat
{
	// Token: 0x02000C03 RID: 3075
	[DataContract(Namespace = "http://tpro.ca")]
	public class ChangeMediaJobVolunteerActiveStatusReq : BaseMessageReq
	{
		// Token: 0x170017DE RID: 6110
		// (get) Token: 0x060040B1 RID: 16561 RVA: 0x0001FBA8 File Offset: 0x0001DDA8
		// (set) Token: 0x060040B2 RID: 16562 RVA: 0x0001FBB0 File Offset: 0x0001DDB0
		[DataMember]
		public int JobVolunteerId { get; set; }

		// Token: 0x170017DF RID: 6111
		// (get) Token: 0x060040B3 RID: 16563 RVA: 0x0001FBB9 File Offset: 0x0001DDB9
		// (set) Token: 0x060040B4 RID: 16564 RVA: 0x0001FBC1 File Offset: 0x0001DDC1
		[DataMember]
		public int VolunteerId { get; set; }

		// Token: 0x170017E0 RID: 6112
		// (get) Token: 0x060040B5 RID: 16565 RVA: 0x0001FBCA File Offset: 0x0001DDCA
		// (set) Token: 0x060040B6 RID: 16566 RVA: 0x0001FBD2 File Offset: 0x0001DDD2
		[DataMember]
		public int MediaJobId { get; set; }

		// Token: 0x170017E1 RID: 6113
		// (get) Token: 0x060040B7 RID: 16567 RVA: 0x0001FBDB File Offset: 0x0001DDDB
		// (set) Token: 0x060040B8 RID: 16568 RVA: 0x0001FBE3 File Offset: 0x0001DDE3
		[DataMember]
		public bool IsActive { get; set; }
	}
}

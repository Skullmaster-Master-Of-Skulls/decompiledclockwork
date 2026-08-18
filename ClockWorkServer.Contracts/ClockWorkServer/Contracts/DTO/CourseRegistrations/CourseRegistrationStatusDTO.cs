using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.CourseRegistrations
{
	// Token: 0x02000836 RID: 2102
	[DataContract(Namespace = "http://tpro.ca")]
	public class CourseRegistrationStatusDTO
	{
		// Token: 0x17000F0A RID: 3850
		// (get) Token: 0x06002AEB RID: 10987 RVA: 0x00014628 File Offset: 0x00012828
		// (set) Token: 0x06002AEC RID: 10988 RVA: 0x00014630 File Offset: 0x00012830
		[DataMember]
		public int CourseRegistrationStatusId { get; set; }

		// Token: 0x17000F0B RID: 3851
		// (get) Token: 0x06002AED RID: 10989 RVA: 0x00014639 File Offset: 0x00012839
		// (set) Token: 0x06002AEE RID: 10990 RVA: 0x00014641 File Offset: 0x00012841
		[DataMember]
		public string Title { get; set; }

		// Token: 0x17000F0C RID: 3852
		// (get) Token: 0x06002AEF RID: 10991 RVA: 0x0001464A File Offset: 0x0001284A
		// (set) Token: 0x06002AF0 RID: 10992 RVA: 0x00014652 File Offset: 0x00012852
		[DataMember]
		public string Description { get; set; }

		// Token: 0x17000F0D RID: 3853
		// (get) Token: 0x06002AF1 RID: 10993 RVA: 0x0001465B File Offset: 0x0001285B
		// (set) Token: 0x06002AF2 RID: 10994 RVA: 0x00014663 File Offset: 0x00012863
		[DataMember]
		public bool IsRegistered { get; set; }
	}
}

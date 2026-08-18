using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Tutoring
{
	// Token: 0x02000189 RID: 393
	[DataContract(Namespace = "http://tpro.ca")]
	public class MyTutorDTO
	{
		// Token: 0x170001A5 RID: 421
		// (get) Token: 0x06000945 RID: 2373 RVA: 0x00004220 File Offset: 0x00002420
		public int TutorPersonId
		{
			get
			{
				return (this.Tutor == null) ? 0 : this.Tutor.PersonId;
			}
		}

		// Token: 0x170001A6 RID: 422
		// (get) Token: 0x06000946 RID: 2374 RVA: 0x00004248 File Offset: 0x00002448
		// (set) Token: 0x06000947 RID: 2375 RVA: 0x00004250 File Offset: 0x00002450
		[DataMember]
		public TutorDTO Tutor { get; set; }

		// Token: 0x170001A7 RID: 423
		// (get) Token: 0x06000948 RID: 2376 RVA: 0x00004259 File Offset: 0x00002459
		// (set) Token: 0x06000949 RID: 2377 RVA: 0x00004261 File Offset: 0x00002461
		[DataMember]
		public int StudentPersonId { get; set; }

		// Token: 0x170001A8 RID: 424
		// (get) Token: 0x0600094A RID: 2378 RVA: 0x0000426A File Offset: 0x0000246A
		// (set) Token: 0x0600094B RID: 2379 RVA: 0x00004272 File Offset: 0x00002472
		[DataMember]
		public DateTime LastDateMetWith { get; set; }
	}
}

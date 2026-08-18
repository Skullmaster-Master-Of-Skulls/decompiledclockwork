using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Tutoring
{
	// Token: 0x02000197 RID: 407
	[DataContract(Namespace = "http://tpro.ca")]
	public class SearchForTutorsResp
	{
		// Token: 0x170001C1 RID: 449
		// (get) Token: 0x06000987 RID: 2439 RVA: 0x000044C6 File Offset: 0x000026C6
		// (set) Token: 0x06000988 RID: 2440 RVA: 0x000044CE File Offset: 0x000026CE
		[DataMember]
		public IList<TutorDTO> Tutors { get; set; }

		// Token: 0x170001C2 RID: 450
		// (get) Token: 0x06000989 RID: 2441 RVA: 0x000044D7 File Offset: 0x000026D7
		// (set) Token: 0x0600098A RID: 2442 RVA: 0x000044DF File Offset: 0x000026DF
		[DataMember]
		public bool IncludingCourse { get; set; }
	}
}

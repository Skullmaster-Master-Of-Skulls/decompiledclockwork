using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Tutoring
{
	// Token: 0x020001AB RID: 427
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAllTutorsResp
	{
		// Token: 0x170001DA RID: 474
		// (get) Token: 0x060009CD RID: 2509 RVA: 0x0000466F File Offset: 0x0000286F
		// (set) Token: 0x060009CE RID: 2510 RVA: 0x00004677 File Offset: 0x00002877
		[DataMember]
		public IList<TutorWithActiveStatusDTO> Tutors { get; set; }
	}
}

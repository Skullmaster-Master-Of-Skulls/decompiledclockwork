using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x0200080C RID: 2060
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAllLookupSubjectsResp
	{
		// Token: 0x17000EA1 RID: 3745
		// (get) Token: 0x060029EC RID: 10732 RVA: 0x00013E4D File Offset: 0x0001204D
		// (set) Token: 0x060029ED RID: 10733 RVA: 0x00013E55 File Offset: 0x00012055
		[DataMember]
		public List<LookupSubjectDTO> Subjects { get; set; }
	}
}

using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x020007BD RID: 1981
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadAcademicTermsResp
	{
		// Token: 0x17000E27 RID: 3623
		// (get) Token: 0x060028A0 RID: 10400 RVA: 0x00013438 File Offset: 0x00011638
		// (set) Token: 0x060028A1 RID: 10401 RVA: 0x00013440 File Offset: 0x00011640
		[DataMember]
		public List<AcademicTermDTO> AcademicTerms { get; set; }
	}
}

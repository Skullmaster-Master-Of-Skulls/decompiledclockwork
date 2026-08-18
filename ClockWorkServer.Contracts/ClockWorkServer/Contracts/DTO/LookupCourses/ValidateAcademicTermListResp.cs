using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.LookupCourses;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses
{
	// Token: 0x020007C5 RID: 1989
	[DataContract(Namespace = "http://tpro.ca")]
	public class ValidateAcademicTermListResp
	{
		// Token: 0x17000E2D RID: 3629
		// (get) Token: 0x060028B4 RID: 10420 RVA: 0x0001349E File Offset: 0x0001169E
		// (set) Token: 0x060028B5 RID: 10421 RVA: 0x000134A6 File Offset: 0x000116A6
		[DataMember]
		public eSessionListValidationResult ValidationResult { get; set; }
	}
}

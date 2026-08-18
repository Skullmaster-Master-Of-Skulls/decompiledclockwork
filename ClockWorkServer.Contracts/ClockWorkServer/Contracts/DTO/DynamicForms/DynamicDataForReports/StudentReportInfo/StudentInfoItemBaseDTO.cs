using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms.DynamicDataForReports.StudentReportInfo
{
	// Token: 0x020006C0 RID: 1728
	[DataContract(Namespace = "http://tpro.ca")]
	[KnownType(typeof(byte[]))]
	[KnownType(typeof(StudentInfoAccExpiryItemDTO))]
	[KnownType(typeof(StudentInfoAgeItemDTO))]
	[KnownType(typeof(StudentInfoAssignedAdvisorItemDTO))]
	[KnownType(typeof(StudentInfoEmailItemDTO))]
	public class StudentInfoItemBaseDTO
	{
		// Token: 0x17000BF7 RID: 3063
		// (get) Token: 0x0600232F RID: 9007 RVA: 0x00010143 File Offset: 0x0000E343
		// (set) Token: 0x06002330 RID: 9008 RVA: 0x0001014B File Offset: 0x0000E34B
		[DataMember]
		public int PersonId { get; set; }
	}
}

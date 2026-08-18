using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms.DynamicDataForReports.StudentReportInfo
{
	// Token: 0x020006BD RID: 1725
	[DataContract(Namespace = "http://tpro.ca")]
	[KnownType(typeof(byte[]))]
	public class StudentInfoAgeItemDTO : StudentInfoItemBaseDTO
	{
		// Token: 0x17000BED RID: 3053
		// (get) Token: 0x06002318 RID: 8984 RVA: 0x00010099 File Offset: 0x0000E299
		// (set) Token: 0x06002319 RID: 8985 RVA: 0x000100A1 File Offset: 0x0000E2A1
		[DataMember]
		public DateTime? DateOfBirth { get; set; }

		// Token: 0x17000BEE RID: 3054
		// (get) Token: 0x0600231A RID: 8986 RVA: 0x000100AA File Offset: 0x0000E2AA
		// (set) Token: 0x0600231B RID: 8987 RVA: 0x000100B2 File Offset: 0x0000E2B2
		[DataMember]
		public int Age { get; set; }
	}
}

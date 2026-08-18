using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms.DynamicDataForReports.StudentReportInfo
{
	// Token: 0x020006BC RID: 1724
	[DataContract(Namespace = "http://tpro.ca")]
	[KnownType(typeof(byte[]))]
	public class StudentInfoAccExpiryItemDTO : StudentInfoItemBaseDTO
	{
		// Token: 0x17000BEC RID: 3052
		// (get) Token: 0x06002315 RID: 8981 RVA: 0x0001007F File Offset: 0x0000E27F
		// (set) Token: 0x06002316 RID: 8982 RVA: 0x00010087 File Offset: 0x0000E287
		[DataMember]
		public DateTime AccExpiry { get; set; }
	}
}

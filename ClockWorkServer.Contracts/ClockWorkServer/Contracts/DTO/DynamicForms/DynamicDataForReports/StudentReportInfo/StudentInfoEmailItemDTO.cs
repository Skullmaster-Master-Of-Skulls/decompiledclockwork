using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms.DynamicDataForReports.StudentReportInfo
{
	// Token: 0x020006BF RID: 1727
	[DataContract(Namespace = "http://tpro.ca")]
	[KnownType(typeof(byte[]))]
	public class StudentInfoEmailItemDTO : StudentInfoItemBaseDTO
	{
		// Token: 0x17000BF6 RID: 3062
		// (get) Token: 0x0600232C RID: 9004 RVA: 0x00010132 File Offset: 0x0000E332
		// (set) Token: 0x0600232D RID: 9005 RVA: 0x0001013A File Offset: 0x0000E33A
		[DataMember]
		public string Email { get; set; }
	}
}

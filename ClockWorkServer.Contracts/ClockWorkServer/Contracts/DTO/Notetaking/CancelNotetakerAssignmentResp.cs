using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Notetaking
{
	// Token: 0x02000453 RID: 1107
	[DataContract(Namespace = "http://tpro.ca")]
	public class CancelNotetakerAssignmentResp
	{
		// Token: 0x17000776 RID: 1910
		// (get) Token: 0x060017B3 RID: 6067 RVA: 0x0000AF38 File Offset: 0x00009138
		// (set) Token: 0x060017B4 RID: 6068 RVA: 0x0000AF40 File Offset: 0x00009140
		[DataMember]
		public NotetakerBaseWithLookupCourseBaseDTO NotetakerAndCourse { get; set; }
	}
}

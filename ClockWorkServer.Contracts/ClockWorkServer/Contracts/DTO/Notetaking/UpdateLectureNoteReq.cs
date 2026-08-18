using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Notetaking
{
	// Token: 0x02000447 RID: 1095
	[DataContract(Namespace = "http://tpro.ca")]
	public class UpdateLectureNoteReq : BaseReportMessageReq
	{
		// Token: 0x17000763 RID: 1891
		// (get) Token: 0x06001781 RID: 6017 RVA: 0x0000ADF5 File Offset: 0x00008FF5
		// (set) Token: 0x06001782 RID: 6018 RVA: 0x0000ADFD File Offset: 0x00008FFD
		[DataMember]
		public LectureNoteDTO LectureNote { get; set; }
	}
}

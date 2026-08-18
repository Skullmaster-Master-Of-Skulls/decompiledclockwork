using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Notetaking
{
	// Token: 0x02000420 RID: 1056
	[DataContract(Namespace = "http://tpro.ca")]
	public class DeleteAllNotesMarkedForDeletionResp
	{
		// Token: 0x17000738 RID: 1848
		// (get) Token: 0x06001704 RID: 5892 RVA: 0x0000AB1A File Offset: 0x00008D1A
		// (set) Token: 0x06001705 RID: 5893 RVA: 0x0000AB22 File Offset: 0x00008D22
		[DataMember]
		public int NumNotesDeleted { get; set; }
	}
}

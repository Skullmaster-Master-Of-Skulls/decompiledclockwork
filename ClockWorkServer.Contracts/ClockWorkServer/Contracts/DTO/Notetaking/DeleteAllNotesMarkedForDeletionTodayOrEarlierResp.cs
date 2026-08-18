using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Notetaking
{
	// Token: 0x0200041E RID: 1054
	[DataContract(Namespace = "http://tpro.ca")]
	public class DeleteAllNotesMarkedForDeletionTodayOrEarlierResp
	{
		// Token: 0x17000737 RID: 1847
		// (get) Token: 0x06001700 RID: 5888 RVA: 0x0000AB09 File Offset: 0x00008D09
		// (set) Token: 0x06001701 RID: 5889 RVA: 0x0000AB11 File Offset: 0x00008D11
		[DataMember]
		public int NumNotesDeleted { get; set; }
	}
}

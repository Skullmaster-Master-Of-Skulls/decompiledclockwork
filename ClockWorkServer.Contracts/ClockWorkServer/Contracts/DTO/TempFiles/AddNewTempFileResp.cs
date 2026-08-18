using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.TempFiles
{
	// Token: 0x020001DB RID: 475
	[DataContract(Namespace = "http://tpro.ca")]
	public class AddNewTempFileResp
	{
		// Token: 0x1700023E RID: 574
		// (get) Token: 0x06000AC2 RID: 2754 RVA: 0x00004F12 File Offset: 0x00003112
		// (set) Token: 0x06000AC3 RID: 2755 RVA: 0x00004F1A File Offset: 0x0000311A
		[DataMember]
		public int NewTempFileId { get; set; }
	}
}

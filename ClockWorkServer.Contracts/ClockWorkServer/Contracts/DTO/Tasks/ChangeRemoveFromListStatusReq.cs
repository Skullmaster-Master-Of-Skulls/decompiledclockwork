using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Tasks
{
	// Token: 0x020001EF RID: 495
	[DataContract(Namespace = "http://tpro.ca")]
	public class ChangeRemoveFromListStatusReq : BaseMessageReq
	{
		// Token: 0x17000282 RID: 642
		// (get) Token: 0x06000B5C RID: 2908 RVA: 0x00005396 File Offset: 0x00003596
		// (set) Token: 0x06000B5D RID: 2909 RVA: 0x0000539E File Offset: 0x0000359E
		[DataMember]
		public int TaskId { get; set; }

		// Token: 0x17000283 RID: 643
		// (get) Token: 0x06000B5E RID: 2910 RVA: 0x000053A7 File Offset: 0x000035A7
		// (set) Token: 0x06000B5F RID: 2911 RVA: 0x000053AF File Offset: 0x000035AF
		[DataMember]
		public bool NewRemoveFromListStatus { get; set; }
	}
}

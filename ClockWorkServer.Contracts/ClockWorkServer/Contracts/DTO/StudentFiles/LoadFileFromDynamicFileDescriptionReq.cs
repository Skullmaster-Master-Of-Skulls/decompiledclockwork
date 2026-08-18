using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.StudentFiles
{
	// Token: 0x0200022A RID: 554
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadFileFromDynamicFileDescriptionReq : BaseMessageReq
	{
		// Token: 0x170002FA RID: 762
		// (get) Token: 0x06000C87 RID: 3207 RVA: 0x00005B8E File Offset: 0x00003D8E
		// (set) Token: 0x06000C88 RID: 3208 RVA: 0x00005B96 File Offset: 0x00003D96
		[DataMember]
		public int StudentPersonId { get; set; }

		// Token: 0x170002FB RID: 763
		// (get) Token: 0x06000C89 RID: 3209 RVA: 0x00005B9F File Offset: 0x00003D9F
		// (set) Token: 0x06000C8A RID: 3210 RVA: 0x00005BA7 File Offset: 0x00003DA7
		[DataMember]
		public DynamicFileDescriptionDTO DynamicFileDescription { get; set; }
	}
}

using System;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Contracts.DTO.Files;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.StudentFiles
{
	// Token: 0x0200022B RID: 555
	[DataContract(Namespace = "http://tpro.ca")]
	public class LoadFileFromDynamicFileDescriptionResp
	{
		// Token: 0x170002FC RID: 764
		// (get) Token: 0x06000C8C RID: 3212 RVA: 0x00005BB0 File Offset: 0x00003DB0
		// (set) Token: 0x06000C8D RID: 3213 RVA: 0x00005BB8 File Offset: 0x00003DB8
		[DataMember]
		public BinaryFileDTO File { get; set; }
	}
}

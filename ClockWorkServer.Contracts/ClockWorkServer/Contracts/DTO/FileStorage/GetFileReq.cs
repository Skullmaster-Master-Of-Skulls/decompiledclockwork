using System;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.FileStorage;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.FileStorage
{
	// Token: 0x020005F5 RID: 1525
	[DataContract(Namespace = "http://tpro.ca")]
	public class GetFileReq : BaseMessageReq
	{
		// Token: 0x17000A64 RID: 2660
		// (get) Token: 0x06001F37 RID: 7991 RVA: 0x0000E316 File Offset: 0x0000C516
		// (set) Token: 0x06001F38 RID: 7992 RVA: 0x0000E31E File Offset: 0x0000C51E
		[DataMember]
		public string Filename { get; set; }

		// Token: 0x17000A65 RID: 2661
		// (get) Token: 0x06001F39 RID: 7993 RVA: 0x0000E327 File Offset: 0x0000C527
		// (set) Token: 0x06001F3A RID: 7994 RVA: 0x0000E32F File Offset: 0x0000C52F
		[DataMember]
		public string ServerFolderPath { get; set; }

		// Token: 0x17000A66 RID: 2662
		// (get) Token: 0x06001F3B RID: 7995 RVA: 0x0000E338 File Offset: 0x0000C538
		// (set) Token: 0x06001F3C RID: 7996 RVA: 0x0000E340 File Offset: 0x0000C540
		[DataMember]
		public eServerStorageSpecialFolders SpecialFolder { get; set; }
	}
}

using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Updates
{
	// Token: 0x02000173 RID: 371
	[DataContract(Namespace = "http://tpro.ca")]
	public class UploadUpdateFileResultDTO
	{
		// Token: 0x17000183 RID: 387
		// (get) Token: 0x060008EB RID: 2283 RVA: 0x00003FDE File Offset: 0x000021DE
		// (set) Token: 0x060008EC RID: 2284 RVA: 0x00003FE6 File Offset: 0x000021E6
		[DataMember]
		public bool WasSuccessfullUpload { get; set; }

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x060008ED RID: 2285 RVA: 0x00003FEF File Offset: 0x000021EF
		// (set) Token: 0x060008EE RID: 2286 RVA: 0x00003FF7 File Offset: 0x000021F7
		[DataMember]
		public string Filename { get; set; }

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x060008EF RID: 2287 RVA: 0x00004000 File Offset: 0x00002200
		// (set) Token: 0x060008F0 RID: 2288 RVA: 0x00004008 File Offset: 0x00002208
		[DataMember]
		public string Folder { get; set; }

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x060008F1 RID: 2289 RVA: 0x00004011 File Offset: 0x00002211
		// (set) Token: 0x060008F2 RID: 2290 RVA: 0x00004019 File Offset: 0x00002219
		[DataMember]
		public string ErrorMessage { get; set; }
	}
}

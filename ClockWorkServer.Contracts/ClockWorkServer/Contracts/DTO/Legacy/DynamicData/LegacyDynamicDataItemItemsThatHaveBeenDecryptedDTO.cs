using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Legacy.DynamicData
{
	// Token: 0x020004DD RID: 1245
	[DataContract(Namespace = "http://tpro.ca")]
	public class LegacyDynamicDataItemItemsThatHaveBeenDecryptedDTO
	{
		// Token: 0x17000880 RID: 2176
		// (get) Token: 0x06001A58 RID: 6744 RVA: 0x0000C2B7 File Offset: 0x0000A4B7
		// (set) Token: 0x06001A59 RID: 6745 RVA: 0x0000C2BF File Offset: 0x0000A4BF
		[DataMember]
		public int Id { get; set; }

		// Token: 0x17000881 RID: 2177
		// (get) Token: 0x06001A5A RID: 6746 RVA: 0x0000C2C8 File Offset: 0x0000A4C8
		// (set) Token: 0x06001A5B RID: 6747 RVA: 0x0000C2D0 File Offset: 0x0000A4D0
		[DataMember]
		public string ControlValueDecryptedString { get; set; }

		// Token: 0x17000882 RID: 2178
		// (get) Token: 0x06001A5C RID: 6748 RVA: 0x0000C2D9 File Offset: 0x0000A4D9
		// (set) Token: 0x06001A5D RID: 6749 RVA: 0x0000C2E1 File Offset: 0x0000A4E1
		[DataMember]
		public string TextForLetter { get; set; }

		// Token: 0x17000883 RID: 2179
		// (get) Token: 0x06001A5E RID: 6750 RVA: 0x0000C2EA File Offset: 0x0000A4EA
		// (set) Token: 0x06001A5F RID: 6751 RVA: 0x0000C2F2 File Offset: 0x0000A4F2
		[DataMember]
		public string PrivateNote { get; set; }

		// Token: 0x17000884 RID: 2180
		// (get) Token: 0x06001A60 RID: 6752 RVA: 0x0000C2FB File Offset: 0x0000A4FB
		// (set) Token: 0x06001A61 RID: 6753 RVA: 0x0000C303 File Offset: 0x0000A503
		[DataMember]
		public string RecommendedToStudentButDeclinedDetail { get; set; }
	}
}

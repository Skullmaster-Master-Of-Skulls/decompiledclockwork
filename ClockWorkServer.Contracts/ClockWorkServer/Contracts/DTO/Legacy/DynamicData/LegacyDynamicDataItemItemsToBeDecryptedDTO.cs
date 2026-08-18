using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Legacy.DynamicData
{
	// Token: 0x020004DE RID: 1246
	[DataContract(Namespace = "http://tpro.ca")]
	public class LegacyDynamicDataItemItemsToBeDecryptedDTO
	{
		// Token: 0x17000885 RID: 2181
		// (get) Token: 0x06001A63 RID: 6755 RVA: 0x0000C30C File Offset: 0x0000A50C
		// (set) Token: 0x06001A64 RID: 6756 RVA: 0x0000C314 File Offset: 0x0000A514
		[DataMember]
		public int Id { get; set; }

		// Token: 0x17000886 RID: 2182
		// (get) Token: 0x06001A65 RID: 6757 RVA: 0x0000C31D File Offset: 0x0000A51D
		// (set) Token: 0x06001A66 RID: 6758 RVA: 0x0000C325 File Offset: 0x0000A525
		[DataMember]
		public byte[] ControlValueBytes { get; set; }

		// Token: 0x17000887 RID: 2183
		// (get) Token: 0x06001A67 RID: 6759 RVA: 0x0000C32E File Offset: 0x0000A52E
		// (set) Token: 0x06001A68 RID: 6760 RVA: 0x0000C336 File Offset: 0x0000A536
		[DataMember]
		public byte[] TextForLetterEncrypted { get; set; }

		// Token: 0x17000888 RID: 2184
		// (get) Token: 0x06001A69 RID: 6761 RVA: 0x0000C33F File Offset: 0x0000A53F
		// (set) Token: 0x06001A6A RID: 6762 RVA: 0x0000C347 File Offset: 0x0000A547
		[DataMember]
		public byte[] PrivateNoteEncrypted { get; set; }

		// Token: 0x17000889 RID: 2185
		// (get) Token: 0x06001A6B RID: 6763 RVA: 0x0000C350 File Offset: 0x0000A550
		// (set) Token: 0x06001A6C RID: 6764 RVA: 0x0000C358 File Offset: 0x0000A558
		[DataMember]
		public byte[] RecommendedToStudentButDeclinedDetailEncrypted { get; set; }
	}
}

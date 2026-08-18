using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.Files
{
	// Token: 0x020005F2 RID: 1522
	[DataContract(Namespace = "http://tpro.ca")]
	public class FileStructureDTO
	{
		// Token: 0x17000A59 RID: 2649
		// (get) Token: 0x06001F1E RID: 7966 RVA: 0x0000E252 File Offset: 0x0000C452
		// (set) Token: 0x06001F1F RID: 7967 RVA: 0x0000E25A File Offset: 0x0000C45A
		[DataMember]
		public byte[] BinaryData { get; set; }

		// Token: 0x17000A5A RID: 2650
		// (get) Token: 0x06001F20 RID: 7968 RVA: 0x0000E263 File Offset: 0x0000C463
		// (set) Token: 0x06001F21 RID: 7969 RVA: 0x0000E26B File Offset: 0x0000C46B
		[DataMember]
		public FileTypeDTO FileType { get; set; }

		// Token: 0x17000A5B RID: 2651
		// (get) Token: 0x06001F22 RID: 7970 RVA: 0x0000E274 File Offset: 0x0000C474
		// (set) Token: 0x06001F23 RID: 7971 RVA: 0x0000E27C File Offset: 0x0000C47C
		[DataMember]
		public string Version { get; set; }

		// Token: 0x17000A5C RID: 2652
		// (get) Token: 0x06001F24 RID: 7972 RVA: 0x0000E285 File Offset: 0x0000C485
		// (set) Token: 0x06001F25 RID: 7973 RVA: 0x0000E28D File Offset: 0x0000C48D
		[DataMember]
		public DateTime UploadDateTime { get; set; }

		// Token: 0x17000A5D RID: 2653
		// (get) Token: 0x06001F26 RID: 7974 RVA: 0x0000E296 File Offset: 0x0000C496
		// (set) Token: 0x06001F27 RID: 7975 RVA: 0x0000E29E File Offset: 0x0000C49E
		[DataMember]
		public bool IsActive { get; set; }

		// Token: 0x17000A5E RID: 2654
		// (get) Token: 0x06001F28 RID: 7976 RVA: 0x0000E2A7 File Offset: 0x0000C4A7
		// (set) Token: 0x06001F29 RID: 7977 RVA: 0x0000E2AF File Offset: 0x0000C4AF
		[DataMember]
		public int WhoUploaded { get; set; }

		// Token: 0x17000A5F RID: 2655
		// (get) Token: 0x06001F2A RID: 7978 RVA: 0x0000E2B8 File Offset: 0x0000C4B8
		// (set) Token: 0x06001F2B RID: 7979 RVA: 0x0000E2C0 File Offset: 0x0000C4C0
		[DataMember]
		public int AddrSize { get; set; }
	}
}

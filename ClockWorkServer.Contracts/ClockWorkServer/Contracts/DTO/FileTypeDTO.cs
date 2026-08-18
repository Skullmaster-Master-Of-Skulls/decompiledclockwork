using System;
using System.Runtime.Serialization;

namespace TechnoPro.ClockWorkServer.Contracts.DTO
{
	// Token: 0x020000FA RID: 250
	[DataContract(Namespace = "http://tpro.ca")]
	public class FileTypeDTO
	{
		// Token: 0x1700007C RID: 124
		// (get) Token: 0x0600065C RID: 1628 RVA: 0x00002C47 File Offset: 0x00000E47
		// (set) Token: 0x0600065D RID: 1629 RVA: 0x00002C4F File Offset: 0x00000E4F
		[DataMember]
		public string Title { get; set; }

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x0600065E RID: 1630 RVA: 0x00002C58 File Offset: 0x00000E58
		// (set) Token: 0x0600065F RID: 1631 RVA: 0x00002C60 File Offset: 0x00000E60
		[DataMember]
		public string Description { get; set; }

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x06000660 RID: 1632 RVA: 0x00002C69 File Offset: 0x00000E69
		// (set) Token: 0x06000661 RID: 1633 RVA: 0x00002C71 File Offset: 0x00000E71
		[DataMember]
		public string Extension { get; set; }

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x06000662 RID: 1634 RVA: 0x00002C7A File Offset: 0x00000E7A
		// (set) Token: 0x06000663 RID: 1635 RVA: 0x00002C82 File Offset: 0x00000E82
		[DataMember]
		public bool AddrSizeVersion { get; set; }

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x06000664 RID: 1636 RVA: 0x00002C8B File Offset: 0x00000E8B
		// (set) Token: 0x06000665 RID: 1637 RVA: 0x00002C93 File Offset: 0x00000E93
		[DataMember]
		public string SecondaryTitle { get; set; }
	}
}

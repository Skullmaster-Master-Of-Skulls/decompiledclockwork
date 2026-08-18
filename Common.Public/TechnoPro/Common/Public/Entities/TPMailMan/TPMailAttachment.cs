using System;

namespace TechnoPro.Common.Public.Entities.TPMailMan
{
	// Token: 0x02000161 RID: 353
	[Serializable]
	public class TPMailAttachment : BusinessBase<string>, ICloneable<TPMailAttachment>, ICloneable
	{
		// Token: 0x1700030A RID: 778
		// (get) Token: 0x0600085E RID: 2142 RVA: 0x00011B4C File Offset: 0x0000FD4C
		// (set) Token: 0x0600085F RID: 2143 RVA: 0x00011B54 File Offset: 0x0000FD54
		public string FileNameForDisplay { get; set; }

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x06000860 RID: 2144 RVA: 0x00011B5D File Offset: 0x0000FD5D
		// (set) Token: 0x06000861 RID: 2145 RVA: 0x00011B65 File Offset: 0x0000FD65
		public byte[] FileBytes { get; set; }

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x06000862 RID: 2146 RVA: 0x00011B6E File Offset: 0x0000FD6E
		// (set) Token: 0x06000863 RID: 2147 RVA: 0x00011B76 File Offset: 0x0000FD76
		public int FileIdForSavedAttachment { get; set; }

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x06000864 RID: 2148 RVA: 0x00011B7F File Offset: 0x0000FD7F
		// (set) Token: 0x06000865 RID: 2149 RVA: 0x00011B87 File Offset: 0x0000FD87
		public int FileAttachmentId { get; set; }

		// Token: 0x06000866 RID: 2150 RVA: 0x00011AE5 File Offset: 0x0000FCE5
		public TPMailAttachment()
		{
		}

		// Token: 0x06000867 RID: 2151 RVA: 0x00011B90 File Offset: 0x0000FD90
		public TPMailAttachment Clone()
		{
			return new TPMailAttachment(this);
		}

		// Token: 0x06000868 RID: 2152 RVA: 0x00011BA8 File Offset: 0x0000FDA8
		object ICloneable.Clone()
		{
			return this.Clone();
		}

		// Token: 0x06000869 RID: 2153 RVA: 0x00011BC0 File Offset: 0x0000FDC0
		public TPMailAttachment(TPMailAttachment item)
		{
			bool flag = item == null;
			if (!flag)
			{
				this.FileNameForDisplay = item.FileNameForDisplay;
				this.FileBytes = item.FileBytes;
				this.FileIdForSavedAttachment = item.FileIdForSavedAttachment;
				this.FileAttachmentId = item.FileAttachmentId;
			}
		}
	}
}

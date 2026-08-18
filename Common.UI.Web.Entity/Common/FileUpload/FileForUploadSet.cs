using System;
using System.Collections.Generic;

namespace TechnoPro.Common.UI.Web.Entity.Common.FileUpload
{
	// Token: 0x0200003F RID: 63
	[Serializable]
	public class FileForUploadSet
	{
		// Token: 0x06000178 RID: 376 RVA: 0x000038E0 File Offset: 0x00001AE0
		public FileForUploadSet()
		{
			this.DateCreated = DateTime.Now;
			this.Guid = System.Guid.NewGuid().ToString();
			this.FilesForUpload = new List<FileForUpload>();
			this.LastActionStatus = "";
		}

		// Token: 0x06000179 RID: 377 RVA: 0x00003933 File Offset: 0x00001B33
		public FileForUploadSet(string guid)
		{
			this.DateCreated = DateTime.Now;
			this.Guid = guid;
			this.FilesForUpload = new List<FileForUpload>();
			this.LastActionStatus = "";
		}

		// Token: 0x170000AE RID: 174
		// (get) Token: 0x0600017A RID: 378 RVA: 0x00003969 File Offset: 0x00001B69
		// (set) Token: 0x0600017B RID: 379 RVA: 0x00003971 File Offset: 0x00001B71
		public DateTime DateCreated { get; set; }

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x0600017C RID: 380 RVA: 0x0000397A File Offset: 0x00001B7A
		// (set) Token: 0x0600017D RID: 381 RVA: 0x00003982 File Offset: 0x00001B82
		public string Guid { get; set; }

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x0600017E RID: 382 RVA: 0x0000398B File Offset: 0x00001B8B
		// (set) Token: 0x0600017F RID: 383 RVA: 0x00003993 File Offset: 0x00001B93
		public IList<FileForUpload> FilesForUpload { get; set; }

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000180 RID: 384 RVA: 0x0000399C File Offset: 0x00001B9C
		// (set) Token: 0x06000181 RID: 385 RVA: 0x000039A4 File Offset: 0x00001BA4
		public string LastActionStatus { get; set; }
	}
}

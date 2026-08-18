using System;
using TechnoPro.Common.Public.Entities.Files;

namespace TechnoPro.Common.Public.Entities.DynamicForms.DynamicLists
{
	// Token: 0x02000379 RID: 889
	public class DynamicFile : BusinessBase<int>
	{
		// Token: 0x17000B6F RID: 2927
		// (get) Token: 0x06001B8B RID: 7051 RVA: 0x0001F614 File Offset: 0x0001D814
		// (set) Token: 0x06001B8C RID: 7052 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int FileId
		{
			get
			{
				return this.Id;
			}
			set
			{
				this.Id = value;
			}
		}

		// Token: 0x17000B70 RID: 2928
		// (get) Token: 0x06001B8D RID: 7053 RVA: 0x0001F62C File Offset: 0x0001D82C
		// (set) Token: 0x06001B8E RID: 7054 RVA: 0x0001F634 File Offset: 0x0001D834
		public eDynamicFileTypeCode FileTypeCode { get; set; }

		// Token: 0x17000B71 RID: 2929
		// (get) Token: 0x06001B8F RID: 7055 RVA: 0x0001F63D File Offset: 0x0001D83D
		// (set) Token: 0x06001B90 RID: 7056 RVA: 0x0001F645 File Offset: 0x0001D845
		public BinaryFile FileContents { get; set; }

		// Token: 0x17000B72 RID: 2930
		// (get) Token: 0x06001B91 RID: 7057 RVA: 0x0001F64E File Offset: 0x0001D84E
		// (set) Token: 0x06001B92 RID: 7058 RVA: 0x0001F656 File Offset: 0x0001D856
		public DateTime DateUploaded { get; set; }

		// Token: 0x17000B73 RID: 2931
		// (get) Token: 0x06001B93 RID: 7059 RVA: 0x0001F65F File Offset: 0x0001D85F
		// (set) Token: 0x06001B94 RID: 7060 RVA: 0x0001F667 File Offset: 0x0001D867
		public int? WhoUploadedPersonId { get; set; }
	}
}

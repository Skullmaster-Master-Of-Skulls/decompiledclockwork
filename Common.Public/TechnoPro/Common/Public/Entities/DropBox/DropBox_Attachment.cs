using System;

namespace TechnoPro.Common.Public.Entities.DropBox
{
	// Token: 0x020003C2 RID: 962
	public class DropBox_Attachment : BusinessBase<int>
	{
		// Token: 0x17000C1B RID: 3099
		// (get) Token: 0x06001D60 RID: 7520 RVA: 0x0002133C File Offset: 0x0001F53C
		// (set) Token: 0x06001D61 RID: 7521 RVA: 0x00021344 File Offset: 0x0001F544
		public virtual DropBox_AttachmentInfo Info { get; set; }

		// Token: 0x17000C1C RID: 3100
		// (get) Token: 0x06001D62 RID: 7522 RVA: 0x0002134D File Offset: 0x0001F54D
		// (set) Token: 0x06001D63 RID: 7523 RVA: 0x00021355 File Offset: 0x0001F555
		public virtual byte[] BinaryData { get; set; }

		// Token: 0x17000C1D RID: 3101
		// (get) Token: 0x06001D64 RID: 7524 RVA: 0x00021360 File Offset: 0x0001F560
		// (set) Token: 0x06001D65 RID: 7525 RVA: 0x0002137D File Offset: 0x0001F57D
		public override int Id
		{
			get
			{
				return this.Info.Id;
			}
			set
			{
				this.Info.Id = value;
			}
		}
	}
}

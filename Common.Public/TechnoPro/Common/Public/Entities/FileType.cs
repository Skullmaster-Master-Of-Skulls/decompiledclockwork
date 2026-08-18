using System;

namespace TechnoPro.Common.Public.Entities
{
	// Token: 0x020000ED RID: 237
	[Serializable]
	public class FileType : BusinessBase<string>
	{
		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x06000585 RID: 1413 RVA: 0x0000E9E4 File Offset: 0x0000CBE4
		// (set) Token: 0x06000586 RID: 1414 RVA: 0x0000E9FC File Offset: 0x0000CBFC
		public virtual string Title
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

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06000587 RID: 1415 RVA: 0x0000EA07 File Offset: 0x0000CC07
		// (set) Token: 0x06000588 RID: 1416 RVA: 0x0000EA0F File Offset: 0x0000CC0F
		public virtual string Description { get; set; }

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x06000589 RID: 1417 RVA: 0x0000EA18 File Offset: 0x0000CC18
		// (set) Token: 0x0600058A RID: 1418 RVA: 0x0000EA20 File Offset: 0x0000CC20
		public virtual string Extension { get; set; }

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x0600058B RID: 1419 RVA: 0x0000EA29 File Offset: 0x0000CC29
		// (set) Token: 0x0600058C RID: 1420 RVA: 0x0000EA31 File Offset: 0x0000CC31
		public virtual bool AddrSizeVersion { get; set; }

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x0600058D RID: 1421 RVA: 0x0000EA3A File Offset: 0x0000CC3A
		// (set) Token: 0x0600058E RID: 1422 RVA: 0x0000EA42 File Offset: 0x0000CC42
		public virtual string SecondaryTitle { get; set; }
	}
}

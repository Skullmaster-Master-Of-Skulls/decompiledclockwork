using System;

namespace TechnoPro.Common.Public.Entities.Appointments
{
	// Token: 0x020004C0 RID: 1216
	[Serializable]
	public class IconInfo : BusinessBase<int>
	{
		// Token: 0x17000F3A RID: 3898
		// (get) Token: 0x060024C5 RID: 9413 RVA: 0x00027C5C File Offset: 0x00025E5C
		// (set) Token: 0x060024C6 RID: 9414 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int IconInfoId
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

		// Token: 0x17000F3B RID: 3899
		// (get) Token: 0x060024C7 RID: 9415 RVA: 0x00027C74 File Offset: 0x00025E74
		// (set) Token: 0x060024C8 RID: 9416 RVA: 0x00027C7C File Offset: 0x00025E7C
		public string IconText { get; set; }

		// Token: 0x17000F3C RID: 3900
		// (get) Token: 0x060024C9 RID: 9417 RVA: 0x00027C85 File Offset: 0x00025E85
		// (set) Token: 0x060024CA RID: 9418 RVA: 0x00027C8D File Offset: 0x00025E8D
		public string IconLetterIdentifier { get; set; }

		// Token: 0x17000F3D RID: 3901
		// (get) Token: 0x060024CB RID: 9419 RVA: 0x00027C96 File Offset: 0x00025E96
		// (set) Token: 0x060024CC RID: 9420 RVA: 0x00027C9E File Offset: 0x00025E9E
		public int IconNum { get; set; }
	}
}

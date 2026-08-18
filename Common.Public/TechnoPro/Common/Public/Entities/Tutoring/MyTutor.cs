using System;

namespace TechnoPro.Common.Public.Entities.Tutoring
{
	// Token: 0x0200015A RID: 346
	public class MyTutor : BusinessBase<int>
	{
		// Token: 0x170002FB RID: 763
		// (get) Token: 0x0600083A RID: 2106 RVA: 0x0001191C File Offset: 0x0000FB1C
		public override int Id
		{
			get
			{
				return (this.Tutor == null) ? 0 : this.Tutor.PersonId;
			}
		}

		// Token: 0x170002FC RID: 764
		// (get) Token: 0x0600083B RID: 2107 RVA: 0x00011944 File Offset: 0x0000FB44
		// (set) Token: 0x0600083C RID: 2108 RVA: 0x0001194C File Offset: 0x0000FB4C
		public Tutor Tutor { get; set; }

		// Token: 0x170002FD RID: 765
		// (get) Token: 0x0600083D RID: 2109 RVA: 0x00011955 File Offset: 0x0000FB55
		// (set) Token: 0x0600083E RID: 2110 RVA: 0x0001195D File Offset: 0x0000FB5D
		public int StudentPersonId { get; set; }

		// Token: 0x170002FE RID: 766
		// (get) Token: 0x0600083F RID: 2111 RVA: 0x00011966 File Offset: 0x0000FB66
		// (set) Token: 0x06000840 RID: 2112 RVA: 0x0001196E File Offset: 0x0000FB6E
		public DateTime LastDateMetWith { get; set; }
	}
}

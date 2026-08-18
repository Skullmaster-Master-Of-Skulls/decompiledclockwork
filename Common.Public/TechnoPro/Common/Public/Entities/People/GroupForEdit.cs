using System;

namespace TechnoPro.Common.Public.Entities.People
{
	// Token: 0x0200025B RID: 603
	public class GroupForEdit : BusinessBase<int>
	{
		// Token: 0x1700077A RID: 1914
		// (get) Token: 0x06001225 RID: 4645 RVA: 0x00018845 File Offset: 0x00016A45
		// (set) Token: 0x06001226 RID: 4646 RVA: 0x0001884D File Offset: 0x00016A4D
		public int GroupId { get; set; }

		// Token: 0x1700077B RID: 1915
		// (get) Token: 0x06001227 RID: 4647 RVA: 0x00018856 File Offset: 0x00016A56
		// (set) Token: 0x06001228 RID: 4648 RVA: 0x0001885E File Offset: 0x00016A5E
		public string Description { get; set; }

		// Token: 0x1700077C RID: 1916
		// (get) Token: 0x06001229 RID: 4649 RVA: 0x00018867 File Offset: 0x00016A67
		// (set) Token: 0x0600122A RID: 4650 RVA: 0x0001886F File Offset: 0x00016A6F
		public bool IsPrimary { get; set; }

		// Token: 0x1700077D RID: 1917
		// (get) Token: 0x0600122B RID: 4651 RVA: 0x00018878 File Offset: 0x00016A78
		// (set) Token: 0x0600122C RID: 4652 RVA: 0x00018880 File Offset: 0x00016A80
		public bool ViewAppsVisible { get; set; }

		// Token: 0x1700077E RID: 1918
		// (get) Token: 0x0600122D RID: 4653 RVA: 0x00018889 File Offset: 0x00016A89
		// (set) Token: 0x0600122E RID: 4654 RVA: 0x00018891 File Offset: 0x00016A91
		public string FullDescription { get; set; }

		// Token: 0x1700077F RID: 1919
		// (get) Token: 0x0600122F RID: 4655 RVA: 0x0001889A File Offset: 0x00016A9A
		// (set) Token: 0x06001230 RID: 4656 RVA: 0x000188A2 File Offset: 0x00016AA2
		public int OrderNum { get; set; }
	}
}

using System;

namespace Telerik.Web.UI
{
	// Token: 0x02000749 RID: 1865
	public class PivotGridBaseModelCell
	{
		// Token: 0x17001585 RID: 5509
		// (get) Token: 0x0600420F RID: 16911 RVA: 0x000CF4E5 File Offset: 0x000CD6E5
		// (set) Token: 0x06004210 RID: 16912 RVA: 0x000CF4ED File Offset: 0x000CD6ED
		public PivotGridField Field { get; internal set; }

		// Token: 0x17001586 RID: 5510
		// (get) Token: 0x06004211 RID: 16913 RVA: 0x000CF4F6 File Offset: 0x000CD6F6
		// (set) Token: 0x06004212 RID: 16914 RVA: 0x000CF4FE File Offset: 0x000CD6FE
		public object Data { get; internal set; }

		// Token: 0x17001587 RID: 5511
		// (get) Token: 0x06004213 RID: 16915 RVA: 0x000CF507 File Offset: 0x000CD707
		// (set) Token: 0x06004214 RID: 16916 RVA: 0x000CF50F File Offset: 0x000CD70F
		public int GroupLevel { get; internal set; }

		// Token: 0x17001588 RID: 5512
		// (get) Token: 0x06004215 RID: 16917 RVA: 0x000CF518 File Offset: 0x000CD718
		// (set) Token: 0x06004216 RID: 16918 RVA: 0x000CF520 File Offset: 0x000CD720
		public bool IsCollapsed { get; internal set; }

		// Token: 0x17001589 RID: 5513
		// (get) Token: 0x06004217 RID: 16919 RVA: 0x000CF529 File Offset: 0x000CD729
		// (set) Token: 0x06004218 RID: 16920 RVA: 0x000CF531 File Offset: 0x000CD731
		public bool HasChildren { get; internal set; }

		// Token: 0x1700158A RID: 5514
		// (get) Token: 0x06004219 RID: 16921 RVA: 0x000CF53A File Offset: 0x000CD73A
		// (set) Token: 0x0600421A RID: 16922 RVA: 0x000CF542 File Offset: 0x000CD742
		public bool IsTotalCell { get; internal set; }

		// Token: 0x1700158B RID: 5515
		// (get) Token: 0x0600421B RID: 16923 RVA: 0x000CF54B File Offset: 0x000CD74B
		// (set) Token: 0x0600421C RID: 16924 RVA: 0x000CF553 File Offset: 0x000CD753
		public bool IsGrandTotalCell { get; internal set; }

		// Token: 0x1700158C RID: 5516
		// (get) Token: 0x0600421D RID: 16925 RVA: 0x000CF55C File Offset: 0x000CD75C
		// (set) Token: 0x0600421E RID: 16926 RVA: 0x000CF564 File Offset: 0x000CD764
		public PivotGridDataCellType CellType { get; internal set; }

		// Token: 0x1700158D RID: 5517
		// (get) Token: 0x0600421F RID: 16927 RVA: 0x000CF56D File Offset: 0x000CD76D
		// (set) Token: 0x06004220 RID: 16928 RVA: 0x000CF575 File Offset: 0x000CD775
		public PivotGridTableCellType TableCellType { get; internal set; }

		// Token: 0x1700158E RID: 5518
		// (get) Token: 0x06004221 RID: 16929 RVA: 0x000CF57E File Offset: 0x000CD77E
		// (set) Token: 0x06004222 RID: 16930 RVA: 0x000CF586 File Offset: 0x000CD786
		internal PivotGridModelCellBase BaseCell { get; set; }
	}
}

using System;

namespace TechnoPro.Common.Public.Entities.DynamicForms
{
	// Token: 0x02000371 RID: 881
	[Serializable]
	public class DynamicForm : DynamicFormBase
	{
		// Token: 0x17000B54 RID: 2900
		// (get) Token: 0x06001B4D RID: 6989 RVA: 0x0001F414 File Offset: 0x0001D614
		// (set) Token: 0x06001B4E RID: 6990 RVA: 0x0001F41C File Offset: 0x0001D61C
		public double ColumnWidthPercent { get; set; }

		// Token: 0x17000B55 RID: 2901
		// (get) Token: 0x06001B4F RID: 6991 RVA: 0x0001F425 File Offset: 0x0001D625
		// (set) Token: 0x06001B50 RID: 6992 RVA: 0x0001F42D File Offset: 0x0001D62D
		public bool BottomLess { get; set; }

		// Token: 0x17000B56 RID: 2902
		// (get) Token: 0x06001B51 RID: 6993 RVA: 0x0001F436 File Offset: 0x0001D636
		// (set) Token: 0x06001B52 RID: 6994 RVA: 0x0001F43E File Offset: 0x0001D63E
		public string CSharp_FormLoad { get; set; }

		// Token: 0x17000B57 RID: 2903
		// (get) Token: 0x06001B53 RID: 6995 RVA: 0x0001F447 File Offset: 0x0001D647
		// (set) Token: 0x06001B54 RID: 6996 RVA: 0x0001F44F File Offset: 0x0001D64F
		public string CSharp_FormSave { get; set; }

		// Token: 0x17000B58 RID: 2904
		// (get) Token: 0x06001B55 RID: 6997 RVA: 0x0001F458 File Offset: 0x0001D658
		// (set) Token: 0x06001B56 RID: 6998 RVA: 0x0001F460 File Offset: 0x0001D660
		public string CSharp_Misc { get; set; }

		// Token: 0x17000B59 RID: 2905
		// (get) Token: 0x06001B57 RID: 6999 RVA: 0x0001F469 File Offset: 0x0001D669
		// (set) Token: 0x06001B58 RID: 7000 RVA: 0x0001F471 File Offset: 0x0001D671
		public string GroupName { get; set; }

		// Token: 0x17000B5A RID: 2906
		// (get) Token: 0x06001B59 RID: 7001 RVA: 0x0001F47A File Offset: 0x0001D67A
		// (set) Token: 0x06001B5A RID: 7002 RVA: 0x0001F482 File Offset: 0x0001D682
		public int LargeImageIndex { get; set; }

		// Token: 0x17000B5B RID: 2907
		// (get) Token: 0x06001B5B RID: 7003 RVA: 0x0001F48B File Offset: 0x0001D68B
		// (set) Token: 0x06001B5C RID: 7004 RVA: 0x0001F493 File Offset: 0x0001D693
		public int SmallImageIndex { get; set; }

		// Token: 0x17000B5C RID: 2908
		// (get) Token: 0x06001B5D RID: 7005 RVA: 0x0001F49C File Offset: 0x0001D69C
		// (set) Token: 0x06001B5E RID: 7006 RVA: 0x0001F4A4 File Offset: 0x0001D6A4
		public new DynamicForm SubForm { get; set; }
	}
}

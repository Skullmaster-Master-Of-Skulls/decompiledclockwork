using System;
using System.Collections.Generic;

namespace TechnoPro.Common.Public.Entities.DynamicForms
{
	// Token: 0x02000350 RID: 848
	public class DynamicFormWithExtendedInfo : DynamicForm
	{
		// Token: 0x17000AF0 RID: 2800
		// (get) Token: 0x06001A57 RID: 6743 RVA: 0x0001E6F3 File Offset: 0x0001C8F3
		// (set) Token: 0x06001A58 RID: 6744 RVA: 0x0001E6FB File Offset: 0x0001C8FB
		public int VerticalControlPadding { get; set; }

		// Token: 0x17000AF1 RID: 2801
		// (get) Token: 0x06001A59 RID: 6745 RVA: 0x0001E704 File Offset: 0x0001C904
		// (set) Token: 0x06001A5A RID: 6746 RVA: 0x0001E70C File Offset: 0x0001C90C
		public int ColumnPadding { get; set; }

		// Token: 0x17000AF2 RID: 2802
		// (get) Token: 0x06001A5B RID: 6747 RVA: 0x0001E715 File Offset: 0x0001C915
		// (set) Token: 0x06001A5C RID: 6748 RVA: 0x0001E71D File Offset: 0x0001C91D
		public DateTime DateAdded { get; set; }

		// Token: 0x17000AF3 RID: 2803
		// (get) Token: 0x06001A5D RID: 6749 RVA: 0x0001E726 File Offset: 0x0001C926
		// (set) Token: 0x06001A5E RID: 6750 RVA: 0x0001E72E File Offset: 0x0001C92E
		public DateTime? DateModified { get; set; }

		// Token: 0x17000AF4 RID: 2804
		// (get) Token: 0x06001A5F RID: 6751 RVA: 0x0001E737 File Offset: 0x0001C937
		// (set) Token: 0x06001A60 RID: 6752 RVA: 0x0001E73F File Offset: 0x0001C93F
		public bool StudentNameNumEditable { get; set; }

		// Token: 0x17000AF5 RID: 2805
		// (get) Token: 0x06001A61 RID: 6753 RVA: 0x0001E748 File Offset: 0x0001C948
		// (set) Token: 0x06001A62 RID: 6754 RVA: 0x0001E750 File Offset: 0x0001C950
		public int ScreenId { get; set; }

		// Token: 0x17000AF6 RID: 2806
		// (get) Token: 0x06001A63 RID: 6755 RVA: 0x0001E759 File Offset: 0x0001C959
		// (set) Token: 0x06001A64 RID: 6756 RVA: 0x0001E761 File Offset: 0x0001C961
		public string FontName { get; set; }

		// Token: 0x17000AF7 RID: 2807
		// (get) Token: 0x06001A65 RID: 6757 RVA: 0x0001E76A File Offset: 0x0001C96A
		// (set) Token: 0x06001A66 RID: 6758 RVA: 0x0001E772 File Offset: 0x0001C972
		public int FontSize { get; set; }

		// Token: 0x17000AF8 RID: 2808
		// (get) Token: 0x06001A67 RID: 6759 RVA: 0x0001E77B File Offset: 0x0001C97B
		// (set) Token: 0x06001A68 RID: 6760 RVA: 0x0001E783 File Offset: 0x0001C983
		public IList<int> GroupIds { get; set; }

		// Token: 0x17000AF9 RID: 2809
		// (get) Token: 0x06001A69 RID: 6761 RVA: 0x0001E78C File Offset: 0x0001C98C
		// (set) Token: 0x06001A6A RID: 6762 RVA: 0x0001E794 File Offset: 0x0001C994
		public bool IsWebScreen { get; set; }

		// Token: 0x17000AFA RID: 2810
		// (get) Token: 0x06001A6B RID: 6763 RVA: 0x0001E79D File Offset: 0x0001C99D
		// (set) Token: 0x06001A6C RID: 6764 RVA: 0x0001E7A5 File Offset: 0x0001C9A5
		public int ControlIdToActivate { get; set; }

		// Token: 0x17000AFB RID: 2811
		// (get) Token: 0x06001A6D RID: 6765 RVA: 0x0001E7AE File Offset: 0x0001C9AE
		// (set) Token: 0x06001A6E RID: 6766 RVA: 0x0001E7B6 File Offset: 0x0001C9B6
		public string StudentNumberCaption { get; set; }

		// Token: 0x17000AFC RID: 2812
		// (get) Token: 0x06001A6F RID: 6767 RVA: 0x0001E7BF File Offset: 0x0001C9BF
		// (set) Token: 0x06001A70 RID: 6768 RVA: 0x0001E7C7 File Offset: 0x0001C9C7
		public string StudentNumberAutoGenerateRule { get; set; }

		// Token: 0x17000AFD RID: 2813
		// (get) Token: 0x06001A71 RID: 6769 RVA: 0x0001E7D0 File Offset: 0x0001C9D0
		// (set) Token: 0x06001A72 RID: 6770 RVA: 0x0001E7D8 File Offset: 0x0001C9D8
		public bool StudentNameHidden { get; set; }
	}
}

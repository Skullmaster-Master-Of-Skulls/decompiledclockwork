using System;

namespace TechnoPro.Common.Public.Entities.DynamicForms
{
	// Token: 0x0200034C RID: 844
	[Serializable]
	public class DynamicFormBase : BusinessBase<int>
	{
		// Token: 0x17000ADF RID: 2783
		// (get) Token: 0x06001A31 RID: 6705 RVA: 0x0001E5C4 File Offset: 0x0001C7C4
		// (set) Token: 0x06001A32 RID: 6706 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int ScreenNum
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

		// Token: 0x17000AE0 RID: 2784
		// (get) Token: 0x06001A33 RID: 6707 RVA: 0x0001E5DC File Offset: 0x0001C7DC
		// (set) Token: 0x06001A34 RID: 6708 RVA: 0x0001E5E4 File Offset: 0x0001C7E4
		public eDynamicFormType FormType { get; set; }

		// Token: 0x17000AE1 RID: 2785
		// (get) Token: 0x06001A35 RID: 6709 RVA: 0x0001E5ED File Offset: 0x0001C7ED
		// (set) Token: 0x06001A36 RID: 6710 RVA: 0x0001E5F5 File Offset: 0x0001C7F5
		public string UniqueId { get; set; }

		// Token: 0x17000AE2 RID: 2786
		// (get) Token: 0x06001A37 RID: 6711 RVA: 0x0001E5FE File Offset: 0x0001C7FE
		// (set) Token: 0x06001A38 RID: 6712 RVA: 0x0001E606 File Offset: 0x0001C806
		public string Title { get; set; }

		// Token: 0x17000AE3 RID: 2787
		// (get) Token: 0x06001A39 RID: 6713 RVA: 0x0001E60F File Offset: 0x0001C80F
		// (set) Token: 0x06001A3A RID: 6714 RVA: 0x0001E617 File Offset: 0x0001C817
		public string SecondaryTitle { get; set; }

		// Token: 0x17000AE4 RID: 2788
		// (get) Token: 0x06001A3B RID: 6715 RVA: 0x0001E620 File Offset: 0x0001C820
		// (set) Token: 0x06001A3C RID: 6716 RVA: 0x0001E628 File Offset: 0x0001C828
		public bool IsEnabled { get; set; }

		// Token: 0x17000AE5 RID: 2789
		// (get) Token: 0x06001A3D RID: 6717 RVA: 0x0001E631 File Offset: 0x0001C831
		// (set) Token: 0x06001A3E RID: 6718 RVA: 0x0001E639 File Offset: 0x0001C839
		public bool ShowAsButton { get; set; }

		// Token: 0x17000AE6 RID: 2790
		// (get) Token: 0x06001A3F RID: 6719 RVA: 0x0001E642 File Offset: 0x0001C842
		// (set) Token: 0x06001A40 RID: 6720 RVA: 0x0001E64A File Offset: 0x0001C84A
		public DynamicForm SubForm { get; set; }
	}
}

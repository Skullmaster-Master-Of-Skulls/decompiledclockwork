using System;

namespace TechnoPro.Common.Public.Entities.DynamicForms
{
	// Token: 0x0200034A RID: 842
	public class DynamicFileDescription : BusinessBase<int>
	{
		// Token: 0x17000AD7 RID: 2775
		// (get) Token: 0x06001A1F RID: 6687 RVA: 0x0001E52C File Offset: 0x0001C72C
		// (set) Token: 0x06001A20 RID: 6688 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int DataId
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

		// Token: 0x17000AD8 RID: 2776
		// (get) Token: 0x06001A21 RID: 6689 RVA: 0x0001E544 File Offset: 0x0001C744
		// (set) Token: 0x06001A22 RID: 6690 RVA: 0x0001E54C File Offset: 0x0001C74C
		public int ControlId { get; set; }

		// Token: 0x17000AD9 RID: 2777
		// (get) Token: 0x06001A23 RID: 6691 RVA: 0x0001E555 File Offset: 0x0001C755
		// (set) Token: 0x06001A24 RID: 6692 RVA: 0x0001E55D File Offset: 0x0001C75D
		public string Filename { get; set; }

		// Token: 0x17000ADA RID: 2778
		// (get) Token: 0x06001A25 RID: 6693 RVA: 0x0001E566 File Offset: 0x0001C766
		// (set) Token: 0x06001A26 RID: 6694 RVA: 0x0001E56E File Offset: 0x0001C76E
		public int FileId { get; set; }

		// Token: 0x17000ADB RID: 2779
		// (get) Token: 0x06001A27 RID: 6695 RVA: 0x0001E577 File Offset: 0x0001C777
		// (set) Token: 0x06001A28 RID: 6696 RVA: 0x0001E57F File Offset: 0x0001C77F
		public eDynamicFormType FormType { get; set; }

		// Token: 0x17000ADC RID: 2780
		// (get) Token: 0x06001A29 RID: 6697 RVA: 0x0001E588 File Offset: 0x0001C788
		// (set) Token: 0x06001A2A RID: 6698 RVA: 0x0001E590 File Offset: 0x0001C790
		public DateTime? DateUploaded { get; set; }

		// Token: 0x17000ADD RID: 2781
		// (get) Token: 0x06001A2B RID: 6699 RVA: 0x0001E599 File Offset: 0x0001C799
		// (set) Token: 0x06001A2C RID: 6700 RVA: 0x0001E5A1 File Offset: 0x0001C7A1
		public string Note { get; set; }
	}
}

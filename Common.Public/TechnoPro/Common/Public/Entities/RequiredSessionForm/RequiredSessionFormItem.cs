using System;
using TechnoPro.Common.Public.Entities.TPMailMan;

namespace TechnoPro.Common.Public.Entities.RequiredSessionForm
{
	// Token: 0x0200020F RID: 527
	[Serializable]
	public class RequiredSessionFormItem : BusinessBase<int>, ICloneable<RequiredSessionFormItem>, ICloneable
	{
		// Token: 0x06001014 RID: 4116 RVA: 0x0000E1E2 File Offset: 0x0000C3E2
		public RequiredSessionFormItem()
		{
		}

		// Token: 0x06001015 RID: 4117 RVA: 0x000173F8 File Offset: 0x000155F8
		public RequiredSessionFormItem(RequiredSessionFormItem item)
		{
			bool flag = item == null;
			if (!flag)
			{
				this.RequiredSessionFormItemId = item.RequiredSessionFormItemId;
				this.ScreenNum = item.ScreenNum;
				this.Title = item.Title;
				this.Disabled = item.Disabled;
				this.Intro = item.Intro;
				TPMailMessage emailTemplate = item.EmailTemplate;
				this.EmailTemplate = ((emailTemplate != null) ? emailTemplate.Clone() : null);
				this.OrderNum = item.OrderNum;
				this.Name = item.Name;
			}
		}

		// Token: 0x17000692 RID: 1682
		// (get) Token: 0x06001016 RID: 4118 RVA: 0x0001748C File Offset: 0x0001568C
		// (set) Token: 0x06001017 RID: 4119 RVA: 0x0000E258 File Offset: 0x0000C458
		public virtual int RequiredSessionFormItemId
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

		// Token: 0x17000693 RID: 1683
		// (get) Token: 0x06001018 RID: 4120 RVA: 0x000174A4 File Offset: 0x000156A4
		// (set) Token: 0x06001019 RID: 4121 RVA: 0x000174AC File Offset: 0x000156AC
		public int ScreenNum { get; set; }

		// Token: 0x17000694 RID: 1684
		// (get) Token: 0x0600101A RID: 4122 RVA: 0x000174B5 File Offset: 0x000156B5
		// (set) Token: 0x0600101B RID: 4123 RVA: 0x000174BD File Offset: 0x000156BD
		public string Title { get; set; }

		// Token: 0x17000695 RID: 1685
		// (get) Token: 0x0600101C RID: 4124 RVA: 0x000174C6 File Offset: 0x000156C6
		// (set) Token: 0x0600101D RID: 4125 RVA: 0x000174CE File Offset: 0x000156CE
		public bool Disabled { get; set; }

		// Token: 0x17000696 RID: 1686
		// (get) Token: 0x0600101E RID: 4126 RVA: 0x000174D7 File Offset: 0x000156D7
		// (set) Token: 0x0600101F RID: 4127 RVA: 0x000174DF File Offset: 0x000156DF
		public string Intro { get; set; }

		// Token: 0x17000697 RID: 1687
		// (get) Token: 0x06001020 RID: 4128 RVA: 0x000174E8 File Offset: 0x000156E8
		// (set) Token: 0x06001021 RID: 4129 RVA: 0x000174F0 File Offset: 0x000156F0
		public TPMailMessage EmailTemplate { get; set; }

		// Token: 0x17000698 RID: 1688
		// (get) Token: 0x06001022 RID: 4130 RVA: 0x000174F9 File Offset: 0x000156F9
		// (set) Token: 0x06001023 RID: 4131 RVA: 0x00017501 File Offset: 0x00015701
		public int OrderNum { get; set; }

		// Token: 0x17000699 RID: 1689
		// (get) Token: 0x06001024 RID: 4132 RVA: 0x0001750A File Offset: 0x0001570A
		// (set) Token: 0x06001025 RID: 4133 RVA: 0x00017512 File Offset: 0x00015712
		public string Name { get; set; }

		// Token: 0x06001026 RID: 4134 RVA: 0x0001751C File Offset: 0x0001571C
		public RequiredSessionFormItem Clone()
		{
			return new RequiredSessionFormItem(this);
		}

		// Token: 0x06001027 RID: 4135 RVA: 0x00017534 File Offset: 0x00015734
		object ICloneable.Clone()
		{
			return this.Clone();
		}
	}
}

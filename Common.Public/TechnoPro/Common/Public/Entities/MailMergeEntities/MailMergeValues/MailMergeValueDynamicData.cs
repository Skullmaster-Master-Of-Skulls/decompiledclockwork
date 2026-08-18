using System;
using TechnoPro.Common.Public.Entities.DynamicForms;

namespace TechnoPro.Common.Public.Entities.MailMergeEntities.MailMergeValues
{
	// Token: 0x020002D7 RID: 727
	public class MailMergeValueDynamicData : MailMergeValueBase
	{
		// Token: 0x17000908 RID: 2312
		// (get) Token: 0x060015ED RID: 5613 RVA: 0x0001B585 File Offset: 0x00019785
		// (set) Token: 0x060015EE RID: 5614 RVA: 0x0001B58D File Offset: 0x0001978D
		public DynamicData Value { get; set; }

		// Token: 0x060015EF RID: 5615 RVA: 0x0001B596 File Offset: 0x00019796
		public override void SetValue(object obj)
		{
			this.Value = base.GetValue<DynamicData>(obj, null);
		}

		// Token: 0x060015F0 RID: 5616 RVA: 0x0001B5A8 File Offset: 0x000197A8
		public override object GetValue()
		{
			return this.Value;
		}
	}
}

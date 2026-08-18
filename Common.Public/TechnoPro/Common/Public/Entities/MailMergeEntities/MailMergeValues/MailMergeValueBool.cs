using System;

namespace TechnoPro.Common.Public.Entities.MailMergeEntities.MailMergeValues
{
	// Token: 0x020002D1 RID: 721
	public class MailMergeValueBool : MailMergeValueBase
	{
		// Token: 0x17000902 RID: 2306
		// (get) Token: 0x060015CF RID: 5583 RVA: 0x0001B398 File Offset: 0x00019598
		// (set) Token: 0x060015D0 RID: 5584 RVA: 0x0001B3A0 File Offset: 0x000195A0
		public bool Value { get; set; }

		// Token: 0x060015D1 RID: 5585 RVA: 0x0001B3A9 File Offset: 0x000195A9
		public override void SetValue(object obj)
		{
			this.Value = base.GetValue<bool>(obj, false);
		}

		// Token: 0x060015D2 RID: 5586 RVA: 0x0001B3BC File Offset: 0x000195BC
		public override object GetValue()
		{
			return this.Value;
		}
	}
}

using System;
using TechnoPro.Common.Public.Entities.MailMergeEntities.Output;

namespace TechnoPro.Common.Public.Entities.MailMergeEntities.MailMergeValues
{
	// Token: 0x020002D3 RID: 723
	public class MailMergeValueCheckedItem : MailMergeValueBase
	{
		// Token: 0x17000904 RID: 2308
		// (get) Token: 0x060015D9 RID: 5593 RVA: 0x0001B414 File Offset: 0x00019614
		// (set) Token: 0x060015DA RID: 5594 RVA: 0x0001B41C File Offset: 0x0001961C
		public MailMergeCheckedItem Value { get; set; }

		// Token: 0x060015DB RID: 5595 RVA: 0x0001B425 File Offset: 0x00019625
		public override void SetValue(object obj)
		{
			this.Value = base.GetValue<MailMergeCheckedItem>(obj, new MailMergeCheckedItem
			{
				Title = "Unknown"
			});
		}

		// Token: 0x060015DC RID: 5596 RVA: 0x0001B448 File Offset: 0x00019648
		public override object GetValue()
		{
			return this.Value;
		}
	}
}

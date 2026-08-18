using System;

namespace TechnoPro.Common.Public.Entities.MailMergeEntities.MailMergeValues
{
	// Token: 0x020002D9 RID: 729
	public class MailMergeValueString : MailMergeValueBase
	{
		// Token: 0x1700090A RID: 2314
		// (get) Token: 0x060015F7 RID: 5623 RVA: 0x0001B601 File Offset: 0x00019801
		// (set) Token: 0x060015F8 RID: 5624 RVA: 0x0001B609 File Offset: 0x00019809
		public string Value { get; set; }

		// Token: 0x060015F9 RID: 5625 RVA: 0x0001B612 File Offset: 0x00019812
		public override void SetValue(object obj)
		{
			this.Value = base.GetValue<string>(obj, null);
		}

		// Token: 0x060015FA RID: 5626 RVA: 0x0001B624 File Offset: 0x00019824
		public override object GetValue()
		{
			return this.Value;
		}
	}
}

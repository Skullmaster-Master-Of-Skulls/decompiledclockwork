using System;

namespace Spire.Xls
{
	// Token: 0x02000047 RID: 71
	public interface IConditionValue
	{
		// Token: 0x170001B4 RID: 436
		// (get) Token: 0x060004F2 RID: 1266
		// (set) Token: 0x060004F3 RID: 1267
		ConditionValueType Type { get; set; }

		// Token: 0x170001B5 RID: 437
		// (get) Token: 0x060004F4 RID: 1268
		// (set) Token: 0x060004F5 RID: 1269
		string Value { get; set; }
	}
}

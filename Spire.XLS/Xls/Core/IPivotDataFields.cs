using System;

namespace Spire.Xls.Core
{
	// Token: 0x02000235 RID: 565
	public interface IPivotDataFields
	{
		// Token: 0x17000C68 RID: 3176
		IPivotDataField this[int index]
		{
			get;
		}

		// Token: 0x0600225E RID: 8798
		IPivotDataField Add(IPivotField field, string name, SubtotalTypes subtotal);

		// Token: 0x17000C69 RID: 3177
		// (get) Token: 0x0600225F RID: 8799
		int Count { get; }
	}
}

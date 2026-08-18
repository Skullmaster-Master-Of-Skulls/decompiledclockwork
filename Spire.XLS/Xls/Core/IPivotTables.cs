using System;

namespace Spire.Xls.Core
{
	// Token: 0x02000230 RID: 560
	public interface IPivotTables
	{
		// Token: 0x17000C5E RID: 3166
		// (get) Token: 0x06002234 RID: 8756
		int Count { get; }

		// Token: 0x17000C5F RID: 3167
		IPivotTable this[int index]
		{
			get;
		}

		// Token: 0x17000C60 RID: 3168
		IPivotTable this[string name]
		{
			get;
		}

		// Token: 0x06002237 RID: 8759
		PivotTable Add(string name, CellRange location, PivotCache cache);

		// Token: 0x06002238 RID: 8760
		void Remove(string name);

		// Token: 0x06002239 RID: 8761
		void RemoveAt(int index);
	}
}

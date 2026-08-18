using System;
using System.Collections;

namespace Spire.Xls.Core
{
	// Token: 0x02000355 RID: 853
	public interface INameRanges : IEnumerable
	{
		// Token: 0x17000CC4 RID: 3268
		// (get) Token: 0x060033CB RID: 13259
		int Count { get; }

		// Token: 0x17000CC5 RID: 3269
		// (get) Token: 0x060033CC RID: 13260
		object Parent { get; }

		// Token: 0x17000CC6 RID: 3270
		INamedRange this[int index]
		{
			get;
		}

		// Token: 0x17000CC7 RID: 3271
		INamedRange this[string name]
		{
			get;
		}

		// Token: 0x060033CF RID: 13263
		INamedRange GetByName(string name);

		// Token: 0x17000CC8 RID: 3272
		// (get) Token: 0x060033D0 RID: 13264
		IWorksheet ParentWorksheet { get; }

		// Token: 0x060033D1 RID: 13265
		INamedRange Add(string name);

		// Token: 0x060033D2 RID: 13266
		INamedRange Add(string name, IXLSRange namedObject);

		// Token: 0x060033D3 RID: 13267
		INamedRange Add(INamedRange name);

		// Token: 0x060033D4 RID: 13268
		void Remove(string name);

		// Token: 0x060033D5 RID: 13269
		void RemoveAt(int index);

		// Token: 0x060033D6 RID: 13270
		bool Contains(string name);
	}
}

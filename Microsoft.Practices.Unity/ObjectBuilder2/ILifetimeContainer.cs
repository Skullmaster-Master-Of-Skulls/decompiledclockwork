using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.Practices.ObjectBuilder2
{
	// Token: 0x0200003D RID: 61
	[SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "Using Container suffix instead of Collection. Aligns with existing Unity nomenclature.")]
	public interface ILifetimeContainer : IEnumerable<object>, IEnumerable, IDisposable
	{
		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000F8 RID: 248
		int Count { get; }

		// Token: 0x060000F9 RID: 249
		void Add(object item);

		// Token: 0x060000FA RID: 250
		bool Contains(object item);

		// Token: 0x060000FB RID: 251
		void Remove(object item);
	}
}

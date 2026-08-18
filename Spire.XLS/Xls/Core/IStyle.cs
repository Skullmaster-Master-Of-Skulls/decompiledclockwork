using System;
using Spire.Xls.Core.Interfaces;

namespace Spire.Xls.Core
{
	// Token: 0x020000FE RID: 254
	public interface IStyle : IExtendedFormat, IOptimizedUpdate
	{
		// Token: 0x170003E7 RID: 999
		// (get) Token: 0x06000B82 RID: 2946
		bool BuiltIn { get; }

		// Token: 0x170003E8 RID: 1000
		// (get) Token: 0x06000B83 RID: 2947
		string Name { get; }

		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x06000B84 RID: 2948
		bool IsInitialized { get; }

		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x06000B85 RID: 2949
		IInterior Interior { get; }
	}
}

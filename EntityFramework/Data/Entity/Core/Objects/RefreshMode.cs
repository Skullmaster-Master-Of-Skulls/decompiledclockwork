using System;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Core.Objects
{
	// Token: 0x020005B8 RID: 1464
	[SuppressMessage("Microsoft.Design", "CA1008:EnumsShouldHaveZeroValue")]
	public enum RefreshMode
	{
		// Token: 0x04001636 RID: 5686
		ClientWins = 2,
		// Token: 0x04001637 RID: 5687
		StoreWins = 1
	}
}

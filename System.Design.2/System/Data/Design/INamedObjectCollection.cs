using System;
using System.Collections;

namespace System.Data.Design
{
	// Token: 0x02000253 RID: 595
	internal interface INamedObjectCollection : ICollection, IEnumerable
	{
		// Token: 0x060016E4 RID: 5860
		INameService GetNameService();
	}
}

using System;
using System.Collections.Generic;

namespace Spire.Xls.Core
{
	// Token: 0x0200017B RID: 379
	public interface IListObjects : IList<IListObject>
	{
		// Token: 0x06001219 RID: 4633
		IListObject Create(string name, IXLSRange range);
	}
}

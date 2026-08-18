using System;
using System.Collections.Generic;

namespace Spire.Xls.Core
{
	// Token: 0x0200024E RID: 590
	public interface IColorScale
	{
		// Token: 0x17000C75 RID: 3189
		// (get) Token: 0x0600239C RID: 9116
		IList<IColorConditionValue> Criteria { get; }

		// Token: 0x0600239D RID: 9117
		void SetConditionCount(int count);
	}
}

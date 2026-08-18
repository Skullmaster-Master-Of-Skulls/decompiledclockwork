using System;
using System.Collections;

namespace System.Data.Design
{
	// Token: 0x02000238 RID: 568
	internal interface IDesignConnectionCollection : INamedObjectCollection, ICollection, IEnumerable
	{
		// Token: 0x06001565 RID: 5477
		IDesignConnection Get(string name);

		// Token: 0x06001566 RID: 5478
		void Set(IDesignConnection connection);

		// Token: 0x06001567 RID: 5479
		void Remove(string name);

		// Token: 0x06001568 RID: 5480
		void Clear();
	}
}

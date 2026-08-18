using System;
using System.Collections;

namespace Org.BouncyCastle.Utilities.Collections
{
	// Token: 0x0200033C RID: 828
	public interface ISet : ICollection, IEnumerable
	{
		// Token: 0x06001DF7 RID: 7671
		void Add(object o);

		// Token: 0x06001DF8 RID: 7672
		void AddAll(IEnumerable e);

		// Token: 0x06001DF9 RID: 7673
		void Clear();

		// Token: 0x06001DFA RID: 7674
		bool Contains(object o);

		// Token: 0x17000539 RID: 1337
		// (get) Token: 0x06001DFB RID: 7675
		bool IsEmpty { get; }

		// Token: 0x06001DFC RID: 7676
		void Remove(object o);

		// Token: 0x06001DFD RID: 7677
		void RemoveAll(IEnumerable e);
	}
}

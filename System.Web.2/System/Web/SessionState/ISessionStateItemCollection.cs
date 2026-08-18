using System;
using System.Collections;
using System.Collections.Specialized;

namespace System.Web.SessionState
{
	// Token: 0x0200012F RID: 303
	public interface ISessionStateItemCollection : ICollection, IEnumerable
	{
		// Token: 0x170005B8 RID: 1464
		object this[string name]
		{
			get;
			set;
		}

		// Token: 0x170005B9 RID: 1465
		object this[int index]
		{
			get;
			set;
		}

		// Token: 0x06001230 RID: 4656
		void Remove(string name);

		// Token: 0x06001231 RID: 4657
		void RemoveAt(int index);

		// Token: 0x06001232 RID: 4658
		void Clear();

		// Token: 0x170005BA RID: 1466
		// (get) Token: 0x06001233 RID: 4659
		NameObjectCollectionBase.KeysCollection Keys { get; }

		// Token: 0x170005BB RID: 1467
		// (get) Token: 0x06001234 RID: 4660
		// (set) Token: 0x06001235 RID: 4661
		bool Dirty { get; set; }
	}
}

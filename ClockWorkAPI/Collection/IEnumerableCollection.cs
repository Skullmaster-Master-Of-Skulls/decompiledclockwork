using System;
using System.Collections;
using System.Collections.Generic;

namespace ClockWorkAPI.Collection
{
	// Token: 0x02000038 RID: 56
	public interface IEnumerableCollection<T> : IEnumerable<T>, ICollection, IEnumerable
	{
		// Token: 0x060002CD RID: 717
		bool Contains(T item);
	}
}

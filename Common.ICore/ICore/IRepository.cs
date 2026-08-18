using System;
using System.Collections.Generic;
using TechnoPro.Common.Public.Entities;

namespace TechnoPro.Common.ICore
{
	// Token: 0x02000004 RID: 4
	public interface IRepository<TU, TV> where TV : BusinessBase<TU>
	{
		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000007 RID: 7
		int Count { get; }

		// Token: 0x06000008 RID: 8
		void Remove(TU id);

		// Token: 0x06000009 RID: 9
		void Remove(TV entity);

		// Token: 0x0600000A RID: 10
		bool Contains(TU id);

		// Token: 0x0600000B RID: 11
		bool Contains(TV entity);

		// Token: 0x17000002 RID: 2
		TV this[TU id]
		{
			get;
			set;
		}

		// Token: 0x0600000E RID: 14
		TV Get(TU id);

		// Token: 0x0600000F RID: 15
		TV Save(TV entity);

		// Token: 0x06000010 RID: 16
		TV SaveOrUpdate(TV entity);

		// Token: 0x06000011 RID: 17
		void Update(TV entity);

		// Token: 0x06000012 RID: 18
		TV FindOne(Predicate<TV> filter);

		// Token: 0x06000013 RID: 19
		ICollection<TV> FindAll(Predicate<TV> filter);

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000014 RID: 20
		IEnumerable<TV> Items { get; }

		// Token: 0x06000015 RID: 21
		int RemoveAll(Predicate<TV> filter);

		// Token: 0x06000016 RID: 22
		void Clear();

		// Token: 0x06000017 RID: 23
		IList<TV> ToList();

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000018 RID: 24
		object SyncObj { get; }
	}
}

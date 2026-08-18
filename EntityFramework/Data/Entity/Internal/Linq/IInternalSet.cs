using System;
using System.Collections;
using System.Data.Entity.Infrastructure;

namespace System.Data.Entity.Internal.Linq
{
	// Token: 0x0200078F RID: 1935
	internal interface IInternalSet : IInternalQuery
	{
		// Token: 0x060057A1 RID: 22433
		void Attach(object entity);

		// Token: 0x060057A2 RID: 22434
		void Add(object entity);

		// Token: 0x060057A3 RID: 22435
		void AddRange(IEnumerable entities);

		// Token: 0x060057A4 RID: 22436
		void RemoveRange(IEnumerable entities);

		// Token: 0x060057A5 RID: 22437
		void Remove(object entity);

		// Token: 0x060057A6 RID: 22438
		void Initialize();

		// Token: 0x060057A7 RID: 22439
		void TryInitialize();

		// Token: 0x060057A8 RID: 22440
		IEnumerator ExecuteSqlQuery(string sql, bool asNoTracking, bool? streaming, object[] parameters);

		// Token: 0x060057A9 RID: 22441
		IDbAsyncEnumerator ExecuteSqlQueryAsync(string sql, bool asNoTracking, bool? streaming, object[] parameters);
	}
}

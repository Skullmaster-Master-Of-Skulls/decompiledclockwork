using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace System.Data.Objects
{
	// Token: 0x02000156 RID: 342
	public interface IObjectSet<TEntity> : IQueryable<TEntity>, IEnumerable<!0>, IEnumerable, IQueryable where TEntity : class
	{
		// Token: 0x06001964 RID: 6500
		void AddObject(TEntity entity);

		// Token: 0x06001965 RID: 6501
		void Attach(TEntity entity);

		// Token: 0x06001966 RID: 6502
		void DeleteObject(TEntity entity);

		// Token: 0x06001967 RID: 6503
		void Detach(TEntity entity);
	}
}

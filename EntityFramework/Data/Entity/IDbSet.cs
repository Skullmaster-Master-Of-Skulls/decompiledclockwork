using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace System.Data.Entity
{
	// Token: 0x0200073C RID: 1852
	[SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix", Justification = "Name is intentional")]
	public interface IDbSet<TEntity> : IQueryable<TEntity>, IEnumerable<!0>, IQueryable, IEnumerable where TEntity : class
	{
		// Token: 0x060053D8 RID: 21464
		TEntity Find(params object[] keyValues);

		// Token: 0x060053D9 RID: 21465
		TEntity Add(TEntity entity);

		// Token: 0x060053DA RID: 21466
		TEntity Remove(TEntity entity);

		// Token: 0x060053DB RID: 21467
		TEntity Attach(TEntity entity);

		// Token: 0x17000E38 RID: 3640
		// (get) Token: 0x060053DC RID: 21468
		ObservableCollection<TEntity> Local { get; }

		// Token: 0x060053DD RID: 21469
		TEntity Create();

		// Token: 0x060053DE RID: 21470
		TDerivedEntity Create<TDerivedEntity>() where TDerivedEntity : class, TEntity;
	}
}

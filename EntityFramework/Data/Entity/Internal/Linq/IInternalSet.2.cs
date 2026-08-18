using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Entity.Internal.Linq
{
	// Token: 0x02000790 RID: 1936
	internal interface IInternalSet<TEntity> : IInternalSet, IInternalQuery<TEntity>, IInternalQuery where TEntity : class
	{
		// Token: 0x060057AA RID: 22442
		TEntity Find(params object[] keyValues);

		// Token: 0x060057AB RID: 22443
		Task<TEntity> FindAsync(CancellationToken cancellationToken, params object[] keyValues);

		// Token: 0x060057AC RID: 22444
		TEntity Create();

		// Token: 0x060057AD RID: 22445
		TEntity Create(Type derivedEntityType);

		// Token: 0x17000F55 RID: 3925
		// (get) Token: 0x060057AE RID: 22446
		ObservableCollection<TEntity> Local { get; }
	}
}

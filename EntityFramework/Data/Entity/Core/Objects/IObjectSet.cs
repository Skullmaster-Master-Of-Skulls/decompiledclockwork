using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace System.Data.Entity.Core.Objects
{
	// Token: 0x0200059B RID: 1435
	[SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix")]
	public interface IObjectSet<TEntity> : IQueryable<TEntity>, IEnumerable<!0>, IQueryable, IEnumerable where TEntity : class
	{
		// Token: 0x0600384C RID: 14412
		void AddObject(TEntity entity);

		// Token: 0x0600384D RID: 14413
		void Attach(TEntity entity);

		// Token: 0x0600384E RID: 14414
		void DeleteObject(TEntity entity);

		// Token: 0x0600384F RID: 14415
		void Detach(TEntity entity);
	}
}

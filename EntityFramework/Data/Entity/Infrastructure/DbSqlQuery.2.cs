using System;
using System.ComponentModel;
using System.Data.Entity.Internal;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x0200029C RID: 668
	[SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix")]
	public class DbSqlQuery<TEntity> : DbRawSqlQuery<TEntity> where TEntity : class
	{
		// Token: 0x060017D3 RID: 6099 RVA: 0x00078DBC File Offset: 0x00076FBC
		internal DbSqlQuery(InternalSqlQuery internalQuery) : base(internalQuery)
		{
		}

		// Token: 0x060017D4 RID: 6100 RVA: 0x00078DC5 File Offset: 0x00076FC5
		protected DbSqlQuery() : this(null)
		{
		}

		// Token: 0x060017D5 RID: 6101 RVA: 0x00078DCE File Offset: 0x00076FCE
		public virtual DbSqlQuery<TEntity> AsNoTracking()
		{
			if (base.InternalQuery != null)
			{
				return new DbSqlQuery<TEntity>(base.InternalQuery.AsNoTracking());
			}
			return this;
		}

		// Token: 0x060017D6 RID: 6102 RVA: 0x00078DEA File Offset: 0x00076FEA
		[Obsolete("Queries are now streaming by default unless a retrying ExecutionStrategy is used. Calling this method will have no effect.")]
		public new virtual DbSqlQuery<TEntity> AsStreaming()
		{
			if (base.InternalQuery != null)
			{
				return new DbSqlQuery<TEntity>(base.InternalQuery.AsStreaming());
			}
			return this;
		}

		// Token: 0x060017D7 RID: 6103 RVA: 0x00078E06 File Offset: 0x00077006
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x060017D8 RID: 6104 RVA: 0x00078E0E File Offset: 0x0007700E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x060017D9 RID: 6105 RVA: 0x00078E17 File Offset: 0x00077017
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x060017DA RID: 6106 RVA: 0x00078E1F File Offset: 0x0007701F
		[EditorBrowsable(EditorBrowsableState.Never)]
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		public new Type GetType()
		{
			return base.GetType();
		}
	}
}

using System;
using System.ComponentModel;
using System.Data.Entity.Internal;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x0200029A RID: 666
	[SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix")]
	[SuppressMessage("Microsoft.Design", "CA1010:CollectionsShouldImplementGenericInterface")]
	public class DbSqlQuery : DbRawSqlQuery
	{
		// Token: 0x0600178B RID: 6027 RVA: 0x0007892B File Offset: 0x00076B2B
		internal DbSqlQuery(InternalSqlQuery internalQuery) : base(internalQuery)
		{
		}

		// Token: 0x0600178C RID: 6028 RVA: 0x00078934 File Offset: 0x00076B34
		protected DbSqlQuery() : this(null)
		{
		}

		// Token: 0x0600178D RID: 6029 RVA: 0x0007893D File Offset: 0x00076B3D
		public virtual DbSqlQuery AsNoTracking()
		{
			if (base.InternalQuery != null)
			{
				return new DbSqlQuery(base.InternalQuery.AsNoTracking());
			}
			return this;
		}

		// Token: 0x0600178E RID: 6030 RVA: 0x00078959 File Offset: 0x00076B59
		[Obsolete("Queries are now streaming by default unless a retrying ExecutionStrategy is used. Calling this method will have no effect.")]
		public new virtual DbSqlQuery AsStreaming()
		{
			if (base.InternalQuery != null)
			{
				return new DbSqlQuery(base.InternalQuery.AsStreaming());
			}
			return this;
		}

		// Token: 0x0600178F RID: 6031 RVA: 0x00078975 File Offset: 0x00076B75
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x06001790 RID: 6032 RVA: 0x0007897D File Offset: 0x00076B7D
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06001791 RID: 6033 RVA: 0x00078986 File Offset: 0x00076B86
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06001792 RID: 6034 RVA: 0x0007898E File Offset: 0x00076B8E
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}
	}
}

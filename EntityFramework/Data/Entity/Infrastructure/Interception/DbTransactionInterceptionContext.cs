using System;
using System.ComponentModel;
using System.Data.Common;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Infrastructure.Interception
{
	// Token: 0x02000181 RID: 385
	public class DbTransactionInterceptionContext : MutableInterceptionContext
	{
		// Token: 0x06000D36 RID: 3382 RVA: 0x0003C0A2 File Offset: 0x0003A2A2
		public DbTransactionInterceptionContext()
		{
		}

		// Token: 0x06000D37 RID: 3383 RVA: 0x0003C0AC File Offset: 0x0003A2AC
		public DbTransactionInterceptionContext(DbInterceptionContext copyFrom) : base(copyFrom)
		{
			DbTransactionInterceptionContext dbTransactionInterceptionContext = copyFrom as DbTransactionInterceptionContext;
			if (dbTransactionInterceptionContext != null)
			{
				this._connection = dbTransactionInterceptionContext.Connection;
			}
			Check.NotNull<DbInterceptionContext>(copyFrom, "copyFrom");
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000D38 RID: 3384 RVA: 0x0003C0E2 File Offset: 0x0003A2E2
		public DbConnection Connection
		{
			get
			{
				return this._connection;
			}
		}

		// Token: 0x06000D39 RID: 3385 RVA: 0x0003C0EC File Offset: 0x0003A2EC
		public DbTransactionInterceptionContext WithConnection(DbConnection connection)
		{
			Check.NotNull<DbConnection>(connection, "connection");
			DbTransactionInterceptionContext dbTransactionInterceptionContext = this.TypedClone();
			dbTransactionInterceptionContext._connection = connection;
			return dbTransactionInterceptionContext;
		}

		// Token: 0x06000D3A RID: 3386 RVA: 0x0003C114 File Offset: 0x0003A314
		public new DbTransactionInterceptionContext AsAsync()
		{
			return (DbTransactionInterceptionContext)base.AsAsync();
		}

		// Token: 0x06000D3B RID: 3387 RVA: 0x0003C121 File Offset: 0x0003A321
		public new DbTransactionInterceptionContext WithDbContext(DbContext context)
		{
			Check.NotNull<DbContext>(context, "context");
			return (DbTransactionInterceptionContext)base.WithDbContext(context);
		}

		// Token: 0x06000D3C RID: 3388 RVA: 0x0003C13B File Offset: 0x0003A33B
		public new DbTransactionInterceptionContext WithObjectContext(ObjectContext context)
		{
			Check.NotNull<ObjectContext>(context, "context");
			return (DbTransactionInterceptionContext)base.WithObjectContext(context);
		}

		// Token: 0x06000D3D RID: 3389 RVA: 0x0003C155 File Offset: 0x0003A355
		private DbTransactionInterceptionContext TypedClone()
		{
			return (DbTransactionInterceptionContext)this.Clone();
		}

		// Token: 0x06000D3E RID: 3390 RVA: 0x0003C162 File Offset: 0x0003A362
		protected override DbInterceptionContext Clone()
		{
			return new DbTransactionInterceptionContext(this);
		}

		// Token: 0x06000D3F RID: 3391 RVA: 0x0003C16A File Offset: 0x0003A36A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x06000D40 RID: 3392 RVA: 0x0003C172 File Offset: 0x0003A372
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06000D41 RID: 3393 RVA: 0x0003C17B File Offset: 0x0003A37B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000D42 RID: 3394 RVA: 0x0003C183 File Offset: 0x0003A383
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x0400039F RID: 927
		private DbConnection _connection;
	}
}

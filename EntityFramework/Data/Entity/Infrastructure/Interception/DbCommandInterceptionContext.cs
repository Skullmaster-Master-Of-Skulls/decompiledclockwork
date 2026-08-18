using System;
using System.ComponentModel;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Infrastructure.Interception
{
	// Token: 0x02000178 RID: 376
	public class DbCommandInterceptionContext : DbInterceptionContext
	{
		// Token: 0x06000C8C RID: 3212 RVA: 0x0003ADED File Offset: 0x00038FED
		public DbCommandInterceptionContext()
		{
		}

		// Token: 0x06000C8D RID: 3213 RVA: 0x0003ADF8 File Offset: 0x00038FF8
		public DbCommandInterceptionContext(DbInterceptionContext copyFrom) : base(copyFrom)
		{
			Check.NotNull<DbInterceptionContext>(copyFrom, "copyFrom");
			DbCommandInterceptionContext dbCommandInterceptionContext = copyFrom as DbCommandInterceptionContext;
			if (dbCommandInterceptionContext != null)
			{
				this._commandBehavior = dbCommandInterceptionContext._commandBehavior;
			}
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000C8E RID: 3214 RVA: 0x0003AE2E File Offset: 0x0003902E
		public CommandBehavior CommandBehavior
		{
			get
			{
				return this._commandBehavior;
			}
		}

		// Token: 0x06000C8F RID: 3215 RVA: 0x0003AE38 File Offset: 0x00039038
		public DbCommandInterceptionContext WithCommandBehavior(CommandBehavior commandBehavior)
		{
			DbCommandInterceptionContext dbCommandInterceptionContext = this.TypedClone();
			dbCommandInterceptionContext._commandBehavior = commandBehavior;
			return dbCommandInterceptionContext;
		}

		// Token: 0x06000C90 RID: 3216 RVA: 0x0003AE54 File Offset: 0x00039054
		private DbCommandInterceptionContext TypedClone()
		{
			return (DbCommandInterceptionContext)this.Clone();
		}

		// Token: 0x06000C91 RID: 3217 RVA: 0x0003AE61 File Offset: 0x00039061
		protected override DbInterceptionContext Clone()
		{
			return new DbCommandInterceptionContext(this);
		}

		// Token: 0x06000C92 RID: 3218 RVA: 0x0003AE69 File Offset: 0x00039069
		public new DbCommandInterceptionContext WithDbContext(DbContext context)
		{
			Check.NotNull<DbContext>(context, "context");
			return (DbCommandInterceptionContext)base.WithDbContext(context);
		}

		// Token: 0x06000C93 RID: 3219 RVA: 0x0003AE83 File Offset: 0x00039083
		public new DbCommandInterceptionContext WithObjectContext(ObjectContext context)
		{
			Check.NotNull<ObjectContext>(context, "context");
			return (DbCommandInterceptionContext)base.WithObjectContext(context);
		}

		// Token: 0x06000C94 RID: 3220 RVA: 0x0003AE9D File Offset: 0x0003909D
		public new DbCommandInterceptionContext AsAsync()
		{
			return (DbCommandInterceptionContext)base.AsAsync();
		}

		// Token: 0x06000C95 RID: 3221 RVA: 0x0003AEAA File Offset: 0x000390AA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x06000C96 RID: 3222 RVA: 0x0003AEB2 File Offset: 0x000390B2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06000C97 RID: 3223 RVA: 0x0003AEBB File Offset: 0x000390BB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000C98 RID: 3224 RVA: 0x0003AEC3 File Offset: 0x000390C3
		[EditorBrowsable(EditorBrowsableState.Never)]
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x04000359 RID: 857
		private CommandBehavior _commandBehavior;
	}
}

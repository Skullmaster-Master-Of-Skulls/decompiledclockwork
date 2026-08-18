using System;
using System.ComponentModel;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Infrastructure.Interception
{
	// Token: 0x02000182 RID: 386
	public class DbTransactionInterceptionContext<TResult> : MutableInterceptionContext<TResult>
	{
		// Token: 0x06000D43 RID: 3395 RVA: 0x0003C18B File Offset: 0x0003A38B
		public DbTransactionInterceptionContext()
		{
		}

		// Token: 0x06000D44 RID: 3396 RVA: 0x0003C193 File Offset: 0x0003A393
		public DbTransactionInterceptionContext(DbInterceptionContext copyFrom) : base(copyFrom)
		{
			Check.NotNull<DbInterceptionContext>(copyFrom, "copyFrom");
		}

		// Token: 0x06000D45 RID: 3397 RVA: 0x0003C1A8 File Offset: 0x0003A3A8
		public new DbTransactionInterceptionContext<TResult> AsAsync()
		{
			return (DbTransactionInterceptionContext<TResult>)base.AsAsync();
		}

		// Token: 0x06000D46 RID: 3398 RVA: 0x0003C1B5 File Offset: 0x0003A3B5
		public new DbTransactionInterceptionContext<TResult> WithDbContext(DbContext context)
		{
			Check.NotNull<DbContext>(context, "context");
			return (DbTransactionInterceptionContext<TResult>)base.WithDbContext(context);
		}

		// Token: 0x06000D47 RID: 3399 RVA: 0x0003C1CF File Offset: 0x0003A3CF
		public new DbTransactionInterceptionContext<TResult> WithObjectContext(ObjectContext context)
		{
			Check.NotNull<ObjectContext>(context, "context");
			return (DbTransactionInterceptionContext<TResult>)base.WithObjectContext(context);
		}

		// Token: 0x06000D48 RID: 3400 RVA: 0x0003C1E9 File Offset: 0x0003A3E9
		protected override DbInterceptionContext Clone()
		{
			return new DbTransactionInterceptionContext<TResult>(this);
		}

		// Token: 0x06000D49 RID: 3401 RVA: 0x0003C1F1 File Offset: 0x0003A3F1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x06000D4A RID: 3402 RVA: 0x0003C1F9 File Offset: 0x0003A3F9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06000D4B RID: 3403 RVA: 0x0003C202 File Offset: 0x0003A402
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000D4C RID: 3404 RVA: 0x0003C20A File Offset: 0x0003A40A
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}
	}
}

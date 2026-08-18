using System;
using System.ComponentModel;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Transactions;

namespace System.Data.Entity.Infrastructure.Interception
{
	// Token: 0x02000183 RID: 387
	public class EnlistTransactionInterceptionContext : DbConnectionInterceptionContext
	{
		// Token: 0x06000D4D RID: 3405 RVA: 0x0003C212 File Offset: 0x0003A412
		public EnlistTransactionInterceptionContext()
		{
		}

		// Token: 0x06000D4E RID: 3406 RVA: 0x0003C21C File Offset: 0x0003A41C
		public EnlistTransactionInterceptionContext(DbInterceptionContext copyFrom) : base(copyFrom)
		{
			Check.NotNull<DbInterceptionContext>(copyFrom, "copyFrom");
			EnlistTransactionInterceptionContext enlistTransactionInterceptionContext = copyFrom as EnlistTransactionInterceptionContext;
			if (enlistTransactionInterceptionContext != null)
			{
				this._transaction = enlistTransactionInterceptionContext._transaction;
			}
		}

		// Token: 0x06000D4F RID: 3407 RVA: 0x0003C252 File Offset: 0x0003A452
		public new EnlistTransactionInterceptionContext AsAsync()
		{
			return (EnlistTransactionInterceptionContext)base.AsAsync();
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000D50 RID: 3408 RVA: 0x0003C25F File Offset: 0x0003A45F
		public Transaction Transaction
		{
			get
			{
				return this._transaction;
			}
		}

		// Token: 0x06000D51 RID: 3409 RVA: 0x0003C268 File Offset: 0x0003A468
		public EnlistTransactionInterceptionContext WithTransaction(Transaction transaction)
		{
			EnlistTransactionInterceptionContext enlistTransactionInterceptionContext = this.TypedClone();
			enlistTransactionInterceptionContext._transaction = transaction;
			return enlistTransactionInterceptionContext;
		}

		// Token: 0x06000D52 RID: 3410 RVA: 0x0003C284 File Offset: 0x0003A484
		private EnlistTransactionInterceptionContext TypedClone()
		{
			return (EnlistTransactionInterceptionContext)this.Clone();
		}

		// Token: 0x06000D53 RID: 3411 RVA: 0x0003C291 File Offset: 0x0003A491
		protected override DbInterceptionContext Clone()
		{
			return new EnlistTransactionInterceptionContext(this);
		}

		// Token: 0x06000D54 RID: 3412 RVA: 0x0003C299 File Offset: 0x0003A499
		public new EnlistTransactionInterceptionContext WithDbContext(DbContext context)
		{
			Check.NotNull<DbContext>(context, "context");
			return (EnlistTransactionInterceptionContext)base.WithDbContext(context);
		}

		// Token: 0x06000D55 RID: 3413 RVA: 0x0003C2B3 File Offset: 0x0003A4B3
		public new EnlistTransactionInterceptionContext WithObjectContext(ObjectContext context)
		{
			Check.NotNull<ObjectContext>(context, "context");
			return (EnlistTransactionInterceptionContext)base.WithObjectContext(context);
		}

		// Token: 0x06000D56 RID: 3414 RVA: 0x0003C2CD File Offset: 0x0003A4CD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x06000D57 RID: 3415 RVA: 0x0003C2D5 File Offset: 0x0003A4D5
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06000D58 RID: 3416 RVA: 0x0003C2DE File Offset: 0x0003A4DE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000D59 RID: 3417 RVA: 0x0003C2E6 File Offset: 0x0003A4E6
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x040003A0 RID: 928
		private Transaction _transaction;
	}
}

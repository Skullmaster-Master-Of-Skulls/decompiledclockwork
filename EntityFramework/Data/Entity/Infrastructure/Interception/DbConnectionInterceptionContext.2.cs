using System;
using System.ComponentModel;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Infrastructure.Interception
{
	// Token: 0x02000174 RID: 372
	public class DbConnectionInterceptionContext : MutableInterceptionContext
	{
		// Token: 0x06000C43 RID: 3139 RVA: 0x0003A70C File Offset: 0x0003890C
		public DbConnectionInterceptionContext()
		{
		}

		// Token: 0x06000C44 RID: 3140 RVA: 0x0003A714 File Offset: 0x00038914
		public DbConnectionInterceptionContext(DbInterceptionContext copyFrom) : base(copyFrom)
		{
			Check.NotNull<DbInterceptionContext>(copyFrom, "copyFrom");
		}

		// Token: 0x06000C45 RID: 3141 RVA: 0x0003A729 File Offset: 0x00038929
		public new DbConnectionInterceptionContext AsAsync()
		{
			return (DbConnectionInterceptionContext)base.AsAsync();
		}

		// Token: 0x06000C46 RID: 3142 RVA: 0x0003A736 File Offset: 0x00038936
		public new DbConnectionInterceptionContext WithDbContext(DbContext context)
		{
			Check.NotNull<DbContext>(context, "context");
			return (DbConnectionInterceptionContext)base.WithDbContext(context);
		}

		// Token: 0x06000C47 RID: 3143 RVA: 0x0003A750 File Offset: 0x00038950
		public new DbConnectionInterceptionContext WithObjectContext(ObjectContext context)
		{
			Check.NotNull<ObjectContext>(context, "context");
			return (DbConnectionInterceptionContext)base.WithObjectContext(context);
		}

		// Token: 0x06000C48 RID: 3144 RVA: 0x0003A76A File Offset: 0x0003896A
		protected override DbInterceptionContext Clone()
		{
			return new DbConnectionInterceptionContext(this);
		}

		// Token: 0x06000C49 RID: 3145 RVA: 0x0003A772 File Offset: 0x00038972
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x06000C4A RID: 3146 RVA: 0x0003A77A File Offset: 0x0003897A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06000C4B RID: 3147 RVA: 0x0003A783 File Offset: 0x00038983
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000C4C RID: 3148 RVA: 0x0003A78B File Offset: 0x0003898B
		[EditorBrowsable(EditorBrowsableState.Never)]
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		public new Type GetType()
		{
			return base.GetType();
		}
	}
}

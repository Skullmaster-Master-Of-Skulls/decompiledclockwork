using System;
using System.ComponentModel;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Infrastructure.Interception
{
	// Token: 0x0200014D RID: 333
	public class DbConfigurationInterceptionContext : DbInterceptionContext
	{
		// Token: 0x06000AE6 RID: 2790 RVA: 0x0003735D File Offset: 0x0003555D
		public DbConfigurationInterceptionContext()
		{
		}

		// Token: 0x06000AE7 RID: 2791 RVA: 0x00037365 File Offset: 0x00035565
		public DbConfigurationInterceptionContext(DbInterceptionContext copyFrom) : base(copyFrom)
		{
			Check.NotNull<DbInterceptionContext>(copyFrom, "copyFrom");
		}

		// Token: 0x06000AE8 RID: 2792 RVA: 0x0003737A File Offset: 0x0003557A
		protected override DbInterceptionContext Clone()
		{
			return new DbConfigurationInterceptionContext(this);
		}

		// Token: 0x06000AE9 RID: 2793 RVA: 0x00037382 File Offset: 0x00035582
		public new DbConfigurationInterceptionContext WithDbContext(DbContext context)
		{
			Check.NotNull<DbContext>(context, "context");
			return (DbConfigurationInterceptionContext)base.WithDbContext(context);
		}

		// Token: 0x06000AEA RID: 2794 RVA: 0x0003739C File Offset: 0x0003559C
		public new DbConfigurationInterceptionContext WithObjectContext(ObjectContext context)
		{
			Check.NotNull<ObjectContext>(context, "context");
			return (DbConfigurationInterceptionContext)base.WithObjectContext(context);
		}

		// Token: 0x06000AEB RID: 2795 RVA: 0x000373B6 File Offset: 0x000355B6
		public new DbConfigurationInterceptionContext AsAsync()
		{
			return (DbConfigurationInterceptionContext)base.AsAsync();
		}

		// Token: 0x06000AEC RID: 2796 RVA: 0x000373C3 File Offset: 0x000355C3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x06000AED RID: 2797 RVA: 0x000373CB File Offset: 0x000355CB
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06000AEE RID: 2798 RVA: 0x000373D4 File Offset: 0x000355D4
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000AEF RID: 2799 RVA: 0x000373DC File Offset: 0x000355DC
		[EditorBrowsable(EditorBrowsableState.Never)]
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		public new Type GetType()
		{
			return base.GetType();
		}
	}
}

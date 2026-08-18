using System;
using System.ComponentModel;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Infrastructure.Interception
{
	// Token: 0x0200016B RID: 363
	public class DbConnectionInterceptionContext<TResult> : MutableInterceptionContext<TResult>
	{
		// Token: 0x06000BB4 RID: 2996 RVA: 0x00039963 File Offset: 0x00037B63
		public DbConnectionInterceptionContext()
		{
		}

		// Token: 0x06000BB5 RID: 2997 RVA: 0x0003996B File Offset: 0x00037B6B
		public DbConnectionInterceptionContext(DbInterceptionContext copyFrom) : base(copyFrom)
		{
			Check.NotNull<DbInterceptionContext>(copyFrom, "copyFrom");
		}

		// Token: 0x06000BB6 RID: 2998 RVA: 0x00039980 File Offset: 0x00037B80
		public new DbConnectionInterceptionContext<TResult> AsAsync()
		{
			return (DbConnectionInterceptionContext<TResult>)base.AsAsync();
		}

		// Token: 0x06000BB7 RID: 2999 RVA: 0x0003998D File Offset: 0x00037B8D
		public new DbConnectionInterceptionContext<TResult> WithDbContext(DbContext context)
		{
			Check.NotNull<DbContext>(context, "context");
			return (DbConnectionInterceptionContext<TResult>)base.WithDbContext(context);
		}

		// Token: 0x06000BB8 RID: 3000 RVA: 0x000399A7 File Offset: 0x00037BA7
		public new DbConnectionInterceptionContext<TResult> WithObjectContext(ObjectContext context)
		{
			Check.NotNull<ObjectContext>(context, "context");
			return (DbConnectionInterceptionContext<TResult>)base.WithObjectContext(context);
		}

		// Token: 0x06000BB9 RID: 3001 RVA: 0x000399C1 File Offset: 0x00037BC1
		protected override DbInterceptionContext Clone()
		{
			return new DbConnectionInterceptionContext<TResult>(this);
		}

		// Token: 0x06000BBA RID: 3002 RVA: 0x000399C9 File Offset: 0x00037BC9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x06000BBB RID: 3003 RVA: 0x000399D1 File Offset: 0x00037BD1
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06000BBC RID: 3004 RVA: 0x000399DA File Offset: 0x00037BDA
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000BBD RID: 3005 RVA: 0x000399E2 File Offset: 0x00037BE2
		[EditorBrowsable(EditorBrowsableState.Never)]
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		public new Type GetType()
		{
			return base.GetType();
		}
	}
}

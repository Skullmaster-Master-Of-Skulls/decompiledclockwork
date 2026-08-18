using System;
using System.ComponentModel;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Infrastructure.Interception
{
	// Token: 0x02000176 RID: 374
	public class DbConnectionPropertyInterceptionContext<TValue> : PropertyInterceptionContext<TValue>
	{
		// Token: 0x06000C63 RID: 3171 RVA: 0x0003A8F6 File Offset: 0x00038AF6
		public DbConnectionPropertyInterceptionContext()
		{
		}

		// Token: 0x06000C64 RID: 3172 RVA: 0x0003A8FE File Offset: 0x00038AFE
		public DbConnectionPropertyInterceptionContext(DbInterceptionContext copyFrom) : base(copyFrom)
		{
			Check.NotNull<DbInterceptionContext>(copyFrom, "copyFrom");
		}

		// Token: 0x06000C65 RID: 3173 RVA: 0x0003A913 File Offset: 0x00038B13
		public new DbConnectionPropertyInterceptionContext<TValue> WithValue(TValue value)
		{
			return (DbConnectionPropertyInterceptionContext<TValue>)base.WithValue(value);
		}

		// Token: 0x06000C66 RID: 3174 RVA: 0x0003A921 File Offset: 0x00038B21
		protected override DbInterceptionContext Clone()
		{
			return new DbConnectionPropertyInterceptionContext<TValue>(this);
		}

		// Token: 0x06000C67 RID: 3175 RVA: 0x0003A929 File Offset: 0x00038B29
		public new DbConnectionPropertyInterceptionContext<TValue> AsAsync()
		{
			return (DbConnectionPropertyInterceptionContext<TValue>)base.AsAsync();
		}

		// Token: 0x06000C68 RID: 3176 RVA: 0x0003A936 File Offset: 0x00038B36
		public new DbConnectionPropertyInterceptionContext<TValue> WithDbContext(DbContext context)
		{
			Check.NotNull<DbContext>(context, "context");
			return (DbConnectionPropertyInterceptionContext<TValue>)base.WithDbContext(context);
		}

		// Token: 0x06000C69 RID: 3177 RVA: 0x0003A950 File Offset: 0x00038B50
		public new DbConnectionPropertyInterceptionContext<TValue> WithObjectContext(ObjectContext context)
		{
			Check.NotNull<ObjectContext>(context, "context");
			return (DbConnectionPropertyInterceptionContext<TValue>)base.WithObjectContext(context);
		}

		// Token: 0x06000C6A RID: 3178 RVA: 0x0003A96A File Offset: 0x00038B6A
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x06000C6B RID: 3179 RVA: 0x0003A972 File Offset: 0x00038B72
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06000C6C RID: 3180 RVA: 0x0003A97B File Offset: 0x00038B7B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000C6D RID: 3181 RVA: 0x0003A983 File Offset: 0x00038B83
		[EditorBrowsable(EditorBrowsableState.Never)]
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		public new Type GetType()
		{
			return base.GetType();
		}
	}
}
